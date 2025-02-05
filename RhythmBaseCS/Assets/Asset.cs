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
					return;
				IsRead = false;
				_name = value;
			}
		}
		public TAssetFile Value
		{
			get
			{
				if (IsRead)
					return cache;
				if (Manager != null)
				{
					cache = (TAssetFile)Manager[Type, Name];
					IsRead = true;
				}
				return cache;
			}
			set
			{
				if (Manager != null)
					Manager[Type, Name] = value;
				else
					cache = value;
				IsRead = true;
			}
		}
		internal AssetManager Manager
		{
			get => _manager;
			set
			{
				_manager = value;
				if (IsRead)
					_manager[Type, Name] = cache;
			}
		}
		internal Asset(AssetManager manager)
		{
			Manager = manager;
		}
		internal Asset() { }
		private AssetManager _manager = null;
		private TAssetFile cache = default;
		private string _name = "";
	}
}
