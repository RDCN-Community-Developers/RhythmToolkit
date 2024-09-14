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
			get
			{
				return _rowType;
			}
			set
			{
				bool flag = value != _rowType;
				if (flag)
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
		public sbyte Index
		{
			get
			{
				return checked((sbyte)Parent._rows.IndexOf(this));
			}
		}


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
			get
			{
				return Sound.Name;
			}
		}


		public int PulseSoundVolume
		{
			get
			{
				return Sound.Volume;
			}
		}


		public int PulseSoundPitch
		{
			get
			{
				return Sound.Pitch;
			}
		}


		public int PulseSoundPan
		{
			get
			{
				return Sound.Pan;
			}
		}


		[JsonConverter(typeof(TimeConverter))]
		public TimeSpan PulseSoundOffset
		{
			get
			{
				return Sound.Offset;
			}
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
