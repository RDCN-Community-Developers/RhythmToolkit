using RhythmBase.Adofai.Components;
using RhythmBase.Global.Components;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to add an object in the game.  
	/// </summary>  
	public class AddObject : BaseEvent, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.AddObject;
		/// <summary>  
		/// Gets or sets the type of the object to be added.  
		/// </summary>  
		public ObjectTypes ObjectType { get; set; }
		/// <summary>  
		/// Gets or sets the type of the planet's color.  
		/// </summary>  
		public PlanetColorTypes PlanetColorType { get; set; }
		/// <summary>  
		/// Gets or sets the color of the planet.  
		/// </summary>  
		public RDColor PlanetColor { get; set; }
		/// <summary>  
		/// Gets or sets the tail color of the planet.  
		/// </summary>  
		public RDColor PlanetTailColor { get; set; }
		/// <summary>  
		/// Gets or sets the type of the track.  
		/// </summary>  
		public TrackTypes TrackType { get; set; }
		/// <summary>  
		/// Gets or sets the angle of the track.  
		/// </summary>  
		public float TrackAngle { get; set; }
		/// <summary>  
		/// Gets or sets the type of the track's color.  
		/// </summary>  
		public TrackColorType TrackColorType { get; set; }
		/// <summary>  
		/// Gets or sets the primary color of the track.  
		/// </summary>  
		public RDColor TrackColor { get; set; }
		/// <summary>  
		/// Gets or sets the secondary color of the track.  
		/// </summary>  
		public RDColor SecondaryTrackColor { get; set; }
		/// <summary>  
		/// Gets or sets the duration of the track color animation.  
		/// </summary>  
		public float TrackColorAnimDuration { get; set; }
		/// <summary>  
		/// Gets or sets the opacity of the track.  
		/// </summary>  
		public float TrackOpacity { get; set; }
		/// <summary>  
		/// Gets or sets the style of the track.  
		/// </summary>  
		public TrackStyle TrackStyle { get; set; }
		/// <summary>  
		/// Gets or sets the icon of the track.  
		/// </summary>  
		public string TrackIcon { get; set; } = string.Empty;
		/// <summary>  
		/// Gets or sets the angle of the track icon.  
		/// </summary>  
		public float TrackIconAngle { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether the track has a red swirl.  
		/// </summary>  
		public bool TrackRedSwirl { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether the track has a gray set speed icon.  
		/// </summary>  
		public bool TrackGraySetSpeedIcon { get; set; }
		/// <summary>  
		/// Gets or sets the BPM value for the track's set speed icon.  
		/// </summary>  
		public float TrackSetSpeedIconBpm { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether the track glow is enabled.  
		/// </summary>  
		public bool TrackGlowEnabled { get; set; }
		/// <summary>  
		/// Gets or sets the glow color of the track.  
		/// </summary>  
		public RDColor TrackGlowColor { get; set; }
		/// <summary>  
		/// Gets or sets the position of the object.  
		/// </summary>  
		public RDPointN Position { get; set; }
		/// <summary>  
		/// Gets or sets the relative position of the camera.  
		/// </summary>  
		public CameraRelativeTo RelativeTo { get; set; }
		/// <summary>  
		/// Gets or sets the pivot offset of the object.  
		/// </summary>  
		public RDSizeN PivotOffset { get; set; }
		/// <summary>  
		/// Gets or sets the rotation of the object.  
		/// </summary>  
		public float Rotation { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether the rotation is locked.  
		/// </summary>  
		public bool LockRotation { get; set; }
		/// <summary>  
		/// Gets or sets the scale of the object.  
		/// </summary>  
		public RDSizeN Scale { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether the scale is locked.  
		/// </summary>  
		public bool LockScale { get; set; }
		/// <summary>  
		/// Gets or sets the depth of the object.  
		/// </summary>  
		public int Depth { get; set; }
		/// <summary>  
		/// Gets or sets the parallax effect of the object.  
		/// </summary>  
		public RDSizeN Parallax { get; set; }
		/// <summary>  
		/// Gets or sets the parallax offset of the object.  
		/// </summary>  
		public RDSizeN ParallaxOffset { get; set; }
		/// <summary>  
		/// Gets or sets the tag associated with the object.  
		/// </summary>  
		public string Tag { get; set; } = string.Empty;
		/// <summary>  
		/// Represents the types of objects that can be added.  
		/// </summary>  
		public enum ObjectTypes
		{
			/// <summary>  
			/// Represents a floor object.  
			/// </summary>  
			Floor,
			/// <summary>  
			/// Represents a planet object.  
			/// </summary>  
			Planet
		}
		/// <summary>  
		/// Represents the types of planet colors.  
		/// </summary>  
		public enum PlanetColorTypes
		{
			/// <summary>  
			/// Default red color.  
			/// </summary>  
			DefaultRed,
			/// <summary>  
			/// Custom planet color type.  
			/// </summary>  
			planetColorType,
			/// <summary>  
			/// Gold color.  
			/// </summary>  
			Gold,
			/// <summary>  
			/// Overseer color.  
			/// </summary>  
			Overseer,
			/// <summary>  
			/// Custom color.  
			/// </summary>  
			Custom
		}
		/// <summary>  
		/// Represents the types of tracks.  
		/// </summary>  
		public enum TrackTypes
		{
			/// <summary>  
			/// Normal track type.  
			/// </summary>  
			Normal,
			/// <summary>  
			/// Midspin track type.  
			/// </summary>  
			Midspin
		}
	}
}
