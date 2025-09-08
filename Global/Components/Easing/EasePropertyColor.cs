using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Extensions;
using System.Reflection;

namespace RhythmBase.Global.Components.Easing
{
	/// <summary>
	/// Represents an easing property with a color value.
	/// </summary>
	public class EasePropertyColor : IEaseProperty<RDColor>
	{
		private EaseValue _r;
		private EaseValue _g;
		private EaseValue _b;
		private EaseValue _a;
#if NETSTANDARD
		/// <inheritdoc/>
		public Type Type => typeof(RDColor);
		private static byte Clamp(byte value, byte min, byte max) => value < min ? min : value > max ? max : value;
#endif
		/// <inheritdoc/>
		public RDColor GetValue(RDBeat beat) => RDColor.FromRgba(
#if NETSTANDARD
			Clamp((byte)_r.GetValue(beat.BeatOnly), 0, 255),
			Clamp((byte)_g.GetValue(beat.BeatOnly), 0, 255),
			Clamp((byte)_b.GetValue(beat.BeatOnly), 0, 255),
			Clamp((byte)_a.GetValue(beat.BeatOnly), 0, 255));
#else
			byte.Clamp((byte)_r.GetValue(beat.BeatOnly), 0, 255),
			byte.Clamp((byte)_g.GetValue(beat.BeatOnly), 0, 255),
			byte.Clamp((byte)_b.GetValue(beat.BeatOnly), 0, 255),
			byte.Clamp((byte)_a.GetValue(beat.BeatOnly), 0, 255));
#endif
		/// <inheritdoc/>
#if NET7_0_OR_GREATER
		static
#endif
		public bool CanConvert(object data) => data is RDColor;
		/// <inheritdoc/>
#if NET7_0_OR_GREATER
		static
#endif
		public EaseNode?[] Convert(IEaseEvent data, PropertyInfo property)
		{
			RDColor? value = (RDColor?)property.GetValue(data);
			return [
				value?.R is byte vr ? new(vr) {
					Duration = data.Duration,
					Time = ((BaseEvent)data).Beat.BeatOnly,
					Type = data.Ease
				} : null,
				value?.G is byte vg ? new(vg) {
					Duration = data.Duration,
					Time = ((BaseEvent)data).Beat.BeatOnly,
					Type = data.Ease
				} : null,
				value?.B is byte vb ? new(vb) {
					Duration = data.Duration,
					Time = ((BaseEvent)data).Beat.BeatOnly,
					Type = data.Ease
				} : null,
				value?.A is byte va ? new(va) {
					Duration = data.Duration,
					Time = ((BaseEvent)data).Beat.BeatOnly,
					Type = data.Ease
				} : null,
			];
		}
		/// <inheritdoc/>
#if NET7_0_OR_GREATER
		static
#endif
		public IEaseProperty<RDColor> CreateEaseProperty(RDColor originalValue, IEaseEvent[] data, PropertyInfo property)
		{
			EaseNode?[][] nodes = [.. data.Select(d => Convert(d, property))];
			return new EasePropertyColor()
			{
				_r = new(originalValue.R, [.. nodes.Select(i => i[0]).Where(i => i is not null).Cast<EaseNode>()]),
				_g = new(originalValue.G, [.. nodes.Select(i => i[1]).Where(i => i is not null).Cast<EaseNode>()]),
				_b = new(originalValue.B, [.. nodes.Select(i => i[2]).Where(i => i is not null).Cast<EaseNode>()]),
				_a = new(originalValue.A, [.. nodes.Select(i => i[3]).Where(i => i is not null).Cast<EaseNode>()])
			};
		}
	}
}
