namespace RhythmBase.RhythmDoctor.Events;

/// <summary>  
/// Represents an event to set a one-shot wave.  
/// </summary>  
public record class SetOneshotWave : BaseBeat
{
	/// <summary>  
	/// Gets or sets the type of wave.  
	/// </summary>  
	public OneshotWaveShapeType WaveType { get; set; } = OneshotWaveShapeType.BoomAndRush;
	/// <summary>  
	/// Gets or sets the height of the wave.  
	/// </summary>  
	public int Height { get; set; } = 100;
	/// <summary>  
	/// Gets or sets the width of the wave.  
	/// </summary>  
	public int Width { get; set; } = 100;
	///<inheritdoc/>
	public override EventType Type => EventType.SetOneshotWave;
}
