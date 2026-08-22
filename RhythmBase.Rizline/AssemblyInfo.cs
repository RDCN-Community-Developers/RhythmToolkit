using RhythmBase.Rizline.Events;
using RhythmBase.Rizline;
[assembly: RhythmBase.JsonConverterId(nameof(RhythmBase.Rizline))]
[assembly: RhythmBase.JsonConverterSourceType<IBaseEvent, EventType>(typeof(RhythmBase.Rizline.Serialization.MemberConverter<>), nameof(IBaseEvent.Type))]
[assembly: RhythmBase.JsonConverterLink<Color, ColorConverter.ArgbObject>]
[assembly: RhythmBase.AdapterType<
	RhythmBase.Rizline.Components.Chart,
	RhythmBase.Rizline.Components.Level,
	RhythmBase.Rizline.Components.BeatCalculator,
	RhythmBase.Rizline.Components.TickTime,
	RhythmBase.Rizline.EventType,
	RhythmBase.Rizline.Events.IBaseEvent
	>]