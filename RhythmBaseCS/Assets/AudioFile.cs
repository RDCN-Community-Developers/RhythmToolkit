using NAudio.Vorbis;
using NAudio.Wave;
namespace RhythmBase.Assets
{
	public class AudioFile : IAudioFile
	{
		public bool IsModified
		{
			get => false;
			private set
			{
			}
		}
		/// <inheritdoc/>
		public bool IsBuiltIn => false;
		public string FilePath { get; private set; }
		internal AudioFile() { }
		public static IAssetFile? Load(string path)
		{
			AudioFile audio = new();
			if (Path.Exists(path))
			{
				audio.FilePath = path;
				string extension = Path.GetExtension(path);
				audio.Stream = extension switch
				{
					".ogg" => new VorbisWaveReader(path),
					".mp3" => new Mp3FileReader(path),
					".wav" or ".wave" => new WaveFileReader(path),
					".aiff" or ".aif" => new AiffFileReader(path),
					_ => (WaveStream)System.IO.Stream.Null,
				};
			}
			return audio;
		}
		public void Save() => throw new NotImplementedException();
		public WaveStream Stream { get; private set; }
		public override string ToString() => Path.GetFileName(FilePath);
	}
}
