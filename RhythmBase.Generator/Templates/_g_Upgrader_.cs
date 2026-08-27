/// <summary>
/// A JSON converter for <see cref="_g_infoRootClassTypeToDisplayString_"/> that uses metadata-aware serializer options.
/// </summary>
internal abstract class BackwardCompatible_g_mtpName_MetadataJsonConverter : RhythmBase.Global.Serialization.MetadataJsonConverter<_g_infoRootClassTypeToDisplayString_>
{
	protected class Upgrater
	{
		internal int MaxVersion { get; init; }
		internal required Action<_g_infoRootClassTypeToDisplayString_> UpgrateFunc { get; init; }
		internal required _g_infoClassTypeEnumToDisplayString_ Type { get; init; }
	}
	private readonly List<Upgrater> _upgraters = [];
	private readonly EnumCollection<_g_infoClassTypeEnumToDisplayString_> _typeHasUpgrater = [];
	private int _maxVersion;
	/// <summary>
	/// The maximum version that this converter can upgrade.
	/// </summary>
	internal int MaxVersion => _maxVersion;
	/// <summary>
	/// The types of events that this converter can upgrade.
	/// </summary>
	internal EnumCollection<_g_infoClassTypeEnumToDisplayString_> TypeHasUpgrater => _typeHasUpgrater;
	/// <summary>
	/// Registers an upgrader for a specific event type and version.
	/// </summary>
	/// <typeparam name="T">The type of the event to upgrade.</typeparam>
	/// <param name="version">
	/// The version for which to register the upgrader.
	/// Versions <b>equal to or lower than</b> this will be affected by this upgrader.
	/// </param>
	/// <param name="upgrateAction">The action to perform when upgrading the event.</param>
	protected void Register<T>(int version, Action<_g_infoRootClassTypeToDisplayString_> upgrateAction) where T : _g_infoRootClassTypeToDisplayString_, new()
	{
		var type = EventTypeRegistry.ToEnum_g_enumSuffix_<T>();
		_maxVersion = int.Max(_maxVersion, version);
		_typeHasUpgrater.Add(type);
		_upgraters.Add(new Upgrater()
		{
			MaxVersion = version,
			Type = type,
			UpgrateFunc = upgrateAction
		});
	}
	/// <summary>
	/// Upgrades the specified event to the latest version if an upgrader is registered for its type and version.
	/// </summary>
	/// <param name="version">The version of the event to upgrade.</param>
	/// <param name="type">The type of the event to upgrade.</param>
	/// <returns>An enumerable of upgraders that can upgrade the event.</returns>
	protected IEnumerable<Upgrater> GetUpgraters(int version, _g_infoClassTypeEnumToDisplayString_ type)
	{
		foreach (Upgrater upgrater in _upgraters)
			if (upgrater.Type == type && upgrater.MaxVersion >= version)
				yield return upgrater;
	}
	internal BackwardCompatible_g_mtpName_MetadataJsonConverter()
	{
		InitializeUpgraters();
	}
	/// <summary>
	/// Initializes the upgraders for this converter. This method is called once when the converter is first used.
	/// </summary>
	protected abstract void InitializeUpgraters();
}
