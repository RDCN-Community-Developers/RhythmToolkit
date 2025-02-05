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
		public RDColor Value
		{
			get
			{
				return (EnablePanel ? (parent[_panel]) : _color ?? throw new RhythmBase.Exceptions.RhythmBaseException());
			}
		}

		/// <inheritdoc/>
		public override string ToString() => EnablePanel ? string.Format("{0}: {1}", _panel, Value) : Value.ToString();

		private int _panel;

		private RDColor? _color;

		internal RDColor[] parent = [];
	}
}
