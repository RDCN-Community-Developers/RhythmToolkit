using RhythmBase.Adofai.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.Adofai.Converters
{
	internal class EventInstantConverterBaseTileEvent<TEvent> : EventInstantConverterBaseEvent<TEvent> where TEvent : IBaseEvent, new()
	{
	}
	internal class EventInstantConverterBaseTaggedTileAction<TEvent> : EventInstantConverterBaseEvent<TEvent> where TEvent : IBaseEvent, new()
	{
	}
}
