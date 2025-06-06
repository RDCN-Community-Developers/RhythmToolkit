﻿using Newtonsoft.Json;
using RhythmBase.Global.Components.RichText;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to show a dialogue in the game.
	/// </summary>
	public class ShowDialogue : BaseEvent
	{
		private RDDialogueExchange dialogueList = [];
		private string text = "";
		/// <summary>
		/// Initializes a new instance of the <see cref="ShowDialogue"/> class.
		/// </summary>
		public ShowDialogue()
		{
			Speed = 1;
			Type = EventType.ShowDialogue;
			Tab = Tabs.Actions;
		}
		/// <summary>
		/// Gets or sets the text of the dialogue.
		/// </summary>
		public string Text
		{
			get => text; set
			{
				text = value;
				dialogueList = RDDialogueExchange.Deserialize(value);
			}
		}
		/// <summary>
		/// Gets or sets the dialogue list. When set, the Text property is updated with the serialized value of the dialogue list.
		/// </summary>
		/// <value>The dialogue list.</value>
		[JsonIgnore]
		public RDDialogueExchange DialogueList
		{
			get => dialogueList; set
			{
				dialogueList = value;
				text = dialogueList.Serialize();
			}
		}
		/// <summary>
		/// Gets or sets the side of the panel where the dialogue will be shown.
		/// </summary>
		public DialogueSides PanelSide { get; set; }
		/// <summary>
		/// Gets or sets the side of the portrait in the dialogue.
		/// </summary>
		public DialoguePortraitSides PortraitSide { get; set; }
		/// <summary>
		/// Gets or sets the speed of the dialogue display.
		/// </summary>
		public int Speed { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether text sounds should be played.
		/// </summary>
		public bool PlayTextSounds { get; set; }
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }
		/// <summary>
		/// Gets the tab where the event is categorized.
		/// </summary>
		public override Tabs Tab { get; }
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}", Text);
	}
	/// <summary>
	/// Specifies the sides where the dialogue panel can be shown.
	/// </summary>
	public enum DialogueSides
	{
		/// <summary>
		/// The bottom side.
		/// </summary>
		Bottom,
		/// <summary>
		/// The top side.
		/// </summary>
		Top
	}
	/// <summary>
	/// Specifies the sides where the portrait can be shown.
	/// </summary>
	public enum DialoguePortraitSides
	{
		/// <summary>
		/// The left side.
		/// </summary>
		Left,
		/// <summary>
		/// The right side.
		/// </summary>
		Right
	}
}
