using System;
using SkiaSharp;

namespace RhythmBase.Components
{
	/// <summary>
	/// Palette color
	/// </summary>

	public class PaletteColor
	{
		/// <summary>
		/// Get or set a custom color.
		/// </summary>

		public SKColor? Color
		{
			get
			{
				SKColor? Color = new SKColor?(EnablePanel ? parent[_panel] : default);
				return Color;
			}
			set
			{
				_panel = -1;
				bool enableAlpha = EnableAlpha;
				if (enableAlpha)
				{
					_color = value;
				}
				else
				{
					_color = (value != null) ? new SKColor?(value.GetValueOrDefault().WithAlpha(byte.MaxValue)) : null;
				}
			}
		}

		/// <summary>
		/// Go back to or set the palette color index.
		/// </summary>

		public int PaletteIndex
		{
			get
			{
				return _panel;
			}
			set
			{
				bool flag = value >= 0;
				if (flag)
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

		public bool EnableAlpha { get; }

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

		public SKColor Value
		{
			get
			{
				return (EnablePanel ? new SKColor?(parent[_panel]) : _color).Value;
			}
		}

		/// <param name="enableAlpha">Specifies whether this object supports alpha channel.</param>

		public PaletteColor(bool enableAlpha)
		{
			this.EnableAlpha = enableAlpha;
		}


		public override string ToString() => EnablePanel ? string.Format("{0}: {1}", _panel, Value) : Value.ToString();


		private int _panel;


		private SKColor? _color;


		internal LimitedList<SKColor> parent;
	}
}
