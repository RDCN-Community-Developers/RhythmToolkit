using System;
using System.Collections.Generic;
using System.Text;
using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Components.Conditions;

public class AccessibilityCondition : BaseConditional
{
	[RDJsonEnumSerializable]
	public enum EffectType
	{
		Flashy,
		Narration,
	}
	public override ConditionType Type { get; }
	public EffectType TargetEffectType { get; set; }
}