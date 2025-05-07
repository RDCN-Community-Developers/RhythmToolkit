using RhythmBase.Events;
using System.Reflection;namespace RhythmBase.Components.Easing
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
		/// <inheritdoc/>
		public RDColor GetValue(RDBeat beat) => RDColor.FromRgba(
			(byte)Math.Clamp(_r.GetValue(beat.BeatOnly), 0, 255),
			(byte)Math.Clamp(_g.GetValue(beat.BeatOnly), 0, 255),
			(byte)Math.Clamp(_b.GetValue(beat.BeatOnly), 0, 255),
			(byte)Math.Clamp(_a.GetValue(beat.BeatOnly), 0, 255));
		/// <inheritdoc/>
		public static bool CanConvert(object data) => data is RDColor;
		/// <inheritdoc/>
		public static EaseNode?[] Convert(IEaseEvent data, PropertyInfo property)
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
		public static IEaseProperty<RDColor> CreateEaseProperty(RDColor originalValue, IEaseEvent[] data, PropertyInfo property)
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
