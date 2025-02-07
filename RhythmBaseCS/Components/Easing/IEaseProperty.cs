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
}
