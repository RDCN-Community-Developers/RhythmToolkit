namespace RhythmBase.Assets
{
	public class Asset<TAssetFile> where TAssetFile : IAssetFile
	{
		public Type Type => typeof(TAssetFile);
		public bool IsRead { get; private set; } = false;
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
				{
					cache = (TAssetFile)Manager[(Type)Type, Name];
					IsRead = true;
				}
				return cache;
			}
			set
			{
				if (Manager != null)
				{
					Manager[(Type)Type, Name] = value;
					IsRead = true;
				}
				else
					cache = value;
			}
		}
		internal AssetManager Manager { get; set; }
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
