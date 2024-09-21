using NAudio.Dsp;
using NAudio.Vorbis;
using NAudio.Wave;

namespace RhythmBase.Utils
{
	public static class WaveDataUtils
	{
		/// <summary>
		/// Get wave stream of the audio file.
		/// </summary>
		/// <param name="filepath">Audio file path.</param>
		/// <returns>The wave stream of the audio file.</returns>
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
		/// Get the time domain data of the stream.
		/// </summary>
		/// <param name="stream">Wave stream.</param>
		/// <returns>A 2D array with format [Channel][Frame] data.</returns>
		/// <exception cref="NotSupportedException">The encoding format was not supported yet.</exception>
		public static float[][] GetTimeDomain(this WaveStream stream)
		{
			List<List<float>> result = [];
			for (int channel = 0; channel < stream.WaveFormat.Channels; channel++)
				result.Add([]);
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
				result[(i / BytesPerSample) % stream.WaveFormat.Channels].Add(value);
			}
			stream.Position = 0;
			return [.. result.Select(i => i.ToArray())];
		}
		/// <summary>
		/// Get the time domain data of the stream which is compressed in average.
		/// </summary>
		/// <param name="stream">Wave stream.</param>
		/// <returns>An array with format [Frame] data.</returns>
		/// <exception cref="NotSupportedException">The encoding format was not supported yet.</exception>
		public static float[] GetAverageTimeDomain(this WaveStream stream)
		{
			List<float> result = [];
			int BytesPerSample = stream.WaveFormat.BitsPerSample;
			byte[] sample = new byte[stream.Length];
			stream.Read(sample, 0, sample.Length);
			for (int i = 0; i < sample.Length; i += BytesPerSample)
			{
				List<float> channelsData = [];
				for (int c = 0; c < stream.WaveFormat.Channels; c++)
				{
					float value = stream.WaveFormat.Encoding switch
					{
						WaveFormatEncoding.IeeeFloat => BitConverter.ToSingle(sample, i),
						WaveFormatEncoding.Pcm => BitConverter.ToInt16(sample, i) / (float)short.MaxValue,
						_ => throw new NotSupportedException(stream.WaveFormat.Encoding.ToString()),
					};
					channelsData.Add(value);
				}
				result.Add(channelsData.Average());
			}
			stream.Position = 0;
			return [.. result];
		}
		/// <summary>
		/// Get a frame of the frequency domain data of a range of sample.
		/// </summary>
		/// <param name="waveFormat">Wave format information.</param>
		/// <param name="samples">Audio sample.</param>
		/// <param name="maxFrequency">the Maximum of the frequency to be retained.</param>
		/// <returns>An array with format [Frequency] data.</returns>
		public static float[] GetFrameFrequencyDomain(WaveFormat waveFormat, float[] samples, int maxFrequency = 2500)
		{
			List<float[]> finalDatas = [];
			int log = (int)Math.Ceiling(Math.Log(samples.Length, 2));
			int newLen = (int)Math.Pow(2, log);
			float[] filledSamples = new float[newLen];
			Array.Copy(samples, filledSamples, samples.Length);
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
			finalDatas.Add(dftData);

			return finalData;
		}
		/// <summary>
		/// Get the frequency domain data of a range of sample.
		/// </summary>
		/// <param name="waveFormat">Wave format information.</param>
		/// <param name="timeDomainData">Audio sample.</param>
		/// <param name="sampleWidth">The sample width.</param>
		/// <param name="maxFrequency">the Maximum of the frequency to be retained.</param>
		/// <returns>An array with format [Channel][Frame][Frequency] data.</returns>
		public static float[][][] GetFrequencyDomain(WaveFormat waveFormat, float[][] timeDomainData, int sampleWidth, int maxFrequency = 2500)
		{
			List<List<float[]>> result = [];
			int index = 0;
			float[] buffer = new float[sampleWidth];
			for (int i = 0; i < waveFormat.Channels; i++)
			{
				result.Add([]);
				while (index + sampleWidth < timeDomainData[i].Length)
				{
					buffer = timeDomainData[i][index..(index + sampleWidth - 1)];
					index += sampleWidth / 2;
					float[] outData = GetFrameFrequencyDomain(waveFormat, buffer, maxFrequency);
					result[i].Add(outData);
				}
				index = 0;
			}
			return [.. result.Select(i => i.ToArray())];
		}
		/// <summary>
		/// Get the frequency domain data of a range of sample which is compressed in average.
		/// </summary>
		/// <param name="waveFormat">Wave format information.</param>
		/// <param name="averageTimeDomainData">Audio sample.</param>
		/// <param name="sampleWidth">The sample width.</param>
		/// <param name="maxFrequency">the Maximum of the frequency to be retained.</param>
		/// <returns>An array with format [Channel][Frame][Frequency] data.</returns>
		public static float[][] GetAverageFrequencyDomain(WaveFormat waveFormat, float[] averageTimeDomainData, int sampleWidth, int maxFrequency = 2500)
		{
			List<float[]> result = [];
			int index = 0;
			float[] buffer = new float[sampleWidth];
			while (index + sampleWidth < averageTimeDomainData.Length)
			{
				buffer = averageTimeDomainData[index..(index + sampleWidth - 1)];
				index += sampleWidth / 2;
				result.Add(GetFrameFrequencyDomain(waveFormat, buffer, maxFrequency));
			}
			return [.. result];
		}
	}
}
