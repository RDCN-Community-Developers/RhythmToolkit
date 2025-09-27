namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event that sets the hold sound properties for a tile in the ADOFAI editor.  
	/// </summary>  
	public class SetHoldSound : BaseTileEvent
	{		/// <inheritdoc/>
		public override EventType Type => EventType.SetHoldSound;		/// <summary>  
		/// Gets or sets the sound to play at the start of the hold.  
		/// </summary>  
		public HoldSounds HoldStartSound { get; set; }		/// <summary>  
		/// Gets or sets the sound to loop during the hold.  
		/// </summary>  
		public HoldSounds HoldLoopSound { get; set; }		/// <summary>  
		/// Gets or sets the sound to play at the end of the hold.  
		/// </summary>  
		public HoldSounds HoldEndSound { get; set; }		/// <summary>  
		/// Gets or sets the sound to play in the middle of the hold.  
		/// </summary>  
		public HoldMidSounds HoldMidSound { get; set; }		/// <summary>  
		/// Gets or sets the type of the mid-hold sound.  
		/// </summary>  
		public HoldMidSoundTypes HoldMidSoundType { get; set; }		/// <summary>  
		/// Gets or sets the delay before the mid-hold sound is played.  
		/// </summary>  
		public float HoldMidSoundDelay { get; set; }		/// <summary>  
		/// Gets or sets the timing reference for the mid-hold sound.  
		/// </summary>  
		public RelativeTypes HoldMidSoundTimingRelativeTo { get; set; }		/// <summary>  
		/// Gets or sets the volume of the hold sound.  
		/// </summary>  
		public int HoldSoundVolume { get; set; }		/// <summary>  
		/// Represents the types of sounds that can be played at the start or end of a hold.  
		/// </summary>  
		[RDJsonEnumSerializable]
		public enum HoldSounds
		{
			/// <summary>  
			/// A fuse sound effect.  
			/// </summary>  
			Fuse,			/// <summary>  
			/// No sound effect.  
			/// </summary>  
			None,
		}
		/// <summary>  
		/// Represents the types of sounds that can be played in the middle of a hold.  
		/// </summary>  
		[RDJsonEnumSerializable]
		public enum HoldMidSounds
		{
			/// <summary>  
			/// A fuse sound effect.  
			/// </summary>  
			Fuse,			/// <summary>  
			/// A "SingSing" sound effect.  
			/// </summary>  
			SingSing,			/// <summary>  
			/// No sound effect.  
			/// </summary>  
			None,
		}
		/// <summary>  
		/// Represents the types of mid-hold sound playback behaviors.  
		/// </summary>  
		[RDJsonEnumSerializable]
		public enum HoldMidSoundTypes
		{
			/// <summary>  
			/// The sound is played once.  
			/// </summary>  
			Once,			/// <summary>  
			/// The sound is repeated.  
			/// </summary>  
			Repeat,
		}
		/// <summary>  
		/// Represents the timing reference for the mid-hold sound.  
		/// </summary>  
		[RDJsonEnumSerializable]
		public enum RelativeTypes
		{
			/// <summary>  
			/// The timing is relative to the start of the hold.  
			/// </summary>  
			Start,			/// <summary>  
			/// The timing is relative to the end of the hold.  
			/// </summary>  
			End,
		}
	}
}
