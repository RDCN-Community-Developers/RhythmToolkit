using RhythmBase.Global.Events;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using System.Reflection;

namespace RhythmBase.Global.Components.Easing
{
	/// <summary>
	/// Represents an easing property with a point value.
	/// </summary>
	public class EasePropertyPoint : IEaseProperty<RDPointN>
	{
		private EaseValue _x;
		private EaseValue _y;
		/// <inheritdoc/>
		public RDPointN GetValue(RDBeat beat) => new(_x.GetValue(beat.BeatOnly), _y.GetValue(beat.BeatOnly));
		/// <inheritdoc/>
#if NET7_0_OR_GREATER
		static
#endif

		public bool CanConvert(object data) => data is RDPoint;
		/// <inheritdoc/>

#if NET7_0_OR_GREATER
		static
#endif
		public EaseNode?[] Convert(IEaseEvent data, PropertyInfo property)
		{
			RDPointE? value = (RDPointE?)property.GetValue(data);
			return [
				value?.X?.Value is float vx ? new(vx) {
					Duration = data.Duration,
					Time = ((BaseEvent)data).Beat.BeatOnly,
					Type = data.Ease
				} : null,
				value?.Y?.Value is float vy ? new(vy) {
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
		public IEaseProperty<RDPointN> CreateEaseProperty(RDPointN originalValue, IEaseEvent[] data, PropertyInfo property)
		{
			EaseNode?[][] nodes = [.. data.Select(d => Convert(d, property))];
			return new EasePropertyPoint()
			{
				_x = new(originalValue.X, [.. nodes.Select(i => i[0]).Where(i => i is not null).Cast<EaseNode>()]),
				_y = new(originalValue.Y, [.. nodes.Select(i => i[1]).Where(i => i is not null).Cast<EaseNode>()])
			};
		}
	}
}
