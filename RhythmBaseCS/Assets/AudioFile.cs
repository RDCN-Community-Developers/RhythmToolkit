using System;
using System.IO;
using Microsoft.VisualBasic.CompilerServices;
using NAudio.Vorbis;
using NAudio.Wave;

namespace RhythmBase.Assets
{

	public class AudioFile(string name) : IAssetFile
	{

		public string Name { get; }

		public void Load(string directory)
		{
			string path = Path.Combine(directory, Name);
			bool flag = Path.Exists(path);
			if (flag)
			{
				string extension = Path.GetExtension(Name);
				if (Operators.CompareString(extension, ".ogg", false) != 0)
				{
					if (Operators.CompareString(extension, ".mp3", false) != 0)
					{
						if (Operators.CompareString(extension, ".wav", false) != 0 && Operators.CompareString(extension, ".wave", false) != 0)
						{
							if (Operators.CompareString(extension, ".aiff", false) != 0 && Operators.CompareString(extension, ".aif", false) != 0)
							{
								_stream = (WaveStream)Stream.Null;
							}
							else
							{
								_stream = new AiffFileReader(path);
							}
						}
						else
						{
							_stream = new WaveFileReader(path);
						}
					}
					else
					{
						_stream = new Mp3FileReader(path);
					}
				}
				else
				{
					_stream = new VorbisWaveReader(path);
				}
			}
		}


		public override string ToString() => Name;


		private readonly string _file = name;


		private WaveStream _stream;
	}
}
