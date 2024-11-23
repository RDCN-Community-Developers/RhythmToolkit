using RhythmBase.Components;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an action to set the counting sound in the rhythm base.
	/// </summary>
	public class SetCountingSound : BaseRowAction
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetCountingSound"/> class.
		/// </summary>
		public SetCountingSound()
		{
			Volume = 100;
			Sounds = new RDAudio[7];
			Type = EventType.SetCountingSound;
			Tab = Tabs.Sounds;
		}

		/// <summary>
		/// Gets or sets the voice source for the counting sound.
		/// </summary>
		public VoiceSources VoiceSource { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="SetCountingSound"/> is enabled.
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// Gets or sets the subdivision offset for the counting sound.
		/// </summary>
		public float SubdivOffset { get; set; }

		/// <summary>
		/// Gets or sets the volume of the counting sound.
		/// </summary>
		public int Volume { get; set; }

		/// <summary>
		/// Gets or sets the list of sounds for the counting sound.
		/// </summary>
		public RDAudio[] Sounds { get; set; }

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Represents the different voice sources for the counting sound.
		/// </summary>
		public enum VoiceSources
		{
			/// <summary>
			/// Jyi Count
			/// </summary>
			JyiCount,
			/// <summary>
			/// Jyi Count Fast
			/// </summary>
			JyiCountFast,
			/// <summary>
			/// Jyi Count Calm
			/// </summary>
			JyiCountCalm,
			/// <summary>
			/// Jyi Count Tired
			/// </summary>
			JyiCountTired,
			/// <summary>
			/// Jyi Count Very Tired
			/// </summary>
			JyiCountVeryTired,
			/// <summary>
			/// Jyi Count Japanese
			/// </summary>
			JyiCountJapanese,
			/// <summary>
			/// Ian Count
			/// </summary>
			IanCount,
			/// <summary>
			/// Ian Count Fast
			/// </summary>
			IanCountFast,
			/// <summary>
			/// Ian Count Calm
			/// </summary>
			IanCountCalm,
			/// <summary>
			/// Ian Count Slow
			/// </summary>
			IanCountSlow,
			/// <summary>
			/// Ian Count Slower
			/// </summary>
			IanCountSlower,
			/// <summary>
			/// Whistle Count
			/// </summary>
			WhistleCount,
			/// <summary>
			/// Bird Count
			/// </summary>
			BirdCount,
			/// <summary>
			/// Parrot Count
			/// </summary>
			ParrotCount,
			/// <summary>
			/// Owl Count
			/// </summary>
			OwlCount,
			/// <summary>
			/// Oriole Count
			/// </summary>
			OrioleCount,
			/// <summary>
			/// Wren Count
			/// </summary>
			WrenCount,
			/// <summary>
			/// Canary Count
			/// </summary>
			CanaryCount,
			/// <summary>
			/// Jyi Count Legacy
			/// </summary>
			JyiCountLegacy,
			/// <summary>
			/// Jyi Count English
			/// </summary>
			JyiCountEnglish,
			/// <summary>
			/// Ian Count English
			/// </summary>
			IanCountEnglish,
			/// <summary>
			/// Ian Count English Calm
			/// </summary>
			IanCountEnglishCalm,
			/// <summary>
			/// Ian Count English Slow
			/// </summary>
			IanCountEnglishSlow,
			/// <summary>
			/// Custom
			/// </summary>
			Custom
		}
	}
}
