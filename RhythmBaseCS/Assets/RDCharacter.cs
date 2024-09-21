using RhythmBase.Components;
namespace RhythmBase.Assets
{
	/// <summary>
	/// A Character.
	/// </summary>
	public class RDCharacter
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
		public Asset<SpriteFile>? CustomCharacter { get; }
		/// <summary>
		/// Construct an in-game character.
		/// </summary>
		/// <param name="character">Character type.</param>
		public RDCharacter(RDLevel level, Characters character)
		{
			IsCustom = false;
			Character = new Characters?(character);
		}
		/// <summary>
		/// Construct a customized character.
		/// </summary>
		/// <param name="character">A sprite.</param>
		public RDCharacter(RDLevel level, string character)
		{
			IsCustom = true;
			CustomCharacter = new Asset<SpriteFile>(level.Manager)
			{
				Name = character
			};
		}

		public override string ToString() => IsCustom ? CustomCharacter?.Name ?? "[Null]" : Character.ToString();
	}
}
