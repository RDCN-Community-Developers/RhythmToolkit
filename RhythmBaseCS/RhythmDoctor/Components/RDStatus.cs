using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// Record the status of RDLevel moment.
	/// </summary>
	public readonly record struct RDStatus()
	{
		/// <summary>
		/// Gets the beat information.
		/// </summary>
		public required RDBeat Beat { get; init; }

		/// <summary>
		/// Gets the room status information.
		/// </summary>
		public required RoomStatus[] RoomStatus { get; init; }

		/// <summary>
		/// Gets the row status information.
		/// </summary>
		public required RowStatus[] RowStatus { get; init; }
	}

	/// <summary>
	/// Represents the status of a room.
	/// </summary>
	public readonly record struct RoomStatus
	{
		/// <summary>
		/// Gets the beat information.
		/// </summary>
		public required RDBeat Beat { get; init; }

		/// <summary>
		/// Gets the running VFX presets.
		/// </summary>
		public required SetVFXPreset[] RunningVFXs { get; init; }

		/// <summary>
		/// Gets the theme of the room.
		/// </summary>
		public required SetTheme.Theme Theme { get; init; }

		/// <summary>
		/// Gets the background color of the room.
		/// </summary>
		public required SetBackgroundColor? Background { get; init; }

		/// <summary>
		/// Gets the foreground settings of the room.
		/// </summary>
		public required SetForeground? Foreground { get; init; }

		/// <summary>
		/// Gets the screen shake settings of the room.
		/// </summary>
		public required ShakeScreen? Shake { get; init; }

		/// <summary>
		/// Gets the stutter settings of the room.
		/// </summary>
		public required Stutter? Stutter { get; init; }

		/// <summary>
		/// Gets the screen flip settings of the room.
		/// </summary>
		public required FlipScreen? Flip { get; init; }

		/// <summary>
		/// Gets the bass drop settings of the room.
		/// </summary>
		public required BassDrop? BassDrop { get; init; }

		/// <summary>
		/// Gets the flash settings of the room.
		/// </summary>
		public required Flash? Flash { get; init; }
	}

	/// <summary>
	/// Represents the status of a row.
	/// </summary>
	public readonly record struct RowStatus()
	{
		/// <summary>
		/// Gets the beat information.
		/// </summary>
		public required RDBeat Beat { get; init; }

		/// <summary>
		/// Gets the parent row event collection.
		/// </summary>
		public required Row ParentRow { get; init; }

		/// <summary>
		/// Gets the player type.
		/// </summary>
		public required PlayerType PlayerType { get; init; }

		/// <summary>
		/// Gets the sound information.
		/// </summary>
		public required RDAudio Sound { get; init; }
	}
}
