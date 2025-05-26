using System.Data;

namespace RhythmBase.Global.Components.Easing
{
	/// <summary>
	/// Represents a value that changes over time according to a series of easing nodes.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="EaseValue"/> class with the specified original value and easing nodes.
	/// </remarks>
	/// <param name="originalValue">The original value before any easing is applied.</param>
	/// <param name="nodes">The collection of easing nodes that define how the value changes over time.</param>
	public struct EaseValue(float originalValue, IEnumerable<EaseNode> nodes)
	{
		/// <summary>
		/// Gets or sets the collection of easing nodes, ordered by their start time.
		/// </summary>
		public EaseNode[] Nodes { get; set; } = [.. nodes.OrderBy(x => x.Time)];
		/// <summary>
		/// Gets or sets the original value before any easing is applied.
		/// </summary>
		public float OriginalValue { get; set; } = originalValue;
		/// <summary>
		/// Gets the value at the specified time, taking into account the easing nodes.
		/// </summary>
		/// <param name="time">The time at which to get the value.</param>
		/// <returns>The value at the specified time.</returns>
		public readonly float GetValue(float time)
		{
			if (Nodes.Length == 0 || time <= Nodes[0].Time)
				return OriginalValue;
			int i = Nodes.Length - 1;
			while (i >= 0 && time <= Nodes[i].Time)
				i--;
			if (Nodes[i].Time + Nodes[i].Duration <= time)
				return Nodes[i].Target;
			Stack<EaseNode> ns = new([Nodes[i]]);
			while (--i > -1)
			{
				if (IsInRange(Nodes[i], ns.Peek().Time))
				{
					ns.Push(Nodes[i]);
				}
			}
			float origin = i == -1 ? OriginalValue : Nodes[i + 1].Target;
			while (ns.Count > 1)
			{
				EaseNode node = ns.Pop();
				origin = GetValue(node, ns.Peek().Time, origin);
			}
			origin = GetValue(ns.Pop(), time, origin);
			return origin;
		}
		/// <summary>
		/// Determines whether the specified time is within the range of the easing node.
		/// </summary>
		/// <param name="node">The easing node to check.</param>
		/// <param name="time">The time to check.</param>
		/// <returns><c>true</c> if the time is within the range of the easing node; otherwise, <c>false</c>.</returns>
		private static bool IsInRange(EaseNode node, float time) => node.Time <= time && time <= node.Time + node.Duration;
		/// <summary>
		/// Calculates the value of the easing node at the specified time.
		/// </summary>
		/// <param name="node">The easing node to calculate the value for.</param>
		/// <param name="time">The time at which to calculate the value.</param>
		/// <param name="origin">The original value before any easing is applied.</param>
		/// <returns>The calculated value at the specified time.</returns>
		private static float GetValue(EaseNode node, float time, float origin) => node.Duration == 0 ? node.Target : (float)node.Type.Calculate((time - node.Time) / node.Duration, origin, node.Target);
		/// <summary>
		/// Fits the data points to an easing function with the specified precision.
		/// </summary>
		/// <param name="originalValue">The original value before any easing is applied.</param>
		/// <param name="values">The array of time and value pairs to fit.</param>
		/// <param name="precision">The precision for fitting the data points.</param>
		/// <returns>An <see cref="EaseValue"/> instance that fits the data points.</returns>
		public static EaseValue Fit(float originalValue, (float time, float value)[] values, float precision = 3f) => Fit(originalValue, values, eases, precision);
		/// <summary>
		/// Fits the data points to an easing function with the specified precision.
		/// </summary>
		/// <param name="originalValue">The original value before any easing is applied.</param>
		/// <param name="values">The array of time and value pairs to fit.</param>
		/// <param name="precision">The precision for fitting the data points.</param>
		/// <param name="easeTypes">The array of easing types to consider.</param>
		/// <returns>An <see cref="EaseValue"/> instance that fits the data points.</returns>
		public static EaseValue Fit(float originalValue, (float time, float value)[] values, EaseType[] easeTypes, float precision = 3f)
		{
			values = [.. values.OrderBy(x => x.time)];
			if (values.Length == 0)
				return new EaseValue(originalValue, []);
			if (values.Length == 1)
				return new EaseValue(originalValue, [new EaseNode(values[0].value) { Duration = values[0].time, Type = EaseType.InOutSine }]);
			if (values.Length == 2)
				return new EaseValue(originalValue, [new EaseNode(values[0].value) { Time = values[0].time, Duration = values[1].time, Type = EaseType.InOutSine }]);
			return Fit([(0, originalValue), .. values], easeTypes, precision);
		}
		/// <summary>
		/// Fits the data points to an easing function with the specified precision.
		/// </summary>
		/// <param name="values">The array of time and value pairs to fit.</param>
		/// <param name="precision">The precision for fitting the data points.</param>
		/// <returns>An <see cref="EaseValue"/> instance that fits the data points.</returns>
		public static EaseValue Fit((float time, float value)[] values, float precision = 3f) => Fit(values, eases, precision);
		/// <summary>
		/// Fits the data points to an easing function with the specified precision and ease types.
		/// </summary>
		/// <param name="values">The array of time and value pairs to fit.</param>
		/// <param name="easeTypes">The array of easing types to consider.</param>
		/// <param name="precision">The precision for fitting the data points.</param>
		/// <remarks>Special thanks to <b>mfgujhgh</b> for the algorithm!</remarks>
		/// <returns>An <see cref="EaseValue"/> instance that fits the data points.</returns>
		public static EaseValue Fit((float time, float value)[] values, EaseType[] easeTypes, float precision = 3f)
		{
			if (easeTypes.Length == 0)
				throw new ArgumentException("At least one ease type must be specified.", nameof(easeTypes));
			values = [.. values.OrderBy(x => x.time)];
			int[] steps = new int[values.Length],
				p = new int[values.Length];
			EaseType[] pe = new EaseType[values.Length];
			steps[0] = 0;
			for (int i = 1; i < values.Length; i++)
			{
				steps[i] = steps[i - 1] + 1;
				p[i] = i - 1;
				pe[i] = easeTypes[0];
				for (int j = 0; j < i - 1; j++)
				{
					if (steps[j] + 1 >= steps[i])
						continue;
					EaseType besttype = easeTypes[0];
					float bests = float.MaxValue;
					foreach (var e in easeTypes)
					{
						float s = Check(values, j, i, e, precision);
						if (s < bests)
						{
							bests = s;
							besttype = e;
						}
					}
					if (bests < precision && steps[i] > steps[j] + 1)
					{
						steps[i] = steps[j] + 1;
						p[i] = j;
						pe[i] = besttype;
					}
				}
			}
#if NETSTANDARD
			EaseNode[] ns = new EaseNode[steps.Last()];
#else
			EaseNode[] ns = new EaseNode[steps[^1]];
#endif
			int k = values.Length - 1;
			while (k != 0)
			{
				ns[--steps[k]] = new EaseNode(values[k].value)
				{
					Time = values[p[k]].time,
					Duration = values[k].time - values[p[k]].time,
					Type = pe[k]
				};
				k = p[k];
			}
			return new EaseValue(values[0].value, ns);
		}
		private static float Check((float time, float value)[] data, int start, int end, EaseType type, float precision)
		{
			float s = 0;
			for (int i = start + 1; i <= end; i++)
			{
				float v = data[start].value
					+ (float)type.Calculate((data[i].time - data[start].time) / (data[end].time - data[start].time))
					* (data[end].value - data[start].value);
				s = Math.Max(s, Math.Abs(v - data[i].value));
				if (s > precision)
				{
					return s;
				}
			}
			return s;
		}
		private static readonly EaseType[] eases = (EaseType[])Enum.GetValues(typeof(EaseType));
	}
}
