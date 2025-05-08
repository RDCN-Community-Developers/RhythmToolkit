using System;
using Newtonsoft.Json;
using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.Adofai.Events
{
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class ADMoveCamera : ADBaseTaggedTileAction, IEaseEvent
	{
		public ADMoveCamera()
		{
			Type = ADEventType.MoveCamera;
		}

		public override ADEventType Type { get; }

		public float Duration { get; set; }

		public EaseType Ease { get; set; }

		public bool DontDisable { get; set; }

		public bool MinVfxOnly { get; set; }

		public ADCameraRelativeTo RelativeTo { get; set; }

		public RDPoint? Position { get; set; }

		public float Rotation { get; set; }

		public float Zoom { get; set; }
	}
}
