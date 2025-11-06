namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents an event to set a one-shot wave.  
	/// </summary>  
	public class SetOneshotWave : BaseBeat
	{
		/// <summary>  
		/// Initializes a new instance of the <see cref="SetOneshotWave"/> class.  
		/// </summary>  
		public SetOneshotWave() { }
		/// <summary>  
		/// Gets or sets the type of wave.  
		/// </summary>  
		public OneshotWaveShapeTypes WaveType { get; set; } = OneshotWaveShapeTypes.BoomAndRush;
		/// <summary>  
		/// Gets or sets the height of the wave.  
		/// </summary>  
		public int Height { get; set; } = 100;
		/// <summary>  
		/// Gets or sets the width of the wave.  
		/// </summary>  
		public int Width { get; set; } = 100;
		/// <summary>  
		/// Gets the type of the event.  
		/// </summary>  
		public override EventType Type => EventType.SetOneshotWave;
	}
	/// <summary>  
	/// Defines the types of waves.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum OneshotWaveShapeTypes
	{
		/// <summary>  
		/// Boom and rush wave.  
		/// </summary>  
		BoomAndRush,
		/// <summary>  
		/// Ball wave.  
		/// </summary>  
		Ball,
		/// <summary>  
		/// Spring wave.  
		/// </summary>  
		Spring,
		/// <summary>  
		/// Spike wave.  
		/// </summary>  
		Spike,
		/// <summary>  
		/// Huge spike wave.  
		/// </summary>  
		SpikeHuge,
		/// <summary>  
		/// Single wave.  
		/// </summary>  
		Single
	}
}
