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
	internal static class FloatExtensions
	{
		extension(float)
		{
			public static float Parse(ReadOnlySpan<byte> value)
			{
				string str = System.Text.Encoding.UTF8.GetString(value.ToArray());
				return float.Parse(str);
			}
			public static float Floor(float value) => (float)Math.Floor(value);
			public static float Ceiling(float value) => (float)Math.Ceiling(value);
			public static float Abs(float value) => Math.Abs(value);
			public static float Round(float value) => (float)Math.Round(value);
		}
	}
	internal static class StringExtensions
	{
		extension(string e)
		{
			public static string Join(char separator, params object[] values) => string.Join(separator.ToString(), values);
			public static string Join(char separator, IEnumerable<string> values) => string.Join(separator.ToString(), values);
			public string[] Split(char separator, int count)
			{
				string[] result = new string[count];
				int startIndex = 0;
				int endIndex = 0;
				for (int i = 0; i < count - 1; i++)
				{
					endIndex = e.IndexOf(separator, startIndex);
					if (endIndex == -1)
						throw new ArgumentException("Not enough separators in the string.", nameof(separator));
					result[i] = e[startIndex..endIndex];
					startIndex = endIndex + 1;
				}
				result[count - 1] = e[startIndex..];
				return result;
			}
			public bool StartsWith(char value) => e.StartsWith(value.ToString());
		}
	}
	internal static class ByteExtensions
	{
		extension(byte)
		{
			public static (byte div, byte rem) DivRem(byte value, byte divisor)
			{
				byte div = (byte)(value / divisor);
				byte rem = (byte)(value % divisor);
				return (div, rem);
			}
		}
	}
}
namespace System.Diagnostics.CodeAnalysis
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
	internal class NotNullWhenAttribute : Attribute
	{
		public NotNullWhenAttribute(bool returnValue)
		{
			ReturnValue = returnValue;
		}
		public bool ReturnValue { get; }
	}
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
	internal sealed class NotNullAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
	internal sealed class AllowNullAttribute : Attribute
	{ }
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullAttribute : Attribute
	{
		public MemberNotNullAttribute(string member) => Members = [member];
		public MemberNotNullAttribute(params string[] members) => Members = members;
		public string[] Members { get; }
	}
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal
			sealed class MemberNotNullWhenAttribute : Attribute
	{
		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			ReturnValue = returnValue;
			Members = [member];
		}
		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			ReturnValue = returnValue;
			Members = members;
		}
		public bool ReturnValue { get; }
		public string[] Members { get; }
	}
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal
			sealed class MaybeNullWhenAttribute : Attribute
	{
		public MaybeNullWhenAttribute(bool returnValue) => ReturnValue = returnValue;
		public bool ReturnValue { get; }
	}
}
namespace System.Runtime.CompilerServices
{
	internal static class IsExternalInit
	{
	}
	internal sealed class CollectionBuilderAttribute : Attribute
	{
		public CollectionBuilderAttribute(Type builderType, string methodName)
		{
			BuilderType = builderType;
			MethodName = methodName;
		}
		public Type BuilderType { get; }
		public string MethodName { get; }
	}
}
namespace System.Text
{
	internal static class EncodingExtensions
	{
		public static string GetString(this Encoding encoding, ReadOnlySpan<byte> bytes) => encoding.GetString(bytes.ToArray());
	}
}
namespace System.IO
{
	internal static class PathExtensions
	{
		extension(Path)
		{
			public static string GetFullPath(string path, string basePath)
			{
				if (System.IO.Path.IsPathRooted(path))
					return path;
				return System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(basePath) ?? "", path));
			}
		}
	}
}
namespace System.Collections.Generic
{
internal static class StackExtensions
{
	public static bool TryPeek<T>(this Stack<T> stack, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out T result)
	{
		if (stack.Count > 0)
		{
			result = stack.Peek();
			return true;
		}

		result = default(T);
		return false;
	}

	public static bool TryPop<T>(this Stack<T> stack, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out T result)
	{
		if (stack.Count > 0)
		{
			result = stack.Pop();
			return true;
		}

		result = default(T);
		return false;
	}
}}
#endif