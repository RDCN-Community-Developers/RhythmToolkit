using RhythmBase.Global.Components.Vector;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Components;

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
	private static void ValidateRDPointValues<T>(ReadOnlySpan<T> values, int limit = 2)
	{
		if (values.Length != limit) throw new ArgumentException("Values must contain exactly 2 elements.", nameof(values));
	}
	public static RDPoint BuildRDPoint(ReadOnlySpan<float?> values)
	{
		ValidateRDPointValues(values);
		return new(values[0], values[1]);
	}
	public static RDPointI BuildRDPointI(ReadOnlySpan<int?> values)
	{
		ValidateRDPointValues(values);
		return new(values[0], values[1]);
	}
	public static RDPointN BuildRDPointN(ReadOnlySpan<float> values)
	{
		ValidateRDPointValues(values);
		return new(values[0], values[1]);
	}
	public static RDPointNI BuildRDPointNI(ReadOnlySpan<int> values)
	{
		ValidateRDPointValues(values);
		return new(values[0], values[1]);
	}
	public static RDPointE BuildRDPointE(ReadOnlySpan<RDExpression?> values)
	{
		ValidateRDPointValues(values);
		return new(values[0], values[1]);
	}
	public static RDSize BuildRDSize(ReadOnlySpan<float?> values)
	{
		ValidateRDPointValues(values);
		return new(values[0], values[1]);
	}
	public static RDSizeI BuildRDSizeI(ReadOnlySpan<int?> values)
	{
		ValidateRDPointValues(values);
		return new(values[0], values[1]);
	}
	public static RDSizeN BuildRDSizeN(ReadOnlySpan<float> values)
	{
		ValidateRDPointValues(values);
		return new(values[0], values[1]);
	}
	public static RDSizeNI BuildRDSizeNI(ReadOnlySpan<int> values)
	{
		ValidateRDPointValues(values);
		return new(values[0], values[1]);
	}
	public static RDSizeE BuildRDSizeE(ReadOnlySpan<RDExpression?> values)
	{
		ValidateRDPointValues(values);
		return new(values[0], values[1]);
	}
	public static RoomOrder BuildRoomOrder(ReadOnlySpan<byte> orders)
	{
		ValidateRDPointValues<byte>(orders,4);
		return new RoomOrder(orders[0], orders[1], orders[2], orders[3]);
	}
}
