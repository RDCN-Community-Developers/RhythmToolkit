using RhythmBase.Exceptions;
using RhythmBase.Utils;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	public struct Beat : IComparable<Beat>, IEquatable<Beat>
	{
		internal readonly RDLevel BaseLevel => _calculator?.Collection ?? [];
		/// <summary>
		/// Whether this beat cannot be calculated.
		/// </summary>
		public readonly bool IsEmpty => _calculator == null || (!_isBeatLoaded && !_isBarBeatLoaded && !_isTimeSpanLoaded);
		/// <summary>
		/// The total number of beats from this moment to the beginning of the level.
		/// </summary>
		public float BeatOnly
		{
			get
			{
				IfNullThrowException();
				if (!_isBeatLoaded)
				{
					if (_isBarBeatLoaded)
						_beat = _calculator!.BarBeatToBeatOnly(_BarBeat.Bar, _BarBeat.Beat) - 1f;
					else if (_isTimeSpanLoaded)
						_beat = _calculator!.TimeSpanToBeatOnly(_TimeSpan) - 1f;
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
				IfNullThrowException();
				if (!_isBarBeatLoaded)
				{
					if (_isBeatLoaded)
						_BarBeat = _calculator!.BeatOnlyToBarBeat(_beat + 1f);
					else if (_isTimeSpanLoaded)
					{
						_beat = _calculator!.TimeSpanToBeatOnly(_TimeSpan) - 1f;
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
				IfNullThrowException();
				if (!_isTimeSpanLoaded)
				{
					if (_isBeatLoaded)
						_TimeSpan = _calculator!.BeatOnlyToTimeSpan(_beat + 1f);
					else
					{
						if (_isBarBeatLoaded)
						{
							_beat = _calculator!.BarBeatToBeatOnly(_BarBeat.Bar, _BarBeat.Beat) - 1f;
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
					_BPM = _calculator.BeatsPerMinuteOf(this);
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
					_CPB = checked((uint)Math.Round((double)_calculator.CrotchetsPerBarOf(this)));
					_isCPBLoaded = true;
				}
				return _CPB;
			}
		}
		/// <summary>
		/// Construct an instance without specifying a calculator.
		/// </summary>
		/// <param name="beatOnly">The total number of beats from this moment to the beginning of the level.</param>
		public Beat(float beatOnly)
		{
			this = default;
			if (beatOnly < 1)
				throw new OverflowException(string.Format("The beat must not be less than 1, but {0} is given", beatOnly));
			_beat = beatOnly - 1f;
			_isBeatLoaded = true;
		}
		public Beat(uint bar, float beat)
		{
			this = default;
			if (bar < 1)
				throw new OverflowException(string.Format("The bar must not be less than 1, but {0} is given", bar));
			if (beat < 1)
				throw new OverflowException(string.Format("The beat must not be less than 1, but {0} is given", beat));
			_BarBeat = new ValueTuple<uint, float>(bar, beat);
			_isBarBeatLoaded = true;
		}
		public Beat(TimeSpan timeSpan)
		{
			this = default;
			if (timeSpan < TimeSpan.Zero)
				throw new OverflowException(string.Format("The time must not be less than zero, but {0} is given", timeSpan));
			_TimeSpan = timeSpan;
			_isTimeSpanLoaded = true;
		}
		/// <summary>
		/// Construct an instance with specifying a calculator.
		/// </summary>
		/// <param name="calculator">Specified calculator.</param>
		/// <param name="beatOnly">The total number of beats from this moment to the beginning of the level.</param>
		public Beat(BeatCalculator calculator, float beatOnly)
		{
			this = new Beat(beatOnly);
			_calculator = calculator;
		}
		/// <summary>
		/// Construct an instance with specifying a calculator.
		/// </summary>
		/// <param name="calculator">Specified calculator.</param>
		/// <param name="bar">The actual bar of this moment.</param>
		/// <param name="beat">The actual beat of this moment.</param>
		public Beat(BeatCalculator calculator, uint bar, float beat)
		{
			this = new Beat(bar, beat);
			_calculator = calculator;
			_beat = _calculator.BarBeatToBeatOnly(bar, beat) - 1f;
		}
		/// <summary>
		/// Construct an instance with specifying a calculator.
		/// </summary>
		/// <param name="calculator">Specified calculator.</param>
		/// <param name="timeSpan">The total amount of time from the start of the level to the moment</param>
		public Beat(BeatCalculator calculator, TimeSpan timeSpan)
		{
			this = new Beat(timeSpan);
			_calculator = calculator;
			_beat = _calculator.TimeSpanToBeatOnly(timeSpan) - 1f;
		}
		/// <summary>
		/// Construct an instance with specifying a calculator.
		/// </summary>
		/// <param name="calculator">Specified calculator.</param>
		/// <param name="beat">Another instance.</param>
		public Beat(BeatCalculator calculator, Beat beat)
		{
			this = default;
			if (beat._isBeatLoaded)
			{
				if (beat._beat < 1f)
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
		public static Beat Default(BeatCalculator calculator)
		{
			Beat Default = new(calculator, 1f);
			return Default;
		}
		/// <summary>
		/// Determine if two beats come from the same level
		/// </summary>
		/// <param name="a">A beat.</param>
		/// <param name="b">Another beat.</param>
		/// <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
		/// <returns></returns>
		public static bool FromSameLevel(Beat a, Beat b, bool @throw = false)
		{
			bool FromSameLevel;
			if (a.BaseLevel.Equals(b.BaseLevel))
				FromSameLevel = true;
			else
			{
				if (@throw)
					throw new RhythmBaseException("Beats must come from the same RDLevel.");
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
		public static bool FromSameLevelOrNull(Beat a, Beat b, bool @throw = false) => a.BaseLevel == null || b.BaseLevel == null || FromSameLevel(a, b, @throw);
		public bool FromSameLevel(Beat b, bool @throw = false) => FromSameLevel(this, b, @throw);
		/// <summary>
		/// Determine if two beats are from the same level.
		/// <br />
		/// If any of them does not come from any level, it will also return true.
		/// </summary>
		/// <param name="b">Another beat.</param>
		/// <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
		/// <returns></returns>	
		public bool FromSameLevelOrNull(Beat b, bool @throw = false) => BaseLevel == null || b.BaseLevel == null || FromSameLevel(b, @throw);
		/// <summary>
		/// Returns a new instance of unbinding the level.
		/// </summary>
		/// <returns>A new instance of unbinding the level.</returns>
		public Beat WithoutBinding()
		{
			Beat result = this;
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
		public void Cache()
		{
			IfNullThrowException();
			//通过属性调用来更新值
			object __ = BeatOnly;
			__ = BarBeat;
			__ = TimeSpan;
		}
		/// <summary>
		///
		/// </summary>
		internal void ResetBPM()
		{
			if (!_isBeatLoaded)
				_beat = _calculator.TimeSpanToBeatOnly(_TimeSpan) - 1f;
			_isBeatLoaded = true;
			_isTimeSpanLoaded = false;
			_isBPMLoaded = false;
		}
		internal void ResetCPB()
		{
			if (!_isBeatLoaded)
				_beat = _calculator.BarBeatToBeatOnly(_BarBeat.Bar, _BarBeat.Beat) - 1f;
			_isBeatLoaded = true;
			_isBarBeatLoaded = false;
			_isCPBLoaded = false;
		}
		public static Beat operator +(Beat a, float b)
		{
			Beat result;
			if (!a.IsEmpty)
				result = new Beat(a._calculator!, a.BeatOnly + b);
			else
			{
				if (!a._isBeatLoaded)
					throw new ArgumentNullException(nameof(a), "The beat cannot be calculate.");
				result = new Beat(a._calculator!, a._beat + b);
			}
			return result;
		}
		public static Beat operator +(Beat a, TimeSpan b)
		{
			Beat result;
			if (!a.IsEmpty)
				result = new Beat(a._calculator!, a.TimeSpan + b);
			else
			{
				if (!a._isBeatLoaded)
					throw new ArgumentNullException(nameof(a), "The beat cannot be calculate.");
				result = new Beat(a._calculator!, a._TimeSpan + b);
			}
			return result;
		}
		public static Beat operator -(Beat a, float b)
		{
			Beat result;
			if (!a.IsEmpty)
				result = new Beat(a._calculator!, a.BeatOnly - b);
			else
			{
				if (!a._isBeatLoaded)
					throw new ArgumentNullException(nameof(a), "The beat cannot be calculate.");
				result = new Beat(a._calculator!, a._beat - b);
			}
			return result;
		}
		public static Beat operator -(Beat a, TimeSpan b)
		{
			Beat result;
			if (!a.IsEmpty)
				result = new Beat(a._calculator!, a.TimeSpan - b);
			else
			{
				if (!a._isBeatLoaded)
					throw new ArgumentNullException(nameof(a), "The beat cannot be calculate.");
				result = new Beat(a._calculator!, a._TimeSpan - b);
			}
			return result;
		}
		public static bool operator >(Beat a, Beat b) => FromSameLevel(a, b, true) && a.BeatOnly > b.BeatOnly;
		public static bool operator <(Beat a, Beat b) => FromSameLevel(a, b, true) && a.BeatOnly < b.BeatOnly;
		public static bool operator >=(Beat a, Beat b) => FromSameLevel(a, b, true) && a.BeatOnly >= b.BeatOnly;
		public static bool operator <=(Beat a, Beat b) => FromSameLevel(a, b, true) && a.BeatOnly <= b.BeatOnly;
		public static bool operator ==(Beat a, Beat b) => (FromSameLevel(a, b, true) && a._beat == b._beat) || (a._isBarBeatLoaded && b._isBarBeatLoaded && a._BarBeat.Bar == b._BarBeat.Bar && a._BarBeat.Beat == b._BarBeat.Beat) || (a._isTimeSpanLoaded && b._isTimeSpanLoaded && a._TimeSpan == b._TimeSpan) || a.BeatOnly == b.BeatOnly;
		public static bool operator !=(Beat a, Beat b) => !(a == b);
		public override string ToString()
		{
			string ToString;
			if (IsEmpty)
				ToString = string.Format("[{0},{1},{2}]", _isBeatLoaded ? _beat.ToString() : "?", _isBarBeatLoaded ? _BarBeat.ToString() : "?", _isTimeSpanLoaded ? _TimeSpan.ToString() : "?");
			else
				ToString = string.Format("[{0},{1}]", BarBeat.bar, BarBeat.beat);
			return ToString;
		}
		public override bool Equals([NotNull] object obj) => obj.GetType() == typeof(Beat) && Equals((obj != null) ? ((Beat)obj) : default);
		public bool Equals(Beat other) => this == other;
		public override int GetHashCode() => HashCode.Combine(BeatOnly, BaseLevel);
		public int CompareTo(Beat other)
		{
			float result = BeatOnly - other.BeatOnly;
			int CompareTo;
			if (result > 0f)
				CompareTo = 1;
			else if (result < 0f)
				CompareTo = -1;
			else
				CompareTo = 0;
			return CompareTo;
		}
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
