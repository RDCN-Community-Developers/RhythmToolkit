using System;
namespace RhythmBase.Adofai.Events
{
	public class ADEditorComment : ADBaseTileEvent
	{
		public ADEditorComment()
		{
			Type = ADEventType.EditorComment;
		}

		public override ADEventType Type { get; }

		public string Comment { get; set; }
	}
}
