namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to set the hitsound in the ADOFAI level.  
	/// </summary>  
	public class SetHitsound : BaseTileEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.SetHitsound;
		/// <summary>  
		/// Gets or sets the game sound associated with the hitsound.  
		/// </summary>  
		public GameSounds GameSound { get; set; }
		/// <summary>  
		/// Gets or sets the custom hitsound file.  
		/// </summary>  
		public string Hitsound { get; set; } = "Kick";
		/// <summary>  
		/// Gets or sets the volume of the hitsound.  
		/// </summary>  
		public int HitsoundVolume { get; set; }
		/// <summary>  
		/// Represents the predefined game sounds available for hitsounds.  
		/// </summary>  
		[RDJsonEnumSerializable]
		public enum GameSounds
		{
			/// <summary>  
			/// The default hitsound.  
			/// </summary>  
			Hitsound,
			/// <summary>  
			/// The sound used for midspin events.  
			/// </summary>  
			Midspin
		}
	}
}
