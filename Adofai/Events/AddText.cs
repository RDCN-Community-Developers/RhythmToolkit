using RhythmBase.Global.Components;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to add text in the game.  
	/// </summary>  
	public class AddText : BaseEvent, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.AddText;
		/// <summary>  
		/// Gets or sets the text to be displayed.  
		/// </summary>  
		public string DecText { get; set; } = string.Empty;
		/// <summary>  
		/// Gets or sets the font of the text.  
		/// </summary>  
		public string Font { get; set; } = string.Empty;
		/// <summary>  
		/// Gets or sets the position of the text.  
		/// </summary>  
		public RDPointN Position { get; set; }
		/// <summary>  
		/// Gets or sets the relative position of the text to the camera.  
		/// </summary>  
		public CameraRelativeTo RelativeTo { get; set; }
		/// <summary>  
		/// Gets or sets the pivot offset of the text.  
		/// </summary>  
		public RDSizeN PivotOffset { get; set; }
		/// <summary>  
		/// Gets or sets the rotation of the text in degrees.  
		/// </summary>  
		public float Rotation { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether the rotation of the text is locked.  
		/// </summary>  
		public bool LockRotation { get; set; }
		/// <summary>  
		/// Gets or sets the scale of the text.  
		/// </summary>  
		public RDSizeN Scale { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether the scale of the text is locked.  
		/// </summary>  
		public bool LockScale { get; set; }
		/// <summary>  
		/// Gets or sets the color of the text.  
		/// </summary>  
		public RDColor Color { get; set; }
		/// <summary>  
		/// Gets or sets the opacity of the text.  
		/// </summary>  
		public float Opacity { get; set; }
		/// <summary>  
		/// Gets or sets the depth of the text in the rendering order.  
		/// </summary>  
		public int Depth { get; set; }
		/// <summary>  
		/// Gets or sets the parallax effect of the text.  
		/// </summary>  
		public RDSizeN Parallax { get; set; }
		/// <summary>  
		/// Gets or sets the parallax offset of the text.  
		/// </summary>  
		public RDSizeN ParallaxOffset { get; set; }
		/// <summary>  
		/// Gets or sets the tag associated with the text.  
		/// </summary>  
		public string Tag { get; set; } = string.Empty;
	}
}
