
namespace RhythmBase.Components;

/// <summary>
/// A Character.
/// </summary>
public readonly struct RDCharacter
{
	/// <summary>
	/// Whether  in-game character or customized character(sprite).
	/// </summary>
	public bool IsCustom { get; }
	/// <summary>
	/// In-game character.
	/// <br />
	/// If using a customized character, this value will be empty
	/// </summary>
	public Characters? Character { get; }
	/// <summary>
	/// Customized character(sprite).
	/// <br />
	/// If using an in-game character, this value will be empty
	/// </summary>
	public string? CustomCharacter { get; }
	/// <summary>
	/// Construct an in-game character.
	/// </summary>
	/// <param name="character">Character type.</param>
	public RDCharacter(Characters character)
	{
		IsCustom = false;
		Character = character;
	}
	/// <summary>
	/// Construct a customized character.
	/// </summary>
	/// <param name="character">A sprite.</param>
	public RDCharacter(string character)
	{
		IsCustom = true;
		CustomCharacter = character;
	}
	/// <inheritdoc/>
	public static implicit operator RDCharacter(Characters character) => new(character);
	/// <inheritdoc/>
	public static implicit operator RDCharacter(string character) => new(character);
	/// <inheritdoc/>
	public override readonly string ToString() => (IsCustom
		? (CustomCharacter)
		: Character?.ToString())
		?? "[Null]";
}
