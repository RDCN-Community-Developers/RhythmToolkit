using System;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public interface IEaseEvent
	{
		Ease.EaseType Ease { get; set; }

		float Duration { get; set; }
	}
}
