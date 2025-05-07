using RhythmBase.Components;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents a custom background event in the Adofai event system.  
	/// </summary>  
	public class ADCustomBackground : ADBaseTaggedTileAction
	{
		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.CustomBackground;		/// <summary>  
		/// Gets or sets the color of the background.  
		/// </summary>  
		public RDColor Color { get; set; }		/// <summary>  
		/// Gets or sets the background image file path.  
		/// </summary>  
		public string BgImage { get; set; } = string.Empty;		/// <summary>  
		/// Gets or sets the color applied to the background image.  
		/// </summary>  
		public RDColor ImageColor { get; set; }		/// <summary>  
		/// Gets or sets the parallax effect values for the background.  
		/// </summary>  
		public RDPoint Parallax { get; set; }		/// <summary>  
		/// Gets or sets the display mode of the background image.  
		/// </summary>  
		public BgDisplayModes BgDisplayMode { get; set; }		/// <summary>  
		/// Gets or sets a value indicating whether image smoothing is enabled.  
		/// </summary>  
		public bool ImageSmoothing { get; set; }		/// <summary>  
		/// Gets or sets a value indicating whether the background rotation is locked.  
		/// </summary>  
		public bool LockRot { get; set; }		/// <summary>  
		/// Gets or sets a value indicating whether the background image should loop.  
		/// </summary>  
		public bool LoopBG { get; set; }		/// <summary>  
		/// Gets or sets the scaling ratio of the background image.  
		/// </summary>  
		public float ScalingRatio { get; set; }		/// <summary>  
		/// Represents the display modes for the background image.  
		/// </summary>  
		public enum BgDisplayModes
		{
			/// <summary>  
			/// Fits the background image to the screen.  
			/// </summary>  
			FitToScreen,			/// <summary>  
			/// Displays the background image without scaling.  
			/// </summary>  
			Unscaled,			/// <summary>  
			/// Tiles the background image.  
			/// </summary>  
			Tiled
		}
	}
}
