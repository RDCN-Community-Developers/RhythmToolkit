using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

#if NETSTANDARD2_0
namespace System
{
	internal readonly struct Index : IEquatable<Index>
	{
		public Index(int value, bool fromEnd = false)
		{
			Value = value;
			IsFromEnd = fromEnd;
		}
		public static Index End { get; } = new Index(0, false);
		public static Index Start { get; } = new Index(0, true);
		public bool IsFromEnd { get; }
		public int Value { get; }
		public static Index FromEnd(int value) => new Index(value, true);
		public static Index FromStart(int value) => new Index(value, false);
		public bool Equals(Index other) => Value == other.Value && IsFromEnd == other.IsFromEnd;
		public override bool Equals([NotNullWhen(true)] object? value) => value is Index other && Equals(other);
		public override int GetHashCode() => Value.GetHashCode() ^ IsFromEnd.GetHashCode();
		public int GetOffset(int length) => IsFromEnd ? length - Value : Value;
		public override string ToString() => IsFromEnd ? "^" + Value.ToString() : Value.ToString();
		public static implicit operator Index(int value) => new Index(value);
	}
	internal readonly struct Range : IEquatable<Range>
	{
		public Range(Index start, Index end)
		{
			Start = start;
			End = end;
		}
		public static Range All { get; }
		public Index End { get; }
		public Index Start { get; }
		public static Range EndAt(Index end) => new(new Index(0, false), end);
		public static Range StartAt(Index start) => new(start, new Index(0, true));
		public override bool Equals([NotNullWhen(true)] object? value) => value is Range other && Equals(other);
		public bool Equals(Range other) => Start.Equals(other.Start) && End.Equals(other.End);
		public override int GetHashCode() => Start.GetHashCode() ^ End.GetHashCode();
		public (int Offset, int Length) GetOffsetAndLength(int length)
		{
			int startOffset = Start.GetOffset(length);
			int endOffset = End.GetOffset(length);
			if (startOffset > endOffset)
				throw new ArgumentOutOfRangeException(nameof(length), "Start index must be less than or equal to end index.");
			return (startOffset, endOffset - startOffset);
		}
		public override string ToString() => $"[{Start}..{End}]";
	}
	internal static class IntExtensions
	{
		extension(int)
		{
			public static int Parse(ReadOnlySpan<byte> value)
			{
				string str = System.Text.Encoding.UTF8.GetString(value.ToArray());
				return int.Parse(str);
			}
		}
	}
}
namespace System.Diagnostics.CodeAnalysis
{
	internal class NotNullWhenAttribute : Attribute
	{
		public NotNullWhenAttribute(bool returnValue)
		{
			ReturnValue = returnValue;
		}
		public bool ReturnValue { get; }
	}
}
namespace System.Runtime.CompilerServices
{
	internal static class IsExternalInit
	{
	}
}
#endif