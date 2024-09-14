using System;

namespace RhythmBase.Adofai.Events
{

	public class ADHide : ADBaseTileEvent
	{

		public ADHide()
		{
			Type = ADEventType.Hide;
		}


		public override ADEventType Type { get; }


		public bool HideJudgment { get; set; }


		public bool HideTileIcon { get; set; }
	}
}
