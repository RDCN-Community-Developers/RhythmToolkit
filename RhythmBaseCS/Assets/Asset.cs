namespace RhythmBase.Assets
{
	public class Asset<TAssetFile> where TAssetFile : IAssetFile
	{
		public object Type => typeof(TAssetFile);
		public string Name
		{
			get => _name;
			set
			{
				if (_name == value)
					cache = default!;
				_name = value;
			}
		}
		public TAssetFile Value
		{
			get
			{
				if (Manager != null)
					cache = (TAssetFile)Manager[(Type)Type, Name];
				return cache;
			}
			set
			{
				if (Manager != null)
					Manager[(Type)Type, Name] = value;
				else
					cache = value;
			}
		}
		private AssetManager Manager { get; set; }
		internal Asset(AssetManager manager)
		{
			Manager = manager;
			_connected = true;
		}
		internal Asset()
		{
		}
		private TAssetFile cache;
		private string _name;
		private readonly bool _connected = false;
	}
}
