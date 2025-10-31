using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to move the camera.
	/// </summary>
	public class MoveCamera : BaseEvent, IEaseEvent, IRoomEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MoveCamera"/> class.
		/// </summary>
		public MoveCamera() { }
		/// <summary>
		/// Gets or sets the rooms associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <summary>
		/// Gets or sets the camera position.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(CameraPosition)} is not null")]
		public RDPoint? CameraPosition { get; set; }
		/// <summary>
		/// Gets or sets the zoom level.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Zoom)} is not null")]
		public int? Zoom { get; set; }
		/// <summary>
		/// Gets or sets the angle of the camera.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Angle)} is not null")]
		public RDExpression? Angle { get; set; }
		/// <summary>
		/// Gets or sets the duration of the event.
		/// </summary>
		public float Duration { get; set; } = 1;
		/// <summary>
		/// Gets or sets a value indicating whether the camera is a real physical device.
		/// </summary>
		[RDJsonProperty("real")]
		[RDJsonCondition($"$&.{nameof(RealCamera)} == true")]
		public bool RealCamera { get; set; } = false;
		/// <summary>
		/// Gets or sets the easing type of the event.
		/// </summary>
		public EaseType Ease { get; set; } = EaseType.Linear;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.MoveCamera;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Actions;
	}
}
