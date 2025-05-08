using Newtonsoft.Json;
using RhythmBase.Global.Components;
using RhythmBase.Global.Events;
using RhythmBase.Global.Components.Easing;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to move the camera in the Adofai game.  
	/// </summary>  
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class MoveCamera : BaseTaggedTileAction, IEaseEvent, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.MoveCamera;
		/// <summary>  
		/// Gets or sets the duration of the camera movement.  
		/// </summary>  
		public float Duration { get; set; }
		/// <summary>  
		/// Gets or sets the easing type for the camera movement.  
		/// </summary>  
		public EaseType Ease { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether the camera should not be disabled after the movement.  
		/// </summary>  
		public bool DontDisable { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether only minimal visual effects should be applied.  
		/// </summary>  
		public bool MinVfxOnly { get; set; }
		/// <summary>  
		/// Gets or sets the reference point for the camera movement.  
		/// </summary>  
		public CameraRelativeTo RelativeTo { get; set; }
		/// <summary>  
		/// Gets or sets the target position of the camera.  
		/// </summary>  
		public RDPoint? Position { get; set; }
		/// <summary>  
		/// Gets or sets the rotation angle of the camera.  
		/// </summary>  
		public float Rotation { get; set; }
		/// <summary>  
		/// Gets or sets the zoom level of the camera.  
		/// </summary>  
		public float Zoom { get; set; }
	}
}
