namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents a blend decoration action in the rhythm base.  
	/// </summary>  
	public class Blend : BaseDecorationAction
	{
		/// <summary>  
		/// Gets the type of the event, which is <see cref="EventType.Blend"/>.  
		/// </summary>  
		public override EventType Type => EventType.Blend;

		/// <summary>  
		/// Gets the tab where this action is categorized, which is <see cref="Tabs.Decorations"/>.  
		/// </summary>  
		public override Tabs Tab => Tabs.Decorations;

		/// <summary>  
		/// Gets or sets the type of blend effect to apply.  
		/// </summary>  
		public BlendTypes BlendType { get; set; } = BlendTypes.None;

	}
	/// <summary>  
	/// Specifies the different types of blend effects available.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum BlendTypes
	{
		/// <summary>  
		/// No blend effect.  
		/// </summary>  
		None,
		/// <summary>
		/// Additive blend effect.
		/// </summary>
		Additive,
		/// <summary>
		/// Multiply blend effect.
		/// </summary>
		Multiply,
		/// <summary>
		/// Invert blend effect.
		/// </summary>
		Invert,
	}
}
