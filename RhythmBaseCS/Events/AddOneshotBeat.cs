﻿using System;
namespace RhythmBase.Events
{
	public class AddOneshotBeat : BaseBeat
	{
		public AddOneshotBeat()
		{
			_delay = 0f;
			Subdivisions = 1;
			Type = EventType.AddOneshotBeat;
		}

		public Pulse PulseType { get; set; }

		public byte Subdivisions { get; set; }

		public bool SubdivSound { get; set; }

		public float Tick { get; set; }

		public uint Loops { get; set; }

		public float Interval { get; set; }

		public bool Skipshot { get; set; }

		public FreezeBurn? FreezeBurnMode { get; set; }

		public float Delay
		{
			get
			{
				return _delay;
			}
			set
			{
				FreezeBurn? freezeBurnMode = FreezeBurnMode;
				int? num = (freezeBurnMode != null) ? new int?((int)freezeBurnMode.GetValueOrDefault()) : null;
				if (((num != null) ? new bool?(num.GetValueOrDefault() == 1) : null).GetValueOrDefault())
				{
					if (value <= 0f)
					{
						_delay = 0.5f;
					}
					else
					{
						_delay = value;
					}
				}
				else
				{
					_delay = 0f;
				}
			}
		}

		public override EventType Type { get; }

		internal bool ShouldSerializeSkipshot() => Skipshot;

		public bool ShouldSerializeFreezeBurnMode() => FreezeBurnMode != null;

		public override string ToString() => base.ToString() + string.Format(" {0} {1}", FreezeBurnMode, PulseType);

		private float _delay;

		public enum Pulse
		{
			Wave,
			Square,
			Triangle,
			Heart
		}

		public enum FreezeBurn
		{
			Wave,
			Freezeshot,
			Burnshot
		}
	}
}
