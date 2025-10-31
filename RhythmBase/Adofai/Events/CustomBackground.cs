namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents a custom background event in the Adofai event system.  
	/// </summary>  
	public class CustomBackground : BaseTaggedTileEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.CustomBackground;
		/// <summary>  
		/// Gets or sets the color of the background.  
		/// </summary>  
		public RDColor Color { get; set; } = RDColor.Black;
		/// <summary>  
		/// Gets or sets the background image file path.  
		/// </summary>  
		[RDJsonProperty("bgImage")]
		public string BackgroundImage { get; set; } = string.Empty;
		/// <summary>  
		/// Gets or sets the color applied to the background image.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(BackgroundImage)}.Length > 0")]
		public RDColor ImageColor { get; set; } = RDColor.White;
		/// <summary>  
		/// Gets or sets the parallax effect values for the background.  
		/// </summary>  
		[RDJsonCondition($"!string.IsNullOrEmpty($&.{nameof(BackgroundImage)}) || !$&.{nameof(LockRot)}")]
		public RDPoint Parallax { get; set; }
		/// <summary>  
		/// Gets or sets the display mode of the background image.  
		/// </summary>  
		[RDJsonProperty("bgDisplayMode")]
		[RDJsonCondition($"!string.IsNullOrEmpty($&.{nameof(BackgroundImage)})")]
		public BackgroundDisplayMode BackgroundDisplayMode { get; set; } = BackgroundDisplayMode.FitToScreen;
		/// <summary>  
		/// Gets or sets a value indicating whether image smoothing is enabled.  
		/// </summary>  
		public bool ImageSmoothing { get; set; } = true;
		/// <summary>  
		/// Gets or sets a value indicating whether the background rotation is locked.  
		/// </summary>  
		[RDJsonCondition($"!string.IsNullOrEmpty($&.{nameof(BackgroundImage)})")]
		public bool LockRot { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether the background image should loop.  
		/// </summary>  
		[RDJsonProperty("loopBG")]
		[RDJsonCondition($"!string.IsNullOrEmpty($&.{nameof(BackgroundImage)})")]
		public bool LoopBackground { get; set; }
		/// <summary>  
		/// Gets or sets the scaling ratio of the background image.  
		/// </summary>  
		[RDJsonCondition($"!string.IsNullOrEmpty($&.{nameof(BackgroundImage)})")]
		public float ScalingRatio { get; set; }
	}
	/// <summary>  
	/// Represents the display modes for the background image.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum BackgroundDisplayMode
	{
		/// <summary>  
		/// Fits the background image to the screen.  
		/// </summary>  
		FitToScreen,
		/// <summary>  
		/// Displays the background image without scaling.  
		/// </summary>  
		Unscaled,
		/// <summary>  
		/// Tiles the background image.  
		/// </summary>  
		Tiled
	}
}
