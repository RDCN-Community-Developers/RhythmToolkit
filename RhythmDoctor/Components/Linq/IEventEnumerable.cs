using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Utils;
using System.Collections;

namespace RhythmBase.RhythmDoctor.Components.Linq
{
	public interface IEventEnumerable<out T> : IEnumerable<T> where T : IBaseEvent
	{
	}
}