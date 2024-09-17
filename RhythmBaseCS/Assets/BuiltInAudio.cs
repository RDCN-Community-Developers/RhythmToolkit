namespace RhythmBase.Assets
{
	public class BuiltInAudio(string name) : IAudioFile
	{
		public bool IsModified => false;
		public bool IsBuiltIn => true;
		public string Name => _name;
		public string FilePath => "";
		internal static IAssetFile? Load(string name) => new BuiltInAudio(name);
		public void Save() { return; }
		private string _name = name;
		public override string ToString() => _name;
	}
}
