using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents the base class for events that have a beats per minute (BPM) value.
/// </summary>
public abstract class BaseBeatsPerMinute : BaseEvent
{
	///<inheritdoc/>
	public override RDBeat Beat
	{
		get => base.Beat;
		set
		{
			base.Beat = value;
			ResetTimeLine();
		}
	}
	///<inheritdoc/>
	public virtual float BeatsPerMinute
	{
		get => _bpm;
		set
		{
			_bpm = value;
			ResetTimeLine();
		}
	}
	/// <summary>
	/// Resets the timeline for all events in the same level that occur after this event.
	/// </summary>
	private void ResetTimeLine()
	{
		if (Beat.BaseLevel != null)
		{
			foreach (IBaseEvent item in from i in Beat.BaseLevel
										where i.Beat > Beat
										select i)
			{
				((BaseEvent)item)._beat.ResetBPM();
			}
		}
	}
	private float _bpm = Utils.Utils.DefaultBPM;
}
