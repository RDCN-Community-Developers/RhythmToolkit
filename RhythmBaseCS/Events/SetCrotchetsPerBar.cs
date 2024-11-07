namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event to set the number of crotchets per bar.
	/// </summary>
	public class SetCrotchetsPerBar : BaseEvent, IBarBeginningEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetCrotchetsPerBar"/> class.
		/// </summary>
		public SetCrotchetsPerBar()
		{
			_crotchetsPerBar = 7U;
			Type = EventType.SetCrotchetsPerBar;
			Tab = Tabs.Sounds;
		}

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Gets or sets the visual beat multiplier.
		/// </summary>
		/// <exception cref="OverflowException">Thrown when the value is less than 1.</exception>
		public float VisualBeatMultiplier { get; set; } = 1;

		/// <summary>
		/// Gets or sets the number of crotchets per bar.
		/// </summary>
		public uint CrotchetsPerBar
		{
			get => (uint)(_crotchetsPerBar + 1);
			set
			{
				_crotchetsPerBar = checked((uint)(unchecked((ulong)value) - 1UL));
				if (_beat._calculator != null)
				{
					Beat += 0f;
				}
			}
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + $" CPB:{_crotchetsPerBar + 1}";

		/// <summary>
		/// The number of crotchets per bar.
		/// </summary>
		protected internal uint _crotchetsPerBar;
	}
}
