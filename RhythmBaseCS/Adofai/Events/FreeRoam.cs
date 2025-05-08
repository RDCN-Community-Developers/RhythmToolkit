using System;
using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Components.Easing;
using RhythmBase.Events;
namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents the Free Roam event in the ADOFAI editor.
	/// </summary>
	public class FreeRoam : BaseTileEvent, IEaseEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.FreeRoam;
		/// <summary>
		/// Gets or sets the duration of the Free Roam event.
		/// </summary>
		public float Duration { get; set; }
		/// <summary>
		/// Gets or sets the size of the Free Roam area.
		/// </summary>
		public int Size { get; set; }
		/// <summary>
		/// Gets or sets the position offset for the Free Roam event.
		/// </summary>
		public int PositionOffset { get; set; }
		/// <summary>
		/// Gets or sets the time at which the Free Roam event ends.
		/// </summary>
		public int OutTime { get; set; }
		/// <summary>
		/// Gets or sets the easing type for the Free Roam event.
		/// </summary>
		[JsonProperty("OutEase")]
		public EaseType Ease { get; set; }
		/// <summary>
		/// Gets or sets the hitsound to be played on beats during the Free Roam event.
		/// </summary>
		public string HitsoundOnBeats { get; set; } = "None";
		/// <summary>
		/// Gets or sets the hitsound to be played off beats during the Free Roam event.
		/// </summary>
		public string HitsoundOffBeats { get; set; } = "None";
		/// <summary>
		/// Gets or sets the number of countdown ticks for the Free Roam event.
		/// </summary>
		public int CountdownTicks { get; set; }
		/// <summary>
		/// Gets or sets the angle correction direction for the Free Roam event.
		/// </summary>
		public int AngleCorrectionDir { get; set; }
	}
}
