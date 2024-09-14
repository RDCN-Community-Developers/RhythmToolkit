using System;
using RhythmBase.Components;
using SkiaSharp;

namespace RhythmBase.Adofai.Events
{

	public class ADAddObject : ADBaseEvent
	{

		public ADAddObject()
		{
			Type = ADEventType.AddObject;
		}


		public override ADEventType Type { get; }


		public ObjectTypes ObjectType { get; set; }


		public PlanetColorTypes PlanetColorType { get; set; }


		public SKColor PlanetColor { get; set; }


		public SKColor PlanetTailColor { get; set; }


		public TrackTypes TrackType { get; set; }


		public float TrackAngle { get; set; }


		public ADTrackColorTypes TrackColorType { get; set; }


		public SKColor TrackColor { get; set; }


		public SKColor SecondaryTrackColor { get; set; }


		public float TrackColorAnimDuration { get; set; }


		public float TrackOpacity { get; set; }


		public ADTrackStyles TrackStyle { get; set; }


		public string TrackIcon { get; set; }


		public float TrackIconAngle { get; set; }


		public bool TrackRedSwirl { get; set; }


		public bool TrackGraySetSpeedIcon { get; set; }


		public float TrackSetSpeedIconBpm { get; set; }


		public bool TrackGlowEnabled { get; set; }


		public SKColor TrackGlowColor { get; set; }


		public RDPointN Position { get; set; }


		public ADCameraRelativeTo RelativeTo { get; set; }


		public RDSizeN PivotOffset { get; set; }


		public float Rotation { get; set; }


		public bool LockRotation { get; set; }


		public RDSizeN Scale { get; set; }


		public bool LockScale { get; set; }


		public int Depth { get; set; }


		public RDSizeN Parallax { get; set; }


		public RDSizeN ParallaxOffset { get; set; }


		public string Tag { get; set; }


		public enum ObjectTypes
		{

			Floor,

			Planet
		}


		public enum PlanetColorTypes
		{

			DefaultRed,

			planetColorType,

			Gold,

			Overseer,

			Custom
		}


		public enum TrackTypes
		{

			Normal,

			Midspin
		}
	}
}
