namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// Subtypes of sound effects.
	/// </summary>
	[RDJsonEnumSerializable]
	public class SoundSubType : RDAudio
	{
		/// <summary>
		/// Gets or sets the sound effect name.
		/// </summary>
		public SoundTypes GroupSubtype { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="SoundSubType"/> is used.
		/// </summary>
		public bool Used { get; set; }
	}
}
