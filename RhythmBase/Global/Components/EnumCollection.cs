using System.Collections;
using System.Runtime.CompilerServices;

namespace RhythmBase.Global.Components
{
	/// <summary>
	/// Represents a compact collection of enum values backed by a bitset.
	/// </summary>
	/// <typeparam name="TEnum">The enum type stored in the collection. Must be an unmanaged enum type.</typeparam>
	internal class EnumCollection<TEnum> : IEnumerable<TEnum> where TEnum : struct, Enum
	{
		private const int bw = sizeof(ulong) * 8;
		internal readonly ulong[] _bits;

		/// <summary>
		/// Initializes a new instance of the <see cref="EnumCollection{TEnum}"/> class with the specified capacity in 64-bit blocks.
		/// </summary>
		/// <param name="capacity">
		/// Number of 64-bit blocks to allocate for the internal bitset. Each block stores 64 possible enum values.
		/// </param>
		public EnumCollection(int capacity)
		{
			_bits = new ulong[capacity];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EnumCollection{TEnum}"/> class sized to fit the maximum defined enum value.
		/// </summary>
		/// <remarks>
		/// This constructor inspects all values of <typeparamref name="TEnum"/> and allocates the minimal number of 64-bit blocks
		/// required to represent the largest enum value.
		/// </remarks>
		public EnumCollection()
		{
			int enumMax = 0;
			foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
			{
				ulong v = ToUL(value);
				if (v > (ulong)enumMax)
					enumMax = (int)v;
			}
			int size = (enumMax / bw) + 1;
			_bits = new ulong[size];
		}

		/// <summary>
		/// Converts an enum value to its unsigned 64-bit representation.
		/// </summary>
		/// <param name="value">The enum value to convert.</param>
		/// <returns>The 64-bit unsigned integer representing the enum value.</returns>
		/// <remarks>
		/// This method uses <see cref="Unsafe.As{TFrom, TTo}"/> for a reinterpret cast.
		/// The behavior assumes that the underlying enum can be represented in 64 bits.
		/// </remarks>
		private static ulong ToUL(TEnum value) => Unsafe.As<TEnum, ulong>(ref value);

		/// <summary>
		/// Converts an unsigned 64-bit value to the enum type.
		/// </summary>
		/// <param name="value">The 64-bit value to convert.</param>
		/// <returns>The enum value corresponding to <paramref name="value"/>.</returns>
		/// <remarks>
		/// This method uses <see cref="Unsafe.As{TFrom, TTo}"/> for a reinterpret cast.
		/// Use with care: the caller must ensure <paramref name="value"/> is a valid representation for <typeparamref name="TEnum"/>.
		/// </remarks>
		private static TEnum ToEnum(ulong value) => Unsafe.As<ulong, TEnum>(ref value);

		/// <summary>
		/// Gets the number of enum values contained in the collection.
		/// </summary>
		/// <remarks>
		/// The implementation counts the number of set bits across the internal bitset using a bit-clearing loop.
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
		/// Adds the specified enum value to the collection.
		/// </summary>
		/// <param name="item">The enum value to add.</param>
		/// <returns>
		/// <c>true</c> if the value was not already present and was added; otherwise <c>false</c>.
		/// </returns>
		/// <exception cref="IndexOutOfRangeException">
		/// Thrown when the numeric value of <paramref name="item"/> maps outside the internally allocated bitset.
		/// </exception>
		public bool Add(TEnum item)
		{
			ulong v = ToUL(item);
			int div = (int)(v / bw);
			int rem = (int)(v % bw);
			if ((_bits[div] & (1ul << rem)) != 0)
				return false;
			_bits[div] |= (1ul << rem);
			return true;
		}

		/// <summary>
		/// Removes the specified enum value from the collection.
		/// </summary>
		/// <param name="item">The enum value to remove.</param>
		/// <returns>
		/// <c>true</c> if the value was present and removed; otherwise <c>false</c>.
		/// </returns>
		/// <exception cref="IndexOutOfRangeException">
		/// Thrown when the numeric value of <paramref name="item"/> maps outside the internally allocated bitset.
		/// </exception>
		public bool Remove(TEnum item)
		{
			ulong v = ToUL(item);
			int div = (int)(v / bw);
			int rem = (int)(v % bw);
			if ((_bits[div] & (1ul << rem)) == 0)
				return false;
			_bits[div] &= ~(1ul << rem);
			return true;
		}

		/// <summary>
		/// Determines whether the specified enum value is present in the collection.
		/// </summary>
		/// <param name="type">The enum value to locate in the collection.</param>
		/// <returns><c>true</c> if the value is present; otherwise <c>false</c>.</returns>
		/// <exception cref="IndexOutOfRangeException">
		/// Thrown when the numeric value of <paramref name="type"/> maps outside the internally allocated bitset.
		/// </exception>
		internal bool Contains(TEnum type)
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
		internal bool ContainsAny(TEnum[] types)
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
		/// Returns an enumerator that iterates through the collection (non-generic implementation).
		/// </summary>
		/// <returns>An <see cref="IEnumerator"/> that can be used to iterate through the collection.</returns>
	 IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
