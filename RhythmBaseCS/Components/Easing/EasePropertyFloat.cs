using RhythmBase.Events;
using System.Reflection;

namespace RhythmBase.Components.Easing
{
	/// <summary>
	/// Represents an easing property with a float value.
	/// </summary>
	public class EasePropertyFloat : IEaseProperty<float>
	{
		private EaseValue _value;
		/// <inheritdoc/>
		public float GetValue(RDBeat beat) => _value.GetValue(beat.BeatOnly);
		/// <inheritdoc/>
		public static bool CanConvert(object data) => data is float;
		/// <inheritdoc/>
		public static EaseNode?[] Convert(IEaseEvent data, PropertyInfo property)
		{
			RDExpression? value = (RDExpression?)property.GetValue(data);
			return [
				value is RDExpression v? new EaseNode(v.Value) {
					Duration = data.Duration,
					Time = ((BaseEvent)data).Beat.BeatOnly,
					Type = data.Ease
				} : null,
			];
		}
		/// <inheritdoc/>
		public static IEaseProperty<float> CreateEaseProperty(float originalValue, IEaseEvent[] data, PropertyInfo property)
		{
			EaseNode?[][] nodes = [.. data.Select(d => Convert(d, property))];
			return new EasePropertyFloat()
			{
				_value = new(originalValue, [.. nodes.Select(i => i[0]).Where(i => i is not null).Cast<EaseNode>()])
			};
		}
	}
}
