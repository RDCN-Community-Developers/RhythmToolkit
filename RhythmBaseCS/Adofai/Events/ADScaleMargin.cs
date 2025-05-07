using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents the ScaleMargin event, which adjusts the margin scaling in the level.
	/// </summary>
	public class ADScaleMargin : ADBaseTileEvent
	{
		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.ScaleMargin;		/// <summary>
		/// Gets or sets the scale value for the margin adjustment.
		/// </summary>
		public int Scale { get; set; }
	}
}
