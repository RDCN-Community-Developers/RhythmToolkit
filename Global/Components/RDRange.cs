using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.Global.Components
{
	/// <summary>
	/// Beat range.
	/// </summary>
	public struct RDRange
	{
		/// <summary>
		/// Start beat.
		/// </summary>
		public RDBeat? Start { get; }
		/// <summary>
		/// End beat.
		/// </summary>
		public RDBeat? End { get; }
		/// <summary>
		/// Beat interval.
		/// </summary>
		public readonly float BeatInterval
		{
			get
			{
				bool flag = Start != null && End != null;
				float BeatInterval;
				if (flag)
				{
					BeatInterval = End!.Value.BeatOnly - Start!.Value.BeatOnly;
				}
				else
				{
					BeatInterval = float.PositiveInfinity;
				}
				return BeatInterval;
			}
		}
		/// <summary>
		/// Time interval.
		/// </summary>
		/// <returns></returns>
		public readonly TimeSpan TimeInterval
		{
			get
			{
				bool flag = Start != null && End != null;
				TimeSpan TimeInterval;
				if (flag)
				{
					if (Start!.Value.BeatOnly == End!.Value.BeatOnly)
					{
						TimeInterval = TimeSpan.Zero;
					}
					else
					{
						TimeInterval = End.Value.TimeSpan - Start.Value.TimeSpan;
					}
				}
				else
				{
					TimeInterval = TimeSpan.MaxValue;
				}
				return TimeInterval;
			}
		}
		/// <param name="start">Start beat.</param>
		/// <param name="end">End beat.</param>
		public RDRange(RDBeat? start, RDBeat? end)
		{
			this = default;
			if (start != null && end != null && !((RDBeat)start).FromSameLevelOrNull((RDBeat)end))
			{
				throw new RhythmBaseException("RDIndexes must come from the same RDLevel.");
			}
			if (start != null && end != null && start > end)
			{
				Start = end;
				End = start;
			}
			else
			{
				Start = start;
				End = end;
			}
		}
		/// <summary>
		/// Determines whether the specified beat is within the range.
		/// </summary>
		/// <param name="b">The beat to check.</param>
		/// <returns>True if the beat is within the range; otherwise, false.</returns>
		public readonly bool Contains(RDBeat b) => (Start == null || Start < b) && (End == null || b < End);
	}
}
