using System;
using RhythmBase.Components;
namespace RhythmBase.Adofai.Events
{
	public class ADAddText : ADBaseEvent
	{
		public ADAddText()
		{
			Type = ADEventType.AddText;
		}

		public override ADEventType Type { get; }

		public string DecText { get; set; }

		public string Font { get; set; }

		public RDPointN Position { get; set; }

		public ADCameraRelativeTo RelativeTo { get; set; }

		public RDSizeN PivotOffset { get; set; }

		public float Rotation { get; set; }

		public bool LockRotation { get; set; }

		public RDSizeN Scale { get; set; }

		public bool LockScale { get; set; }

		public RDColor Color { get; set; }

		public float Opacity { get; set; }

		public int Depth { get; set; }

		public RDSizeN Parallax { get; set; }

		public RDSizeN ParallaxOffset { get; set; }

		public string Tag { get; set; }
	}
}
