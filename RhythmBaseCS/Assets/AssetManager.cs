using Microsoft.VisualBasic.CompilerServices;
using RhythmBase.Components;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace RhythmBase.Assets
{
	public class AssetManager(RDLevel level)
	{
		public IAssetFile this[Type type, string name]
		{
			get => data[new TypeNameIndex(type, name)];
			set => data[new TypeNameIndex(type, name)] = (IAssetFile)value;
		}
		public IAssetFile this[Asset<IAssetFile> index]
		{
			get => this[(Type)index.Type, index.Name];
			set => this[(Type)index.Type, index.Name] = value;
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
