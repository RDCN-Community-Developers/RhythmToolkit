using Newtonsoft.Json;
namespace RhythmBase.Components.Conditions
{
	/// <summary>
	/// Represents a condition based on the game language.
	/// </summary>
	public class LanguageCondition : BaseConditional
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LanguageCondition"/> class.
		/// </summary>
		public LanguageCondition()
		{
			Type = ConditionType.Language;
		}		/// <summary>
		/// Gets or sets the game language.
		/// </summary>
		[JsonProperty(nameof(Language))]
		public Languages Language
		{
			get;
			set;
		}		/// <summary>
		/// Gets the type of the condition.
		/// </summary>
		public override ConditionType Type { get; }		/// <summary>
		/// Represents the supported game languages.
		/// </summary>
		public enum Languages
		{
			/// <summary>
			/// English language.
			/// </summary>
			English,			/// <summary>
			/// Spanish language.
			/// </summary>
			Spanish,			/// <summary>
			/// Portuguese language.
			/// </summary>
			Portuguese,			/// <summary>
			/// Simplified Chinese language.
			/// </summary>
			ChineseSimplified,			/// <summary>
			/// Traditional Chinese language.
			/// </summary>
			ChineseTraditional,			/// <summary>
			/// Korean language.
			/// </summary>
			Korean,			/// <summary>
			/// Polish language.
			/// </summary>
			Polish,			/// <summary>
			/// Japanese language.
			/// </summary>
			Japanese,			/// <summary>
			/// German language.
			/// </summary>
			German
		}
	}
}
