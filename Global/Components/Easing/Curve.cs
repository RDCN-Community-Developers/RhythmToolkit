namespace RhythmBase.Global.Components.Easing
{
	public struct EaseInfo
	{
		public EaseType Type { get; }
		public float Duration { get; }
		internal EaseInfo(EaseType type, float duration)
		{
			Type = type;
			Duration = duration;
		}
	}
	public interface INode
	{
		public float Time { get; }
	}
	public struct CurveNode<T> : INode
	{
		public float Time { get; }
		public T Value { get; }
		internal CurveNode(float time, T value)
		{
			Time = time;
			Value = value;
		}
	}
	public struct TweenCurveNode<T> : INode
	{
		public float Time { get; }
		public T Target { get; }
		public EaseInfo EaseInfo { get; }
		internal TweenCurveNode(float time, T target, EaseInfo easeInfo)
		{
			Time = time;
			Target = target;
			EaseInfo = easeInfo;
		}
	}
	public struct Curve<T>
	{
		private readonly T _default;
		private CurveNode<T>[] _nodes = [];
		internal Curve(CurveNode<T>[] values, T defaultValue)
		{
			_nodes = [.. values.OrderBy(i => i.Time)];
			_default = defaultValue;
		}
		public readonly T this[float time]
		{
			get
			{
				if (_nodes.Length == 0)
					return _default;
				if (time < _nodes[0].Time)
					return _default;
				if (time == _nodes[0].Time)
					return _nodes[0].Value;
				if (time >= _nodes[_nodes.Length - 1].Time)
					return _nodes[_nodes.Length - 1].Value;
				int left = 0, right = _nodes.Length - 1;
				while (left < right - 1)
				{
					int mid = (left + right) / 2;
					if (_nodes[mid].Time <= time)
						left = mid;
					else
						right = mid;
				}
				var leftNode = _nodes[left];
				return leftNode.Value;
			}
		}
	}
	public struct TweenCurve<T>
	{
		private readonly T _default;
		private TweenCurveNode<T>[] _nodes = [];
		private Func<T, T, double, T> _interpolator;
		internal TweenCurve(TweenCurveNode<T>[] values, T defaultValue, Func<T, T, double, T> interpolator)
		{
			_nodes = [.. values.OrderBy(i => i.Time)];
			_default = defaultValue;
			_interpolator = interpolator;
		}
		public readonly T this[float time]
		{
			get
			{
				if (_nodes.Length == 0 || time <= _nodes[0].Time)
					return _default;
				int left = 0, right = _nodes.Length - 1;
				if(time >= _nodes[_nodes.Length - 1].Time)
					left = _nodes.Length - 1;
				if (time >= _nodes[_nodes.Length - 1].Time + _nodes[_nodes.Length - 1].EaseInfo.Duration)
					return _nodes[_nodes.Length - 1].Target;
				while (left < right - 1)
				{
					int mid = (left + right) / 2;
					if (_nodes[mid].Time <= time)
						left = mid;
					else
						right = mid;
				}
				int cur = left;
				while (left > 0)
				{
					TweenCurveNode<T> prevNode = _nodes[left - 1];
					if (prevNode.Time + prevNode.EaseInfo.Duration < _nodes[left--].Time)
						break;
				}
				T origin = left == 0 ? _default : _nodes[left - 1].Target;
				while (left < cur)
				{
					origin = _interpolator(
						origin,
						_nodes[left].Target,
						_nodes[left].EaseInfo.Type.Calculate(
							(_nodes[left + 1].Time - _nodes[left].Time) / _nodes[left].EaseInfo.Duration
						));
					left++;
				}
				origin = _interpolator(
					origin,
					_nodes[left].Target,
					_nodes[left].EaseInfo.Type.Calculate(
						(time - _nodes[left].Time) / _nodes[left].EaseInfo.Duration
					));
				return origin;
			}
		}
	}
	public struct VectorTweenCurve<T>
	{
		private readonly float[] _default;
		private TweenCurveNode<float>[][] _nodes = [];
		private Func<float, float, double, float> _interpolator;
		private Func<float[], T> _fromArray;
		internal VectorTweenCurve(
			TweenCurveNode<float>[][] values,
			T defaultValue,
			Func<float, float, double, float> interpolator,
			Func<T, float[]> toArray,
			Func<float[], T> fromArray)
		{
			_nodes = [.. values.Select(i => i.OrderBy(j => j.Time).ToArray())];
			_default = toArray(defaultValue);
			_interpolator = interpolator;
			_fromArray = fromArray;
		}
		public readonly T this[float time]
		{
			get
			{
				float[] result = new float[_nodes.Length];
				for (int i = 0; i < _nodes.Length; i++)
				{
					if (_nodes[i].Length == 0 || time <= _nodes[i][0].Time)
					{
						result[i] = _default[i];
						continue;
					}
					int left = 0, right = _nodes[i].Length - 1;
					if (time >= _nodes[i][_nodes[i].Length - 1].Time)
						left = _nodes[i].Length - 1;
					if (time >= _nodes[i][_nodes[i].Length - 1].Time + _nodes[i][_nodes[i].Length - 1].EaseInfo.Duration)
					{
						result[i] = _nodes[i][_nodes[i].Length - 1].Target;
						continue;
					}
					while (left < right - 1)
					{
						int mid = (left + right) / 2;
						if (_nodes[i][mid].Time <= time)
							left = mid;
						else
							right = mid;
					}
					int cur = left;
					while (left > 0)
					{
						TweenCurveNode<float> prevNode = _nodes[i][left - 1];
						if (prevNode.Time + prevNode.EaseInfo.Duration < _nodes[i][left--].Time)
							break;
					}
					float origin = left == 0 ? _default[i] : _nodes[i][left - 1].Target;
					while (left < cur)
					{
						origin = _interpolator(
							origin,
							_nodes[i][left].Target,
							_nodes[i][left].EaseInfo.Type.Calculate(
								(_nodes[i][left + 1].Time - _nodes[i][left].Time) / _nodes[i][left].EaseInfo.Duration
							));
						left++;
					}
					origin = _interpolator(
						origin,
						_nodes[i][left].Target,
						_nodes[i][left].EaseInfo.Type.Calculate(
							(time - _nodes[i][left].Time) / _nodes[i][left].EaseInfo.Duration
						));
					result[i] = origin;
				}
				return _fromArray(result);
			}
		}
	}
}
