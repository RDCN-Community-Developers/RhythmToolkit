using RhythmBase.RhythmDoctor.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Components;

/// <summary>
/// A Character.
/// </summary>
[JsonConverter(typeof(CharacterConverter))]
public readonly struct RDCharacter : IEquatable<RDCharacter>
{
	/// <summary>
	/// Whether  in-game character or customized character(sprite).
	/// </summary>
#if NET7_0_OR_GREATER
	[MemberNotNull(nameof(Character))]
#endif
	public bool IsCustom { get; }
	/// <summary>
	/// In-game character.
	/// <br />
	/// If using a customized character, this value will be empty
	/// </summary>
	public RDCharacters? Character { get; }
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
	public RDCharacter(RDCharacters character)
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
	internal IEnumerable<FileReference> GetAllPossibleFileReferences()
	{
		if (IsCustom && CustomCharacter is string cc)
		{
			if (Path.GetExtension(cc) == ".png")
				yield return cc;
			else
			{
				yield return cc + ".png";
				yield return cc + ".json";
				yield return cc + "_glow.png";
				yield return cc + "_outline.png";
				yield return cc + "_freeze.png";
			}
		}
	}
	/// <inheritdoc/>
	public static implicit operator RDCharacter(RDCharacters character) => new(character);
	/// <inheritdoc/>
	public static implicit operator RDCharacter(string character) => new(character);
	public static bool operator ==(RDCharacter left, RDCharacter right) => left.Equals(right);
	public static bool operator !=(RDCharacter left, RDCharacter right) => !(left == right);
	/// <inheritdoc/>
	public override readonly string ToString() => (IsCustom
		? CustomCharacter
		: Character?.ToString())
		?? "[Null]";

	public bool Equals(RDCharacter other)
	{
		return IsCustom == other.IsCustom
			&& Character == other.Character
			&& CustomCharacter == other.CustomCharacter;
	}
	public override int GetHashCode()
	{
#if NET7_0_OR_GREATER
		return HashCode.Combine(IsCustom, Character, CustomCharacter);
#else
		unchecked
		{
			int hash = 17;
			hash = hash * 23 + IsCustom.GetHashCode();
			hash = hash * 23 + Character.GetHashCode();
			hash = hash * 23 + (CustomCharacter != null ? CustomCharacter.GetHashCode() : 0);
			return hash;
		}
#endif
	}
}
