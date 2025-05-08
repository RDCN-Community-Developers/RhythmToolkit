using RhythmBase.Adofai.Components;
using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Events;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to recolor a track in the Adofai level.  
	/// </summary>  
	public class RecolorTrack : BaseTaggedTileAction, IEaseEvent, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type { get; }
		/// <summary>  
		/// Gets or sets the starting tile reference for the recolor action.  
		/// </summary>  
		public TileReference StartTile { get; set; }
		/// <summary>  
		/// Gets or sets the ending tile reference for the recolor action.  
		/// </summary>  
		public TileReference EndTile { get; set; }
		/// <summary>  
		/// Gets or sets the gap length between tiles for the recolor action.  
		/// </summary>  
		public int GapLength { get; set; }
		/// <summary>  
		/// Gets or sets the duration of the recolor animation.  
		/// </summary>  
		public float Duration { get; set; }
		/// <summary>  
		/// Gets or sets the type of track color to apply.  
		/// </summary>  
		public TrackColorType TrackColorType { get; set; }
		/// <summary>  
		/// Gets or sets the primary track color.  
		/// </summary>  
		public RDColor TrackColor { get; set; }
		/// <summary>  
		/// Gets or sets the secondary track color.  
		/// </summary>  
		public RDColor SecondaryTrackColor { get; set; }
		/// <summary>  
		/// Gets or sets the duration of the track color animation.  
		/// </summary>  
		public float TrackColorAnimDuration { get; set; }
		/// <summary>  
		/// Gets or sets the track color pulse type.  
		/// </summary>  
		public TrackColorPulses TrackColorPulse { get; set; }
		/// <summary>  
		/// Gets or sets the length of the track pulse.  
		/// </summary>  
		public float TrackPulseLength { get; set; }
		/// <summary>  
		/// Gets or sets the style of the track.  
		/// </summary>  
		public TrackStyle TrackStyle { get; set; }
		/// <summary>  
		/// Gets or sets the intensity of the track glow effect.  
		/// </summary>  
		public float TrackGlowIntensity { get; set; }
		/// <summary>  
		/// Gets or sets the easing type for the recolor animation.  
		/// </summary>  
		public EaseType Ease { get; set; }
	}
}
