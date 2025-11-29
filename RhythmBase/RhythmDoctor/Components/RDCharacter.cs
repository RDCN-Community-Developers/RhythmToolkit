using RhythmBase.RhythmDoctor.Converters;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Components;

/// <summary>
/// A Character.
/// </summary>
[JsonConverter(typeof(CharacterConverter))]
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
	internal IEnumerable<FileReference> GetAllFileReferences()
	{
		if (IsCustom && CustomCharacter is string cc)
		{
			if (Path.GetExtension(cc) == ".png")
				yield return cc;
			else
			{
				yield return cc + ".png";
				yield return cc + ".json";
				string glowPath = cc + "_glow.png";
				string outlinePath = cc + "_outline.png";
				string freezePath = cc + "_freeze.png";
				if (File.Exists(glowPath))
					yield return glowPath;
				if (File.Exists(outlinePath))
					yield return outlinePath;
				if (File.Exists(freezePath))
					yield return freezePath;
			}
		}
	}
	/// <inheritdoc/>
	public static implicit operator RDCharacter(RDCharacters character) => new(character);
	/// <inheritdoc/>
	public static implicit operator RDCharacter(string character) => new(character);
	/// <inheritdoc/>
	public override readonly string ToString() => (IsCustom
		? CustomCharacter
		: Character?.ToString())
		?? "[Null]";
}
