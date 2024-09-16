using Microsoft.VisualBasic.CompilerServices;
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
				var key = new TypeNameIndex(type, name);
				if (!data.ContainsKey(key))
					data[key] = IAssetFile.Load(type, Path.Combine(baseLevel.Directory, name));
				return data[key];
			}
			set => data[new TypeNameIndex(type, name)] = value;
		}
		public IAssetFile this[Asset<IAssetFile> index]
		{
			get
			{
				var key = new TypeNameIndex(index.Type, index.Name);
				if (!data.ContainsKey(key))
					data[key] = IAssetFile.Load(index.Type, Path.Combine( baseLevel.Directory,index.Name));
				return data[key];
			}

			set => this[index.Type, index.Name] = value;
		}
		public Asset<TAsset> Create<TAsset>(string name) where TAsset : IAssetFile => new(this) { Name = name };
		public Asset<TAsset> Create<TAsset>(string name, IAssetFile asset) where TAsset : IAssetFile => new(this)
		{
			Name = name,
			Value = (TAsset)(object)asset
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
			public override bool Equals([NotNullWhen(true)] object? obj) => obj!.GetType() == typeof(TypeNameIndex) | Equals((obj != null) ? ((TypeNameIndex)obj) : default);
			public bool Equals(TypeNameIndex other) => Operators.CompareString(Name, other.Name, false) == 0 && Operators.CompareString(Name, other.Name, false) == 0;
			public override int GetHashCode() => HashCode.Combine(Type, Name);
			public Type Type;
			public string Name;
		}
	}
}
