using RhythmBase.Events;
using System.Reflection;

namespace RhythmBase.Components.Easing
{
	/// <summary>
	/// Represents an easing property.
	/// </summary>
	public interface IEaseProperty
	{
	}
	/// <summary>
	/// Represents an easing property with a specific value type.
	/// </summary>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	public interface IEaseProperty<TValue> : IEaseProperty where TValue : new()
	{
		/// <summary>
		/// Gets the type of the value.
		/// </summary>
		static Type Type => typeof(TValue);

		/// <summary>
		/// Gets the value at the specified beat.
		/// </summary>
		/// <param name="beat">The beat at which to get the value.</param>
		/// <returns>The value at the specified beat.</returns>
		abstract TValue GetValue(RDBeat beat);

		/// <summary>
		/// Determines whether the specified data can be converted to the value type.
		/// </summary>
		/// <param name="data">The data to check.</param>
		/// <returns><c>true</c> if the data can be converted; otherwise, <c>false</c>.</returns>
		static abstract bool CanConvert(object data);

		/// <summary>
		/// Converts the specified easing event data to an array of easing nodes.
		/// </summary>
		/// <param name="data">The easing event data to convert.</param>
		/// <param name="property">The property information of the easing event.</param>
		/// <returns>An array of easing nodes.</returns>
		static abstract EaseNode?[] Convert(IEaseEvent data, PropertyInfo property);

		/// <summary>
		/// Creates an easing property with the specified original value and easing event data.
		/// </summary>
		/// <param name="originalValue">The original value before any easing is applied.</param>
		/// <param name="data">The array of easing event data.</param>
		/// <param name="property">The property information of the easing event.</param>
		/// <returns>An easing property instance.</returns>
		static abstract IEaseProperty<TValue> CreateEaseProperty(TValue originalValue, IEaseEvent[] data, PropertyInfo property);
	}
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
		public static bool CanConvert(object data) => data is RDPoint;
		/// <inheritdoc/>
		public static EaseNode?[] Convert(IEaseEvent data, PropertyInfo property)
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
		public static IEaseProperty<RDPointN> CreateEaseProperty(RDPointN originalValue, IEaseEvent[] data, PropertyInfo property)
		{
			EaseNode?[][] nodes = [.. data.Select(d => Convert(d, property))];
			return new EasePropertyPoint()
			{
				_x = new(originalValue.X, [.. nodes.Select(i => i[0]).Where(i => i is not null).Cast<EaseNode>()]),
				_y = new(originalValue.Y, [.. nodes.Select(i => i[1]).Where(i => i is not null).Cast<EaseNode>()])
			};
		}
	}
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
		public static bool CanConvert(object data) => data is RDSize;
		/// <inheritdoc/>
		public static EaseNode?[] Convert(IEaseEvent data, PropertyInfo property)
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
		public static IEaseProperty<RDSizeN> CreateEaseProperty(RDSizeN originalValue, IEaseEvent[] data, PropertyInfo property)
		{
			EaseNode?[][] nodes = [.. data.Select(d => Convert(d, property))];
			return new EasePropertySize()
			{
				_width = new(originalValue.Width, [.. nodes.Select(i => i[0]).Where(i => i is not null).Cast<EaseNode>()]),
				_height = new(originalValue.Height, [.. nodes.Select(i => i[1]).Where(i => i is not null).Cast<EaseNode>()])
			};
		}
	}
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
