using System;
using System.Diagnostics.CodeAnalysis;
using RhythmBase.Adofai.Utils;
using RhythmBase.Exceptions;
namespace RhythmBase.Adofai.Components
{
	public struct ADBeat : IComparable<ADBeat>, IEquatable<ADBeat>
	{
		internal ADLevel baseLevel
		{
			get
			{
				return _calculator.Collection;
			}
		}

		public float BeatOnly
		{
			get
			{
				return _beat + 1f;
			}
			set
			{
			}
		}

		public TimeSpan TimeSpan
		{
			get
			{
				return _timeSpan;
			}
			set
			{
			}
		}

		public ADBeat(float beat)
		{
			this = default;
			_beat = beat;
			_isBeatLoaded = true;
		}

		public ADBeat(TimeSpan timeSpan)
		{
			this = default;
			_timeSpan = timeSpan;
			_isTimeSpanLoaded = true;
		}

		public ADBeat(ADBeatCalculator calculator, float beat)
		{
			this = default;
			_calculator = calculator;
			_beat = beat;
			_isBeatLoaded = true;
		}

		public ADBeat(ADBeatCalculator calculator, TimeSpan timeSpan)
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
			bool flag = a.baseLevel.Equals(b.baseLevel);
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

		public bool FromSameLevel(ADBeat b, bool @throw = false) => FromSameLevel(this, b, @throw);
		/// <summary>
		/// Determine if two beats are from the same level.
		/// <br />
		/// If any of them does not come from any level, it will also return true.
		/// </summary>
		/// <param name="b">Another beat.</param>
		/// <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
		/// <returns></returns>	
		public bool FromSameLevelOrNull(ADBeat b, bool @throw = false) => baseLevel == null || b.baseLevel == null || FromSameLevel(b, @throw);
		/// <summary>
		/// Returns a new instance of unbinding the level.
		/// </summary>
		/// <returns>A new instance of unbinding the level.</returns>
		public ADBeat WithoutBinding()
		{
			ADBeat result = this;
			result._calculator = null;
			return result;
		}

		private void IfNullThrowException()
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

		public bool IsEmpty
		{
			get
			{
				return _calculator == null || (!_isBeatLoaded && !_isTimeSpanLoaded);
			}
		}

		public static ADBeat operator +(ADBeat a, float b)
		{
			ADBeat result = new(a._calculator, a.BeatOnly + b);
			return result;
		}

		public static ADBeat operator +(ADBeat a, TimeSpan b)
		{
			ADBeat result = new(a._calculator, a.TimeSpan + b);
			return result;
		}

		public static ADBeat operator -(ADBeat a, float b)
		{
			ADBeat result = new(a._calculator, a.BeatOnly - b);
			return result;
		}

		public static ADBeat operator -(ADBeat a, TimeSpan b)
		{
			ADBeat result = new(a._calculator, a.TimeSpan - b);
			return result;
		}

		public static bool operator >(ADBeat a, ADBeat b) => FromSameLevel(a, b, true) && a.BeatOnly > b.BeatOnly;

		public static bool operator <(ADBeat a, ADBeat b) => FromSameLevel(a, b, true) && a.BeatOnly < b.BeatOnly;

		public static bool operator >=(ADBeat a, ADBeat b) => FromSameLevel(a, b, true) && a.BeatOnly >= b.BeatOnly;

		public static bool operator <=(ADBeat a, ADBeat b) => FromSameLevel(a, b, true) && a.BeatOnly <= b.BeatOnly;

		public static bool operator ==(ADBeat a, ADBeat b) => (FromSameLevel(a, b, true) && a._beat == b._beat) || (a._isTimeSpanLoaded && b._isTimeSpanLoaded && a._timeSpan == b._timeSpan) || a.BeatOnly == b.BeatOnly;

		public static bool operator !=(ADBeat a, ADBeat b) => !(a == b);

		public int CompareTo(ADBeat other) => checked((int)Math.Round((double)unchecked(_beat - other._beat)));

		public override string ToString() => string.Format("[{0}]", BeatOnly);

		public override bool Equals([NotNull] object obj) => obj.GetType() == typeof(ADBeat) && Equals((obj != null) ? ((ADBeat)obj) : default);

		public bool Equals(ADBeat other) => this == other;

		public override int GetHashCode() => HashCode.Combine(BeatOnly, baseLevel);

		internal ADBeatCalculator _calculator;

		private bool _isBeatLoaded;

		private bool _isTimeSpanLoaded;

		private bool _isBpmLoaded;

		private float _beat;

		private TimeSpan _timeSpan;

		private float _bpm;
	}
}
