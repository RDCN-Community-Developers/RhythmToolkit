using System;
using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Events
{
	[JsonConverter(typeof(TabsConverter))]
	public enum Tabs
	{
		Sounds,
		Rows,
		Actions,
		Decorations,
		Rooms,
		Unknown
	}
}
