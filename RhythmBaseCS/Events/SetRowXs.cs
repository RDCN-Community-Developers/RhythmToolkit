using System;
using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Converters;
using RhythmBase.Extensions;

namespace RhythmBase.Events
{

	public class SetRowXs : BaseBeat
	{

		public SetRowXs()
		{
			_pattern = new LimitedList<Patterns>(6U, Patterns.None);
			Type = EventType.SetRowXs;
			SyncoBeat = -1;
			SyncoVolume = 100;
		}


		public override EventType Type { get; }


		[JsonConverter(typeof(PatternConverter))]
		public LimitedList<Patterns> Pattern
		{
			get
			{
				return _pattern;
			}
			set
			{
				_pattern = value;
			}
		}


		public sbyte SyncoBeat { get; set; }


		public float SyncoSwing { get; set; }


		public bool SyncoPlayModifierSound { get; set; }


		public int SyncoVolume { get; set; }


		public override string ToString() => base.ToString() + string.Format(" {0}", this.GetPatternString());


		private LimitedList<Patterns> _pattern;
	}
}
