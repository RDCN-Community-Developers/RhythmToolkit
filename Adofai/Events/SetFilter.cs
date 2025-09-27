using RhythmBase.Global.Components.Easing;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event that sets a filter effect in the Adofai level.  
	/// </summary>  
	public class SetFilter : BaseTaggedTileAction, IEaseEvent, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.SetFilter;
		/// <summary>  
		/// Gets or sets the filter to be applied.  
		/// </summary>  
		public Filters Filter { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether the filter is enabled.  
		/// </summary>  
		public bool Enabled { get; set; }
		/// <summary>  
		/// Gets or sets the intensity of the filter effect.  
		/// </summary>  
		public int Intensity { get; set; }
		/// <summary>  
		/// Gets or sets the duration of the filter effect.  
		/// </summary>  
		public float Duration { get; set; }
		/// <summary>  
		/// Gets or sets the easing type for the filter effect.  
		/// </summary>  
		public EaseType Ease { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether other filters should be disabled.  
		/// </summary>  
		public bool DisableOthers { get; set; }
		/// <summary>  
		/// Represents the available filter types.  
		/// </summary>  
		[RDJsonEnumSerializable]
		public enum Filters
		{
			/// <summary>  
			/// Grayscale filter.  
			/// </summary>  
			Grayscale,
			/// <summary>  
			/// Sepia filter.  
			/// </summary>  
			Sepia,
			/// <summary>  
			/// Invert colors filter.  
			/// </summary>  
			Invert,
			/// <summary>  
			/// VHS effect filter.  
			/// </summary>  
			VHS,
			/// <summary>  
			/// 1980s TV effect filter.  
			/// </summary>  
			EightiesTV,
			/// <summary>  
			/// 1950s TV effect filter.  
			/// </summary>  
			FiftiesTV,
			/// <summary>  
			/// Arcade effect filter.  
			/// </summary>  
			Arcade,
			/// <summary>  
			/// LED effect filter.  
			/// </summary>  
			LED,
			/// <summary>  
			/// Rain effect filter.  
			/// </summary>  
			Rain,
			/// <summary>  
			/// Blizzard effect filter.  
			/// </summary>  
			Blizzard,
			/// <summary>  
			/// Pixel snow effect filter.  
			/// </summary>  
			PixelSnow,
			/// <summary>  
			/// Compression effect filter.  
			/// </summary>  
			Compression,
			/// <summary>  
			/// Glitch effect filter.  
			/// </summary>  
			Glitch,
			/// <summary>  
			/// Pixelate effect filter.  
			/// </summary>  
			Pixelate,
			/// <summary>  
			/// Waves effect filter.  
			/// </summary>  
			Waves,
			/// <summary>  
			/// Static effect filter.  
			/// </summary>  
			Static,
			/// <summary>  
			/// Grain effect filter.  
			/// </summary>  
			Grain,
			/// <summary>  
			/// Motion blur effect filter.  
			/// </summary>  
			MotionBlur,
			/// <summary>  
			/// Fisheye effect filter.  
			/// </summary>  
			Fisheye,
			/// <summary>  
			/// Chromatic aberration effect filter.  
			/// </summary>  
			Aberration,
			/// <summary>  
			/// Drawing effect filter.  
			/// </summary>  
			Drawing,
			/// <summary>  
			/// Neon effect filter.  
			/// </summary>  
			Neon,
			/// <summary>  
			/// Handheld camera effect filter.  
			/// </summary>  
			Handheld,
			/// <summary>  
			/// Night vision effect filter.  
			/// </summary>  
			NightVision,
			/// <summary>  
			/// Funk effect filter.  
			/// </summary>  
			Funk,
			/// <summary>  
			/// Tunnel effect filter.  
			/// </summary>  
			Tunnel,
			/// <summary>  
			/// Weird 3D effect filter.  
			/// </summary>  
			Weird3D,
			/// <summary>  
			/// Blur effect filter.  
			/// </summary>  
			Blur,
			/// <summary>  
			/// Blur focus effect filter.  
			/// </summary>  
			BlurFocus,
			/// <summary>  
			/// Gaussian blur effect filter.  
			/// </summary>  
			GaussianBlur,
			/// <summary>  
			/// Hexagon black effect filter.  
			/// </summary>  
			HexagonBlack,
			/// <summary>  
			/// Posterize effect filter.  
			/// </summary>  
			Posterize,
			/// <summary>  
			/// Sharpen effect filter.  
			/// </summary>  
			Sharpen,
			/// <summary>  
			/// Contrast effect filter.  
			/// </summary>  
			Contrast,
			/// <summary>  
			/// Edge black line effect filter.  
			/// </summary>  
			EdgeBlackLine,
			/// <summary>  
			/// Oil paint effect filter.  
			/// </summary>  
			OilPaint,
			/// <summary>  
			/// Super dot effect filter.  
			/// </summary>  
			SuperDot,
			/// <summary>  
			/// Water drop effect filter.  
			/// </summary>  
			WaterDrop,
			/// <summary>  
			/// Light water effect filter.  
			/// </summary>  
			LightWater,
			/// <summary>  
			/// Petals effect filter.  
			/// </summary>  
			Petals,
			/// <summary>  
			/// Instant petals effect filter.  
			/// </summary>  
			PetalsInstant
		}
	}
}
