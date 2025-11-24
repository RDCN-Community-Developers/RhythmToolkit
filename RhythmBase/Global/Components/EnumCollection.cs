using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RhythmBase.Global.Components
{
	internal class EnumCollection<TEnum> : IEnumerable<TEnum> where TEnum : struct, Enum
	{
		private const int bw = sizeof(ulong) * 8;
		private readonly ulong mask;
		internal readonly ulong[] _bits;
		public EnumCollection(int capacity)
		{
			_bits = new ulong[capacity];
			int byteWidth = Unsafe.SizeOf<TEnum>();
			int bitWidth = byteWidth * 8;
			mask = (bitWidth >= 64) ? ulong.MaxValue : ((1ul << bitWidth) - 1ul);
		}
		public EnumCollection()
		{
			ulong enumMax = 0;
			int byteWidth = Unsafe.SizeOf<TEnum>();
			ulong enumMask = (byteWidth * 8 >= 64) ? ulong.MaxValue : ((1ul << (byteWidth * 8)) - 1ul);
			foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
			{
				TEnum vt = value;
				ulong v = Convert.ToUInt64(vt) & enumMask;
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
		}
		private ulong ToUL(TEnum value) => Unsafe.As<TEnum, ulong>(ref value) & mask;
		private static TEnum ToEnum(ulong value) => Unsafe.As<ulong, TEnum>(ref value);
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
		public bool IsEmpty
		{
			get
			{
				foreach (ulong block in _bits)
					if (block != 0) return false;
				return true;
			}
		}
		public bool Add(TEnum item)
		{
			ulong v = ToUL(item);
			int div = (int)(v / bw);
			int rem = (int)(v % bw);
			if (div >= _bits.Length || div < 0) throw new ArgumentOutOfRangeException(nameof(item), "Enum value out of collection range.");
			if ((_bits[div] & (1ul << rem)) != 0)
				return false;
			_bits[div] |= (1ul << rem);
			return true;
		}
		public bool Remove(TEnum item)
		{
			ulong v = ToUL(item);
			int div = (int)(v / bw);
			int rem = (int)(v % bw);
			if (div >= _bits.Length || div < 0) throw new ArgumentOutOfRangeException(nameof(item), "Enum value out of collection range.");
			if ((_bits[div] & (1ul << rem)) == 0)
				return false;
			_bits[div] &= ~(1ul << rem);
			return true;
		}
		internal bool Contains(TEnum type)
		{
			ulong v = ToUL(type);
			int div = (int)(v / bw);
			int rem = (int)(v % bw);
			if (div >= _bits.Length || div < 0) return false;
			return (_bits[div] & (1ul << rem)) != 0;
		}
		internal bool ContainsAny(params TEnum[] types)
		{
			foreach (TEnum type in types)
				if (Contains(type)) return true;
			return false;
		}
		internal bool ContainsAny(params IEnumerable<TEnum> types)
		{
			foreach (TEnum type in types)
				if (Contains(type)) return true;
			return false;
		}
		internal bool ContainsAll(params TEnum[] types)
		{
			foreach (TEnum type in types)
				if (!Contains(type)) return false;
			return true;
		}
		internal bool ContainsAll(params IEnumerable<TEnum> types)
		{
			foreach (TEnum type in types)
				if (!Contains(type)) return false;
			return true;
		}
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
}
