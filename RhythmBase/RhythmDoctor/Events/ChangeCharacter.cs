using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>  
/// Represents an action to change the character in a row event.  
/// </summary>  
public record class ChangeCharacter : BaseRowAction
{
	/// <inheritdoc/>  
	public override EventType Type => EventType.ChangeCharacter;

	/// <inheritdoc/>  
	public override Tab Tab => Tab.Actions;

	/// <summary>  
	/// Gets or sets the character to be changed to.  
	/// </summary>  
	[RDJsonIgnore]
	public RDCharacter Character { get; set; } = RDCharacters.Samurai;
	[RDJsonNotIgnore]
	[RDJsonAlias("character")]
	internal RDCharacters EnumCharacter
	{
		get => Character.Character ?? RDCharacters.Custom; set => Character = value;
	}

	[RDJsonNotIgnore]
	[RDJsonCondition($"$&.{nameof(Character)}.{nameof(RDCharacter.IsCustom)} && !string.IsNullOrEmpty($&.{nameof(StringCharacter)})")]
	[RDJsonAlias("customCharacter")]
	internal string StringCharacter { get => Character.IsCustom ? Character.CustomCharacter ?? string.Empty : string.Empty; set => Character = value; }
	/// <summary>  
	/// Gets or sets the transition type for the character change.  
	/// </summary>  
	public Transition Transition { get; set; } = Transition.Instant;
}
