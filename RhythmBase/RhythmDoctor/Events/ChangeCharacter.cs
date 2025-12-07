using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents an action to change the character in a row event.  
	/// </summary>  
	public class ChangeCharacter : BaseRowAction
	{
		/// <inheritdoc/>  
		public override EventType Type => EventType.ChangeCharacter;

		/// <inheritdoc/>  
		public override Tabs Tab => Tabs.Actions;

		/// <summary>  
		/// Gets or sets the character to be changed to.  
		/// </summary>  
		[RDJsonIgnore]
		public RDCharacter Character { get; set; } = RDCharacters.Samurai;
		[RDJsonNotIgnore]
		internal RDCharacters EnumCharacter => Character.Character ?? RDCharacters.Custom;
		[RDJsonNotIgnore]
		[RDJsonCondition($"$&.{nameof(Character)}.{nameof(RDCharacter.IsCustom)}")]
		internal string StringCharacter => Character.CustomCharacter ?? string.Empty;
		/// <summary>  
		/// Gets or sets the transition type for the character change.  
		/// </summary>  
		public Transitions Transition { get; set; } = Transitions.Instant;
	}
}
