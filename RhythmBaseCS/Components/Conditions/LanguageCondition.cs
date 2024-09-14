using System;

namespace RhythmBase.Components.Conditions
{
	/// <summary>
	/// Game Language.
	/// </summary>

	public class LanguageCondition : BaseConditional
	{
		public LanguageCondition()
		{
			Type = ConditionType.Language;
		}
		/// <summary>
		/// Game Language.
		/// </summary>
		public Languages Language { get; set; }
		public override ConditionType Type { get; }
		/// <summary>
		/// Game Language.
		/// </summary>
		public enum Languages
		{
			English,
			Spanish,
			Portuguese,
			ChineseSimplified,
			ChineseTraditional,
			Korean,
			Polish,
			Japanese,
			German
		}
	}
}
