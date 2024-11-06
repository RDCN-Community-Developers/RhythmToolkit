using System.Text.RegularExpressions;

namespace RhythmBase.Components
{
	/// <summary>
	/// Provides a custom format provider for RDColor.
	/// </summary>
	public class RDColorFormatInfo : IFormatProvider
	{
		/// <inheritdoc/>
		public object? GetFormat(Type? formatType) => formatType == typeof(ICustomFormatter) ? new RDColorFormatter() : (object?)null;
	}

	/// <summary>
	/// Custom formatter for RDColor.
	/// </summary>
	public class RDColorFormatter : ICustomFormatter, IFormatProvider
	{
		/// <inheritdoc/>
		public string Format(string? format, object? arg, IFormatProvider? formatProvider)
		{
			if (arg is RDColor color)
			{
				format ??= "";
				format = ReplaceColorComponent(format, 'R', color.R);
				format = ReplaceColorComponent(format, 'G', color.G);
				format = ReplaceColorComponent(format, 'B', color.B);
				format = ReplaceColorComponent(format, 'A', color.A);
				format = ReplaceColorComponent(format, 'r', color.R);
				format = ReplaceColorComponent(format, 'g', color.G);
				format = ReplaceColorComponent(format, 'b', color.B);
				format = ReplaceColorComponent(format, 'a', color.A);
				return format;
			}
			return arg?.ToString() ?? string.Empty;
		}

		private static string ReplaceColorComponent(string format, char component, int value)
		{
			int startIndex = 0;
			while ((startIndex = format.IndexOf(component, startIndex)) != -1)
			{
				int length = 1;
				while (startIndex + length < format.Length && format[startIndex + length] == component)
				{
					length++;
				}
				string replacement = char.IsUpper(component) ? value.ToString($"X{length}") : value.ToString();
				format = string.Concat(format.AsSpan(0, startIndex), replacement, format.AsSpan(startIndex + length));
				startIndex += replacement.Length;
			}
			return format;
		}

		/// <inheritdoc/>
		public object? GetFormat(Type? formatType) => formatType == typeof(ICustomFormatter) ? this : (object?)null;
	}
}
