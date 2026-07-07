using RhythmBase.Global.Serialization;
using RhythmBase.RhythmDoctor.Events;

[assembly: RhythmBase.JsonConverterId(nameof(RhythmBase.RhythmDoctor))]
[assembly: RhythmBase.JsonConverterSourceType(typeof(IBaseEvent), typeof(RhythmBase.RhythmDoctor.EventType), typeof(RhythmBase.RhythmDoctor.Serialization.MemberConverter<>), nameof(IBaseEvent.Type))]
[assembly: RhythmBase.JsonConverterLink(typeof(Color), typeof(ColorConverter.RgbaHex))]
[assembly: RhythmBase.AdapterType(
	typeof(RhythmBase.RhythmDoctor.Components.Level),
	typeof(RhythmBase.RhythmDoctor.Utils.BeatCalculator),
	typeof(RhythmBase.RhythmDoctor.Components.TickTime),
	typeof(RhythmBase.RhythmDoctor.EventType),
	typeof(RhythmBase.RhythmDoctor.Events.IBaseEvent)
)]