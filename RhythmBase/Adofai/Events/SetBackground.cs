﻿using RhythmBase.Adofai.Components;

namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to set the background properties in the Adofai event system.  
	/// </summary>  
	public class SetBackground : BaseTaggedTileEvent, IBeginningEvent
	{
		/// <summary>  
		/// Gets the type of the event, which is <see cref="EventType.SetBackground"/>.  
		/// </summary>  
		public override EventType Type { get; } = EventType.SetBackground;

		/// <summary>  
		/// Gets or sets the background color.  
		/// </summary>  
		public RDColor Color { get; set; } = RDColor.Black;

		/// <summary>  
		/// Gets or sets the background image file path.  
		/// </summary>  
		public string BgImage { get; set; } = string.Empty;

		/// <summary>  
		/// Gets or sets the color applied to the background image.  
		/// </summary>  
		public RDColor ImageColor { get; set; } = RDColor.White;

		/// <summary>  
		/// Gets or sets the parallax effect values for the background.  
		/// </summary>  
		public RDPointN Parallax { get; set; } = new RDPointN(100, 100);

		/// <summary>  
		/// Gets or sets the display mode of the background image.  
		/// </summary>  
		public BgDisplayMode BgDisplayMode { get; set; }

		/// <summary>  
		/// Gets or sets a value indicating whether image smoothing is enabled.  
		/// </summary>  
		public bool ImageSmoothing { get; set; }

		/// <summary>  
		/// Gets or sets a value indicating whether the background rotation is locked.  
		/// </summary>  
		public bool LockRot { get; set; }

		/// <summary>  
		/// Gets or sets a value indicating whether the background image should loop.  
		/// </summary>  
		public bool LoopBG { get; set; }

		/// <summary>  
		/// Gets or sets the scaling ratio of the background image.  
		/// </summary>  
		public float ScalingRatio { get; set; } = 100f;
	}
}
