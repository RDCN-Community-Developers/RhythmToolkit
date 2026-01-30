using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RhythmBase.Global.Components;

/// <summary>
/// Represents a read-only collection of unique enumeration values of type <typeparamref name="TEnum"/>.
/// </summary>
/// <remarks>This collection provides efficient storage and lookup for sets of enumeration values. It is
/// immutable after creation; values cannot be added or removed. The collection supports enumeration and membership
/// checks, making it suitable for scenarios where a fixed set of enum values needs to be referenced or queried without
/// modification.</remarks>
/// <typeparam name="TEnum">The enumeration type contained in the collection. Must be a value type that derives from <see cref="System.Enum"/>.</typeparam>
public class ReadOnlyEnumCollection<TEnum> : IEnumerable<TEnum> where TEnum : struct, Enum
{
	private const int bw = sizeof(ulong) * 8;
	private readonly ulong mask;
	private readonly ulong[] _bits;
	/// <summary>
	/// Initializes a new instance of the <see cref="ReadOnlyEnumCollection{TEnum}"/> class that contains the elements of the specified
	/// <see cref="EnumCollection{TEnum}"/>.
	/// </summary>
	/// <remarks>The new collection is a read-only snapshot of the source collection at the time of construction.
	/// Subsequent changes to the source collection are not reflected in the read-only collection.</remarks>
	/// <param name="source">The <see cref="EnumCollection{TEnum}"/> whose elements are copied to the new read-only collection.</param>
	public ReadOnlyEnumCollection(EnumCollection<TEnum> source)
	{
		int size = source._bits.Length;
		_bits = new ulong[size];
		Array.Copy(source._bits, _bits, source._bits.Length);
		int byteWidth = Unsafe.SizeOf<TEnum>();
		mask = (byteWidth * 8 >= 64) ? ulong.MaxValue : ((1ul << (byteWidth * 8)) - 1ul);
	}
	/// <summary>
	/// Initializes a new instance of the ReadOnlyEnumCollection class containing the specified enumeration values.
	/// </summary>
	/// <remarks>This constructor creates a read-only collection of enumeration values. The collection is optimized
	/// for storage and lookup of enum values. All values provided must be within the valid range of the underlying enum
	/// type.</remarks>
	/// <param name="values">An array of enumeration values of type TEnum to include in the collection.</param>
	/// <exception cref="InvalidOperationException">Thrown when an enumeration value exceeds the maximum supported range for the collection.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Thrown when an enumeration value is outside the valid range for the collection.</exception>
	public ReadOnlyEnumCollection(params TEnum[] values)
	{
		ulong enumMax = 0;
		int byteWidth = Unsafe.SizeOf<TEnum>();
		ulong enumMask = (byteWidth * 8 >= 64) ? ulong.MaxValue : ((1ul << (byteWidth * 8)) - 1ul);
		foreach (TEnum value in values)
		{
			ulong v = Convert.ToUInt64(value) & enumMask;
			if (v > enumMax)
				enumMax = v;
		}
		int size;
		try
		{
			size = checked((int)(enumMax / bw) + 1);
		}
		catch
		{
			throw new InvalidOperationException("The enum value is too big.");
		}
		_bits = new ulong[size];
		mask = enumMask;
		foreach (TEnum value in values)
		{
			ulong v = ToUL(value);
			int div = (int)(v / bw);
			int rem = (int)(v % bw);
			if (div < 0 || div >= _bits.Length) throw new ArgumentOutOfRangeException(nameof(values), "Enum value out of collection range.");
			_bits[div] |= (1ul << rem);
		}
	}
	/// <summary>
	/// Initializes a new instance of the ReadOnlyEnumCollection class with the specified capacity and enum values.
	/// </summary>
	/// <remarks>This constructor allows you to pre-populate the collection with a set of enum values. The
	/// collection is read-only after initialization and cannot be modified.</remarks>
	/// <param name="capacity">The number of enum value slots to allocate in the collection. Must be greater than zero.</param>
	/// <param name="values">An array of enum values to include in the collection. Each value must be within the valid range for the collection.</param>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if any value in <paramref name="values"/> is outside the range defined by <paramref name="capacity"/>.</exception>
	public ReadOnlyEnumCollection(int capacity, params TEnum[] values)
	{
		_bits = new ulong[capacity];
		int byteWidth = Unsafe.SizeOf<TEnum>();
		mask = (byteWidth * 8 >= 64) ? ulong.MaxValue : ((1ul << (byteWidth * 8)) - 1ul);
		foreach (TEnum value in values)
		{
			ulong v = ToUL(value);
			int div = (int)(v / bw);
			int rem = (int)(v % bw);
			if (div < 0 || div >= _bits.Length) throw new ArgumentOutOfRangeException(nameof(values), "Enum value out of collection range.");
			_bits[div] |= (1ul << rem);
		}
	}
	/// <summary>
	/// Gets the number of elements contained in the collection.
	/// </summary>
	public int Count
	{
		get
		{
			int count = 0;
			foreach (ulong block in _bits)
			{
				ulong b = block;
				while (b != 0)
				{
					b &= (b - 1);
					count++;
				}
			}
			return count;
		}
	}
	/// <summary>
	/// Gets a value indicating whether the collection contains no elements.
	/// </summary>
	public bool IsEmpty
	{
		get
		{
			foreach (ulong block in _bits)
				if (block != 0) return false;
			return true;
		}
	}
	private ulong ToUL(TEnum value) => Unsafe.As<TEnum, ulong>(ref value) & mask;
	private static TEnum ToEnum(ulong value) => Unsafe.As<ulong, TEnum>(ref value);
	/// <summary>
	/// Determines whether the specified enumeration value is present in the set.
	/// </summary>
	/// <param name="type">The enumeration value to locate within the set.</param>
	/// <returns>true if the set contains the specified enumeration value; otherwise, false.</returns>
	public bool Contains(TEnum type)
	{
		ulong v = ToUL(type);
		int div = (int)(v / bw);
		int rem = (int)(v % bw);
		if (div < 0 || div >= _bits.Length) return false;
		return (_bits[div] & (1ul << rem)) != 0;
	}
	/// <summary>
	/// Determines whether the collection contains any of the specified enumeration values.
	/// </summary>
	/// <param name="types">An array of enumeration values to check for presence in the collection. Can be empty.</param>
	/// <returns>true if at least one of the specified enumeration values is present in the collection; otherwise, false.</returns>
	public bool ContainsAny(params TEnum[] types)
	{
		foreach (TEnum type in types)
			if (Contains(type)) return true;
		return false;
	}
	/// <summary>
	/// Determines whether the collection contains any of the specified enumeration values.
	/// </summary>
	/// <param name="types">A parameter array of enumeration values to check for presence in the collection. Cannot be null.</param>
	/// <returns>true if at least one of the specified enumeration values is present in the collection; otherwise, false.</returns>
	public bool ContainsAny(params IEnumerable<TEnum> types)
	{
			foreach (TEnum type in types)
				if (Contains(type)) return true;
		return false;
	}
	/// <summary>
	/// Determines whether the collection contains any of the specified enumeration values.
	/// </summary>
	/// <param name="types">A parameter array of enumeration value collections to check for presence in the collection. Each enumerable may
	/// contain one or more values to test.</param>
	/// <returns>true if at least one of the specified enumeration values is present in the collection; otherwise, false.</returns>
	public bool ContainsAny(ReadOnlyEnumCollection<TEnum> types)
	{
		for (int i = 0; i < Math.Min(_bits.Length, types._bits.Length); i++)
			if ((_bits[i] & types._bits[i]) != 0)
				return true;
		return false;
	}
	/// <summary>
	/// Determines whether the collection contains all of the specified enumeration values.
	/// </summary>
	/// <param name="types">An array of enumeration values to check for presence in the collection. Cannot be null.</param>
	/// <returns>true if the collection contains every value specified in types; otherwise, false.</returns>
	public bool ContainsAll(params TEnum[] types)
	{
		foreach (TEnum type in types)
			if (!Contains(type)) return false;
		return true;
	}
	/// <summary>
	/// Determines whether all specified enumeration values are contained within the current set.
	/// </summary>
	/// <remarks>If any of the provided collections is null, a NullReferenceException may occur. The method
	/// returns false as soon as a value is not found, without checking remaining values.</remarks>
	/// <param name="types">A parameter array of enumeration value collections to check for containment. Each collection must not be null.</param>
	/// <returns>true if every enumeration value in all provided collections is contained; otherwise, false.</returns>
	public bool ContainsAll(params IEnumerable<TEnum> types)
	{
		foreach (TEnum type in types)
			if (!Contains(type)) return false;
		return true;
	}
	/// <summary>
	/// Determines whether all specified enumeration values are contained within the current set.
	/// </summary>
	/// <remarks>If any collection in the parameter array is null, the method may throw an exception. The method
	/// returns false as soon as a missing value is found, which may improve performance for large sets.</remarks>
	/// <param name="types">A parameter array of enumeration value collections to check for containment. Each collection must not be null.</param>
	/// <returns>true if every enumeration value in all provided collections is contained; otherwise, false.</returns>
	public bool ContainsAll(ReadOnlyEnumCollection<TEnum> types)
	{
		for (int i = 0; i < Math.Min(_bits.Length, types._bits.Length); i++)
		{
			ulong tBits = types._bits[i];
			if (tBits != 0 && (_bits[i] & tBits) != tBits)
				return false;
		}
		return true;
	}
	/// <summary>
	/// Combines the values from the specified collection into the current collection, adding any elements that are
	/// present in the other collection but not already included.
	/// </summary>
	/// <param name="other">The collection whose elements are to be added to the current collection. Cannot be null.</param>
	/// <returns>true if the current collection was changed as a result of the operation; otherwise, false.</returns>
	/// <exception cref="ArgumentNullException">Thrown if <paramref name="other"/> is null.</exception>
	public ReadOnlyEnumCollection<TEnum> Concat(ReadOnlyEnumCollection<TEnum> other)
	{
		if (other == null) throw new ArgumentNullException(nameof(other));
		ReadOnlyEnumCollection<TEnum> result = new();
		int size = Math.Max(_bits.Length, other._bits.Length);
		for (int i = 0; i < size; i++)
		{
			ulong a = (i < _bits.Length) ? _bits[i] : 0ul;
			ulong b = (i < other._bits.Length) ? other._bits[i] : 0ul;
			ulong combined = a | b;
			result._bits[i] = combined;
		}
		return result;
	}
	/// <summary>
	/// Returns an enumerator that iterates through the set of values contained in the collection.
	/// </summary>
	/// <remarks>Enumeration reflects the current state of the collection at the time the enumerator is created.
	/// Modifying the collection during enumeration may result in undefined behavior.</remarks>
	/// <returns>An enumerator that can be used to iterate through the collection of <typeparamref name="TEnum"/> values.</returns>
	public IEnumerator<TEnum> GetEnumerator()
	{
		for (int div = 0; div < _bits.Length; div++)
		{
			ulong block = _bits[div];
			while (block != 0)
			{
				int rem = TrailingZeroCount(block);
				ulong value = (ulong)(div * bw + rem);
				yield return ToEnum(value);
				block &= (block - 1);
			}
		}
	}
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private static int TrailingZeroCount(ulong v)
	{
#if NET8_0_OR_GREATER
		return System.Numerics.BitOperations.TrailingZeroCount(v);
#else
		int c = 0;
		while ((v & 1ul) == 0)
		{
			v >>= 1;
			c++;
		}
		return c;
#endif
	}
}
