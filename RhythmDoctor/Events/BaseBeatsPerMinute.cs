using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Extensions;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents the base class for events that have a beats per minute (BPM) value.
	/// </summary>
	public abstract class BaseBeatsPerMinute : BaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BaseBeatsPerMinute"/> class with a default BPM of 100.
		/// </summary>
		protected BaseBeatsPerMinute()
		{
			_bpm = RhythmDoctor.Utils.Utils.DefaultBPM;
		}
		/// <summary>
		/// Gets or sets the beat associated with this event.
		/// </summary>
		public override RDBeat Beat
		{
			get
			{
				return base.Beat;
			}
			set
			{
				base.Beat = value;
				ResetTimeLine();
			}
		}
		/// <summary>
		/// Gets or sets the beats per minute (BPM) for this event.
		/// </summary>
		public virtual float BeatsPerMinute
		{
			get
			{
				return _bpm;
			}
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
		private float _bpm;
	}
}
