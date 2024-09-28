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
					_ => throw new NotSupportedException(extension),
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
			int c = stream.WaveFormat.Channels;
			List<List<float>> result = [];
			for (int i = 0; i < c; i++)
				result.Add([]);

			ISampleProvider provider = stream.ToSampleProvider();
			float[] floats = new float[c];
			int read = 0;
			while ((read = provider.Read(floats, 0, c)) > 0)
				for (int i = 0; i < c; i++)
					result[i].Add(floats[i]);
			return [.. result.Select(i => i.ToArray())];
		}
		public static float[] Average(this float[][] timeDomain)
		{
			return Enumerable.Range(0, timeDomain[0].Length)
				.Select(i => Enumerable.Range(0, timeDomain.Length)
					.Select(c => timeDomain[c][i])
					.Average())
				.ToArray();
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
		/// <param name="windowWidth">The sample width.</param>
		/// <param name="maxFrequency">the Maximum of the frequency to be retained.</param>
		/// <returns>An array with format [Channel][Frame][Frequency] data.</returns>
		public static float[][][] GetFrequencyDomain(WaveFormat waveFormat, float[][] timeDomainData, int windowWidth, int maxFrequency = 2500)
		{
			List<List<float[]>> result = [];
			int index = 0;
			float[] buffer = new float[windowWidth];
			for (int i = 0; i < waveFormat.Channels; i++)
			{
				result.Add([]);
				while (index + windowWidth < timeDomainData[i].Length)
				{
					buffer = timeDomainData[i][index..(index + windowWidth - 1)];
					index += windowWidth / 2;
					float[] outData = GetFrameFrequencyDomain(waveFormat, buffer, maxFrequency);
					result[i].Add(outData);
				}
				index = 0;
			}
			return [.. result.Select(i => i.ToArray())];
		}
		public static float[][] ToEnergy(WaveFormat waveFormat, float[][] timeDomainData, int windowWidth) {
			List<float[]> result = [];
			for(int channel = 0;channel < waveFormat.Channels; channel++)
			{
				List<float> volume = [];
				for(int j = 0; j < timeDomainData.Length - windowWidth; j+=windowWidth/2)
				{
					volume.Add ((float)Math.Sqrt(timeDomainData[channel][j..(j+windowWidth)].Select(i=>i*i).Average()));
				}
				result.Add([.. volume]);
			}
			return [.. result];
		}
	}
}
