namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to set the number of crotchets per bar.
/// </summary>
public record class SetCrotchetsPerBar : BaseEvent, IBarBeginningEvent
{
	/// <summary>
	/// Gets the type of the event.
	/// </summary>
	public override EventType Type => EventType.SetCrotchetsPerBar;
	/// <summary>
	/// Gets the tab associated with the event.
	/// </summary>
	public override Tab Tab => Tab.Sounds;
	/// <summary>
	/// Gets or sets the visual beat multiplier.
	/// </summary>
	/// <exception cref="OverflowException">Thrown when the value is less than 1.</exception>
	public float VisualBeatMultiplier { get; set; } = 1;
	/// <summary>
	/// Gets or sets the number of crotchets per bar.
	/// </summary>
	public int CrotchetsPerBar
	{
		get => _crotchetsPerBar + 1;
		set
		{
			_crotchetsPerBar = Math.Max(1, value - 1);
			if (_beat._calculator != null)
			{
				Beat += 0f;
			}
		}
	}
	/// <summary>
	/// Returns a string that represents the current object.
	/// </summary>
	public override string ToString() => base.ToString() + $" CPB:{_crotchetsPerBar + 1}";
	/// <summary>
	/// The number of crotchets per bar.
	/// </summary>
	protected internal int _crotchetsPerBar = 7;
}
