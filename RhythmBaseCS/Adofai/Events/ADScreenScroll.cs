using System;
using RhythmBase.Global.Components;
namespace RhythmBase.Adofai.Events
{
	public class ADScreenScroll : ADBaseTaggedTileAction
	{
		public ADScreenScroll()
		{
			Type = ADEventType.ScreenScroll;
		}

		public override ADEventType Type { get; }

		public RDSizeN Scroll { get; set; }
	}
}
