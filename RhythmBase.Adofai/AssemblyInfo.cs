using RhythmBase.Adofai;
using RhythmBase.Adofai.Components.Filters;
using RhythmBase.Adofai.Events;
using RhythmBase.Adofai.Serialization;

[assembly: RhythmBase.JsonConverterId(nameof(RhythmBase.Adofai))]
[assembly: RhythmBase.JsonConverterSourceType<IBaseEvent, EventType>(typeof(RhythmBase.Adofai.Serialization.MemberConverter<>), nameof(IBaseEvent.Type))]
[assembly: RhythmBase.JsonConverterSourceType<IFilter, AdvancedFilter>(typeof(FilterMemberConverter<>), nameof(IFilter.Type))]
[assembly: RhythmBase.JsonConverterLink<Color, ColorConverter.RgbaHex>]
[assembly: RhythmBase.AdapterType<
	RhythmBase.Adofai.Components.Level,
	RhythmBase.Adofai.Components.Level,
	RhythmBase.Adofai.Utils.BeatCalculator,
	RhythmBase.Adofai.Components.TickTime,
	RhythmBase.Adofai.EventType,
	RhythmBase.Adofai.Events.IBaseEvent
	>]