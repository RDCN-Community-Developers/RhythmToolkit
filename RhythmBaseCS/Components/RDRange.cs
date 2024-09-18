using RhythmBase.Exceptions;
namespace RhythmBase.Components
{
	/// <summary>
	/// Beat range.
	/// </summary>
	public struct RDRange
	{
		/// <summary>
		/// Start beat.
		/// </summary>
		public Beat? Start { get; }
		/// <summary>
		/// End beat.
		/// </summary>
		public Beat? End { get; }
		/// <summary>
		/// Beat interval.
		/// </summary>
		public float BeatInterval
		{
			get
			{
				bool flag = Start != null && End != null;
				float BeatInterval;
				if (flag)
				{
					BeatInterval = End.Value.BeatOnly - Start.Value.BeatOnly;
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
		public TimeSpan TimeInterval
		{
			get
			{
				bool flag = Start != null && End != null;
				TimeSpan TimeInterval;
				if (flag)
				{
					if (Start.Value.BeatOnly == End.Value.BeatOnly)
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
		public RDRange(Beat? start, Beat? end)
		{
			this = default;
			if (start != null && end != null && !((Beat)start).FromSameLevelOrNull((Beat)end))
			{
				throw new RhythmBaseException("RDIndexes must come from the same RDLevel.");
			}
			if (start != null && end != null && start > end)
			{
				this.Start = end;
				this.End = start;
			}
			else
			{
				this.Start = start;
				this.End = end;
			}
		}
		public readonly bool Contains(Beat b) => (Start == null || Start < b) && (End == null || b < End);
	}
}
