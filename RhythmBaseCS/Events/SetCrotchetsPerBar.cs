using System;
namespace RhythmBase.Events
{
	public class SetCrotchetsPerBar : BaseEvent, IBarBeginningEvent
	{
		public SetCrotchetsPerBar()
		{
			_visualBeatMultiplier = 0f;
			_crotchetsPerBar = 7U;
			Type = EventType.SetCrotchetsPerBar;
			Tab = Tabs.Sounds;
		}

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public float VisualBeatMultiplier
		{
			get
			{
				return _visualBeatMultiplier + 1f;
			}
			set
			{
				if (value < 1f)
				{
					throw new OverflowException("VisualBeatMultiplier must greater than 1.");
				}
				_visualBeatMultiplier = value - 1f;
			}
		}

		public uint CrotchetsPerBar
		{
			get
			{
				return checked((uint)(unchecked((ulong)_crotchetsPerBar) + 1UL));
			}
			set
			{
				_crotchetsPerBar = checked((uint)(unchecked((ulong)value) - 1UL));
				if (_beat._calculator != null)
				{
					Beat += 0f;
				}
			}
		}

		public override string ToString() => base.ToString() + string.Format(" CPB:{0}", (long)checked(unchecked((ulong)_crotchetsPerBar) + 1UL));

		private float _visualBeatMultiplier;

		protected internal uint _crotchetsPerBar;
	}
}
