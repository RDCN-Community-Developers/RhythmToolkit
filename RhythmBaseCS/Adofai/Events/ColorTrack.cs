﻿using RhythmBase.Adofai.Components;
using RhythmBase.Global.Components;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents the ColorTrack event in ADOFAI.  
	/// </summary>  
	public class ColorTrack : BaseTileEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.ColorTrack;
		/// <summary>  
		/// Gets or sets the type of track color.  
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
		/// Gets or sets the texture of the track.  
		/// </summary>  
		public string TrackTexture { get; set; } = string.Empty;
		/// <summary>  
		/// Gets or sets the scale of the track texture.  
		/// </summary>  
		public float TrackTextureScale { get; set; }
		/// <summary>  
		/// Gets or sets the intensity of the track glow.  
		/// </summary>  
		public float TrackGlowIntensity { get; set; }
	}
}
