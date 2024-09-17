using System;
using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Events
{
	[JsonConverter(typeof(PatternConverter))]
	public enum Patterns
	{
		None,
		X,
		Up,
		Down,
		Banana,
		Return
	}
}
