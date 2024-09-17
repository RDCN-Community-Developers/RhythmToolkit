using System;
using Newtonsoft.Json;
using RhythmBase.Assets;
using RhythmBase.Converters;
using RhythmBase.Events;
namespace RhythmBase.Components
{
	/// <summary>
	/// A row.
	/// </summary>
	[JsonObject]
	public class RowEventCollection : OrderedEventCollection<BaseRowAction>
	{
		public RDCharacter Character { get; set; }
		/// <summary>
		/// Row type.
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
		/// Decoration index.
		/// </summary>
		[JsonProperty("row", DefaultValueHandling = DefaultValueHandling.Include)]
		public sbyte Index => (sbyte)Parent._rows.IndexOf(this);

		public SingleRoom Rooms { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool HideAtStart { get; set; }
		/// <summary>
		/// Initial play mode for this row.
		/// </summary>
		public PlayerMode Player { get; set; }
		/// <summary>
		/// Initial beat sound for this row.
		/// </summary>
		[JsonIgnore]
		public Audio Sound { get; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool MuteBeats { get; set; }
		/// <summary>
		/// Mirroring of the row.
		/// </summary>
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public sbyte RowToMimic { get; set; }

		public string PulseSound
		{
			get => Sound.Name;
			set => Sound.Name = value;
		}

		public int PulseSoundVolume
		{
			get => Sound.Volume;
			set => Sound.Volume = value;
		}

		public int PulseSoundPitch
		{
			get => Sound.Pitch;
			set => Sound.Pitch = value;
		}

		public int PulseSoundPan
		{
			get => Sound.Pan;
			set => Sound.Pan = value;
		}

		[JsonConverter(typeof(TimeConverter))]
		public TimeSpan PulseSoundOffset
		{
			get => Sound.Offset;
			set => Sound.Offset = value;
		}

		internal RowEventCollection()
		{
			Rooms = new SingleRoom(0);
			this.Sound = new Audio();
			RowToMimic = -1;
		}
		/// <summary>
		/// Add an item to row.
		/// </summary>
		/// <param name="item">Row event.</param>
		public override void Add(BaseRowAction item)
		{
			item._parent = this;
			Parent.Add(item);
		}

		internal void AddSafely(BaseRowAction item) => base.Add(item);
		/// <summary>
		/// Remove an item from row.
		/// </summary>
		/// <param name="item">Row event.</param>
		public override bool Remove(BaseRowAction item) => Parent.Remove(item);

		internal bool RemoveSafely(BaseRowAction item) => base.Remove(item);

		private RowType _rowType;

		[JsonIgnore]
		internal RDLevel Parent;
		/// <summary>
		/// Player mode.
		/// </summary>
		public enum PlayerMode
		{
			P1,
			P2,
			CPU
		}
	}
}
