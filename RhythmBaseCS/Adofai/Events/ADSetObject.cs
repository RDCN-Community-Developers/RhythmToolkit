using Newtonsoft.Json;
using RhythmBase.Adofai.Components;
using RhythmBase.Components;
using RhythmBase.Components.Easing;
using RhythmBase.Events;
namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents an event to set object properties in the Adofai editor.
	/// </summary>
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class ADSetObject : ADBaseTaggedTileAction, IEaseEvent
	{		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.SetObject;		/// <summary>
		/// Gets or sets the duration of the event.
		/// </summary>
		public float Duration { get; set; }		/// <summary>
		/// Gets or sets the tag associated with the object.
		/// </summary>
		public string Tag { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets the easing type for the event.
		/// </summary>
		public EaseType Ease { get; set; }		/// <summary>
		/// Gets or sets the color of the planet.
		/// </summary>
		public RDColor? PlanetColor { get; set; }		/// <summary>
		/// Gets or sets the color of the planet's tail.
		/// </summary>
		public RDColor? PlanetTailColor { get; set; }		/// <summary>
		/// Gets or sets the angle of the track.
		/// </summary>
		public float? TrackAngle { get; set; }		/// <summary>
		/// Gets or sets the type of the track color.
		/// </summary>
		public TrackColorType? TrackColorType { get; set; }		/// <summary>
		/// Gets or sets the primary color of the track.
		/// </summary>
		public RDColor? TrackColor { get; set; }		/// <summary>
		/// Gets or sets the secondary color of the track.
		/// </summary>
		public RDColor? SecondaryTrackColor { get; set; }		/// <summary>
		/// Gets or sets the duration of the track color animation.
		/// </summary>
		public float? TrackColorAnimDuration { get; set; }		/// <summary>
		/// Gets or sets the opacity of the track.
		/// </summary>
		public float? TrackOpacity { get; set; }		/// <summary>
		/// Gets or sets the style of the track.
		/// </summary>
		public TrackStyle? TrackStyle { get; set; }		/// <summary>
		/// Gets or sets the icon of the track.
		/// </summary>
		public TrackIcon? TrackIcon { get; set; }		/// <summary>
		/// Gets or sets the angle of the track icon.
		/// </summary>
		public float? TrackIconAngle { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether the track has a red swirl.
		/// </summary>
		public bool? TrackRedSwirl { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether the track has a gray set speed icon.
		/// </summary>
		public bool? TrackGraySetSpeedIcon { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether the track glow is enabled.
		/// </summary>
		public bool? TrackGlowEnabled { get; set; }		/// <summary>
		/// Gets or sets the color of the track glow.
		/// </summary>
		public RDColor? TrackGlowColor { get; set; }
	}
	/// <summary>  
	/// Represents the various track icons that can be used in the Adofai editor.  
	/// </summary>  
	public enum TrackIcon
	{
		/// <summary>  
		/// No icon.  
		/// </summary>  
		None,		/// <summary>  
		/// A single snail icon.  
		/// </summary>  
		Snall,		/// <summary>  
		/// A double snail icon.  
		/// </summary>  
		DoubleSnall,		/// <summary>  
		/// A rabbit icon.  
		/// </summary>  
		Rabbit,		/// <summary>  
		/// A double rabbit icon.  
		/// </summary>  
		DoubleRabbit,		/// <summary>  
		/// A swirl icon.  
		/// </summary>  
		Swirl,		/// <summary>  
		/// A checkpoint icon.  
		/// </summary>  
		Checkpoint,		/// <summary>  
		/// A short hold arrow icon.  
		/// </summary>  
		HoldArrowShort,		/// <summary>  
		/// A long hold arrow icon.  
		/// </summary>  
		HoldArrowLong,		/// <summary>  
		/// A short hold release icon.  
		/// </summary>  
		HoldReleaseShort,		/// <summary>  
		/// A long hold release icon.  
		/// </summary>  
		HoldReleaseLong,		/// <summary>  
		/// A two-planet multi-planet icon.  
		/// </summary>  
		MultiPlanetTwo,		/// <summary>  
		/// A three-planet multi-planet icon.  
		/// </summary>  
		MultiPlanetThree,		/// <summary>  
		/// A portal icon.  
		/// </summary>  
		Portal,
	}
}
