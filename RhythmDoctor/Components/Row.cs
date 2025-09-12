using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Extensions;
using static RhythmBase.RhythmDoctor.Utils.EventTypeUtils;
namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// A collection of row events.
	/// </summary>
	public class Row : OrderedEventCollection<BaseRowAction>
	{
		/// <summary>
		/// Gets or sets the character associated with the row.
		/// </summary>
		public RDCharacter Character { get; set; } = RDCharacters.Samurai;
		/// <summary>
		/// Gets or sets the type of the row.
		/// </summary>
		public RowTypes RowType
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
		public sbyte Index => (sbyte)(Parent?.Rows.IndexOf(this) ?? throw new RhythmBaseException());
		/// <summary>
		/// Gets or sets the rooms associated with the row.
		/// </summary>
		public RDSingleRoom Rooms { get; set; } = new(RDRoomIndex.Room1);
		/// <summary>
		/// Gets or sets a value indicating whether the row is hidden at the start.
		/// </summary>
		public bool HideAtStart { get; set; }
		/// <summary>
		/// Gets or sets the initial player mode for the row.
		/// </summary>
		public PlayerType Player { get; set; } = PlayerType.P1;
		/// <summary>
		/// Gets the initial beat sound for the row.
		/// </summary>
		public RDAudio Sound { get; set; } = new RDAudio();
		/// <summary>
		/// Gets or sets a value indicating whether the beats are muted.
		/// </summary>
		public bool MuteBeats { get; set; }
		/// <summary>
		/// Gets or sets the row to mimic.
		/// </summary>
		public sbyte RowToMimic { get; set; } = -1;
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
		public TimeSpan PulseSoundOffset
		{
			get => Sound.Offset;
			set => Sound.Offset = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Row"/> class.
		/// </summary>
		public Row() { }
		/// <summary>
		/// Adds an item to the row.
		/// </summary>
		/// <param name="item">The row event to add.</param>
		public override void Add(BaseRowAction item)
		{
			if (item is not BaseBeat ||
				item is BaseBeat && (
				EventTypeEnumsForRowClassic.Contains(item.Type) && RowType is Events.RowTypes.Classic ||
				EventTypeEnumsForRowOneshot.Contains(item.Type) && RowType is Events.RowTypes.Oneshot
				)
				)
			{
				item._parent?.Remove(item);
				item._parent = this;
				Parent?.Add(item);
				return;
			}
			throw new IllegalRowEventTypeException(item.Type, RowType);
		}
		/// <summary>
		/// Adds an item to the row safely.
		/// </summary>
		/// <param name="item">The row event to add.</param>
		internal void AddInternal(BaseRowAction item)
		{
			item._parent = this;
			base.Add(item);
		}

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
		internal bool RemoveInternal(BaseRowAction item) => base.Remove(item);

		private RowTypes _rowType;
		[RDJsonIgnore]
		internal RDLevel? Parent = null;
	}
}
