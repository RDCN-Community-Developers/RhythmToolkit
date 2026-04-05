using System.Diagnostics;

namespace RhythmBase.RhythmDoctor.Components;

/// <summary>
/// Palette color
/// </summary>
/// <remarks>
/// 
/// </remarks>
[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public struct PaletteColor
{
	/// <summary>
	/// Get or set a custom color.
	/// </summary>
	public RDColor Color
	{
		readonly get
		{
			RDColor Color = EnablePanel ? default : _color;
			return Color;
		}
		set
		{
			_panel = -1;
			_color = value.WithAlpha(byte.MaxValue);
		}
	}
	/// <summary>
	/// Go back to or set the palette color index.
	/// </summary>
	public int PaletteIndex
	{
		readonly get => _panel;
		set
		{
			if (value >= 0)
			{
				_color = default;
				_panel = value;
			}
		}
	}
	/// <summary>
	/// Specifies whether this object is used for this color.
	/// </summary>
	public readonly bool EnablePanel
	{
		get
		{
			return PaletteIndex >= 0;
		}
	}
	/// <summary>
	/// The actual color of this object.<br />
	/// If comes from a palette, it's a palette color.
	/// If not, it's a custom color.
	/// </summary>
	public readonly RDColor Value => EnablePanel ? default : _color;
	/// <summary>
	/// Initializes a new instance of <see cref="PaletteColor"/> using the provided custom color.
	/// </summary>
	/// <param name="color">The custom <see cref="RDColor"/> value to be assigned.</param>
	public PaletteColor(RDColor color)
	{
		Color = color;
	}
	/// <summary>
	/// Initializes a new instance of <see cref="PaletteColor"/> with a default color value.
	/// </summary>
	/// <param name="panelIndex">The index of the palette color to be used.</param>
	public PaletteColor(int panelIndex)
	{
		_panel = panelIndex;
	}
	/// <inheritdoc/>
	public override readonly string ToString() => EnablePanel ? $"[{_panel}][?]" : "[-]" + Value.ToString("#RRGGBB");
	/// <summary>
	/// Deserializes the specified string value into the current <see cref="PaletteColor"/> instance.
	/// </summary>
	/// <param name="value">
	/// The string representation of the palette color. If the value starts with "pal", it is interpreted as a palette index (e.g., "pal3").
	/// Otherwise, it is interpreted as a custom color in RGBA format.
	/// </param>
	public void Deserialize(string value)
	{
		if (value.StartsWith("pal"))
		{
			_panel = int.Parse(value[3..]);
			_color = default;
		}
		else
		{
			_color = RDColor.FromRgba(value);
			_panel = -1;
		}
	}
	/// <summary>
	/// Serializes the current <see cref="PaletteColor"/> instance to a string representation.
	/// </summary>
	/// <returns>
	/// A string representing the palette color. If the color is from a palette, returns "pal" followed by the palette index.
	/// Otherwise, returns the color in "RRGGBBAA" or "RRGGBB" format depending on whether alpha is enabled.
	/// </returns>
	public readonly string Serialize() => EnablePanel ? $"pal{_panel}" :  Value.ToString("RRGGBB");
	/// <summary>  
	/// Implicitly converts a <see cref="PaletteColor"/> instance to an <see cref="RDColor"/>.  
	/// </summary>  
	/// <param name="paletteColor">The <see cref="PaletteColor"/> instance to convert.</param>  
	/// <returns>The <see cref="RDColor"/> value of the <see cref="PaletteColor"/>.</returns>  
	public static implicit operator RDColor(PaletteColor paletteColor) => paletteColor.Value;
	/// <summary>  
	/// Implicitly converts an <see cref="RDColor"/> to a <see cref="PaletteColor"/> instance.  
	/// </summary>  
	/// <param name="color">The <see cref="RDColor"/> to convert.</param>  
	/// <returns>A new <see cref="PaletteColor"/> instance with the specified <see cref="RDColor"/>.</returns>  
	public static implicit operator PaletteColor(RDColor color) => new(color);
	private int _panel;
	private RDColor _color;
	private readonly string GetDebuggerDisplay()
	{
		return ToString();
	}
}
