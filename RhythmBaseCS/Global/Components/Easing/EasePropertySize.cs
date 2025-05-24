using RhythmBase.Global.Events;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using System.Reflection;

namespace RhythmBase.Global.Components.Easing
{
	/// <summary>
	/// Represents an easing property with a size value.
	/// </summary>
	public class EasePropertySize : IEaseProperty<RDSizeN>
	{
		private EaseValue _width;
		private EaseValue _height;
		/// <inheritdoc/>
		public RDSizeN GetValue(RDBeat beat) => new(_width.GetValue(beat.BeatOnly), _height.GetValue(beat.BeatOnly));
		/// <inheritdoc/>
#if NET7_0_OR_GREATER
static
#endif
		public bool CanConvert(object data) => data is RDSize;
		/// <inheritdoc/>
#if NET7_0_OR_GREATER
static
#endif
		public EaseNode?[] Convert(IEaseEvent data, PropertyInfo property)
		{
			RDSizeE? value = (RDSizeE?)property.GetValue(data);
			return [
				value?.Width?.Value is float vx ? new(vx) {
					Duration = data.Duration,
					Time = ((BaseEvent)data).Beat.BeatOnly,
					Type = data.Ease
				} : null,
				value?.Height?.Value is float vy ? new(vy) {
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
		public IEaseProperty<RDSizeN> CreateEaseProperty(RDSizeN originalValue, IEaseEvent[] data, PropertyInfo property)
		{
			EaseNode?[][] nodes = [.. data.Select(d => Convert(d, property))];
			return new EasePropertySize()
			{
				_width = new(originalValue.Width, [.. nodes.Select(i => i[0]).Where(i => i is not null).Cast<EaseNode>()]),
				_height = new(originalValue.Height, [.. nodes.Select(i => i[1]).Where(i => i is not null).Cast<EaseNode>()])
			};
		}
	}
}
