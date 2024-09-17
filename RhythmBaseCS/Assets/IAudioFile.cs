namespace RhythmBase.Assets
{
	public interface IAudioFile : IAssetFile
	{
		public abstract bool IsBuiltIn {  get; }
		static IAssetFile? Load(Type type, string path)
		{
			if (Path.GetExtension(path) == "")
				return BuiltInAudio.Load(path);
			else
				return AudioFile.Load(path);
		}
	}
}
