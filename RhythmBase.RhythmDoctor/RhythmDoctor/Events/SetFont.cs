using RhythmBase.RhythmDoctor.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events
{
	public record class SetFont : BaseDecorationAction, IFontFileEvent
	{
		public override EventType Type => EventType.SetFont;
		public FontName Font { get; set; } = FontName.Default;
		[JsonCondition($"$&.{nameof(Font)}.IsCustom")]
		public bool Bold { get; set; }
		[JsonCondition($"$&.{nameof(Font)}.IsCustom")]
		public bool Italic { get; set; }
		[JsonCondition($"$&.{nameof(Font)}.IsCustom")]
		public bool Underline { get; set; }
		[JsonCondition($"$&.{nameof(Font)}.IsCustom")]
		public float CharacterSpacing { get; set; } = 0f;
		[JsonCondition($"$&.{nameof(Font)}.IsCustom")]
		public float OutlineWidth { get; set; } = 0f;
		public float WrappingWidth { get; set; } = 100f;
		IEnumerable<FileReference> IFontFileEvent.FontFiles => Font.IsCustom ? [Font.Value] : [];
		IEnumerable<FileReference> IFileEvent.Files => Font.IsCustom ? [Font.Value] : [];
	}
}
