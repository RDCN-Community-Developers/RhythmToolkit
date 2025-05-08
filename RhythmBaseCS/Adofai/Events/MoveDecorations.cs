using System;
using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Components.Easing;
using RhythmBase.Events;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to move decorations in the Adofai editor.  
	/// </summary>  
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class MoveDecorations : BaseTaggedTileAction, IEaseEvent, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.MoveDecorations;		/// <summary>  
		/// Gets or sets the duration of the event.  
		/// </summary>  
		public float Duration { get; set; }		/// <summary>  
		/// Gets or sets the tag associated with the decoration.  
		/// </summary>  
		public string Tag { get; set; } = string.Empty;		/// <summary>  
		/// Gets or sets the easing type for the event.  
		/// </summary>  
		public EaseType Ease { get; set; }		/// <summary>  
		/// Gets or sets the position offset for the decoration.  
		/// </summary>  
		public RDPoint? PositionOffset { get; set; }		/// <summary>  
		/// Gets or sets the parallax offset for the decoration.  
		/// </summary>  
		public RDPoint? ParallaxOffset { get; set; }		/// <summary>  
		/// Gets or sets a value indicating whether the decoration is visible.  
		/// </summary>  
		public bool? Visible { get; set; }		/// <summary>  
		/// Gets or sets the reference point for the decoration's position.  
		/// </summary>  
		public DecorationRelativeTo? RelativeTo { get; set; }		/// <summary>  
		/// Gets or sets the image used for the decoration.  
		/// </summary>  
		public string DecorationImage { get; set; } = string.Empty;		/// <summary>  
		/// Gets or sets the pivot offset for the decoration.  
		/// </summary>  
		public RDSize? PivotOffset { get; set; }		/// <summary>  
		/// Gets or sets the rotation offset for the decoration.  
		/// </summary>  
		public float? RotationOffset { get; set; }		/// <summary>  
		/// Gets or sets the scale of the decoration.  
		/// </summary>  
		public RDSize? Scale { get; set; }		/// <summary>  
		/// Gets or sets the color of the decoration.  
		/// </summary>  
		public RDColor? Color { get; set; }		/// <summary>  
		/// Gets or sets the opacity of the decoration.  
		/// </summary>  
		public float? Opacity { get; set; }		/// <summary>  
		/// Gets or sets the depth of the decoration.  
		/// </summary>  
		public int? Depth { get; set; }		/// <summary>  
		/// Gets or sets the parallax value for the decoration.  
		/// </summary>  
		public RDPoint? Parallax { get; set; }		/// <summary>  
		/// Gets or sets the masking type for the decoration.  
		/// </summary>  
		public MaskingTypes? MaskingType { get; set; }		/// <summary>  
		/// Gets or sets a value indicating whether to use masking depth.  
		/// </summary>  
		public bool? UseMaskingDepth { get; set; }		/// <summary>  
		/// Gets or sets the front depth for masking.  
		/// </summary>  
		public int? MaskingFrontDepth { get; set; }		/// <summary>  
		/// Gets or sets the back depth for masking.  
		/// </summary>  
		public int? MaskingBackDepth { get; set; }		/// <summary>  
		/// Specifies the masking types available for the decoration.  
		/// </summary>  
		public enum MaskingTypes
		{
			/// <summary>  
			/// No masking is applied.  
			/// </summary>  
			None,			/// <summary>  
			/// Applies a mask to the decoration.  
			/// </summary>  
			Mask,			/// <summary>  
			/// Makes the decoration visible only inside the mask.  
			/// </summary>  
			VisibleInsideMask,			/// <summary>  
			/// Makes the decoration visible only outside the mask.  
			/// </summary>  
			VisibleOutsideMask
		}
	}
}
