namespace RhythmBase.Assets
{

	public class BuiltInAudio(string name) : IAudioFile
	{
		public string Name => _name;
		public string FilePath => "";
		internal static IAssetFile? Load(string name) => new BuiltInAudio(name);
		public void Save() { throw new NotImplementedException(); }
		private string _name = name;
		public override string ToString() => _name;
	}
}
