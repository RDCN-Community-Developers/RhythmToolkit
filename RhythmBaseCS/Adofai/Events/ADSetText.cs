using System;

namespace RhythmBase.Adofai.Events
{

	public class ADSetText : ADBaseTaggedTileAction
	{

		public ADSetText()
		{
			Type = ADEventType.SetText;
		}


		public override ADEventType Type { get; }


		public string DecText { get; set; }


		public string Tag { get; set; }
	}
}
