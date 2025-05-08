using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using System.Reflection;
namespace RhythmBase.RhythmDoctor.Converters
{
	internal class RDContractResolver : DefaultContractResolver
	{
		public static RDContractResolver Instance => new RDContractResolver();
		public RDContractResolver() : base()
		{
			NamingStrategy = new CamelCaseNamingStrategy();
		}
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty p = base.CreateProperty(member, memberSerialization);
			Predicate<object>? f = null;
			if (p.DeclaringType == typeof(Row))
				f = p.PropertyName!.ToUpperCamelCase() switch
				{
					nameof(Row.RowToMimic) => i => ((Row)i).RowToMimic >= 0,
					_ => null
				};
			if (p.DeclaringType?.IsAssignableTo(typeof(BaseEvent)) == true)
				f = p.PropertyName!.ToUpperCamelCase() switch
				{
					nameof(BaseEvent.Active) => i => !((BaseEvent)i).Active,
					_ => null
				};
			if (p.DeclaringType == typeof(MoveRow))
				f = p.PropertyName!.ToUpperCamelCase() switch
				{
					nameof(MoveRow.CustomPosition) => i => ((MoveRow)i).Target == MoveRow.Targets.WholeRow,
					_ => null
				};
			if (p.DeclaringType == typeof(SetVFXPreset))
				f = p.PropertyName!.ToUpperCamelCase() switch
				{
					nameof(SetVFXPreset.Enable) => i => i is SetVFXPreset e && e.Preset is not SetVFXPreset.Presets.DisableAll,
					nameof(SetVFXPreset.Threshold) => i => i is SetVFXPreset e && e.Enable && e.Preset == SetVFXPreset.Presets.Bloom,
					nameof(SetVFXPreset.Intensity) => i => i is SetVFXPreset e && e.Enable && durationPresets.Contains(e.Preset) && e.Preset is not (SetVFXPreset.Presets.TileN or SetVFXPreset.Presets.CustomScreenScroll),
					nameof(SetVFXPreset.Color) => i => i is SetVFXPreset e && e.Enable && e.Preset is SetVFXPreset.Presets.Bloom or SetVFXPreset.Presets.Tutorial,
					nameof(SetVFXPreset.FloatX) or
					nameof(SetVFXPreset.FloatY) => i => i is SetVFXPreset e && e.Enable && e.Preset is SetVFXPreset.Presets.TileN or SetVFXPreset.Presets.CustomScreenScroll,
					nameof(SetVFXPreset.Ease) => i => i is SetVFXPreset e && e.Enable && durationPresets.Contains(e.Preset),
					nameof(SetVFXPreset.Duration) => i => i is SetVFXPreset e && e.Enable && durationPresets.Contains(e.Preset),
					_ => null
				};
			if (p.DeclaringType == typeof(TintRows))
				f = p.PropertyName!.ToUpperCamelCase() switch
				{
					nameof(TintRows.Ease) or nameof(TintRows.Duration) => i => i is TintRows e && e.Duration != 0f,
					_ => null
				};
			if (p.DeclaringType == typeof(Tint))
				f = p.PropertyName!.ToUpperCamelCase() switch
				{
					nameof(Tint.Ease) or nameof(Tint.Duration) => i => i is Tint e && e.Duration != 0f,
					_ => null
				};
			if (p.DeclaringType == typeof(Tile))
				f = p.PropertyName!.ToUpperCamelCase() switch
				{
					nameof(Tile.TilingType) => i => i is Tile e && e.Speed is not null,
					nameof(Tile.Interval) => i => i is Tile e && e.TilingType == Tile.TilingTypes.Pulse,
					_ => null
				};
			if (p.DeclaringType == typeof(SoundSubType))
				f = p.PropertyName!.ToUpperCamelCase() switch
				{
					nameof(SoundSubType.Filename) or
					nameof(SoundSubType.Pan) or
					nameof(SoundSubType.Offset) => i => i is SoundSubType e && e.Used,
					nameof(SoundSubType.Volume) => i => i is SoundSubType e && e.Used && e.Volume != 100,
					nameof(SoundSubType.Pitch) => i => i is SoundSubType e && e.Used && e.Pitch != 100,
					_ => null
				};
			if (p.DeclaringType == typeof(SetGameSound))
				f = p.PropertyName!.ToUpperCamelCase() switch
				{
					nameof(SetGameSound.Filename) or
					nameof(SetGameSound.Pan) or
					nameof(SetGameSound.Offset) => i => i is SetGameSound e &&
						e.SoundType is not (
							SoundTypes.ClapSoundHold or
							SoundTypes.FreezeshotSound or
							SoundTypes.BurnshotSound),
					nameof(SetGameSound.Volume) => i => i is SetGameSound e &&
						e.SoundType is not (
							SoundTypes.ClapSoundHold or
							SoundTypes.FreezeshotSound or
							SoundTypes.BurnshotSound) && e.Volume != 100,
					nameof(SetGameSound.Pitch) => i => i is SetGameSound e &&
						e.SoundType is not (
							SoundTypes.ClapSoundHold or
							SoundTypes.FreezeshotSound or
							SoundTypes.BurnshotSound) && e.Pitch != 100,
					_ => null
				};
			if (p.DeclaringType == typeof(SetCountingSound))
				f = p.PropertyName!.ToUpperCamelCase() switch
				{
					nameof(SetCountingSound.VoiceSource) => i => i is SetCountingSound e && e.VoiceSource == SetCountingSound.VoiceSources.Custom,
					_ => null
				};
			if (p.DeclaringType == typeof(AddOneshotBeat))
				f = p.PropertyName!.ToUpperCamelCase() switch
				{
					nameof(AddOneshotBeat.Skipshot) => i => i is AddOneshotBeat e && e.Skipshot,
					nameof(AddOneshotBeat.FreezeBurnMode) => i => i is AddOneshotBeat e && e.FreezeBurnMode != null,
					_ => null
				};
			if (p.DeclaringType == typeof(Comment))
				f = p.PropertyName!.ToUpperCamelCase() switch
				{
					nameof(Comment.Target) => i => i is Comment e && e.Tab == Tabs.Decorations,
					_ => null
				};
			if (f != null)
				p.ShouldSerialize = f;
			return p;
		}
		private static readonly SetVFXPreset.Presets[] durationPresets =
		[
			SetVFXPreset.Presets.HueShift,
			SetVFXPreset.Presets.Brightness,
			SetVFXPreset.Presets.Contrast,
			SetVFXPreset.Presets.Saturation,
			SetVFXPreset.Presets.Rain,
			SetVFXPreset.Presets.Bloom,
			SetVFXPreset.Presets.TileN,
			SetVFXPreset.Presets.CustomScreenScroll,
			SetVFXPreset.Presets.JPEG,
			SetVFXPreset.Presets.Mosaic,
			SetVFXPreset.Presets.ScreenWaves,
			SetVFXPreset.Presets.Grain,
			SetVFXPreset.Presets.Blizzard,
			SetVFXPreset.Presets.Drawing,
			SetVFXPreset.Presets.Aberration,
			SetVFXPreset.Presets.Blur,
			SetVFXPreset.Presets.RadialBlur,
			SetVFXPreset.Presets.Dots,
			SetVFXPreset.Presets.Tutorial,
		];
	}
}
