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
		public SetOneshotWave()
		{
			Type = EventType.SetOneshotWave;
		}
		/// <summary>  
		/// Gets or sets the type of wave.  
		/// </summary>  
		public Waves WaveType { get; set; }
		/// <summary>  
		/// Gets or sets the height of the wave.  
		/// </summary>  
		public int Height { get; set; }
		/// <summary>  
		/// Gets or sets the width of the wave.  
		/// </summary>  
		public int Width { get; set; }
		/// <summary>  
		/// Gets the type of the event.  
		/// </summary>  
		public override EventType Type { get; }
		/// <summary>  
		/// Defines the types of waves.  
		/// </summary>  
		public enum Waves
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
}
