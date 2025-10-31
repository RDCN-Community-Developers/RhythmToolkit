using RhythmBase.Adofai.Utils;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Adofai.Components
{
	/// <summary>
	/// Represents a beat in the ADLevel.
	/// </summary>
	public struct ADBeat : IComparable<ADBeat>, IEquatable<ADBeat>
	{
		internal readonly ADLevel? baseLevel => _calculator?.Collection;
		/// <summary>
		/// Gets or sets the beat only value.
		/// </summary>
		public readonly float BeatOnly
		{
			get => _beat + 1f;
			set
			{
			}
		}
		/// <summary>
		/// Gets or sets the time span.
		/// </summary>
		public readonly TimeSpan TimeSpan
		{
			get => _timeSpan;
			set
			{
			}
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ADBeat"/> struct with a specified beat.
		/// </summary>
		/// <param name="beat">The beat value.</param>
		public ADBeat(float beat)
		{
			this = default;
			_beat = beat;
			_isBeatLoaded = true;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ADBeat"/> struct with a specified time span.
		/// </summary>
		/// <param name="timeSpan">The time span value.</param>
		public ADBeat(TimeSpan timeSpan)
		{
			this = default;
			_timeSpan = timeSpan;
			_isTimeSpanLoaded = true;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ADBeat"/> struct with a specified calculator and beat.
		/// </summary>
		/// <param name="calculator">The beat calculator.</param>
		/// <param name="beat">The beat value.</param>
		public ADBeat(ADBeatCalculator? calculator, float beat)
		{
			this = default;
			_calculator = calculator;
			_beat = beat;
			_isBeatLoaded = true;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ADBeat"/> struct with a specified calculator and time span.
		/// </summary>
		/// <param name="calculator">The beat calculator.</param>
		/// <param name="timeSpan">The time span value.</param>
		/// <exception cref="OverflowException">Thrown when the time span is less than zero.</exception>
		public ADBeat(ADBeatCalculator? calculator, TimeSpan timeSpan)
		{
			this = default;
			if (timeSpan < TimeSpan.Zero)
			{
				throw new OverflowException(string.Format("The time must not be less than zero, but {0} is given", timeSpan));
			}
			_calculator = calculator;
			_timeSpan = timeSpan;
			_isTimeSpanLoaded = true;
		}
		/// <summary>
		/// Construct a beat of the 1st beat from the calculator
		/// </summary>
		/// <param name="calculator">Specified calculator.</param>
		/// <returns>The first beat tied to the level.</returns>
		public static ADBeat Default(ADBeatCalculator calculator)
		{
			ADBeat Default = new(calculator, 1f);
			return Default;
		}
		/// <summary>
		/// Determine if two beats come from the same level
		/// </summary>
		/// <param name="a">A beat.</param>
		/// <param name="b">Another beat.</param>
		/// <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
		/// <returns></returns>
		public static bool FromSameLevel(ADBeat a, ADBeat b, bool @throw = false)
		{
			bool flag = a.baseLevel?.Equals(b.baseLevel) ?? true;
			bool FromSameLevel;
			if (flag)
			{
				FromSameLevel = true;
			}
			else
			{
				if (@throw)
				{
					throw new RhythmBaseException("Beats must come from the same ADLevel.");
				}
				FromSameLevel = false;
			}
			return FromSameLevel;
		}
		/// <summary>
		/// Determine if two beats are from the same level.
		/// <br />
		/// If any of them does not come from any level, it will also return true.
		/// </summary>
		/// <param name="a">A beat.</param>
		/// <param name="b">Another beat.</param>
		/// <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
		/// <returns></returns>
		public static bool FromSameLevelOrNull(ADBeat a, ADBeat b, bool @throw = false) => a.baseLevel == null || b.baseLevel == null || FromSameLevel(a, b, @throw);
		/// <summary>  
		/// Determines if the current beat and the specified beat are from the same level.  
		/// </summary>  
		/// <param name="b">The beat to compare with the current beat.</param>  
		/// <param name="throw">If set to <c>true</c>, an exception will be thrown if the beats are not from the same level.</param>  
		/// <returns>  
		/// <c>true</c> if the beats are from the same level; otherwise, <c>false</c>.  
		/// </returns>  
		public readonly bool FromSameLevel(ADBeat b, bool @throw = false) => FromSameLevel(this, b, @throw);
		/// <summary>
		/// Determine if two beats are from the same level.
		/// <br />
		/// If any of them does not come from any level, it will also return true.
		/// </summary>
		/// <param name="b">Another beat.</param>
		/// <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
		/// <returns></returns>	
		public readonly bool FromSameLevelOrNull(ADBeat b, bool @throw = false) => baseLevel == null || b.baseLevel == null || FromSameLevel(b, @throw);
		/// <summary>
		/// Returns a new instance of unbinding the level.
		/// </summary>
		/// <returns>A new instance of unbinding the level.</returns>
		public readonly ADBeat WithoutBinding()
		{
			ADBeat result = this;
			result._calculator = null;
			return result;
		}
		private readonly void IfNullThrowException()
		{
			if (IsEmpty)
			{
				throw new InvalidRDBeatException();
			}
		}
		/// <summary>
		/// Refresh the cache.
		/// </summary>
		public void ResetCache()
		{
			float i = BeatOnly;
			_isTimeSpanLoaded = false;
		}
		internal void ResetBPM()
		{
			_isBeatLoaded = true;
			_isTimeSpanLoaded = false;
			_isBpmLoaded = false;
		}
		internal void ResetCPB() => _isBeatLoaded = true;
		/// <summary>
		/// Gets a value indicating whether this instance is empty.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
		/// </value>
		public readonly bool IsEmpty
		{
			get
			{
				return _calculator == null || (!_isBeatLoaded && !_isTimeSpanLoaded);
			}
		}
		/// <inheritdoc/>
		public static ADBeat operator +(ADBeat a, float b)
		{
			ADBeat result = new(a._calculator, a.BeatOnly + b);
			return result;
		}
		/// <inheritdoc/>
		public static ADBeat operator +(ADBeat a, TimeSpan b)
		{
			ADBeat result = new(a._calculator, a.TimeSpan + b);
			return result;
		}
		/// <inheritdoc/>
		public static ADBeat operator -(ADBeat a, float b)
		{
			ADBeat result = new(a._calculator, a.BeatOnly - b);
			return result;
		}
		/// <inheritdoc/>
		public static ADBeat operator -(ADBeat a, TimeSpan b)
		{
			ADBeat result = new(a._calculator, a.TimeSpan - b);
			return result;
		}
		/// <inheritdoc/>
		public static bool operator >(ADBeat a, ADBeat b) => FromSameLevel(a, b, true) && a.BeatOnly > b.BeatOnly;
		/// <inheritdoc/>
		public static bool operator <(ADBeat a, ADBeat b) => FromSameLevel(a, b, true) && a.BeatOnly < b.BeatOnly;
		/// <inheritdoc/>
		public static bool operator >=(ADBeat a, ADBeat b) => FromSameLevel(a, b, true) && a.BeatOnly >= b.BeatOnly;
		/// <inheritdoc/>
		public static bool operator <=(ADBeat a, ADBeat b) => FromSameLevel(a, b, true) && a.BeatOnly <= b.BeatOnly;
		/// <inheritdoc/>
		public static bool operator ==(ADBeat a, ADBeat b) => (FromSameLevel(a, b, true) && a._beat == b._beat) || (a._isTimeSpanLoaded && b._isTimeSpanLoaded && a._timeSpan == b._timeSpan) || a.BeatOnly == b.BeatOnly;
		/// <inheritdoc/>
		public static bool operator !=(ADBeat a, ADBeat b) => !(a == b);
		/// <inheritdoc/>
		public readonly int CompareTo(ADBeat other) => checked((int)Math.Round((double)unchecked(_beat - other._beat)));
		/// <inheritdoc/>
		public override readonly string ToString() => string.Format("[{0}]", BeatOnly);
		/// <inheritdoc/>
		public override readonly bool Equals(object? obj) => obj is ADBeat b && Equals((ADBeat)obj);
		/// <inheritdoc/>
#if NETSTANDARD
		public readonly bool Equals(ADBeat other) => this == other;
#else
		public readonly bool Equals([NotNull] ADBeat other) => this == other;
#endif
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly int GetHashCode()
		{
			int hash = 17;
			hash = hash * 23 + BeatOnly.GetHashCode();
			hash = hash * 23 + (baseLevel?.GetHashCode() ?? 0);
			return hash;
		}
#else
		public override readonly int GetHashCode() => HashCode.Combine(BeatOnly, baseLevel);
#endif
		internal ADBeatCalculator? _calculator;
		private bool _isBeatLoaded;
		private bool _isTimeSpanLoaded;
		private bool _isBpmLoaded;
		private float _beat;
		private TimeSpan _timeSpan;
		private float _bpm;
	}
}
