namespace RhythmBase.RhythmDoctor.Components.Conditions;

/// <summary>
/// Represents a condition that determines accessibility based on specific effects.
/// </summary>
public class AccessibilityCondition : BaseConditional
{
	///<inheritdoc/>
	public override ConditionType Type => ConditionType.Accessibility;

	/// <summary>
	/// Gets or sets the effect type whose accessibility should be evaluated.
	/// </summary>
	public EffectType TargetEffectType { get; set; }
	public override string ToString()
	{
		return $"effect: {TargetEffectType}";
	}
}