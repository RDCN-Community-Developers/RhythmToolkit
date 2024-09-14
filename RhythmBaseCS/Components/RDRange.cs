using System;
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
					bool flag2 = Start.Value.BeatOnly == End.Value.BeatOnly;
					if (flag2)
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
		/// <param name="[end]">End beat.</param>

		public RDRange(Beat? start, Beat? end)
		{
			this = default;
			bool flag = start != null && end != null && !start.Value._calculator.Equals(end.Value._calculator);
			if (flag)
			{
				throw new RhythmBaseException("RDIndexes must come from the same RDLevel.");
			}
			bool flag2 = start != null && end != null && start.Value.BeatOnly > end.Value.BeatOnly;
			if (flag2)
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
	}
}
