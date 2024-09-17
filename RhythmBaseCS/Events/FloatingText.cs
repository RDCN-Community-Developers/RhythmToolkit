using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Converters;
using SkiaSharp;
namespace RhythmBase.Events
{
	public class FloatingText : BaseEvent, IRoomEvent
	{
		public override EventType Type { get; }

		public override Tabs Tab { get; }

		[JsonIgnore]
		public List<AdvanceText> Children
		{
			get
			{
				return _children;
			}
		}

		public Room Rooms { get; set; }

		public float FadeOutRate { get; set; }

		public PaletteColor Color { get; set; }

		public float Angle { get; set; }

		public uint Size { get; set; }

		public PaletteColor OutlineColor { get; set; }

		[JsonProperty]
		internal int Id
		{
			get
			{
				return checked((int)GeneratedId);
			}
		}

		public RDPoint TextPosition { get; set; }

		public AnchorStyle Anchor { get; set; }

		public OutMode Mode
		{
			get
			{
				return _mode;
			}
			set
			{
				_mode = value;
			}
		}

		public bool ShowChildren { get; set; }

		public string Text { get; set; }

		public FloatingText()
		{
			_children = [];
			_mode = OutMode.FadeOut;
			Type = EventType.FloatingText;
			Tab = Tabs.Actions;
			Rooms = new Room(true, new byte[1]);
			Color = new PaletteColor(true)
			{
				Color = new SKColor?(new SKColor(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue))
			};
			OutlineColor = new PaletteColor(true)
			{
				Color = new SKColor?(new SKColor(0, 0, 0, byte.MaxValue))
			};
			TextPosition = new RDPoint(new float?(50f), new float?(50f));
			ShowChildren = false;
			Text = "等呀等得好心慌……";
			GeneratedId = _PrivateId;
			_PrivateId = checked((uint)(unchecked((ulong)_PrivateId) + 1UL));
		}

		public override string ToString() => base.ToString() + string.Format(" {0}", this.Text);

		private static uint _PrivateId = 0U;

		private readonly uint GeneratedId;

		private readonly List<AdvanceText> _children;

		private OutMode _mode;

		[Flags]
		public enum OutMode
		{
			FadeOut = 0,
			HideAbruptly = 1
		}

		[JsonConverter(typeof(AnchorStyleConverter))]
		[Flags]
		public enum AnchorStyle
		{
			Lower = 1,
			Upper = 2,
			Left = 4,
			Right = 8,
			Center = 0
		}
	}
}
