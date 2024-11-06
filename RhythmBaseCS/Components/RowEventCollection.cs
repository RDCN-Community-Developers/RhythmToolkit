using Newtonsoft.Json;
using RhythmBase.Converters;
using RhythmBase.Events;
using RhythmBase.Exceptions;
namespace RhythmBase.Components
{
	/// <summary>
	/// A collection of row events.
	/// </summary>
	[JsonObject]
	public class RowEventCollection : OrderedEventCollection<BaseRowAction>
	{
		/// <summary>
		/// Gets or sets the character associated with the row.
		/// </summary>
		public RDCharacter Character { get; set; }

		/// <summary>
		/// Gets or sets the type of the row.
		/// </summary>
		public RowType RowType
		{
			get => _rowType;
			set
			{
				if (value != _rowType)
				{
					Clear();
					_rowType = value;
				}
			}
		}

		/// <summary>
		/// Gets the index of the row.
		/// </summary>
		[JsonProperty("row", DefaultValueHandling = DefaultValueHandling.Include)]
		public sbyte Index => (sbyte)(Parent?.ModifiableRows.IndexOf(this) ?? throw new RhythmBaseException());

		/// <summary>
		/// Gets or sets the rooms associated with the row.
		/// </summary>
		public SingleRoom Rooms { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the row is hidden at the start.
		/// </summary>
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool HideAtStart { get; set; }

		/// <summary>
		/// Gets or sets the initial player mode for the row.
		/// </summary>
		public PlayerMode Player { get; set; } = PlayerMode.P1;

		/// <summary>
		/// Gets the initial beat sound for the row.
		/// </summary>
		[JsonIgnore]
		public RDAudio Sound { get; }

		/// <summary>
		/// Gets or sets a value indicating whether the beats are muted.
		/// </summary>
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool MuteBeats { get; set; }

		/// <summary>
		/// Gets or sets the row to mimic.
		/// </summary>
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public sbyte RowToMimic { get; set; }

		/// <summary>
		/// Gets or sets the name of the pulse sound.
		/// </summary>
		public string PulseSound
		{
			get => Sound.Filename;
			set => Sound.Filename = value;
		}

		/// <summary>
		/// Gets or sets the volume of the pulse sound.
		/// </summary>
		public int PulseSoundVolume
		{
			get => Sound.Volume;
			set => Sound.Volume = value;
		}

		/// <summary>
		/// Gets or sets the pitch of the pulse sound.
		/// </summary>
		public int PulseSoundPitch
		{
			get => Sound.Pitch;
			set => Sound.Pitch = value;
		}

		/// <summary>
		/// Gets or sets the pan of the pulse sound.
		/// </summary>
		public int PulseSoundPan
		{
			get => Sound.Pan;
			set => Sound.Pan = value;
		}

		/// <summary>
		/// Gets or sets the offset of the pulse sound.
		/// </summary>
		[JsonConverter(typeof(TimeConverter))]
		public TimeSpan PulseSoundOffset
		{
			get => Sound.Offset;
			set => Sound.Offset = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RowEventCollection"/> class.
		/// </summary>
		internal RowEventCollection()
		{
			Rooms = new SingleRoom(RoomIndex.Room1);
			Sound = new RDAudio();
			RowToMimic = -1;
		}

		/// <summary>
		/// Adds an item to the row.
		/// </summary>
		/// <param name="item">The row event to add.</param>
		public override void Add(BaseRowAction item)
		{
			item._parent?.Remove(item);
			item._parent = this;
			Parent?.Add(item);
		}

		/// <summary>
		/// Adds an item to the row safely.
		/// </summary>
		/// <param name="item">The row event to add.</param>
		internal void AddSafely(BaseRowAction item) => base.Add(item);

		/// <summary>
		/// Removes an item from the row.
		/// </summary>
		/// <param name="item">The row event to remove.</param>
		/// <returns>True if the item was successfully removed; otherwise, false.</returns>
		public override bool Remove(BaseRowAction item) => Parent?.Remove(item) ?? throw new RhythmBaseException();

		/// <summary>
		/// Removes an item from the row safely.
		/// </summary>
		/// <param name="item">The row event to remove.</param>
		/// <returns>True if the item was successfully removed; otherwise, false.</returns>
		internal bool RemoveSafely(BaseRowAction item) => base.Remove(item);

		private RowType _rowType;

		[JsonIgnore]
		internal RDLevel? Parent = null;

		/// <summary>
		/// Represents the player mode.
		/// </summary>
		public enum PlayerMode
		{
			/// <summary>
			/// Player 1.
			/// </summary>
			P1 = PlayerType.P1,

			/// <summary>
			/// Player 2.
			/// </summary>
			P2 = PlayerType.P2,

			/// <summary>
			/// CPU player.
			/// </summary>
			CPU = PlayerType.CPU,
		}
	}
}
