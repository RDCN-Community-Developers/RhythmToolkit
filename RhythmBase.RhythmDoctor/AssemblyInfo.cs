using RhythmBase.Global.Serialization;
using RhythmBase.RhythmDoctor.Events;
using System.Runtime.CompilerServices;

[assembly: RhythmBase.JsonConverterId(nameof(RhythmBase.RhythmDoctor))]
[assembly: RhythmBase.JsonConverterSourceType<IBaseEvent, RhythmBase.RhythmDoctor.EventType>(typeof(RhythmBase.RhythmDoctor.Serialization.MemberConverter<>), nameof(IBaseEvent.Type))]
[assembly: RhythmBase.JsonConverterLink<Color, ColorConverter.RgbaHex>]
[assembly: RhythmBase.AdapterType<
	RhythmBase.RhythmDoctor.Components.Chart,
	RhythmBase.RhythmDoctor.Components.Level,
	RhythmBase.RhythmDoctor.Utils.BeatCalculator,
	RhythmBase.RhythmDoctor.Components.TickTime,
	RhythmBase.RhythmDoctor.EventType,
	RhythmBase.RhythmDoctor.Events.IBaseEvent
>]
[assembly: InternalsVisibleTo("FastTest")]