namespace RhythmBase.Components
{
	/// <summary>
	/// Palette color
	/// </summary>
	/// <remarks>
	/// 
	/// </remarks>
	/// <param name="enableAlpha">Specifies whether this object supports alpha channel.</param>
	public class PaletteColor(bool enableAlpha)
	{
		/// <summary>
		/// Get or set a custom color.
		/// </summary>
		public RDColor? Color
		{
			get
			{
				RDColor? Color = new RDColor?(EnablePanel ? parent[_panel] : default);
				return Color;
			}
			set
			{
				_panel = -1;
				_color = EnableAlpha
					? value
					: value == null
					? null
					: new RDColor?(value.GetValueOrDefault().WithAlpha(byte.MaxValue));
			}
		}
		/// <summary>
		/// Go back to or set the palette color index.
		/// </summary>
		public int PaletteIndex
		{
			get => _panel;
			set
			{
				if (value >= 0)
				{
					_color = null;
					_panel = value;
				}
			}
		}
		/// <summary>
		/// Specifies whether this object supports alpha channel.
		/// </summary>
		/// <returns></returns>
		public bool EnableAlpha { get; } = enableAlpha;
		/// <summary>
		/// Specifies whether this object is used for this color.
		/// </summary>
		public bool EnablePanel
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
		public RDColor Value => (EnablePanel ? (parent[_panel]) : _color ?? throw new Exceptions.RhythmBaseException());
		/// <inheritdoc/>
		public override string ToString() => EnablePanel ? $"[{_panel}]{Value:#AARRGGBB}" : "[-]" + Value.ToString();
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
		public static implicit operator PaletteColor(RDColor color) => new PaletteColor(color.A != byte.MaxValue)
		{
			Color = color
		};
		private int _panel;
		private RDColor? _color;
		internal RDColor[] parent = [];
	}
}
