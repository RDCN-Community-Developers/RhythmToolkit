namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents an event to set input configurations in the Adofai event system.
	/// </summary>
	public class SetInputEvent : BaseTaggedTileAction, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.SetInputEvent;

		/// <summary>
		/// Gets or sets the target input action for this event.
		/// </summary>
		public Targets Target { get; set; }

		/// <summary>
		/// Gets or sets the key state (pressed or released) for the target input.
		/// </summary>
		public KeyState state { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether input should be ignored.
		/// </summary>
		public bool IgnoreInput { get; set; }

		/// <summary>
		/// Gets or sets the tag of the target event associated with this input configuration.
		/// </summary>
		public string TargetEventTag { get; set; } = string.Empty;

		/// <summary>
		/// Specifies the possible input targets for the event.
		/// </summary>
		[RDJsonEnumSerializable]
		public enum Targets
		{
			/// <summary>
			/// Any input target.
			/// </summary>
			Any,
			/// <summary>
			/// The first action button.
			/// </summary>
			Action1,
			/// <summary>
			/// The second action button.
			/// </summary>
			Action2,
			/// <summary>
			/// The confirm button.
			/// </summary>
			Confirm,
			/// <summary>
			/// The up direction.
			/// </summary>
			Up,
			/// <summary>
			/// The down direction.
			/// </summary>
			Down,
			/// <summary>
			/// The left direction.
			/// </summary>
			Left,
			/// <summary>
			/// The right direction.
			/// </summary>
			Right,
		}

		/// <summary>
		/// Specifies the possible states of a key.
		/// </summary>
		[RDJsonEnumSerializable]
		public enum KeyState
		{
			/// <summary>
			/// The key is pressed down.
			/// </summary>
			Down,
			/// <summary>
			/// The key is released.
			/// </summary>
			Up,
		}
	}

}
