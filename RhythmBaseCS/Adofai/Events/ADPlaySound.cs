using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event that plays a sound in the Adofai level.  
	/// </summary>  
	public class ADPlaySound : ADBaseTaggedTileAction
	{
		/// <summary>  
		/// Gets the type of the event, which is <see cref="ADEventType.PlaySound"/>.  
		/// </summary>  
		public override ADEventType Type => ADEventType.PlaySound;		/// <summary>  
		/// Gets or sets the name of the hitsound to be played.  
		/// </summary>  
		public string Hitsound { get; set; } = "Hat";		/// <summary>  
		/// Gets or sets the volume of the hitsound.  
		/// </summary>  
		public int HitsoundVolume { get; set; }
	}
}
