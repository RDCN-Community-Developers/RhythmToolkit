using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Converters;

namespace RhythmBase.Events
{
	/// <summary>
	/// Represents a floating text event in a room.
	/// </summary>
	public class FloatingText : BaseEvent, IRoomEvent,IDurationEvent
	{
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.FloatingText;

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Actions;

		/// <summary>
		/// Gets the list of child advance texts.
		/// </summary>
		[JsonIgnore]
		public List<AdvanceText> Children => _children;

		/// <summary>
		/// Gets or sets the room associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; } = new RDRoom(true, [0]);

		/// <summary>
		/// Gets or sets the fade out rate of the text.
		/// </summary>
		public float FadeOutRate { get; set; }
		float IDurationEvent.Duration => FadeOutRate;

		/// <summary>
		/// Gets or sets the color of the text.
		/// </summary>
		public PaletteColor Color { get; set; } = new PaletteColor(true)
		{
			Color = RDColor.White,
		};

		/// <summary>
		/// Gets or sets the angle of the text.
		/// </summary>
		public float Angle { get; set; }

		/// <summary>
		/// Gets or sets the size of the text.
		/// </summary>
		public uint Size { get; set; }

		/// <summary>
		/// Gets or sets the outline color of the text.
		/// </summary>
		public PaletteColor OutlineColor { get; set; } = new PaletteColor(true)
		{
			Color = RDColor.Black,
		};

		/// <summary>
		/// Gets the ID of the event.
		/// </summary>
		[JsonProperty]
		internal int Id => (int)GeneratedId;

		/// <summary>
		/// Gets or sets the position of the text.
		/// </summary>
		public RDPoint TextPosition { get; set; } = new RDPoint(new float?(50f), new float?(50f));

		/// <summary>
		/// Gets or sets the anchor style of the text.
		/// </summary>
		public AnchorStyle Anchor { get; set; }

		/// <summary>
		/// Specifies the anchor style of the text.
		/// </summary>
		[JsonConverter(typeof(AnchorStyleConverter))]
		[Flags]
		public enum AnchorStyle
		{
			/// <summary>
			/// The lower anchor style.
			/// </summary>
			Lower = 1,

			/// <summary>
			/// The upper anchor style.
			/// </summary>
			Upper = 2,

			/// <summary>
			/// The left anchor style.
			/// </summary>
			Left = 4,

			/// <summary>
			/// The right anchor style.
			/// </summary>
			Right = 8,

			/// <summary>
			/// The center anchor style.
			/// </summary>
			Center = 0
		}

		/// <summary>
		/// Gets or sets the mode of the text.
		/// </summary>
		public OutMode Mode { get; set; } = OutMode.FadeOut;

		/// <summary>
		/// Gets or sets a value indicating whether to show child texts.
		/// </summary>
		public bool ShowChildren { get; set; } = false;

		/// <summary>
		/// Gets or sets the text content.
		/// </summary>
		public string Text { get; set; } = "等呀等得好心慌……";

		/// <summary>
		/// Initializes a new instance of the <see cref="FloatingText"/> class.
		/// </summary>
		public FloatingText()
		{
			GeneratedId = _PrivateId;
			_PrivateId = checked((uint)(unchecked((ulong)_PrivateId) + 1UL));
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + $" {Text}";

		private static uint _PrivateId = 0U;

		private readonly uint GeneratedId;

		private readonly List<AdvanceText> _children = [];

		/// <summary>
		/// Specifies the mode of the text.
		/// </summary>
		[Flags]
		public enum OutMode
		{
			/// <summary>
			/// The text will fade out gradually.
			/// </summary>
			FadeOut = 0,

			/// <summary>
			/// The text will hide abruptly.
			/// </summary>
			HideAbruptly = 1
		}
	}
}
