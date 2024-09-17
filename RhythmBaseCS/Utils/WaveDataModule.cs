using NAudio.Dsp;
using NAudio.Vorbis;
using NAudio.Wave;

namespace RhythmBase.Utils
{
    internal static class WaveDataModule
    {
        /// <summary>
        /// 获取音频文件流
        /// </summary>
        /// <param name="filepath">音频文件夹</param>
        /// <returns></returns>
        public static WaveStream GetWaveStream(string filepath)
        {
            if (Path.Exists(filepath))
            {
                string extension = Path.GetExtension(filepath);
                return extension switch
                {
                    ".ogg" => new VorbisWaveReader(filepath),
                    ".mp3" => new Mp3FileReader(filepath),
                    ".wav" or ".wave" => new WaveFileReader(filepath),
                    ".aiff" or ".aif" => new AiffFileReader(filepath),
                    _ => (WaveStream)Stream.Null,
                };
            }
            return (WaveStream)Stream.Null;
        }
        /// <summary>
        /// 获取时域数据
        /// </summary>
        /// <param name="stream">音频文件流</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static float[] GetTimeDomain(this WaveStream stream)
        {
            List<float> result = [];
            int BytesPerSample = stream.WaveFormat.BitsPerSample;
            byte[] sample = new byte[stream.Length];
            stream.Read(sample, 0, sample.Length);
            for (int i = 0; i < sample.Length; i += BytesPerSample)
            {
                float value = stream.WaveFormat.Encoding switch
                {
                    WaveFormatEncoding.IeeeFloat => BitConverter.ToSingle(sample, i),
                    WaveFormatEncoding.Pcm => BitConverter.ToInt16(sample, i) / (float)short.MaxValue,
                    _ => throw new NotSupportedException(stream.WaveFormat.Encoding.ToString()),
                };
                result.Add(float.IsNaN(value) ? 0 : value);
            }
            stream.Position = 0;
            return [.. result];
        }
        /// <summary>
        /// 获取帧频域数据
        /// </summary>
        /// <param name="waveFormat">音频格式</param>
        /// <param name="data">音频数据</param>
        /// <param name="maxFrequency">保留频率上限</param>
        /// <returns></returns>
        public static float[] GetFrameFrequencyDomain(WaveFormat waveFormat, float[] data, int maxFrequency = 2500)
        {
            int channelCount = waveFormat.Channels;

            float[][] channelSamples = Enumerable
                .Range(0, channelCount)
                .Select(channel => Enumerable
                    .Range(0, data.Length / channelCount)
                    .Select(i => data[channel + i * channelCount])
                    .ToArray())
                .ToArray();

            float[] averageSamples = Enumerable
                .Range(0, data.Length / channelCount)
                .Select(index => Enumerable
                    .Range(0, channelCount)
                    .Select(channel => channelSamples[channel][index])
                    .Average())
                .ToArray();

            int log = (int)Math.Ceiling(Math.Log(averageSamples.Length, 2));
            int newLen = (int)Math.Pow(2, log);
            float[] filledSamples = new float[newLen];
            Array.Copy(averageSamples, filledSamples, averageSamples.Length);
            Complex[] complexSrc = filledSamples
                .Select(v => new Complex() { X = v })
                .ToArray();
            FastFourierTransform.FFT(false, log, complexSrc);

            Complex[] halfData = complexSrc
                .Take(complexSrc.Length / 2)
                .ToArray();
            float[] dftData = halfData
                .Select(v => (float)Math.Sqrt(v.X * v.X + v.Y * v.Y))
                .ToArray();

            int count = maxFrequency / (waveFormat.SampleRate / filledSamples.Length);
            float[] finalData = dftData.Take(count).ToArray();

            return finalData;
        }
        /// <summary>
        /// 获取频域数据
        /// </summary>
        /// <param name="waveFormat">音频格式信息</param>
        /// <param name="data">音频数据</param>
        /// <param name="width">采样宽度</param>
        /// <param name="maxFrequency">保留频率上限</param>
        /// <returns></returns>
        public static float[][] GetFrequencyDomain(WaveFormat waveFormat, float[] data, int width, int maxFrequency = 2500)
        {
            List<float[]> result = [];
            int index = 0;
            float[] buffer = new float[width];
            while (index + width < data.Length)
            {
                buffer = data[index..(index + width - 1)];
                index += width / 2;
                result.Add(GetFrameFrequencyDomain(waveFormat, buffer, maxFrequency));
            }
            return [.. result];
        }
    }
}
