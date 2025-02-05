using RhythmBase.Components;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Assets
{
	public class AssetManager(RDLevel level)
	{
		public IAssetFile this[Type type, string name]
		{
			get
			{
				TypeNameIndex key = new(type, name);
				IAssetFile? value = IAssetFile.Load(type, Path.Combine(baseLevel.Directory, name));
				if (!data.ContainsKey(key) && value != null)
					data[key] = value;
				return data[key];
			}
			set => data[new TypeNameIndex(type, name)] = value;
		}
		public IAssetFile this[Asset<IAssetFile> index]
		{
			get
			{
				TypeNameIndex key = new(index.Type, index.Name);
				IAssetFile? value = IAssetFile.Load(index.Type, Path.Combine(baseLevel.Directory, index.Name));
				if (!data.ContainsKey(key) && value != null)
					data[key] = value;
				return data[key];
			}
			set => this[index.Type, index.Name] = value;
		}
		public Asset<TAsset> Create<TAsset>(string name) where TAsset : IAssetFile => new(this) { Name = name };
		public Asset<TAsset> Create<TAsset>(string name, IAssetFile asset) where TAsset : IAssetFile => new(this)
		{
			Name = name,
			Value = (TAsset)(object)asset,
		};
		private readonly RDLevel baseLevel = level;
		private readonly Dictionary<TypeNameIndex, IAssetFile> data = [];
		private struct TypeNameIndex : IEquatable<TypeNameIndex>
		{
			public TypeNameIndex(Type type, string name)
			{
				this = default;
				Type = type;
				Name = name;
			}
			public readonly override bool Equals([NotNullWhen(true)] object? obj) => obj!.GetType() == typeof(TypeNameIndex) | Equals((obj != null) ? ((TypeNameIndex)obj) : default);
			public readonly bool Equals(TypeNameIndex other) => Name == other.Name && Type == other.Type;
			public override readonly int GetHashCode() => HashCode.Combine(Type, Name);
			public Type Type;
			public string Name;
		}
	}
}
