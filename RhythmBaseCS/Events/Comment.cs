using System;
using Newtonsoft.Json;
using RhythmBase.Components;
using SkiaSharp;
namespace RhythmBase.Events
{
	public class Comment : BaseDecorationAction
	{
		public Comment()
		{
			Text = "";
			Color = new PaletteColor(false)
			{
				Color = new SKColor?(new SKColor(242, 230, 68))
			};
			Type = EventType.Comment;
		}

		[JsonProperty("tab")]
		public Tabs CustomTab { get; set; }

		[JsonIgnore]
		public override Tabs Tab
		{
			get
			{
				return CustomTab;
			}
		}

		public bool Show { get; set; }

		public string Text { get; set; }

		public override string Target
		{
			get
			{
				return base.Target;
			}
		}

		public PaletteColor Color { get; set; }

		public override EventType Type { get; }

		public bool ShouldSerializeTarget() => Tab == Tabs.Decorations;

		public override string ToString() => base.ToString() + string.Format(" {0}", Text);
	}
}
