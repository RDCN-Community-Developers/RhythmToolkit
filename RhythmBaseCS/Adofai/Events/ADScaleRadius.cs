using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents the ScaleRadius event in the ADOFAI editor.
	/// </summary>
	public class ADScaleRadius : ADBaseTileEvent
	{
		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.ScaleRadius;		/// <summary>
		/// Gets or sets the scale value for the radius adjustment.
		/// </summary>
		public int Scale { get; set; }
	}
}
