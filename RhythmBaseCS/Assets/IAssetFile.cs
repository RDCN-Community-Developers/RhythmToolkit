using RhythmBase.Exceptions;
namespace RhythmBase.Assets
{
	/// <summary>
	/// Store data.
	/// </summary>
	public interface IAssetFile
	{
		public bool IsModified { get; }
		string FilePath { get; }
		static virtual IAssetFile? Load(string path) => throw new NotImplementedException();
		static IAssetFile? Load(Type type, string path) =>
			type.Name switch
			{
				nameof(AudioFile) => AudioFile.Load(path),
				nameof(BuiltInAudio) => BuiltInAudio.Load(path),
				nameof(SpriteFile) => SpriteFile.Load(path),
				nameof(ImageFile) => ImageFile.Load(path),
				nameof(ISpriteFile) => ISpriteFile.Load(type, path),
				nameof(IAudioFile) => IAudioFile.Load(type, path),
				_ => throw new TypeNotSupportedException(),
			};
		abstract void Save();
	}
}
