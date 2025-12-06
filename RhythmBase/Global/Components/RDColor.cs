using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text.Json.Serialization;

namespace RhythmBase.Global.Components
{
	/// <summary>
	/// Represents a color with red, green, blue, and alpha components.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	[JsonConverter(typeof(ColorConverter))]
	public struct RDColor(uint hex) :
#if NET7_0_OR_GREATER
		IEqualityOperators<RDColor, RDColor, bool>,
#endif
		IEquatable<RDColor>, IFormattable
	{
		/// <summary>
		/// #AARRGGBB
		/// </summary>
		private uint color = hex;
		/// <summary>
		/// Gets or sets the alpha component of the color.
		/// </summary>
		public byte A
		{
			readonly get => (byte)(color >> 24 & 0xFFu);
			set => color = color & 0x00FFFFFFu | (uint)value << 24;
		}
		/// <summary>
		/// Gets or sets the red component of the color.
		/// </summary>
		public byte R
		{
			readonly get => (byte)(color >> 16 & 0xFFu);
			set => color = color & 0xFF00FFFFu | (uint)value << 16;
		}
		/// <summary>
		/// Gets or sets the green component of the color.
		/// </summary>
		public byte G
		{
			readonly get => (byte)(color >> 8 & 0xFFu);
			set => color = color & 0xFFFF00FFu | (uint)value << 8;
		}
		/// <summary>
		/// Gets or sets the blue component of the color.
		/// </summary>
		public byte B
		{
			readonly get => (byte)(color & 0xFFu);
			set => color = color & 0xFFFFFF00u | value;
		}
		/// <summary>
		/// Returns a new RDColor instance with the specified alpha value.
		/// </summary>
		/// <param name="alpha">The alpha value to set.</param>
		/// <returns>A new RDColor instance with the specified alpha value.</returns>
		public readonly RDColor WithAlpha(byte alpha) => new(color & 0x00FFFFFFu | (uint)alpha << 24);
		/// <summary>
		/// Returns a new RDColor instance with the specified red value.
		/// </summary>
		/// <param name="red">The red value to set.</param>
		/// <returns>A new RDColor instance with the specified red value.</returns>
		public readonly RDColor WithRed(byte red) => new(color & 0xFF00FFFFu | (uint)red << 16);
		/// <summary>
		/// Returns a new RDColor instance with the specified green value.
		/// </summary>
		/// <param name="green">The green value to set.</param>
		/// <returns>A new RDColor instance with the specified green value.</returns>
		public readonly RDColor WithGreen(byte green) => new(color & 0xFFFF00FFu | (uint)green << 8);
		/// <summary>
		/// Returns a new RDColor instance with the specified blue value.
		/// </summary>
		/// <param name="blue">The blue value to set.</param>
		/// <returns>A new RDColor instance with the specified blue value.</returns>
		public readonly RDColor WithBlue(byte blue) => new(color & 0xFFFFFF00u | blue);
		/// <summary>
		/// Converts the color to HSL (Hue, Saturation, Lightness) color space.
		/// </summary>
		/// <param name="h">The hue component.</param>
		/// <param name="s">The saturation component.</param>
		/// <param name="l">The lightness component.</param>
		public readonly void ToHsl(out float h, out float s, out float l)
		{
			float r = R / 255f;
			float g = G / 255f;
			float b = B / 255f;

			float max = Math.Max(r, Math.Max(g, b));
			float min = Math.Min(r, Math.Min(g, b));
			float delta = max - min;
			h = 0;
			if (delta != 0)
			{
				if (max == r)
				{
					h = (g - b) / delta + (g < b ? 6 : 0);
				}
				else if (max == g)
				{
					h = (b - r) / delta + 2;
				}
				else
				{
					h = (r - g) / delta + 4;
				}
				h /= 6;
			}
			l = (max + min) / 2;
			s = delta == 0 ? 0 : delta / (1 - Math.Abs(2 * l - 1));
			h *= 360;
			s *= 100;
			l *= 100;
		}
		/// <summary>
		/// Converts the color to HSV (Hue, Saturation, Value) color space.
		/// </summary>
		/// <param name="h">The hue component.</param>
		/// <param name="s">The saturation component.</param>
		/// <param name="v">The value component.</param>
		public readonly void ToHsv(out float h, out float s, out float v)
		{
			float r = R / 255f;
			float g = G / 255f;
			float b = B / 255f;

			float max = Math.Max(r, Math.Max(g, b));
			float min = Math.Min(r, Math.Min(g, b));
			float delta = max - min;
			h = 0f;
			if (delta != 0)
			{
				if (max == r)
				{
					h = (g - b) / delta + (g < b ? 6 : 0);
				}
				else if (max == g)
				{
					h = (b - r) / delta + 2;
				}
				else if (max == b)
				{
					h = (r - g) / delta + 4;
				}
				h /= 6;
			}
			s = max == 0 ? 0 : delta / max;
			v = max;
			h *= 360;
			s *= 100;
			v *= 100;
		}
		/// <summary>
		/// Creates an RDColor instance from RGBA values.
		/// </summary>
		/// <param name="r">Red component</param>
		/// <param name="g">Green component</param>
		/// <param name="b">Blue component</param>
		/// <param name="a">Alpha component (default is 255)</param>
		/// <returns>RDColor instance</returns>
		public static RDColor FromRgba(byte r, byte g, byte b, byte a = 255) => new((uint)(a << 24 | r << 16 | g << 8 | b));
		/// <summary>
		/// Creates an <see cref="RDColor"/> instance from a hexadecimal string in RGBA format.
		/// Supports hexadecimal strings of length 3, 4, 6, or 8.
		/// </summary>
		/// <param name="hex">The hexadecimal string representing the color.</param>
		/// <returns>An <see cref="RDColor"/> instance created from the hexadecimal string.</returns>
		/// <exception cref="ArgumentException">Thrown when the hexadecimal string length is not 3, 4, 6, or 8.</exception>
		public static RDColor FromRgba(string hex)
		{
			if (TryFromRgba(hex, out RDColor color))
				return color;
			throw new ArgumentException("Hex string must be 3, 4, 6, or 8 characters long.");
		}

		/// <summary>
		/// Creates an <see cref="RDColor"/> instance from a hexadecimal byte span in RGBA format.
		/// Supports hexadecimal strings of length 3, 4, 6, or 8.
		/// </summary>
		/// <param name="hex">The byte span representing the hexadecimal color string.</param>
		/// <returns>An <see cref="RDColor"/> instance created from the hexadecimal byte span.</returns>
		/// <exception cref="ArgumentException">Thrown when the hexadecimal string length is not 3, 4, 6, or 8.</exception>
		public static RDColor FromRgba(ReadOnlySpan<byte> hex)
		{
			if (TryFromRgba(hex, out RDColor color))
				return color;
			throw new ArgumentException("Hex string must be 3, 4, 6, or 8 characters long.");
		}
		/// <summary>
		/// Converts a byte representing a hexadecimal character to its corresponding numeric value.
		/// </summary>
		/// <param name="b">The byte representing a hexadecimal character. Must be a valid ASCII character in the ranges '0'-'9', 'a'-'f', or
		/// 'A'-'F'.</param>
		/// <returns>The numeric value of the hexadecimal character, where '0'-'9' map to 0-9, 'a'-'f' map to 10-15, and 'A'-'F' map to
		/// 10-15.</returns>
		/// <exception cref="ArgumentException">Thrown if <paramref name="b"/> is not a valid hexadecimal character.</exception>
		private static byte FromChar(byte b)
		{
			return b switch
			{
				>= (byte)'0' and <= (byte)'9' => (byte)(b - (byte)'0'),
				>= (byte)'a' and <= (byte)'f' => (byte)(b - (byte)'a' + 10),
				>= (byte)'A' and <= (byte)'F' => (byte)(b - (byte)'A' + 10),
				_ => throw new ArgumentException("Invalid hex character."),
			};
		}
		/// <summary>
		/// Attempts to create an <see cref="RDColor"/> instance from a hexadecimal RGBA color string.
		/// </summary>
		/// <remarks>The method trims any leading or trailing whitespace from the input string. If the string starts
		/// with a '#', it is ignored during parsing. The method supports both shorthand (3 or 4 characters) and full-length
		/// (6 or 8 characters) hexadecimal color formats.</remarks>
		/// <param name="hex">A string representing the color in hexadecimal format. The string may optionally start with a '#' character and
		/// can be in one of the following formats: RGB (3 characters), RGBA (4 characters), RRGGBB (6 characters), or
		/// RRGGBBAA (8 characters).</param>
		/// <param name="color">When this method returns, contains the <see cref="RDColor"/> instance corresponding to the specified hexadecimal
		/// color string, if the conversion succeeded; otherwise, contains the default value of <see cref="RDColor"/>.</param>
		/// <returns><see langword="true"/> if the hexadecimal string was successfully converted to an <see cref="RDColor"/>;
		/// otherwise, <see langword="false"/>.</returns>
#if NETSTANDARD
		public static bool TryFromRgba(string hex, out RDColor color)
#else
		public static bool TryFromRgba(string hex, [MaybeNullWhen(false)] out RDColor color)
#endif
		{
			color = default;
			if (string.IsNullOrEmpty(hex))
				return false;
			hex = hex.Trim();
			if (hex.Length == 0)
			{
				color = default;
				return false;
			}
			if (hex[0] == '#')
#if NETSTANDARD
				hex = hex.Substring(1);
#else
				hex = hex[1..];
#endif
			switch (hex.Length)
			{
				case 3:
					color = new RDColor(
						(uint)(255 << 24 |
						(Convert.ToByte(new string(hex[0], 2), 16) << 16) |
						Convert.ToByte(new string(hex[1], 2), 16) << 8 |
						Convert.ToByte(new string(hex[2], 2), 16)));
					return true;
				case 4:
					color = new RDColor(
						(uint)(Convert.ToByte(new string(hex[3], 2), 16) << 24 |
						(Convert.ToByte(new string(hex[0], 2), 16) << 16) |
						Convert.ToByte(new string(hex[1], 2), 16) << 8 |
						Convert.ToByte(new string(hex[2], 2), 16)));
					return true;
				case 6:
					color = new RDColor(
						(uint)(255 << 24 |
						(Convert.ToByte(hex.Substring(0, 2), 16) << 16) |
						Convert.ToByte(hex.Substring(2, 2), 16) << 8 |
						Convert.ToByte(hex.Substring(4, 2), 16)));
					return true;
				case 8:
					color = new RDColor(
						(uint)(Convert.ToByte(hex.Substring(6, 2), 16) << 24 |
						(Convert.ToByte(hex.Substring(0, 2), 16) << 16) |
						Convert.ToByte(hex.Substring(2, 2), 16) << 8 |
						Convert.ToByte(hex.Substring(4, 2), 16)));
					return true;
				default:
					color = default;
					return false;
			}
		}
		/// <summary>
		/// Attempts to create an <see cref="RDColor"/> instance from a hexadecimal RGBA color representation.
		/// </summary>
		/// <remarks>This method does not throw exceptions for invalid input. Instead, it returns <see
		/// langword="false"/> if the input is not in a valid format. The method supports both shorthand (e.g., RGB, RGBA) and
		/// full-length (e.g., RRGGBB, RRGGBBAA) hexadecimal color representations.</remarks>
		/// <param name="hex">A read-only span of bytes representing the hexadecimal RGBA color. The input can optionally start with a '#'
		/// character and must be in one of the following formats: RGB (3 characters), RGBA (4 characters), RRGGBB (6
		/// characters), or RRGGBBAA (8 characters).</param>
		/// <param name="color">When this method returns, contains the resulting <see cref="RDColor"/> if the conversion was successful;
		/// otherwise, the default value of <see cref="RDColor"/>.</param>
		/// <returns><see langword="true"/> if the conversion was successful; otherwise, <see langword="false"/>.</returns>
#if NETSTANDARD
		public static bool TryFromRgba(ReadOnlySpan<byte> hex, out RDColor color)
#else
		public static bool TryFromRgba(ReadOnlySpan<byte> hex, [MaybeNullWhen(false)] out RDColor color)
#endif
		{
			if (hex.Length == 0)
			{
				color = default;
				return false;
			}
			if (hex[0] == '#')
				hex = hex.Slice(1);
			switch (hex.Length)
			{
				case 3:
					color = new RDColor(
						(uint)(
						255 << 24 |
						((FromChar(hex[0]) * 17) << 16) |
						((FromChar(hex[1]) * 17) << 8) |
						(FromChar(hex[2]) * 17)));
					return true;
				case 4:
					color = new RDColor(
						(uint)(
						((FromChar(hex[3]) * 17) << 24) |
						((FromChar(hex[0]) * 17) << 16) |
						((FromChar(hex[1]) * 17) << 8) |
						(FromChar(hex[2]) * 17)));
					return true;
				case 6:
					color = new RDColor(
						(uint)(
						255 << 24 |
						((FromChar(hex[0]) * 16 + FromChar(hex[1])) << 16) |
						((FromChar(hex[2]) * 16 + FromChar(hex[3])) << 8) |
						(FromChar(hex[4]) * 16 + FromChar(hex[5]))));
					return true;
				case 8:
					string str = hex.Slice(6, 2).ToString();
					color = new RDColor(
						(uint)(
						((FromChar(hex[6]) * 16 + FromChar(hex[7])) << 24) |
						((FromChar(hex[0]) * 16 + FromChar(hex[1])) << 16) |
						((FromChar(hex[2]) * 16 + FromChar(hex[3])) << 8) |
						(FromChar(hex[4]) * 16 + FromChar(hex[5]))));
					return true;
				default:
					color = default;
					return false;
			}
		}
		/// <summary>
		/// Creates an RDColor instance from a 32-bit RGBA value.
		/// </summary>
		/// <param name="hex">The 32-bit RGBA value.</param>
		/// <returns>A new RDColor instance.</returns>
		public static RDColor FromRgba(uint hex)
		{
			uint argb = hex & 0x00FFFFFFu | (hex & 0xFFu) << 24;
			return new RDColor(argb);
		}
		/// <summary>
		/// Creates an RDColor instance from ARGB values.
		/// </summary>
		/// <param name="a">Alpha component</param>
		/// <param name="r">Red component</param>
		/// <param name="g">Green component</param>
		/// <param name="b">Blue component</param>
		/// <returns>RDColor instance</returns>
		public static RDColor FromArgb(byte a, byte r, byte g, byte b) => new((uint)(a << 24 | r << 16 | g << 8 | b));
		/// <summary>
		/// Creates an RDColor instance from a hexadecimal string in ARGB format.
		/// Supports hexadecimal strings of length 3, 4, 6, or 8.
		/// </summary>
		/// <param name="hex">Hexadecimal string</param>
		/// <returns>RDColor instance</returns>
		/// <exception cref="ArgumentException">Thrown when the hexadecimal string length is not 3, 4, 6, or 8</exception>
		public static RDColor FromArgb(string hex)
		{
			hex = hex.Trim();
#if NETSTANDARD
			if (hex.StartsWith("#"))
				hex = hex.Substring(1);
#else
			if (hex.StartsWith('#'))
				hex = hex[1..];
#endif
			hex = hex.Length switch
			{
				3 => $"FF{hex[0]}{hex[0]}{hex[1]}{hex[1]}{hex[2]}{hex[2]}",
				4 => $"{hex[0]}{hex[0]}{hex[1]}{hex[1]}{hex[2]}{hex[2]}{hex[3]}{hex[3]}",
				6 => $"FF{hex}",
				8 => hex,
				_ => throw new ArgumentException("Hex string must be 3, 4, 6, or 8 characters long."),
			};
			return new RDColor(Convert.ToUInt32(hex, 16));
		}
		/// <summary>
		/// Tries to create an RDColor instance from a hexadecimal string.
		/// Supports hexadecimal strings of length 3, 4, 6, or 8.
		/// </summary>
		/// <param name="hex">The hexadecimal string representing the color.</param>
		/// <param name="color">When this method returns, contains the RDColor instance created from the hexadecimal string, if the conversion succeeded, or the default value if the conversion failed.</param>
		/// <returns>true if the hexadecimal string was converted successfully; otherwise, false.</returns>
#if NETSTANDARD
		public static bool TryFromArgb(string hex, out RDColor color)
#else
		public static bool TryFromArgb(string hex, [MaybeNullWhen(false)] out RDColor color)
#endif
		{
			hex = hex.Trim();
#if NETSTANDARD
			if (hex.StartsWith("#"))
				hex = hex.Substring(1);
#else
			if (hex.StartsWith('#'))
				hex = hex[1..];
#endif
			string? hex2 = hex.Length switch
			{
				3 => $"FF{hex[0]}{hex[0]}{hex[1]}{hex[1]}{hex[2]}{hex[2]}",
				4 => $"{hex[0]}{hex[0]}{hex[1]}{hex[1]}{hex[2]}{hex[2]}{hex[3]}{hex[3]}",
				6 => $"FF{hex}",
				8 => hex,
				_ => null,
			};
			if (hex2 is null)
			{
				color = default;
				return false;
			}
			color = new RDColor(Convert.ToUInt32(hex2, 16));
			return true;
		}
		/// <summary>
		/// Attempts to create an <see cref="RDColor"/> instance from a hexadecimal ARGB color representation.
		/// </summary>
		/// <remarks>This method does not validate the content of the hexadecimal string beyond its length and
		/// expected format. The caller must ensure that the input contains valid hexadecimal characters.</remarks>
		/// <param name="hex">A read-only span of bytes representing the hexadecimal ARGB color value. The input may optionally start with a '#'
		/// character. Supported formats include 3-digit (RGB), 4-digit (ARGB), 6-digit (RRGGBB), and 8-digit (AARRGGBB)
		/// hexadecimal strings.</param>
		/// <param name="color">When this method returns, contains the resulting <see cref="RDColor"/> instance if the conversion was successful;
		/// otherwise, contains the default value of <see cref="RDColor"/>.</param>
		/// <returns><see langword="true"/> if the conversion was successful; otherwise, <see langword="false"/>.</returns>
#if NETSTANDARD
		public static bool TryFromArgb(ReadOnlySpan<byte> hex, out RDColor color)
#else
		public static bool TryFromArgb(ReadOnlySpan<byte> hex, [MaybeNullWhen(true)] out RDColor color)
#endif
		{
			if (hex.Length == 0)
			{
				color = default;
				return false;
			}
			if (hex[0] == '#')
				hex = hex.Slice(1);
			switch (hex.Length)
			{
				case 3:
					color = new RDColor(
						(uint)(
						255 << 24 |
						((FromChar(hex[0]) * 17) << 16) |
						((FromChar(hex[1]) * 17) << 8) |
						(FromChar(hex[2]) * 17)));
					return true;
				case 4:
					color = new RDColor(
						(uint)(
						((FromChar(hex[0]) * 17) << 24) |
						((FromChar(hex[1]) * 17) << 16) |
						((FromChar(hex[2]) * 17) << 8) |
						(FromChar(hex[3]) * 17)));
					return true;
				case 6:
					color = new RDColor(
						(uint)(
						255 << 24 |
						((FromChar(hex[0]) * 16 + FromChar(hex[1])) << 16) |
						((FromChar(hex[2]) * 16 + FromChar(hex[3])) << 8) |
						(FromChar(hex[4]) * 16 + FromChar(hex[5]))));
					return true;
				case 8:
					string str = hex.Slice(6, 2).ToString();
					color = new RDColor(
						(uint)(
						((FromChar(hex[0]) * 16 + FromChar(hex[1])) << 24) |
						((FromChar(hex[2]) * 16 + FromChar(hex[3])) << 16) |
						((FromChar(hex[4]) * 16 + FromChar(hex[5])) << 8) |
						(FromChar(hex[6]) * 16 + FromChar(hex[7]))));
					return true;
				default:
					color = default;
					return false;
			}
		}
		/// <summary>
		/// Creates an <see cref="RDColor"/> instance from a 32-bit ARGB value.
		/// </summary>
		/// <param name="hex">The 32-bit ARGB value.</param>
		/// <returns>A new <see cref="RDColor"/> instance.</returns>
		public static RDColor FromArgb(uint hex) => new(hex);
		/// <summary>
		/// Creates an RDColor object from the HSL color space.
		/// </summary>
		/// <param name="h">Hue (0-360)</param>
		/// <param name="s">Saturation (0-1)</param>
		/// <param name="l">Lightness (0-1)</param>
		/// <param name="a">Alpha (0-255)</param>
		/// <returns>Corresponding RDColor object</returns>
		public static RDColor FromHsl(float h, float s, float l, byte a = 255)
		{
			h /= 360;
			s /= 100;
			l /= 100;
			float r, g, b;
			if (s == 0)
			{
				r = g = b = l; // achromatic
			}
			else
			{
				static float hue2rgb(float p, float q, float t)
				{
					if (t < 0) t += 1;
					if (t > 1) t -= 1;
					if (t < 1 / 6.0) return p + (q - p) * 6 * t;
					if (t < 1 / 2.0) return q;
					if (t < 2 / 3.0) return p + (q - p) * (2 / 3.0f - t) * 6;
					return p;
				}
				float q = l < 0.5 ? l * (1 + s) : l + s - l * s;
				float p = 2 * l - q;
				r = hue2rgb(p, q, h + 1 / 3.0f);
				g = hue2rgb(p, q, h);
				b = hue2rgb(p, q, h - 1 / 3.0f);
			}
			return new RDColor((uint)(a << 24 | ((int)(r * 255) << 16) | (int)(g * 255) << 8 | (int)(b * 255)));
		}
		/// <summary>
		/// Creates an RDColor object from HSV values.
		/// </summary>
		/// <param name="h">Hue (0-360)</param>
		/// <param name="s">Saturation (0-1)</param>
		/// <param name="v">Value (0-1)</param>
		/// <param name="a">Alpha (0-255)</param>
		/// <returns>Returns an RDColor object.</returns>
		public static RDColor FromHsv(float h, float s, float v, byte a = 255)
		{
			h /= 360;
			s /= 100;
			v /= 100;
			int hi = (int)(h * 6);
			float f = h * 6 - hi;
			float p = v * (1 - s);
			float q = v * (1 - f * s);
			float t = v * (1 - (1 - f) * s);
			float r = 0, g = 0, b = 0;
			switch (hi % 6)
			{
				case 0: r = v; g = t; b = p; break;
				case 1: r = q; g = v; b = p; break;
				case 2: r = p; g = v; b = t; break;
				case 3: r = p; g = q; b = v; break;
				case 4: r = t; g = p; b = v; break;
				case 5: r = v; g = p; b = q; break;
			}
			return new RDColor((uint)(a << 24 | ((int)(r * 255) << 16) | (int)(g * 255) << 8 | (int)(b * 255)));
		}
		/// <summary>
		/// Creates an RDColor instance from a color name.
		/// </summary>
		/// <param name="name">The name of the color.</param>
		/// <returns>An RDColor instance corresponding to the specified color name.</returns>
		/// <exception cref="ArgumentException">Thrown when the color name is invalid.</exception>
		public static RDColor FromName(string name)
		{
			if (TryFromName(name, out RDColor color))
				return color;
			throw new ArgumentException($"Invalid color name: {name}.");
		}
		/// <summary>
		/// Tries to create an RDColor instance from a color name.
		/// </summary>
		/// <param name="name">The name of the color.</param>
		/// <param name="color">When this method returns, contains the RDColor instance created from the color name, if the conversion succeeded, or the default value if the conversion failed.</param>
		/// <returns>true if the color name was converted successfully; otherwise, false.</returns>
#if NETSTANDARD
		public static bool TryFromName(string name, out RDColor color)
#else
		public static bool TryFromName(string name, [MaybeNullWhen(false)] out RDColor color)
#endif
		{
			RDColor? color2 = name.ToLower() switch
			{
				"aliceblue" => AliceBlue,
				"antiquewhite" => AntiqueWhite,
				"aqua" => Aqua,
				"aquamarine" => Aquamarine,
				"azure" => Azure,
				"beige" => Beige,
				"bisque" => Bisque,
				"black" => Black,
				"blanchedalmond" => BlanchedAlmond,
				"blue" => Blue,
				"blueviolet" => BlueViolet,
				"brown" => Brown,
				"burlywood" => BurlyWood,
				"cadetblue" => CadetBlue,
				"chartreuse" => Chartreuse,
				"chocolate" => Chocolate,
				"coral" => Coral,
				"cornflowerblue" => CornflowerBlue,
				"cornsilk" => Cornsilk,
				"crimson" => Crimson,
				"cyan" => Cyan,
				"darkblue" => DarkBlue,
				"darkcyan" => DarkCyan,
				"darkgoldenrod" => DarkGoldenrod,
				"darkgray" => DarkGray,
				"darkgreen" => DarkGreen,
				"darkkhaki" => DarkKhaki,
				"darkmagenta" => DarkMagenta,
				"darkolivegreen" => DarkOliveGreen,
				"darkorange" => DarkOrange,
				"darkorchid" => DarkOrchid,
				"darkred" => DarkRed,
				"darksalmon" => DarkSalmon,
				"darkseagreen" => DarkSeaGreen,
				"darkslateblue" => DarkSlateBlue,
				"darkslategray" => DarkSlateGray,
				"darkturquoise" => DarkTurquoise,
				"darkviolet" => DarkViolet,
				"deeppink" => DeepPink,
				"deepskyblue" => DeepSkyBlue,
				"dimgray" => DimGray,
				"dodgerblue" => DodgerBlue,
				"firebrick" => Firebrick,
				"floralwhite" => FloralWhite,
				"forestgreen" => ForestGreen,
				"fuchsia" => Fuchsia,
				"gainsboro" => Gainsboro,
				"ghostwhite" => GhostWhite,
				"gold" => Gold,
				"goldenrod" => Goldenrod,
				"gray" => Gray,
				"green" => Green,
				"greenyellow" => GreenYellow,
				"honeydew" => Honeydew,
				"hotpink" => HotPink,
				"indianred" => IndianRed,
				"indigo" => Indigo,
				"ivory" => Ivory,
				"khaki" => Khaki,
				"lavender" => Lavender,
				"lavenderblush" => LavenderBlush,
				"lawngreen" => LawnGreen,
				"lemonchiffon" => LemonChiffon,
				"lightblue" => LightBlue,
				"lightcoral" => LightCoral,
				"lightcyan" => LightCyan,
				"lightgoldenrodyellow" => LightGoldenrodYellow,
				"lightgray" => LightGray,
				"lightgreen" => LightGreen,
				"lightpink" => LightPink,
				"lightsalmon" => LightSalmon,
				"lightseagreen" => LightSeaGreen,
				"lightskyblue" => LightSkyBlue,
				"lightslategray" => LightSlateGray,
				"lightsteelblue" => LightSteelBlue,
				"lightyellow" => LightYellow,
				"lime" => Lime,
				"limegreen" => LimeGreen,
				"linen" => Linen,
				"magenta" => Magenta,
				"maroon" => Maroon,
				"mediumaquamarine" => MediumAquamarine,
				"mediumblue" => MediumBlue,
				"mediumorchid" => MediumOrchid,
				"mediumpurple" => MediumPurple,
				"mediumseagreen" => MediumSeaGreen,
				"mediumslateblue" => MediumSlateBlue,
				"mediumspringgreen" => MediumSpringGreen,
				"mediumturquoise" => MediumTurquoise,
				"mediumvioletred" => MediumVioletRed,
				"midnightblue" => MidnightBlue,
				"mintcream" => MintCream,
				"mistyrose" => MistyRose,
				"moccasin" => Moccasin,
				"navajowhite" => NavajoWhite,
				"navy" => Navy,
				"oldlace" => OldLace,
				"olive" => Olive,
				"olivedrab" => OliveDrab,
				"orange" => Orange,
				"orangered" => OrangeRed,
				"orchid" => Orchid,
				"palegoldenrod" => PaleGoldenrod,
				"palegreen" => PaleGreen,
				"paleturquoise" => PaleTurquoise,
				"palevioletred" => PaleVioletRed,
				"papayawhip" => PapayaWhip,
				"peachpuff" => PeachPuff,
				"peru" => Peru,
				"pink" => Pink,
				"plum" => Plum,
				"powderblue" => PowderBlue,
				"purple" => Purple,
				"red" => Red,
				"rosybrown" => RosyBrown,
				"royalblue" => RoyalBlue,
				"saddlebrown" => SaddleBrown,
				"salmon" => Salmon,
				"sandybrown" => SandyBrown,
				"seagreen" => SeaGreen,
				"seashell" => SeaShell,
				"sienna" => Sienna,
				"silver" => Silver,
				"skyblue" => SkyBlue,
				"slateblue" => SlateBlue,
				"slategray" => SlateGray,
				"snow" => Snow,
				"springgreen" => SpringGreen,
				"steelblue" => SteelBlue,
				"tan" => Tan,
				"teal" => Teal,
				"thistle" => Thistle,
				"tomato" => Tomato,
				"turquoise" => Turquoise,
				"violet" => Violet,
				"wheat" => Wheat,
				"white" => White,
				"whitesmoke" => WhiteSmoke,
				"yellow" => Yellow,
				"yellowgreen" => YellowGreen,
				"transparent" => Transparent,
				"empty" => Empty,
				_ => null,
			};
			if (color2 is null)
			{
				color = default;
				return false;
			}
			color = color2.Value;
			return true;
		}
		/// <summary>
		/// Tries to get the name(s) of the color.
		/// </summary>
		/// <param name="names">When this method returns, contains the name(s) of the color if the conversion succeeded, or an empty array if the conversion failed.</param>
		/// <returns>true if the color name(s) were found; otherwise, false.</returns>
		/// <remarks>
		/// For colors with multiple names, the CMYK name is preferred.
		/// </remarks>
		public readonly bool TryGetName(out string[] names)
		{
			names = color switch
			{
				4293982463u => ["AliceBlue"],
				4294634455u => ["AntiqueWhite"],
				4278255615u => ["Cyan", "Aqua"],
				4286578644u => ["Aquamarine"],
				4293984255u => ["Azure"],
				4294309340u => ["Beige"],
				4294960324u => ["Bisque"],
				4278190080u => ["Black"],
				4294962125u => ["BlanchedAlmond"],
				4278190335u => ["Blue"],
				4287245282u => ["BlueViolet"],
				4289014314u => ["Brown"],
				4292786311u => ["BurlyWood"],
				4284456608u => ["CadetBlue"],
				4286578432u => ["Chartreuse"],
				4291979550u => ["Chocolate"],
				4294934352u => ["Coral"],
				4284782061u => ["CornflowerBlue"],
				4294965468u => ["Cornsilk"],
				4292613180u => ["Crimson"],
				4278190219u => ["DarkBlue"],
				4278225803u => ["DarkCyan"],
				4290283019u => ["DarkGoldenrod"],
				4289309097u => ["DarkGray"],
				4278215680u => ["DarkGreen"],
				4290623339u => ["DarkKhaki"],
				4287299723u => ["DarkMagenta"],
				4283788079u => ["DarkOliveGreen"],
				4294937600u => ["DarkOrange"],
				4288230092u => ["DarkOrchid"],
				4287299584u => ["DarkRed"],
				4293498490u => ["DarkSalmon"],
				4287609995u => ["DarkSeaGreen"],
				4282924427u => ["DarkSlateBlue"],
				4281290575u => ["DarkSlateGray"],
				4278243025u => ["DarkTurquoise"],
				4287889619u => ["DarkViolet"],
				4294907027u => ["DeepPink"],
				4278239231u => ["DeepSkyBlue"],
				4285098345u => ["DimGray"],
				4280193279u => ["DodgerBlue"],
				4289864226u => ["Firebrick"],
				4294966000u => ["FloralWhite"],
				4280453922u => ["ForestGreen"],
				4294902015u => ["Magenta", "Fuchsia"],
				4292664540u => ["Gainsboro"],
				4294506751u => ["GhostWhite"],
				4294956800u => ["Gold"],
				4292519200u => ["Goldenrod"],
				4286611584u => ["Gray"],
				4278222848u => ["Green"],
				4289593135u => ["GreenYellow"],
				4293984240u => ["Honeydew"],
				4294928820u => ["HotPink"],
				4291648604u => ["IndianRed"],
				4283105410u => ["Indigo"],
				4294967280u => ["Ivory"],
				4293977740u => ["Khaki"],
				4293322490u => ["Lavender"],
				4294963445u => ["LavenderBlush"],
				4286381056u => ["LawnGreen"],
				4294965965u => ["LemonChiffon"],
				4289583334u => ["LightBlue"],
				4293951616u => ["LightCoral"],
				4292935679u => ["LightCyan"],
				4294638290u => ["LightGoldenrodYellow"],
				4292072403u => ["LightGray"],
				4287688336u => ["LightGreen"],
				4294948545u => ["LightPink"],
				4294942842u => ["LightSalmon"],
				4280332970u => ["LightSeaGreen"],
				4287090426u => ["LightSkyBlue"],
				4286023833u => ["LightSlateGray"],
				4289774814u => ["LightSteelBlue"],
				4294967264u => ["LightYellow"],
				4278255360u => ["Lime"],
				4281519410u => ["LimeGreen"],
				4294635750u => ["Linen"],
				4286578688u => ["Maroon"],
				4284927402u => ["MediumAquamarine"],
				4278190285u => ["MediumBlue"],
				4290401747u => ["MediumOrchid"],
				4287852763u => ["MediumPurple"],
				4282168177u => ["MediumSeaGreen"],
				4286277870u => ["MediumSlateBlue"],
				4278254234u => ["MediumSpringGreen"],
				4282962380u => ["MediumTurquoise"],
				4291237253u => ["MediumVioletRed"],
				4279834992u => ["MidnightBlue"],
				4294311930u => ["MintCream"],
				4294960353u => ["MistyRose"],
				4294960309u => ["Moccasin"],
				4294958765u => ["NavajoWhite"],
				4278190208u => ["Navy"],
				4294833638u => ["OldLace"],
				4286611456u => ["Olive"],
				4285238819u => ["OliveDrab"],
				4294944000u => ["Orange"],
				4294919424u => ["OrangeRed"],
				4292505814u => ["Orchid"],
				4293847210u => ["PaleGoldenrod"],
				4288215960u => ["PaleGreen"],
				4289720046u => ["PaleTurquoise"],
				4292571283u => ["PaleVioletRed"],
				4294963157u => ["PapayaWhip"],
				4294957753u => ["PeachPuff"],
				4291659071u => ["Peru"],
				4294951115u => ["Pink"],
				4292714717u => ["Plum"],
				4289781990u => ["PowderBlue"],
				4286578816u => ["Purple"],
				4294901760u => ["Red"],
				4290547599u => ["RosyBrown"],
				4282477025u => ["RoyalBlue"],
				4287317267u => ["SaddleBrown"],
				4294606962u => ["Salmon"],
				4294222944u => ["SandyBrown"],
				4281240407u => ["SeaGreen"],
				4294964718u => ["SeaShell"],
				4288696877u => ["Sienna"],
				4290822336u => ["Silver"],
				4287090411u => ["SkyBlue"],
				4285160141u => ["SlateBlue"],
				4285563024u => ["SlateGray"],
				4294966010u => ["Snow"],
				4278255487u => ["SpringGreen"],
				4282811060u => ["SteelBlue"],
				4291998860u => ["Tan"],
				4278222976u => ["Teal"],
				4292394968u => ["Thistle"],
				4294927175u => ["Tomato"],
				4282441936u => ["Turquoise"],
				4293821166u => ["Violet"],
				4294303411u => ["Wheat"],
				uint.MaxValue => ["White"],
				4294309365u => ["WhiteSmoke"],
				4294967040u => ["Yellow"],
				4288335154u => ["YellowGreen"],
				16777215u => ["Transparent"],
				0u => ["Empty"],
				_ => [],
			};
			if (names.Length == 0)
				return false;
			return true;
		}
		/// <summary>
		/// Linearly interpolates between two <see cref="RDColor"/> values based on a weighting factor.
		/// </summary>
		/// <remarks>This method interpolates each color channel (alpha, red, green, and blue)
		/// independently.</remarks>
		/// <param name="a">The starting color.</param>
		/// <param name="b">The ending color.</param>
		/// <param name="t">A value between 0 and 1 that specifies the interpolation factor.  If <paramref name="t"/> is 0, the result is
		/// <paramref name="a"/>.  If <paramref name="t"/> is 1, the result is <paramref name="b"/>.  Values outside the range
		/// [0, 1] are clamped to the nearest boundary.</param>
		/// <returns>A new <see cref="RDColor"/> that represents the interpolated color.</returns>
		public static RDColor Lerp(RDColor a, RDColor b, float t)
		{
			t = t < 0 ? 0 : t > 1 ? 1 : t;
			byte aA = a.A, aR = a.R, aG = a.G, aB = a.B;
			byte bA = b.A, bR = b.R, bG = b.G, bB = b.B;
			byte rA = (byte)(aA + (bA - aA) * t);
			byte rR = (byte)(aR + (bR - aR) * t);
			byte rG = (byte)(aG + (bG - aG) * t);
			byte rB = (byte)(aB + (bB - aB) * t);
			return FromArgb(rA, rR, rG, rB);
		}
		/// <inheritdoc/>
		public static bool operator ==(RDColor left, RDColor right) => left.color == right.color;
		/// <inheritdoc/>
		public static bool operator !=(RDColor left, RDColor right) => left.color != right.color;
		/// <summary>
		/// Defines an implicit conversion from a 32-bit unsigned integer to an RDColor instance.
		/// </summary>
		/// <remarks>This operator allows a uint value to be assigned directly to an RDColor variable without explicit
		/// casting. The interpretation of the uint value depends on the RDColor format, typically encoding color channels in
		/// ARGB or RGBA order.</remarks>
		/// <param name="color">The 32-bit unsigned integer representing the color value to convert.</param>
		public static implicit operator RDColor(uint color) => new(color);
		/// <summary>
		/// Converts an RDColor instance to its 32-bit unsigned integer representation.
		/// </summary>
		/// <remarks>This operator enables implicit conversion from RDColor to uint, allowing RDColor values to be
		/// used wherever a 32-bit unsigned integer is expected. The returned value represents the packed color value as
		/// defined by the RDColor structure.</remarks>
		/// <param name="color">The RDColor instance to convert.</param>
		public static implicit operator uint(RDColor color) => color.color;
		/// <inheritdoc/>
		public override readonly string ToString() => ToString("#AARRGGBB");
		private readonly string GetDebuggerDisplay() => ToString();
		/// <inheritdoc/>
		public readonly bool Equals(RDColor other) => color == other.color;
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly bool Equals(object? obj) => obj is RDColor e && Equals(e);
#else
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDColor e && Equals(e);
#endif
		/// <inheritdoc/>
		public override readonly int GetHashCode() => color.GetHashCode();
		/// <inheritdoc/>
		public readonly string ToString(string? format, IFormatProvider? formatProvider) =>
			(formatProvider ?? new RDColorFormatInfo()).GetFormat(typeof(ICustomFormatter)) is ICustomFormatter formatter
				? formatter.Format(format, this, formatProvider)
				: ToString();
		/// <inheritdoc/>
		public readonly string ToString(string? format) => ToString(format, null);
		///<summary>
		///Gets the predefined color of alice blue, or #FFF0F8FF.
		///</summary>
		public static readonly RDColor AliceBlue = new(4293982463u);
		///<summary>
		///Gets the predefined color of antique white, or #FFFAEBD7.
		///</summary>
		public static readonly RDColor AntiqueWhite = new(4294634455u);
		///<summary>
		///Gets the predefined color of aqua, or #FF00FFFF.
		///</summary>
		public static readonly RDColor Aqua = new(4278255615u);
		///<summary>
		///Gets the predefined color of aquamarine, or #FF7FFFD4.
		///</summary>
		public static readonly RDColor Aquamarine = new(4286578644u);
		///<summary>
		///Gets the predefined color of azure, or #FFF0FFFF.
		///</summary>
		public static readonly RDColor Azure = new(4293984255u);
		///<summary>
		///Gets the predefined color of beige, or #FFF5F5DC.
		///</summary>
		public static readonly RDColor Beige = new(4294309340u);
		///<summary>
		///Gets the predefined color of bisque, or #FFFFE4C4.
		///</summary>
		public static readonly RDColor Bisque = new(4294960324u);
		///<summary>
		///Gets the predefined color of black, or #FF000000.
		///</summary>
		public static readonly RDColor Black = new(4278190080u);
		///<summary>
		///Gets the predefined color of blanched almond, or #FFFFEBCD.
		///</summary>
		public static readonly RDColor BlanchedAlmond = new(4294962125u);
		///<summary>
		///Gets the predefined color of blue, or #FF0000FF.
		///</summary>
		public static readonly RDColor Blue = new(4278190335u);
		///<summary>
		///Gets the predefined color of blue violet, or #FF8A2BE2.
		///</summary>
		public static readonly RDColor BlueViolet = new(4287245282u);
		///<summary>
		///Gets the predefined color of brown, or #FFA52A2A.
		///</summary>
		public static readonly RDColor Brown = new(4289014314u);
		///<summary>
		///Gets the predefined color of burly wood, or #FFDEB887.
		///</summary>
		public static readonly RDColor BurlyWood = new(4292786311u);
		///<summary>
		///Gets the predefined color of cadet blue, or #FF5F9EA0.
		///</summary>
		public static readonly RDColor CadetBlue = new(4284456608u);
		///<summary>
		///Gets the predefined color of chartreuse, or #FF7FFF00.
		///</summary>
		public static readonly RDColor Chartreuse = new(4286578432u);
		///<summary>
		///Gets the predefined color of chocolate, or #FFD2691E.
		///</summary>
		public static readonly RDColor Chocolate = new(4291979550u);
		///<summary>
		///Gets the predefined color of coral, or #FFFF7F50.
		///</summary>
		public static readonly RDColor Coral = new(4294934352u);
		///<summary>
		///Gets the predefined color of cornflower blue, or #FF6495ED.
		///</summary>
		public static readonly RDColor CornflowerBlue = new(4284782061u);
		///<summary>
		///Gets the predefined color of cornsilk, or #FFFFF8DC.
		///</summary>
		public static readonly RDColor Cornsilk = new(4294965468u);
		///<summary>
		///Gets the predefined color of crimson, or #FFDC143C.
		///</summary>
		public static readonly RDColor Crimson = new(4292613180u);
		///<summary>
		///Gets the predefined color of cyan, or #FF00FFFF.
		///</summary>
		public static readonly RDColor Cyan = new(4278255615u);
		///<summary>
		///Gets the predefined color of dark blue, or #FF00008B.
		///</summary>
		public static readonly RDColor DarkBlue = new(4278190219u);
		///<summary>
		///Gets the predefined color of dark cyan, or #FF008B8B.
		///</summary>
		public static readonly RDColor DarkCyan = new(4278225803u);
		///<summary>
		///Gets the predefined color of dark goldenrod, or #FFB8860B.
		///</summary>
		public static readonly RDColor DarkGoldenrod = new(4290283019u);
		///<summary>
		///Gets the predefined color of dark gray, or #FFA9A9A9.
		///</summary>
		public static readonly RDColor DarkGray = new(4289309097u);
		///<summary>
		///Gets the predefined color of dark green, or #FF006400.
		///</summary>
		public static readonly RDColor DarkGreen = new(4278215680u);
		///<summary>
		///Gets the predefined color of dark khaki, or #FFBDB76B.
		///</summary>
		public static readonly RDColor DarkKhaki = new(4290623339u);
		///<summary>
		///Gets the predefined color of dark magenta, or #FF8B008B.
		///</summary>
		public static readonly RDColor DarkMagenta = new(4287299723u);
		///<summary>
		///Gets the predefined color of dark olive green, or #FF556B2F.
		///</summary>
		public static readonly RDColor DarkOliveGreen = new(4283788079u);
		///<summary>
		///Gets the predefined color of dark orange, or #FFFF8C00.
		///</summary>
		public static readonly RDColor DarkOrange = new(4294937600u);
		///<summary>
		///Gets the predefined color of dark orchid, or #FF9932CC.
		///</summary>
		public static readonly RDColor DarkOrchid = new(4288230092u);
		///<summary>
		///Gets the predefined color of dark red, or #FF8B0000.
		///</summary>
		public static readonly RDColor DarkRed = new(4287299584u);
		///<summary>
		///Gets the predefined color of dark salmon, or #FFE9967A.
		///</summary>
		public static readonly RDColor DarkSalmon = new(4293498490u);
		///<summary>
		///Gets the predefined color of dark sea green, or #FF8FBC8B.
		///</summary>
		public static readonly RDColor DarkSeaGreen = new(4287609995u);
		///<summary>
		///Gets the predefined color of dark slate blue, or #FF483D8B.
		///</summary>
		public static readonly RDColor DarkSlateBlue = new(4282924427u);
		///<summary>
		///Gets the predefined color of dark slate gray, or #FF2F4F4F.
		///</summary>
		public static readonly RDColor DarkSlateGray = new(4281290575u);
		///<summary>
		///Gets the predefined color of dark turquoise, or #FF00CED1.
		///</summary>
		public static readonly RDColor DarkTurquoise = new(4278243025u);
		///<summary>
		///Gets the predefined color of dark violet, or #FF9400D3.
		///</summary>
		public static readonly RDColor DarkViolet = new(4287889619u);
		///<summary>
		///Gets the predefined color of deep pink, or #FFFF1493.
		///</summary>
		public static readonly RDColor DeepPink = new(4294907027u);
		///<summary>
		///Gets the predefined color of deep sky blue, or #FF00BFFF.
		///</summary>
		public static readonly RDColor DeepSkyBlue = new(4278239231u);
		///<summary>
		///Gets the predefined color of dim gray, or #FF696969.
		///</summary>
		public static readonly RDColor DimGray = new(4285098345u);
		///<summary>
		///Gets the predefined color of dodger blue, or #FF1E90FF.
		///</summary>
		public static readonly RDColor DodgerBlue = new(4280193279u);
		///<summary>
		///Gets the predefined color of firebrick, or #FFB22222.
		///</summary>
		public static readonly RDColor Firebrick = new(4289864226u);
		///<summary>
		///Gets the predefined color of floral white, or #FFFFFAF0.
		///</summary>
		public static readonly RDColor FloralWhite = new(4294966000u);
		///<summary>
		///Gets the predefined color of forest green, or #FF228B22.
		///</summary>
		public static readonly RDColor ForestGreen = new(4280453922u);
		///<summary>
		///Gets the predefined color of fuchsia, or #FFFF00FF.
		///</summary>
		public static readonly RDColor Fuchsia = new(4294902015u);
		///<summary>
		///Gets the predefined color of gainsboro, or #FFDCDCDC.
		///</summary>
		public static readonly RDColor Gainsboro = new(4292664540u);
		///<summary>
		///Gets the predefined color of ghost white, or #FFF8F8FF.
		///</summary>
		public static readonly RDColor GhostWhite = new(4294506751u);
		///<summary>
		///Gets the predefined color of gold, or #FFFFD700.
		///</summary>
		public static readonly RDColor Gold = new(4294956800u);
		///<summary>
		///Gets the predefined color of goldenrod, or #FFDAA520.
		///</summary>
		public static readonly RDColor Goldenrod = new(4292519200u);
		///<summary>
		///Gets the predefined color of gray, or #FF808080.
		///</summary>
		public static readonly RDColor Gray = new(4286611584u);
		///<summary>
		///Gets the predefined color of green, or #FF008000.
		///</summary>
		public static readonly RDColor Green = new(4278222848u);
		///<summary>
		///Gets the predefined color of green yellow, or #FFADFF2F.
		///</summary>
		public static readonly RDColor GreenYellow = new(4289593135u);
		///<summary>
		///Gets the predefined color of honeydew, or #FFF0FFF0.
		///</summary>
		public static readonly RDColor Honeydew = new(4293984240u);
		///<summary>
		///Gets the predefined color of hot pink, or #FFFF69B4.
		///</summary>
		public static readonly RDColor HotPink = new(4294928820u);
		///<summary>
		///Gets the predefined color of indian red, or #FFCD5C5C.
		///</summary>
		public static readonly RDColor IndianRed = new(4291648604u);
		///<summary>
		///Gets the predefined color of indigo, or #FF4B0082.
		///</summary>
		public static readonly RDColor Indigo = new(4283105410u);
		///<summary>
		///Gets the predefined color of ivory, or #FFFFFFF0.
		///</summary>
		public static readonly RDColor Ivory = new(4294967280u);
		///<summary>
		///Gets the predefined color of khaki, or #FFF0E68C.
		///</summary>
		public static readonly RDColor Khaki = new(4293977740u);
		///<summary>
		///Gets the predefined color of lavender, or #FFE6E6FA.
		///</summary>
		public static readonly RDColor Lavender = new(4293322490u);
		///<summary>
		///Gets the predefined color of lavender blush, or #FFFFF0F5.
		///</summary>
		public static readonly RDColor LavenderBlush = new(4294963445u);
		///<summary>
		///Gets the predefined color of lawn green, or #FF7CFC00.
		///</summary>
		public static readonly RDColor LawnGreen = new(4286381056u);
		///<summary>
		///Gets the predefined color of lemon chiffon, or #FFFFFACD.
		///</summary>
		public static readonly RDColor LemonChiffon = new(4294965965u);
		///<summary>
		///Gets the predefined color of light blue, or #FFADD8E6.
		///</summary>
		public static readonly RDColor LightBlue = new(4289583334u);
		///<summary>
		///Gets the predefined color of light coral, or #FFF08080.
		///</summary>
		public static readonly RDColor LightCoral = new(4293951616u);
		///<summary>
		///Gets the predefined color of light cyan, or #FFE0FFFF.
		///</summary>
		public static readonly RDColor LightCyan = new(4292935679u);
		///<summary>
		///Gets the predefined color of light goldenrod yellow, or #FFFAFAD2.
		///</summary>
		public static readonly RDColor LightGoldenrodYellow = new(4294638290u);
		///<summary>
		///Gets the predefined color of light gray, or #FFD3D3D3.
		///</summary>
		public static readonly RDColor LightGray = new(4292072403u);
		///<summary>
		///Gets the predefined color of light green, or #FF90EE90.
		///</summary>
		public static readonly RDColor LightGreen = new(4287688336u);
		///<summary>
		///Gets the predefined color of light pink, or #FFFFB6C1.
		///</summary>
		public static readonly RDColor LightPink = new(4294948545u);
		///<summary>
		///Gets the predefined color of light salmon, or #FFFFA07A.
		///</summary>
		public static readonly RDColor LightSalmon = new(4294942842u);
		///<summary>
		///Gets the predefined color of light sea green, or #FF20B2AA.
		///</summary>
		public static readonly RDColor LightSeaGreen = new(4280332970u);
		///<summary>
		///Gets the predefined color of light sky blue, or #FF87CEFA.
		///</summary>
		public static readonly RDColor LightSkyBlue = new(4287090426u);
		///<summary>
		///Gets the predefined color of light slate gray, or #FF778899.
		///</summary>
		public static readonly RDColor LightSlateGray = new(4286023833u);
		///<summary>
		///Gets the predefined color of light steel blue, or #FFB0C4DE.
		///</summary>
		public static readonly RDColor LightSteelBlue = new(4289774814u);
		///<summary>
		///Gets the predefined color of light yellow, or #FFFFFFE0.
		///</summary>
		public static readonly RDColor LightYellow = new(4294967264u);
		///<summary>
		///Gets the predefined color of lime, or #FF00FF00.
		///</summary>
		public static readonly RDColor Lime = new(4278255360u);
		///<summary>
		///Gets the predefined color of lime green, or #FF32CD32.
		///</summary>
		public static readonly RDColor LimeGreen = new(4281519410u);
		///<summary>
		///Gets the predefined color of linen, or #FFFAF0E6.
		///</summary>
		public static readonly RDColor Linen = new(4294635750u);
		///<summary>
		///Gets the predefined color of magenta, or #FFFF00FF.
		///</summary>
		public static readonly RDColor Magenta = new(4294902015u);
		///<summary>
		///Gets the predefined color of maroon, or #FF800000.
		///</summary>
		public static readonly RDColor Maroon = new(4286578688u);
		///<summary>
		///Gets the predefined color of medium aquamarine, or #FF66CDAA.
		///</summary>
		public static readonly RDColor MediumAquamarine = new(4284927402u);
		///<summary>
		///Gets the predefined color of medium blue, or #FF0000CD.
		///</summary>
		public static readonly RDColor MediumBlue = new(4278190285u);
		///<summary>
		///Gets the predefined color of medium orchid, or #FFBA55D3.
		///</summary>
		public static readonly RDColor MediumOrchid = new(4290401747u);
		///<summary>
		///Gets the predefined color of medium purple, or #FF9370DB.
		///</summary>
		public static readonly RDColor MediumPurple = new(4287852763u);
		///<summary>
		///Gets the predefined color of medium sea green, or #FF3CB371.
		///</summary>
		public static readonly RDColor MediumSeaGreen = new(4282168177u);
		///<summary>
		///Gets the predefined color of medium slate blue, or #FF7B68EE.
		///</summary>
		public static readonly RDColor MediumSlateBlue = new(4286277870u);
		///<summary>
		///Gets the predefined color of medium spring green, or #FF00FA9A.
		///</summary>
		public static readonly RDColor MediumSpringGreen = new(4278254234u);
		///<summary>
		///Gets the predefined color of medium turquoise, or #FF48D1CC.
		///</summary>
		public static readonly RDColor MediumTurquoise = new(4282962380u);
		///<summary>
		///Gets the predefined color of medium violet red, or #FFC71585.
		///</summary>
		public static readonly RDColor MediumVioletRed = new(4291237253u);
		///<summary>
		///Gets the predefined color of midnight blue, or #FF191970.
		///</summary>
		public static readonly RDColor MidnightBlue = new(4279834992u);
		///<summary>
		///Gets the predefined color of mint cream, or #FFF5FFFA.
		///</summary>
		public static readonly RDColor MintCream = new(4294311930u);
		///<summary>
		///Gets the predefined color of misty rose, or #FFFFE4E1.
		///</summary>
		public static readonly RDColor MistyRose = new(4294960353u);
		///<summary>
		///Gets the predefined color of moccasin, or #FFFFE4B5.
		///</summary>
		public static readonly RDColor Moccasin = new(4294960309u);
		///<summary>
		///Gets the predefined color of navajo white, or #FFFFDEAD.
		///</summary>
		public static readonly RDColor NavajoWhite = new(4294958765u);
		///<summary>
		///Gets the predefined color of navy, or #FF000080.
		///</summary>
		public static readonly RDColor Navy = new(4278190208u);
		///<summary>
		///Gets the predefined color of old lace, or #FFFDF5E6.
		///</summary>
		public static readonly RDColor OldLace = new(4294833638u);
		///<summary>
		///Gets the predefined color of olive, or #FF808000.
		///</summary>
		public static readonly RDColor Olive = new(4286611456u);
		///<summary>
		///Gets the predefined color of olive drab, or #FF6B8E23.
		///</summary>
		public static readonly RDColor OliveDrab = new(4285238819u);
		///<summary>
		///Gets the predefined color of orange, or #FFFFA500.
		///</summary>
		public static readonly RDColor Orange = new(4294944000u);
		///<summary>
		///Gets the predefined color of orange red, or #FFFF4500.
		///</summary>
		public static readonly RDColor OrangeRed = new(4294919424u);
		///<summary>
		///Gets the predefined color of orchid, or #FFDA70D6.
		///</summary>
		public static readonly RDColor Orchid = new(4292505814u);
		///<summary>
		///Gets the predefined color of pale goldenrod, or #FFEEE8AA.
		///</summary>
		public static readonly RDColor PaleGoldenrod = new(4293847210u);
		///<summary>
		///Gets the predefined color of pale green, or #FF98FB98.
		///</summary>
		public static readonly RDColor PaleGreen = new(4288215960u);
		///<summary>
		///Gets the predefined color of pale turquoise, or #FFAFEEEE.
		///</summary>
		public static readonly RDColor PaleTurquoise = new(4289720046u);
		///<summary>
		///Gets the predefined color of pale violet red, or #FFDB7093.
		///</summary>
		public static readonly RDColor PaleVioletRed = new(4292571283u);
		///<summary>
		///Gets the predefined color of papaya whip, or #FFFFEFD5.
		///</summary>
		public static readonly RDColor PapayaWhip = new(4294963157u);
		///<summary>
		///Gets the predefined color of peach puff, or #FFFFDAB9.
		///</summary>
		public static readonly RDColor PeachPuff = new(4294957753u);
		///<summary>
		///Gets the predefined color of peru, or #FFCD853F.
		///</summary>
		public static readonly RDColor Peru = new(4291659071u);
		///<summary>
		///Gets the predefined color of pink, or #FFFFC0CB.
		///</summary>
		public static readonly RDColor Pink = new(4294951115u);
		///<summary>
		///Gets the predefined color of plum, or #FFDDA0DD.
		///</summary>
		public static readonly RDColor Plum = new(4292714717u);
		///<summary>
		///Gets the predefined color of powder blue, or #FFB0E0E6.
		///</summary>
		public static readonly RDColor PowderBlue = new(4289781990u);
		///<summary>
		///Gets the predefined color of purple, or #FF800080.
		///</summary>
		public static readonly RDColor Purple = new(4286578816u);
		///<summary>
		///Gets the predefined color of red, or #FFFF0000.
		///</summary>
		public static readonly RDColor Red = new(4294901760u);
		///<summary>
		///Gets the predefined color of rosy brown, or #FFBC8F8F.
		///</summary>
		public static readonly RDColor RosyBrown = new(4290547599u);
		///<summary>
		///Gets the predefined color of royal blue, or #FF4169E1.
		///</summary>
		public static readonly RDColor RoyalBlue = new(4282477025u);
		///<summary>
		///Gets the predefined color of saddle brown, or #FF8B4513.
		///</summary>
		public static readonly RDColor SaddleBrown = new(4287317267u);
		///<summary>
		///Gets the predefined color of salmon, or #FFFA8072.
		///</summary>
		public static readonly RDColor Salmon = new(4294606962u);
		///<summary>
		///Gets the predefined color of sandy brown, or #FFF4A460.
		///</summary>
		public static readonly RDColor SandyBrown = new(4294222944u);
		///<summary>
		///Gets the predefined color of sea green, or #FF2E8B57.
		///</summary>
		public static readonly RDColor SeaGreen = new(4281240407u);
		///<summary>
		///Gets the predefined color of sea shell, or #FFFFF5EE.
		///</summary>
		public static readonly RDColor SeaShell = new(4294964718u);
		///<summary>
		///Gets the predefined color of sienna, or #FFA0522D.
		///</summary>
		public static readonly RDColor Sienna = new(4288696877u);
		///<summary>
		///Gets the predefined color of silver, or #FFC0C0C0.
		///</summary>
		public static readonly RDColor Silver = new(4290822336u);
		///<summary>
		///Gets the predefined color of sky blue, or #FF87CEEB.
		///</summary>
		public static readonly RDColor SkyBlue = new(4287090411u);
		///<summary>
		///Gets the predefined color of slate blue, or #FF6A5ACD.
		///</summary>
		public static readonly RDColor SlateBlue = new(4285160141u);
		///<summary>
		///Gets the predefined color of slate gray, or #FF708090.
		///</summary>
		public static readonly RDColor SlateGray = new(4285563024u);
		///<summary>
		///Gets the predefined color of snow, or #FFFFFAFA.
		///</summary>
		public static readonly RDColor Snow = new(4294966010u);
		///<summary>
		///Gets the predefined color of spring green, or #FF00FF7F.
		///</summary>
		public static readonly RDColor SpringGreen = new(4278255487u);
		///<summary>
		///Gets the predefined color of steel blue, or #FF4682B4.
		///</summary>
		public static readonly RDColor SteelBlue = new(4282811060u);
		///<summary>
		///Gets the predefined color of tan, or #FFD2B48C.
		///</summary>
		public static readonly RDColor Tan = new(4291998860u);
		///<summary>
		///Gets the predefined color of teal, or #FF008080.
		///</summary>
		public static readonly RDColor Teal = new(4278222976u);
		///<summary>
		///Gets the predefined color of thistle, or #FFD8BFD8.
		///</summary>
		public static readonly RDColor Thistle = new(4292394968u);
		///<summary>
		///Gets the predefined color of tomato, or #FFFF6347.
		///</summary>
		public static readonly RDColor Tomato = new(4294927175u);
		///<summary>
		///Gets the predefined color of turquoise, or #FF40E0D0.
		///</summary>
		public static readonly RDColor Turquoise = new(4282441936u);
		///<summary>
		///Gets the predefined color of violet, or #FFEE82EE.
		///</summary>
		public static readonly RDColor Violet = new(4293821166u);
		///<summary>
		///Gets the predefined color of wheat, or #FFF5DEB3.
		///</summary>
		public static readonly RDColor Wheat = new(4294303411u);
		///<summary>
		///Gets the predefined color of white, or #FFFFFFFF.
		///</summary>
		public static readonly RDColor White = new(uint.MaxValue);
		///<summary>
		///Gets the predefined color of white smoke, or #FFF5F5F5.
		///</summary>
		public static readonly RDColor WhiteSmoke = new(4294309365u);
		///<summary>
		///Gets the predefined color of yellow, or #FFFFFF00.
		///</summary>
		public static readonly RDColor Yellow = new(4294967040u);
		///<summary>
		///Gets the predefined color of yellow green, or #FF9ACD32.
		///</summary>
		public static readonly RDColor YellowGreen = new(4288335154u);
		///<summary>
		///Gets the predefined color of white transparent, or #00FFFFFF.
		///</summary>
		public static readonly RDColor Transparent = new(16777215u);
		///<summary>
		///Gets the predefined empty color (black transparent), or #00000000.
		///</summary>
		public static readonly RDColor Empty = new(0u);
	}
}