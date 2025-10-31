using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Utils;
using System.Collections;

namespace RhythmBase.RhythmDoctor.Components.Linq
{
	/// <summary>
	/// Represents a collection of events that can be enumerated.
	/// </summary>
	/// <typeparam name="T">The type of events in the collection. Must implement <see cref="IBaseEvent"/>.</typeparam>
	public interface IEventEnumerable<out T> : IEnumerable<T> where T : IBaseEvent
	{
	}
}