using System.Collections;
using System.Runtime.CompilerServices;

namespace RhythmBase.Global.Components
{
	/// <summary>
	/// Represents a read-only, compact collection of enum values backed by a bitset.
	/// </summary>
	/// <typeparam name="TEnum">The enum type stored in the collection. Must be an enum value type.</typeparam>
	public class ReadOnlyEnumCollection<TEnum> : IEnumerable<TEnum> where TEnum : struct, Enum
	{
		private const int bw = sizeof(ulong) * 8;
		private readonly ulong mask;
		private readonly ulong[] _bits;
		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyEnumCollection{TEnum}"/> class
		/// by copying the internal bitset from an existing <see cref="EnumCollection{TEnum}"/>.
		/// </summary>
		/// <param name="source">The source <see cref="EnumCollection{TEnum}"/> to copy bits from.</param>
		internal ReadOnlyEnumCollection(EnumCollection<TEnum> source)
		{
			int size = source._bits.Length;
			_bits = new ulong[size];
			Array.Copy(source._bits, _bits, source._bits.Length);
			while (size-- > 0)
			{
				mask <<= 8;
				mask |= byte.MaxValue;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyEnumCollection{TEnum}"/> class
		/// from the supplied enum values. The internal bitset is sized to fit the largest provided value.
		/// </summary>
		/// <param name="values">A list of enum values to include in the collection.</param>
		public ReadOnlyEnumCollection(params TEnum[] values)
		{
			ulong enumMax = 0;
			int byteWidth = Unsafe.SizeOf<TEnum>();
			ulong enumMask = (1ul << (byteWidth * 8)) - 1;
			foreach (TEnum value in values)
			{
				TEnum vt = value;
				ulong v = Convert.ToUInt64(value);
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
			while (size-- > 0)
			{
				mask <<= 8;
				mask |= byte.MaxValue;
			}
			foreach (TEnum value in values)
			{
				ulong v = ToUL(value);
				int div = (int)(v / bw);
				int rem = (int)(v % bw);
				_bits[div] |= (1ul << rem);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyEnumCollection{TEnum}"/> class
		/// with a preallocated internal bitset of the given size (in 64-bit blocks) and the specified values set.
		/// </summary>
		/// <param name="capacity">Number of 64-bit blocks to allocate for the internal bitset.</param>
		/// <param name="values">A list of enum values to include in the collection.</param>
		internal ReadOnlyEnumCollection(int capacity, params TEnum[] values)
		{
			_bits = new ulong[capacity];
			while (capacity-- > 0)
			{
				mask <<= 8;
				mask |= byte.MaxValue;
			}
			foreach (TEnum value in values)
			{
				ulong v = ToUL(value);
				int div = (int)(v / bw);
				int rem = (int)(v % bw);
				_bits[div] |= (1ul << rem);
			}
		}

		/// <summary>
		/// Gets the number of enum values contained in the collection.
		/// </summary>
		/// <remarks>
		/// The count is computed by summing the set bits across the internal 64-bit blocks.
		/// A simple bit-clearing loop (Brian Kernighan's algorithm) is used for each block.
		/// </remarks>
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
		/// Reinterprets the supplied enum value as an unsigned 64-bit integer.
		/// </summary>
		/// <param name="value">The enum value to convert.</param>
		/// <returns>The 64-bit unsigned integer representation of <paramref name="value"/>.</returns>
		/// <remarks>
		/// This method uses <see cref="Unsafe.As{TFrom, TTo}(ref TFrom)"/> for a reinterpret cast.
		/// The caller must ensure that the underlying enum can be safely represented as 64 bits.
		/// </remarks>
		private ulong ToUL(TEnum value) => Unsafe.As<TEnum, ulong>(ref value) & mask;

		/// <summary>
		/// Reinterprets the supplied 64-bit unsigned integer as an enum value of type <typeparamref name="TEnum"/>.
		/// </summary>
		/// <param name="value">The 64-bit value to convert.</param>
		/// <returns>The enum value corresponding to <paramref name="value"/>.</returns>
		/// <remarks>
		/// This method uses <see cref="Unsafe.As{TFrom, TTo}(ref TFrom)"/> for a reinterpret cast.
		/// The caller must ensure <paramref name="value"/> is a valid representation for <typeparamref name="TEnum"/>.
		/// </remarks>
		private static TEnum ToEnum(ulong value) => Unsafe.As<ulong, TEnum>(ref value);

		/// <summary>
		/// Determines whether the specified enum value is present in the collection.
		/// </summary>
		/// <param name="type">The enum value to locate in the collection.</param>
		/// <returns><c>true</c> if the value is present; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// If the numeric value of <paramref name="type"/> maps outside the internally allocated bitset,
		/// an <see cref="IndexOutOfRangeException"/> or <see cref="IndexOutOfRangeException"/>-like error may be thrown
		/// by the underlying array access. Callers should ensure values are within the expected range.
		/// </remarks>
		public bool Contains(TEnum type)
		{
			ulong v = ToUL(type);
			int div = (int)(v / bw);
			int rem = (int)(v % bw);
			return (_bits[div] & (1ul << rem)) != 0;
		}

		/// <summary>
		/// Determines whether any of the specified enum values are present in the collection.
		/// </summary>
		/// <param name="types">An array of enum values to check for presence.</param>
		/// <returns><c>true</c> if any value in <paramref name="types"/> is present; otherwise <c>false</c>.</returns>
		public bool ContainsAny(TEnum[] types)
		{
			foreach (TEnum type in types)
				if (Contains(type)) return true;
			return false;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the set enum values present in the collection.
		/// </summary>
		/// <remarks>
		/// The enumerator yields values in ascending numeric order by their underlying integral value.
		/// Only values whose corresponding bit is set are returned.
		/// </remarks>
		/// <returns>An enumerator of <typeparamref name="TEnum"/>.</returns>
		public IEnumerator<TEnum> GetEnumerator()
		{
			for (int div = 0; div < _bits.Length; div++)
			{
				ulong block = _bits[div];
				for (int rem = 0; rem < bw; rem++)
				{
					if ((block & (1ul << rem)) != 0)
					{
						ulong value = (ulong)(div * bw + rem);
						yield return ToEnum(value);
					}
				}
			}
		}

		/// <summary>
		/// Returns a non-generic enumerator that iterates through the collection.
		/// </summary>
		/// <returns>An <see cref="IEnumerator"/> that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
