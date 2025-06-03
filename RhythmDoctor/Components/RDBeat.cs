
using RhythmBase.Global.Exceptions;
using RhythmBase.RhythmDoctor.Utils;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// A beat.
	/// </summary>
	public struct RDBeat : IComparable<RDBeat>, IEquatable<RDBeat>
#if NET7_0_OR_GREATER
		, IComparisonOperators<RDBeat, RDBeat, bool>
#endif
	{
		internal readonly RDLevel? BaseLevel => _calculator?.Collection;
		/// <summary>
		/// Whether this beat cannot be calculated.
		/// </summary>
#if NET7_0_OR_GREATER
		[MemberNotNullWhen(false, nameof(_calculator))]
#endif
		public readonly bool IsEmpty => _calculator == null || !_isBeatLoaded && !_isBarBeatLoaded && !_isTimeSpanLoaded;
		/// <summary>
		/// The total number of beats from this moment to the beginning of the level.
		/// </summary>
		public float BeatOnly
		{
			get
			{
				if (!_isBeatLoaded && _calculator is not null)
				{
					if (_isBarBeatLoaded)
						_beat = _calculator.BarBeatToBeatOnly(_BarBeat.Bar, _BarBeat.Beat) - 1f;
					else if (_isTimeSpanLoaded)
						_beat = _calculator.TimeSpanToBeatOnly(_TimeSpan) - 1f;
					_isBeatLoaded = true;
				}
				return _beat + 1f;
			}
		}
		/// <summary>
		/// The actual bar and beat of this moment.
		/// </summary>
		public (uint bar, float beat) BarBeat
		{
			get
			{
				if (!_isBarBeatLoaded && _calculator is not null)
				{
					if (_isBeatLoaded)
						_BarBeat = _calculator.BeatOnlyToBarBeat(_beat + 1f);
					else if (_isTimeSpanLoaded)
					{
						_beat = _calculator.TimeSpanToBeatOnly(_TimeSpan) - 1f;
						_isBeatLoaded = true;
						_BarBeat = _calculator.BeatOnlyToBarBeat(_beat + 1f);
					}
					_isBarBeatLoaded = true;
				}
				return _BarBeat;
			}
		}
		/// <summary>
		/// The total amount of time from the beginning of the level to this beat.
		/// </summary>
		public TimeSpan TimeSpan
		{
			get
			{
				if (!_isTimeSpanLoaded && _calculator is not null)
				{
					if (_isBeatLoaded)
						_TimeSpan = _calculator.BeatOnlyToTimeSpan(_beat + 1f);
					else
					{
						if (_isBarBeatLoaded)
						{
							_beat = _calculator.BarBeatToBeatOnly(_BarBeat.Bar, _BarBeat.Beat) - 1f;
							_isBeatLoaded = true;
							_TimeSpan = _calculator.BeatOnlyToTimeSpan(_beat + 1f);
						}
					}
					_isTimeSpanLoaded = true;
				}
				return _TimeSpan;
			}
		}
		/// <summary>
		/// The number of beats per minute followed at this moment.
		/// </summary>
		public float BPM
		{
			get
			{
				if (!_isBPMLoaded)
				{
					_BPM = _calculator?.BeatsPerMinuteOf(this) ?? throw new InvalidRDBeatException();
					_isBPMLoaded = true;
				}
				return _BPM;
			}
		}
		/// <summary>
		/// The number of beats per bar followed at this moment.
		/// </summary>
		public float CPB
		{
			get
			{
				if (!_isCPBLoaded)
				{
					_CPB = (uint)Math.Round(_calculator?.CrotchetsPerBarOf(this) ?? throw new InvalidRDBeatException());
					_isCPBLoaded = true;
				}
				return _CPB;
			}
		}
		/// <summary>
		/// Construct an instance without specifying a calculator.
		/// </summary>
		/// <param name="beatOnly">The total number of beats from this moment to the beginning of the level.</param>
		public RDBeat(float beatOnly)
		{
			this = default;
			if (beatOnly < 1)
				beatOnly = 1;
			_beat = beatOnly - 1f;
			_isBeatLoaded = true;
		}
		/// <summary>
		/// Constructs an instance of RDBeat with the specified bar and beat.
		/// </summary>
		/// <param name="bar">The actual bar of this moment. Must be greater than or equal to 1.</param>
		/// <param name="beat">The actual beat of this moment. Must be greater than or equal to 1.</param>
		/// <exception cref="OverflowException">Thrown when the bar or beat is less than 1.</exception>
		public RDBeat(uint bar, float beat)
		{
			this = default;
			if (bar < 1)
				bar = 1;
			if (beat < 1)
				beat = 1;
			_BarBeat = new ValueTuple<uint, float>(bar, beat);
			_isBarBeatLoaded = true;
		}
		/// <summary>
		/// Constructs an instance of RDBeat with the specified time span.
		/// </summary>
		/// <param name="timeSpan">The total amount of time from the start of the level to the moment.</param>
		/// <exception cref="OverflowException">Thrown when the time span is less than zero.</exception>
		public RDBeat(TimeSpan timeSpan)
		{
			this = default;
			if (timeSpan < TimeSpan.Zero)
				timeSpan = TimeSpan.Zero;
			_TimeSpan = timeSpan;
			_isTimeSpanLoaded = true;
		}
		/// <summary>
		/// Construct an instance with specifying a calculator.
		/// </summary>
		/// <param name="calculator">Specified calculator.</param>
		/// <param name="beatOnly">The total number of beats from this moment to the beginning of the level.</param>
		public RDBeat(BeatCalculator calculator, float beatOnly)
		{
			this = new RDBeat(beatOnly);
			_calculator = calculator;
		}
		/// <summary>
		/// Construct an instance with specifying a calculator.
		/// </summary>
		/// <param name="calculator">Specified calculator.</param>
		/// <param name="bar">The actual bar of this moment.</param>
		/// <param name="beat">The actual beat of this moment.</param>
		public RDBeat(BeatCalculator calculator, uint bar, float beat)
		{
			this = new RDBeat(bar, beat);
			_calculator = calculator;
			_beat = _calculator.BarBeatToBeatOnly(bar, beat) - 1f;
		}
		/// <summary>
		/// Construct an instance with specifying a calculator.
		/// </summary>
		/// <param name="calculator">Specified calculator.</param>
		/// <param name="timeSpan">The total amount of time from the start of the level to the moment</param>
		public RDBeat(BeatCalculator calculator, TimeSpan timeSpan)
		{
			this = new RDBeat(timeSpan);
			_calculator = calculator;
			_beat = _calculator.TimeSpanToBeatOnly(timeSpan) - 1f;
		}
		/// <summary>
		/// Construct an instance with specifying a calculator.
		/// </summary>
		/// <param name="calculator">Specified calculator.</param>
		/// <param name="beat">Another instance.</param>
		public RDBeat(BeatCalculator calculator, RDBeat beat)
		{
			this = default;
			if (beat._isBeatLoaded)
			{
				if (beat._beat < 0f)
					throw new OverflowException(string.Format("The beat must not be less than 1, but {0} is given", beat._beat));
				_beat = beat._beat;
				_isBeatLoaded = true;
				_calculator = calculator;
			}
			else if (beat._isBarBeatLoaded)
			{
				if (beat._BarBeat.Bar < 1)
					throw new OverflowException(string.Format("The bar must not be less than 1, but {0} is given", beat._BarBeat.Bar));
				if (beat._BarBeat.Beat < 1)
					throw new OverflowException(string.Format("The beat must not be less than 1, but {0} is given", beat._BarBeat.Beat));
				_BarBeat = beat._BarBeat;
				_isBarBeatLoaded = true;
				_calculator = calculator;
				_beat = _calculator.BarBeatToBeatOnly(beat._BarBeat.Bar, beat._BarBeat.Beat) - 1f;
			}
			else if (beat._isTimeSpanLoaded)
			{
				if (beat._TimeSpan < TimeSpan.Zero)
					throw new OverflowException(string.Format("The time must not be less than zero, but {0} is given", beat._TimeSpan));
				_TimeSpan = beat._TimeSpan;
				_isTimeSpanLoaded = true;
				_calculator = calculator;
				_beat = _calculator.TimeSpanToBeatOnly(TimeSpan) - 1f;
			}
		}
		/// <summary>
		/// Construct a beat of the 1st beat from the calculator
		/// </summary>
		/// <param name="calculator">Specified calculator.</param>
		/// <returns>The first beat tied to the level.</returns>
		public static RDBeat Default(BeatCalculator calculator)
		{
			RDBeat Default = new(calculator, 1f);
			return Default;
		}
		/// <summary>
		/// Determine if two beats come from the same level
		/// </summary>
		/// <param name="a">A beat.</param>
		/// <param name="b">Another beat.</param>
		/// <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
		/// <returns></returns>
		public static bool FromSameLevel(RDBeat a, RDBeat b, bool @throw = false) =>
			(a._calculator?.Equals(b._calculator) ?? true)
			|| (@throw ? throw new RhythmBaseException("Beats must come from the same RDLevel.") : false);
		/// <summary>
		/// Determine if two beats are from the same level.
		/// <br />
		/// If any of them does not come from any level, it will also return true.
		/// </summary>
		/// <param name="a">A beat.</param>
		/// <param name="b">Another beat.</param>
		/// <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
		/// <returns></returns>
		public static bool FromSameLevelOrNull(RDBeat a, RDBeat b, bool @throw = false) => a._calculator == null || b._calculator == null || FromSameLevel(a, b, @throw);
		/// <summary>
		/// Determine if two beats are from the same level.
		/// </summary>
		/// <param name="b">Another beat.</param>
		/// <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
		/// <returns></returns>
#if NET7_0_OR_GREATER
		[MemberNotNullWhen(true)]
#endif
		public readonly bool FromSameLevel(RDBeat b, bool @throw = false) => FromSameLevel(this, b, @throw);
		/// <summary>
		/// Determine if two beats are from the same level.
		/// <br />
		/// If any of them does not come from any level, it will also return true.
		/// </summary>
		/// <param name="b">Another beat.</param>
		/// <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
		/// <returns></returns>	
		public readonly bool FromSameLevelOrNull(RDBeat b, bool @throw = false) => BaseLevel == null || b.BaseLevel == null || FromSameLevel(b, @throw);
		/// <summary>
		/// Returns a new instance of unbinding the level.
		/// </summary>
		/// <returns>A new instance of unbinding the level.</returns>
		public readonly RDBeat WithoutLink()
		{
			RDBeat result = this;
			if (result._calculator != null)
				result.Cache();
			result._calculator = null;
			return result;
		}
		/// <summary>
		/// 校验这个实例是否缺少在转换单位时必需的参数并抛出异常
		/// </summary>
		/// <exception cref="InvalidRDBeatException"></exception>
		internal readonly void IfNullThrowException()
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
			object __ = BeatOnly;
			_isBarBeatLoaded = false;
			_isTimeSpanLoaded = false;
		}
		/// <summary>
		/// Caches the current state of the beat by accessing its properties.
		/// </summary>
		/// <exception cref="InvalidRDBeatException">Thrown when the beat cannot be calculated.</exception>
		public void Cache()
		{
			IfNullThrowException();
			object __ = BeatOnly;
			__ = BarBeat;
			__ = TimeSpan;
		}
		internal void ResetBPM()
		{
			if (!_isBeatLoaded)
				_beat = _calculator?.TimeSpanToBeatOnly(_TimeSpan) - 1f ?? throw new InvalidRDBeatException();
			_isBeatLoaded = true;
			_isTimeSpanLoaded = false;
			_isBPMLoaded = false;
		}
		internal void ResetCPB()
		{
			if (!_isBeatLoaded)
				_beat = _calculator?.BarBeatToBeatOnly(_BarBeat.Bar, _BarBeat.Beat) - 1f ?? throw new InvalidRDBeatException();
			_isBeatLoaded = true;
			_isBarBeatLoaded = false;
			_isCPBLoaded = false;
		}
		///  <inheritdoc/>
		public static RDBeat operator +(RDBeat a, float b)
		{
			RDBeat result;
			if (!a.IsEmpty)
				result = new RDBeat(a._calculator, a.BeatOnly + b);
			else
			{
				result = new RDBeat();
				if (!a._isBeatLoaded && !a._isBarBeatLoaded)
					throw new ArgumentNullException(nameof(a), "The beat cannot be calculated.");
				if (a._isBeatLoaded)
				{
					result._beat = a._beat + b;
					result._isBeatLoaded = true;
				}
				if (a._isBarBeatLoaded)
				{
					result._BarBeat = (a._BarBeat.Bar, a._BarBeat.Beat + b);
					result._isBarBeatLoaded = true;
				}
				result._isTimeSpanLoaded = b == 0;
			}
			return result;
		}
		///  <inheritdoc/>
		public static RDBeat operator +(RDBeat a, TimeSpan b)
		{
			RDBeat result;
			if (!a.IsEmpty)
				result = new RDBeat(a._calculator, a.TimeSpan + b);
			else
			{
				result = new RDBeat();
				if (a._isTimeSpanLoaded)
				{
					result._TimeSpan = a._TimeSpan + b;
					result._isTimeSpanLoaded = true;
					result._isBeatLoaded = result._isBarBeatLoaded = b == TimeSpan.Zero;
				}
				else
					throw new ArgumentNullException(nameof(a), "The beat cannot be calculated.");
			}
			return result;
		}
		///  <inheritdoc/>
		public static RDBeat operator -(RDBeat a, float b)
		{
			RDBeat result;
			if (!a.IsEmpty)
				result = new RDBeat(a._calculator, a.BeatOnly - b);
			else
			{
				result = new RDBeat();
				if (!a._isBeatLoaded && !a._isBarBeatLoaded)
					throw new ArgumentNullException(nameof(a), "The beat cannot be calculated.");
				if (a._isBeatLoaded)
				{
					result._beat = a._beat - b;
					result._isBeatLoaded = true;
				}
				if (a._isBarBeatLoaded)
				{
					result._BarBeat = (a._BarBeat.Bar, a._BarBeat.Beat - b);
					result._isBarBeatLoaded = true;
				}
				result._isTimeSpanLoaded = b == 0;
			}
			return result;
		}
		///  <inheritdoc/>
		public static RDBeat operator -(RDBeat a, TimeSpan b)
		{
			RDBeat result;
			if (!a.IsEmpty)
				result = new RDBeat(a._calculator, a.TimeSpan - b);
			else
			{
				result = new RDBeat();
				if (a._isTimeSpanLoaded)
				{
					result._TimeSpan = a._TimeSpan - b;
					result._isTimeSpanLoaded = true;
					result._isBeatLoaded = result._isBarBeatLoaded = b == TimeSpan.Zero;
				}
				else
					throw new ArgumentNullException(nameof(a), "The beat cannot be calculated.");
			}
			return result;
		}
		///  <inheritdoc/>
		public static bool operator >(RDBeat a, RDBeat b) => FromSameLevel(a, b, false)
			? a.BeatOnly > b.BeatOnly
			: a._isBeatLoaded && b._isBeatLoaded || a._isBarBeatLoaded && b._isBarBeatLoaded || a._isTimeSpanLoaded && b._isTimeSpanLoaded
			? a._isBeatLoaded && b._isBeatLoaded && a._beat > b._beat
			|| a._isBarBeatLoaded && b._isBarBeatLoaded && a._BarBeat.Bar > b._BarBeat.Bar
			|| a._isTimeSpanLoaded && b._isTimeSpanLoaded && a._TimeSpan > b._TimeSpan
			 : throw new RhythmBaseException("The beat cannot be compared.");
		///  <inheritdoc/>
		public static bool operator <(RDBeat a, RDBeat b) => FromSameLevel(a, b, false)
			? a.BeatOnly < b.BeatOnly
			: a._isBeatLoaded && b._isBeatLoaded || a._isBarBeatLoaded && b._isBarBeatLoaded || a._isTimeSpanLoaded && b._isTimeSpanLoaded
			? a._isBeatLoaded && b._isBeatLoaded && a._beat < b._beat
			|| a._isBarBeatLoaded && b._isBarBeatLoaded && a._BarBeat.Bar < b._BarBeat.Bar
			|| a._isTimeSpanLoaded && b._isTimeSpanLoaded && a._TimeSpan < b._TimeSpan
			 : throw new RhythmBaseException("The beat cannot be compared.");
		///  <inheritdoc/>
		public static bool operator >=(RDBeat a, RDBeat b) => FromSameLevel(a, b, false)
			? a.BeatOnly >= b.BeatOnly
			: a._isBeatLoaded && b._isBeatLoaded || a._isBarBeatLoaded && b._isBarBeatLoaded || a._isTimeSpanLoaded && b._isTimeSpanLoaded
			? a._isBeatLoaded && b._isBeatLoaded && a._beat >= b._beat
			|| a._isBarBeatLoaded && b._isBarBeatLoaded && a._BarBeat.Bar >= b._BarBeat.Bar
			|| a._isTimeSpanLoaded && b._isTimeSpanLoaded && a._TimeSpan >= b._TimeSpan
			 : throw new RhythmBaseException("The beat cannot be compared.");
		///  <inheritdoc/>
		public static bool operator <=(RDBeat a, RDBeat b) => FromSameLevel(a, b, false)
			? a.BeatOnly <= b.BeatOnly
			: a._isBeatLoaded && b._isBeatLoaded || a._isBarBeatLoaded && b._isBarBeatLoaded || a._isTimeSpanLoaded && b._isTimeSpanLoaded
			? a._isBeatLoaded && b._isBeatLoaded && a._beat <= b._beat
			|| a._isBarBeatLoaded && b._isBarBeatLoaded && a._BarBeat.Bar <= b._BarBeat.Bar
			|| a._isTimeSpanLoaded && b._isTimeSpanLoaded && a._TimeSpan <= b._TimeSpan
			 : throw new RhythmBaseException("The beat cannot be compared.");
		///  <inheritdoc/>
		public static bool operator ==(RDBeat a, RDBeat b) => FromSameLevel(a, b, false)
			? a.BeatOnly == b.BeatOnly
			: a._isBeatLoaded && b._isBeatLoaded || a._isBarBeatLoaded && b._isBarBeatLoaded || a._isTimeSpanLoaded && b._isTimeSpanLoaded
			? a._isBeatLoaded && b._isBeatLoaded && a._beat == b._beat
			|| a._isBarBeatLoaded && b._isBarBeatLoaded && a._BarBeat.Bar == b._BarBeat.Bar
			|| a._isTimeSpanLoaded && b._isTimeSpanLoaded && a._TimeSpan == b._TimeSpan
			 : throw new RhythmBaseException("The beat cannot be compared.");
		///  <inheritdoc/>
		public static bool operator !=(RDBeat a, RDBeat b) => !(a == b);
		/// <inheritdoc/>
		public override string ToString()
		{
			string ToString;
			if (IsEmpty)
				ToString = string.Format("[{0},{1},{2}]", _isBeatLoaded ? _beat.ToString() : "?", _isBarBeatLoaded ? _BarBeat.ToString() : "?", _isTimeSpanLoaded ? _TimeSpan.ToString() : "?");
			else
				ToString = string.Format("[{0},{1}]", BarBeat.bar, BarBeat.beat);
			return ToString;
		}
		///  <inheritdoc/>
		#if NETSTANDARD
		public readonly override bool Equals(object? obj) => obj is RDBeat e && Equals(e);
#else
		public readonly override bool Equals([NotNullWhen(true)] object? obj) => obj is RDBeat e && Equals(e);
#endif
		///  <inheritdoc/>
		public readonly bool Equals(RDBeat other) => this == other;
		///  <inheritdoc/>
	#if NETSTANDARD
		public override int GetHashCode()
		{
			int hash = 17;
			hash = hash * 31 + (_calculator?.GetHashCode() ?? 0);
			hash = hash * 31 + _isBeatLoaded.GetHashCode();
			hash = hash * 31 + _isBarBeatLoaded.GetHashCode();
			hash = hash * 31 + _isTimeSpanLoaded.GetHashCode();
			hash = hash * 31 + _beat.GetHashCode();
			hash = hash * 31 + _BarBeat.GetHashCode();
			hash = hash * 31 + _TimeSpan.GetHashCode();
			return hash;
		}
#else
		public override int GetHashCode() => HashCode.Combine(BeatOnly, BaseLevel);
#endif
		///  <inheritdoc/>
		public readonly int CompareTo(RDBeat other) => this > other ? 1 : this == other ? 0 : -1;
		internal BeatCalculator? _calculator;
		private bool _isBeatLoaded;
		private bool _isBarBeatLoaded;
		private bool _isTimeSpanLoaded;
		private bool _isBPMLoaded;
		private bool _isCPBLoaded;
		private float _beat;
		private (uint Bar, float Beat) _BarBeat;
		private TimeSpan _TimeSpan;
		private float _BPM;
		private uint _CPB;
	}
}
