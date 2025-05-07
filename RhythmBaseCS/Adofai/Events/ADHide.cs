namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event that hides specific elements in the level.  
	/// </summary>  
	public class ADHide : ADBaseTileEvent
	{
		/// <inheritdoc/>  
		public override ADEventType Type => ADEventType.Hide;		/// <summary>  
		/// Gets or sets a value indicating whether the judgment should be hidden.  
		/// </summary>  
		public bool HideJudgment { get; set; }		/// <summary>  
		/// Gets or sets a value indicating whether the tile icon should be hidden.  
		/// </summary>  
		public bool HideTileIcon { get; set; }
	}
}
