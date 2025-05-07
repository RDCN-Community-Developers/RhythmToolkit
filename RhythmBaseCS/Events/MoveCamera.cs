using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Components.Easing;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event to move the camera.
	/// </summary>
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class MoveCamera : BaseEvent, IEaseEvent, IRoomEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MoveCamera"/> class.
		/// </summary>
		public MoveCamera()
		{
			Rooms = new RDRoom(true, [0]);
			Type = EventType.MoveCamera;
			Tab = Tabs.Actions;
		}		/// <summary>
		/// Gets or sets the rooms associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; }		/// <summary>
		/// Gets or sets the camera position.
		/// </summary>
		[EaseProperty]
		public RDPointE? CameraPosition { get; set; }		/// <summary>
		/// Gets or sets the zoom level.
		/// </summary>
		[EaseProperty]
		public int? Zoom { get; set; }		/// <summary>
		/// Gets or sets the angle of the camera.
		/// </summary>
		[EaseProperty]
		public RDExpression? Angle { get; set; }		/// <summary>
		/// Gets or sets the duration of the event.
		/// </summary>
		public float Duration { get; set; }		/// <summary>
		/// Gets or sets the easing type of the event.
		/// </summary>
		public EaseType Ease { get; set; }		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }
	}
}
