using RhythmBase.BeatBlock;
using RhythmBase.BeatBlock.Events;

[assembly: RhythmBase.JsonConverterId(nameof(RhythmBase.BeatBlock))]
[assembly: RhythmBase.JsonConverterSourceType<IBaseEvent, EventType>(typeof(RhythmBase.BeatBlock.Serialization.MemberConverter<>), nameof(IBaseEvent.Type))]
[assembly: RhythmBase.JsonConverterLink<Color, ColorConverter.RgbObject>]
[assembly: RhythmBase.AdapterType<
	RhythmBase.BeatBlock.Components.Chart,
	RhythmBase.BeatBlock.Components.Level,
	RhythmBase.BeatBlock.Components.BeatCalculator,
	RhythmBase.BeatBlock.Components.TickTime,
	RhythmBase.BeatBlock.EventType,
	RhythmBase.BeatBlock.Events.IBaseEvent
	>]
[assembly: RhythmBase.JsonEnumCasting(typeof(RhythmBase.Global.Components.Easing.EaseType), false)]