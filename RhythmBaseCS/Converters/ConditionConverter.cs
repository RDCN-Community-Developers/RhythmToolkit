using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using System.Collections;
using System.Text.RegularExpressions;
namespace RhythmBase.Converters
{
	internal partial class ConditionConverter(List<BaseConditional> Conditionals) : JsonConverter<Condition>
	{
		public override void WriteJson(JsonWriter writer, Condition? value, JsonSerializer serializer) => writer.WriteValue(value!.Serialize());
		public override Condition ReadJson(JsonReader reader, Type objectType, Condition? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string J = JToken.Load(reader).ToObject<string>()!;
			Condition Value = new();
			MatchCollection ConditionIds = ConditionsRegex().Matches(J);
			foreach(Match match in ConditionIds) 
			{
				double vs = Conversion.Val("~2");
				BaseConditional Parent = (from i in conditionals
										  where i.Id == Conversions.ToInteger(ConditionIndexRegex().Match(match.Value).Value)
										  select i).First();
				Value.ConditionLists.Add(new ValueTuple<bool, BaseConditional>(match.Value[0] != '~', Parent));
			}
			Value.Duration = Conversions.ToSingle(ConditionDurationRegex().Match(J).Value);
			return Value;
		}
		private readonly List<BaseConditional> conditionals = Conditionals;
		[GeneratedRegex("~?\\d+(?=[&d])")]
		private static partial Regex ConditionsRegex();
		[GeneratedRegex("\\d+")]
		private static partial Regex ConditionIndexRegex();
		[GeneratedRegex("(?<=d)[\\d\\.]+")]
		private static partial Regex ConditionDurationRegex();
	}
}
