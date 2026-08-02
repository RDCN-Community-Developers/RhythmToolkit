namespace RhythmBase.RhythmDoctor.Components.Conditions;

/// <summary>
/// Represents a condition based on the game language.
/// </summary>
public class LanguageCondition : BaseConditional
{
	/// <summary>
	/// Gets or sets the game language.
	/// </summary>
	public Language TargetLanguage { get; set; }
	///<inheritdoc/>
	public override ConditionType Type => ConditionType.Language;
	public override string ToString()
	{
		return $"lang: {TargetLanguage}";
	}
}
