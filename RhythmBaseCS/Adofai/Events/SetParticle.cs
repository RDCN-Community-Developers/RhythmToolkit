using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Events;

namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents an event to set particle effects in the Adofai event system.
	/// </summary>
	public class SetParticle : BaseTaggedTileAction, IEaseEvent, IStartEvent
	{
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type => EventType.SetParticle;

		/// <summary>
		/// Gets or sets the duration of the particle effect.
		/// </summary>
		public float Duration { get; set; }

		/// <summary>
		/// Gets or sets the tag associated with the particle effect.
		/// </summary>
		public string Tag { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the target mode for the particle effect.
		/// </summary>
		public TargetMode TargetMode { get; set; }

		/// <summary>
		/// Gets or sets the easing type for the particle effect.
		/// </summary>
		public EaseType Ease { get; set; }
	}
	/// <summary>
	/// Specifies the target mode for the particle effect.
	/// </summary>
	public enum TargetMode
	{
		/// <summary>
		/// Starts the particle effect.
		/// </summary>
		Start,

		/// <summary>
		/// Stops the particle effect.
		/// </summary>
		Stop,

		/// <summary>
		/// Clears the particle effect.
		/// </summary>
		Clear,
	}

}
