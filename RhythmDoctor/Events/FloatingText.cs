using RhythmBase.Converters;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Specifies the category of the narration.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum NarrationCategorys
	{
		/// <summary>  
		/// Fallback category, used as a default when no other category applies.  
		/// </summary>  
		Fallback,
		/// <summary>  
		/// Navigation category, used for guiding the user through the interface or level.  
		/// </summary>  
		Navigation,
		/// <summary>  
		/// Instruction category, used for providing instructions or tutorials.  
		/// </summary>  
		Instruction,
		/// <summary>  
		/// Notification category, used for alerts or notifications.  
		/// </summary>  
		Notification,
		/// <summary>  
		/// Dialogue category, used for character or story dialogues.  
		/// </summary>  
		Dialogue,
		/// <summary>  
		/// Description category, used for descriptive text or explanations.  
		/// </summary>  
		Description,
		/// <summary>  
		/// Subtitles category, used for displaying subtitles.  
		/// </summary>  
		Subtitles,
	}
	/// <summary>
	/// Represents a floating text event in a room.
	/// </summary>
	//[RDJsonObjectNotSerializable]
	public class FloatingText : BaseEvent, IRoomEvent, IDurationEvent, IColorEvent
	{
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type => EventType.FloatingText;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab => Tabs.Actions;
		/// <summary>
		/// Gets the list of child advance texts.
		/// </summary>
		[RDJsonIgnore]
		public List<AdvanceText> Children => _children;
		/// <summary>
		/// Gets or sets the room associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <summary>
		/// Gets or sets the fade out rate of the text.
		/// </summary>
		public float FadeOutRate { get; set; } = 3;
		float IDurationEvent.Duration { get => FadeOutRate; set => FadeOutRate = value; }
		/// <summary>
		/// Gets or sets the color of the text.
		/// </summary>
		public PaletteColor Color { get; set; } = RDColor.White;
		/// <summary>
		/// Gets or sets the angle of the text.
		/// </summary>
		public float Angle { get; set; } = 0;
		/// <summary>
		/// Gets or sets the size of the text.
		/// </summary>
		public uint Size { get; set; } = 8;
		/// <summary>
		/// Gets or sets the outline color of the text.
		/// </summary>
		public PaletteColor OutlineColor { get; set; } =  RDColor.Black;
		/// <summary>
		/// Gets the ID of the event.
		/// </summary>
		[RDJsonNotIgnore]
		internal int Id => _beat.BaseLevel?._floatingTexts.IndexOf(this) ?? -1;
		/// <summary>
		/// Gets or sets the position of the text.
		/// </summary>
		public RDPoint TextPosition { get; set; } = new(50f, 50f);
		/// <summary>
		/// Gets or sets the anchor style of the text.
		/// </summary>
		[RDJsonConverter(typeof(FloatingTextAnchorStylesConverter))]
		public FloatingTextAnchorStyles Anchor { get; set; } = FloatingTextAnchorStyles.Center;
		/// <summary>
		/// Gets or sets a value indicating whether to narrate the text.
		/// </summary>
		public bool Narrate { get; set; } = true;
		/// <summary>
		/// Gets or sets the narration category of the text.
		/// </summary>
		public NarrationCategorys NarrationCategory { get; set; } = NarrationCategorys.Subtitles;
		/// <summary>
		/// Gets or sets the mode of the text.
		/// </summary>
		public FloatingTextFadeOutModes Mode { get; set; } = FloatingTextFadeOutModes.FadeOut;
		/// <summary>
		/// Gets or sets a value indicating whether to show child texts.
		/// </summary>
		public bool ShowChildren { get; set; } = true;
		/// <summary>
		/// Gets or sets the text content.
		/// </summary>
		public string Text { get; set; } = "等/呀/等/得/好/心/慌……";
		/// <summary>
		/// Initializes a new instance of the <see cref="FloatingText"/> class.
		/// </summary>
		public FloatingText() { }
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + $" {Text}";
		private readonly List<AdvanceText> _children = [];
	}
	/// <summary>
	/// Specifies the mode of the text.
	/// </summary>
	[Flags]
	[RDJsonEnumSerializable]
	public enum FloatingTextFadeOutModes
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
	/// <summary>
	/// Specifies the anchor style of the text.
	/// </summary>
	[Flags]
	public enum FloatingTextAnchorStyles
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
}
