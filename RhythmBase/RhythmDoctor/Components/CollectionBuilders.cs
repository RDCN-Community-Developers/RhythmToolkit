namespace RhythmBase.RhythmDoctor.Components;

#pragma warning disable CS1591
public static class CollectionBuilders
{
	public static RDRoom BuildRoom(ReadOnlySpan<byte> rooms)
	{
		RDRoom result = default;
		foreach (byte item in rooms)
		{
			if (item < 0 || item > 4) continue;
			result[item] = true;
		}
		return result;
	}
	public static EnumCollection<TEnum> BuildEnumCollection<TEnum>(ReadOnlySpan<TEnum> values) where TEnum : struct, Enum
	{
		EnumCollection<TEnum> collection = new(values);
		return collection;
	}
	public static ReadOnlyEnumCollection<TEnum> BuildReadOnlyEnumCollection<TEnum>(ReadOnlySpan<TEnum> values) where TEnum : struct, Enum
	{
		ReadOnlyEnumCollection<TEnum> collection = new(values);
		return collection;
	}
}