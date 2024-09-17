using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace RhythmBase.Components
{
	/// <summary>
	/// Condition.
	/// </summary>
	public abstract class BaseConditional
	{
		/// <summary>
		/// Type of this condition
		/// </summary>
		public abstract ConditionType Type { get; }
		/// <summary>
		/// Condition tag. Its role has not been clarified.
		/// </summary>
		public string Tag { get; set; }
		/// <summary>
		/// Condition name.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 1-based serial number.
		/// </summary>
		public int Id
		{
			get
			{
				return checked(ParentCollection.IndexOf(this) + 1);
			}
		}

		public override string ToString() => Name;

		[JsonIgnore]
		internal List<BaseConditional> ParentCollection;
		/// <summary>
		/// Type of condition
		/// </summary>
		public enum ConditionType
		{
			LastHit,
			Custom,
			TimesExecuted,
			Language,
			PlayerMode
		}
	}
}
