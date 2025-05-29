using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// Record the status of RDLevel moment.
	/// </summary>
	public
#if NET5_0_OR_GREATER
readonly 
#endif
		record struct RDStatus
	{
		public RDStatus() { }
		/// <summary>
		/// Gets the beat information.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public RDBeat Beat
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}

		/// <summary>
		/// Gets the room status information.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public RoomStatus[] RoomStatus
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}

		/// <summary>
		/// Gets the row status information.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public RowStatus[] RowStatus
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}
	}

	/// <summary>
	/// Represents the status of a room.
	/// </summary>
	public
#if NET5_0_OR_GREATER
readonly 
#endif
record struct RoomStatus
	{
		/// <summary>
		/// Gets the beat information.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public RDBeat Beat
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}

		/// <summary>
		/// Gets the running VFX presets.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public SetVFXPreset[] RunningVFXs
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}

		/// <summary>
		/// Gets the theme of the room.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public SetTheme.Theme Theme
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}

		/// <summary>
		/// Gets the background color of the room.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public SetBackgroundColor? Background
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}

		/// <summary>
		/// Gets the foreground settings of the room.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public SetForeground? Foreground
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}

		/// <summary>
		/// Gets the screen shake settings of the room.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public ShakeScreen? Shake
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}

		/// <summary>
		/// Gets the stutter settings of the room.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public Stutter? Stutter
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}

		/// <summary>
		/// Gets the screen flip settings of the room.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public FlipScreen? Flip
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}

		/// <summary>
		/// Gets the bass drop settings of the room.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public BassDrop? BassDrop
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}

		/// <summary>
		/// Gets the flash settings of the room.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public Flash? Flash
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}
	}

	/// <summary>
	/// Represents the status of a row.
	/// </summary>
	public
#if NET7_0_OR_GREATER
readonly 
#endif
record struct RowStatus
	{
		public RowStatus() { }
		/// <summary>
		/// Gets the beat information.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public RDBeat Beat
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}

		/// <summary>
		/// Gets the parent row event collection.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public Row ParentRow
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}

		/// <summary>
		/// Gets the player type.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public PlayerType PlayerType
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}

		/// <summary>
		/// Gets the sound information.
		/// </summary>
#if NET7_0_OR_GREATER
	required
#endif
		public RDAudio Sound
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		}
	}
}
