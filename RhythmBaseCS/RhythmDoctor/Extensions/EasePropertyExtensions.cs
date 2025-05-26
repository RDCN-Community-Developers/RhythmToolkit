using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Events;
using RhythmBase.RhythmDoctor.Components;
using System.Reflection;
namespace RhythmBase.RhythmDoctor.Extensions
{
	/// <summary>
	/// Provides extension methods for retrieving easing properties from events.
	/// </summary>
	public static class EasePropertyExtensions
	{
		private static readonly Dictionary<Type, PropertyInfo[]> EaseProperties = [];
		/// <summary>
		/// Retrieves the easing properties of the specified type of event.
		/// </summary>
		/// <typeparam name="TEvent">The type of the event that implements <see cref="IEaseEvent"/>.</typeparam>
		/// <param name="obj">The collection of events to retrieve the easing properties from.</param>
		/// <returns>A dictionary containing the names and corresponding easing properties of the specified event type.</returns>
		/// <exception cref="NotSupportedException">Thrown when an unsupported property type is encountered.</exception>
		public static Dictionary<string, IEaseProperty> GetEaseProperties<TEvent>(this IEnumerable<TEvent> obj) where TEvent : IEaseEvent, new()
		{
			if (!EaseProperties.TryGetValue(typeof(TEvent), out PropertyInfo[]? properties))
			{
				EaseProperties[obj.GetType()] = properties = typeof(TEvent).GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.Where(p => p.GetCustomAttribute<EasePropertyAttribute>() != null)
				.ToArray();
			}
			Dictionary<string, IEaseProperty> values = [];
			foreach (var property in properties)
			{
#if NET7_0_OR_GREATER					
				if (property.PropertyType.IsAssignableFrom(typeof(RDExpression)))
				{
					values[property.Name] =	EasePropertyFloat.CreateEaseProperty(0, [.. obj], property);
				}
				else if (property.PropertyType.IsAssignableFrom(typeof(RDPointE)))
				{
					values[property.Name] = EasePropertyPoint.CreateEaseProperty(default, [.. obj], property);
				}
				else if (property.PropertyType.IsAssignableFrom(typeof(RDSizeE)))
				{
					values[property.Name] = EasePropertySize.CreateEaseProperty(default, [.. obj], property);
				}
				else if (property.PropertyType.IsAssignableFrom(typeof(RDColor)))
				{
					values[property.Name] = EasePropertyColor.CreateEaseProperty(default, [.. obj], property);
				}
				else
				{
					throw new NotSupportedException($"Unsupported property type {property.PropertyType}");
				}
#else
				if (property.PropertyType.IsAssignableFrom(typeof(RDExpression)))
				{
					values[property.Name] =new	EasePropertyFloat().CreateEaseProperty(0, [.. obj], property);
				}
				else if (property.PropertyType.IsAssignableFrom(typeof(RDPointE)))
				{
					values[property.Name] = new EasePropertyPoint().CreateEaseProperty(default, [.. obj], property);
				}
				else if (property.PropertyType.IsAssignableFrom(typeof(RDSizeE)))
				{
					values[property.Name] = new EasePropertySize().CreateEaseProperty(default, [.. obj], property);
				}
				else if (property.PropertyType.IsAssignableFrom(typeof(RDColor)))
				{
					values[property.Name] = new EasePropertyColor().CreateEaseProperty(default, [.. obj], property);
				}
				else
				{
					throw new NotSupportedException($"Unsupported property type {property.PropertyType}");
				}
#endif
			}
			return values;
		}
	}
}
