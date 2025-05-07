using System.Text.RegularExpressions;namespace RhythmBase.Components
{
	/// <summary>
	/// Provides a custom format provider for RDColor.
	/// </summary>
	internal class RDColorFormatInfo : IFormatProvider
	{
		/// <inheritdoc/>
		public object? GetFormat(Type? formatType) => formatType == typeof(ICustomFormatter) ? new RDColorFormatter() : (object?)null;
	}	/// <summary>
	/// Custom formatter for RDColor.
	/// </summary>
	internal class RDColorFormatter : ICustomFormatter, IFormatProvider
	{
		/// <inheritdoc/>
		public string Format(string? format, object? arg, IFormatProvider? formatProvider)
		{
			return arg is RDColor color
				? format switch
				{
					"RRGGBB" => $"{color.R:X2}{color.G:X2}{color.B:X2}",
					"RRGGBBAA" => $"{color.R:X2}{color.G:X2}{color.B:X2}{color.A:X2}",
					"AARRGGBB" => $"{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}",
					"#RRGGBB" => $"#{color.R:X2}{color.G:X2}{color.B:X2}",
					"#RRGGBBAA" => $"#{color.R:X2}{color.G:X2}{color.B:X2}{color.A:X2}",
					"#AARRGGBB" => $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}",
					"rrggbb" => $"{color.R:x2}{color.G:x2}{color.B:x2}",
					"rrggbbaa" => $"{color.R:x2}{color.G:x2}{color.B:x2}{color.A:x2}",
					"aarrggbb" => $"{color.A:x2}{color.R:x2}{color.G:x2}{color.B:x2}",
					"#rrggbb" => $"#{color.R:x2}{color.G:x2}{color.B:x2}",
					"#rrggbbaa" => $"#{color.R:x2}{color.G:x2}{color.B:x2}{color.A:x2}",
					"#aarrggbb" => $"#{color.A:x2}{color.R:x2}{color.G:x2}{color.B:x2}",
					"R,G,B" or "r,g,b" => $"{color.R},{color.G},{color.B}",
					"R,G,B,A" or "r,g,b,a" => $"{color.R},{color.G},{color.B},{color.A}",
					"A,R,G,B" or "a,r,g,b" => $"{color.A},{color.R},{color.G},{color.B}",
					"RGB" or "rgb" => $"R:{color.R},G:{color.G},B:{color.B}",
					"RGBA" or "rgba" => $"R:{color.R},G:{color.G},B:{color.B},A:{color.A}",
					"ARGB" or "argb" or _ => $"A:{color.A},R:{color.R},G:{color.G},B:{color.B}",
				}
				: arg?.ToString() ?? string.Empty;
		}		private static string ReplaceColorComponent(string format, char component, int value)
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
		}		/// <inheritdoc/>
		public object? GetFormat(Type? formatType) => formatType == typeof(ICustomFormatter) ? this : (object?)null;
	}
}
