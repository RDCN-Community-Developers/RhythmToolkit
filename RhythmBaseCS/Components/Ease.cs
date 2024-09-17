using System;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
namespace RhythmBase.Components
{
	/// <summary>
	/// Ease Calculate module.
	/// </summary>
	[StandardModule]
	public static class Ease
	{
		/// <summary>
		/// Calculates the value with the specified ease type.
		/// </summary>
		/// <param name="Type">Ease type.</param>
		/// <param name="x">A floating-point number in the range of 0 to 1.</param>
		/// <returns>Easing result.</returns>
		public static float Calculate(this EaseType Type, float x)
		{
			float Calculate;
			switch (Type)
			{
				case EaseType.Unset:
					Calculate = Conversions.ToSingle(EaseFunction.None(x));
					break;
				case EaseType.Linear:
					Calculate = Conversions.ToSingle(EaseFunction.Linear(x));
					break;
				case EaseType.InSine:
					Calculate = Conversions.ToSingle(EaseFunction.InSine(x));
					break;
				case EaseType.OutSine:
					Calculate = Conversions.ToSingle(EaseFunction.OutSine(x));
					break;
				case EaseType.InOutSine:
					Calculate = Conversions.ToSingle(EaseFunction.InOutSine(x));
					break;
				case EaseType.InQuad:
					Calculate = Conversions.ToSingle(EaseFunction.InQuad(x));
					break;
				case EaseType.OutQuad:
					Calculate = Conversions.ToSingle(EaseFunction.OutQuad(x));
					break;
				case EaseType.InOutQuad:
					Calculate = Conversions.ToSingle(EaseFunction.InOutQuad(x));
					break;
				case EaseType.InCubic:
					Calculate = Conversions.ToSingle(EaseFunction.InCubic(x));
					break;
				case EaseType.OutCubic:
					Calculate = Conversions.ToSingle(EaseFunction.OutCubic(x));
					break;
				case EaseType.InOutCubic:
					Calculate = Conversions.ToSingle(EaseFunction.InOutCubic(x));
					break;
				case EaseType.InQuart:
					Calculate = Conversions.ToSingle(EaseFunction.InQuart(x));
					break;
				case EaseType.OutQuart:
					Calculate = Conversions.ToSingle(EaseFunction.OutQuart(x));
					break;
				case EaseType.InOutQuart:
					Calculate = Conversions.ToSingle(EaseFunction.InOutQuart(x));
					break;
				case EaseType.InQuint:
					Calculate = Conversions.ToSingle(EaseFunction.InQuint(x));
					break;
				case EaseType.OutQuint:
					Calculate = Conversions.ToSingle(EaseFunction.OutQuint(x));
					break;
				case EaseType.InOutQuint:
					Calculate = Conversions.ToSingle(EaseFunction.InOutQuint(x));
					break;
				case EaseType.InExpo:
					Calculate = Conversions.ToSingle(EaseFunction.InExpo(x));
					break;
				case EaseType.OutExpo:
					Calculate = Conversions.ToSingle(EaseFunction.OutExpo(x));
					break;
				case EaseType.InOutExpo:
					Calculate = Conversions.ToSingle(EaseFunction.InOutExpo(x));
					break;
				case EaseType.InCirc:
					Calculate = Conversions.ToSingle(EaseFunction.InCirc(x));
					break;
				case EaseType.OutCirc:
					Calculate = Conversions.ToSingle(EaseFunction.OutCirc(x));
					break;
				case EaseType.InOutCirc:
					Calculate = Conversions.ToSingle(EaseFunction.InOutCirc(x));
					break;
				case EaseType.InElastic:
					Calculate = Conversions.ToSingle(EaseFunction.InElastic(x));
					break;
				case EaseType.OutElastic:
					Calculate = Conversions.ToSingle(EaseFunction.OutElastic(x));
					break;
				case EaseType.InOutElastic:
					Calculate = Conversions.ToSingle(EaseFunction.InOutElastic(x));
					break;
				case EaseType.InBack:
					Calculate = Conversions.ToSingle(EaseFunction.InBack(x));
					break;
				case EaseType.OutBack:
					Calculate = Conversions.ToSingle(EaseFunction.OutBack(x));
					break;
				case EaseType.InOutBack:
					Calculate = Conversions.ToSingle(EaseFunction.InOutBack(x));
					break;
				case EaseType.InBounce:
					Calculate = Conversions.ToSingle(EaseFunction.InBounce(x));
					break;
				case EaseType.OutBounce:
					Calculate = Conversions.ToSingle(EaseFunction.OutBounce(x));
					break;
				case EaseType.InOutBounce:
					Calculate = Conversions.ToSingle(EaseFunction.InOutBounce(x));
					break;
				case EaseType.SmoothStep:
					Calculate = Conversions.ToSingle(EaseFunction.SmoothStep(x));
					break;
				default:
					Calculate = 0f;
					break;
			}
			return Calculate;
		}
		/// <summary>
		/// Calculates the value with the specified ease type.
		/// </summary>
		/// <param name="Type">Ease type.</param>
		/// <param name="x">A floating-point number in the range of 0 to 1.</param>
		/// <param name="from">The starting value of the easing result.</param>
		/// <param name="to">The endding value of the easing result</param>
		/// <returns>Easing result.</returns>
		public static float Calculate(this EaseType Type, float x, float from, float to) => Type.Calculate(x) * (to - from) + from;
		/// <summary>
		/// Ease types.
		/// </summary>
		public enum EaseType
		{
			Unset = -1,
			Linear,
			InSine,
			OutSine,
			InOutSine,
			InQuad,
			OutQuad,
			InOutQuad,
			InCubic,
			OutCubic,
			InOutCubic,
			InQuart,
			OutQuart,
			InOutQuart,
			InQuint,
			OutQuint,
			InOutQuint,
			InExpo,
			OutExpo,
			InOutExpo,
			InCirc,
			OutCirc,
			InOutCirc,
			InElastic,
			OutElastic,
			InOutElastic,
			InBack,
			OutBack,
			InOutBack,
			InBounce,
			OutBounce,
			InOutBounce,
			SmoothStep
		}

		private class EaseFunction
		{
			public static object None(float x) => x - x;

			public static object Linear(float x) => x;

			public static object InSine(float x) => 1.0 - Math.Cos((double)x * 3.141592653589793 / 2.0);

			public static object OutSine(float x) => Math.Sin((double)x * 3.141592653589793 / 2.0);

			public static object InOutSine(float x) => -(Math.Cos((double)x * 3.141592653589793) - 1.0) / 2.0;

			public static object InQuad(float x) => Math.Pow((double)x, 2.0);

			public static object OutQuad(float x) => 1.0 - Math.Pow((double)(1f - x), 2.0);

			public static object InOutQuad(float x) => Interaction.IIf((double)x < 0.5, 2.0 * Math.Pow((double)x, 2.0), 1.0 - Math.Pow((double)(-2f * x + 2f), 2.0) / 2.0);

			public static object InCubic(float x) => Math.Pow((double)x, 3.0);

			public static object OutCubic(float x) => 1.0 - Math.Pow((double)(1f - x), 3.0);

			public static object InOutCubic(float x) => Interaction.IIf((double)x < 0.5, 4.0 * Math.Pow((double)x, 3.0), 1.0 - Math.Pow((double)(-2f * x + 2f), 3.0) / 2.0);

			public static object InQuart(float x) => Math.Pow((double)x, 4.0);

			public static object OutQuart(float x) => 1.0 - Math.Pow((double)(1f - x), 4.0);

			public static object InOutQuart(float x) => Interaction.IIf((double)x < 0.5, 8.0 * Math.Pow((double)x, 4.0), 1.0 - Math.Pow((double)(-2f * x + 2f), 4.0) / 2.0);

			public static object InQuint(float x) => Math.Pow((double)x, 5.0);

			public static object OutQuint(float x) => 1.0 - Math.Pow((double)(1f - x), 5.0);

			public static object InOutQuint(float x) => Interaction.IIf((double)x < 0.5, 16.0 * Math.Pow((double)x, 5.0), 1.0 - Math.Pow((double)(-2f * x + 2f), 5.0) / 2.0);

			public static object InExpo(float x) => Interaction.IIf(x == 0f, 0, Math.Pow(2.0, (double)(10f * x - 10f)));

			public static object OutExpo(float x) => Interaction.IIf(x == 1f, 1, 1.0 - Math.Pow(2.0, (double)(-10f * x)));

			public static object InOutExpo(float x) => Interaction.IIf(x == 0f, 0, RuntimeHelpers.GetObjectValue(Interaction.IIf(x == 1f, 1, RuntimeHelpers.GetObjectValue(Interaction.IIf((double)x < 0.5, Math.Pow(2.0, (double)(20f * x - 10f)) / 2.0, (2.0 - Math.Pow(2.0, (double)(-20f * x + 10f))) / 2.0)))));

			public static object InCirc(float x) => 1.0 - Math.Sqrt(1.0 - Math.Pow((double)x, 2.0));

			public static object OutCirc(float x) => Math.Sqrt(1.0 - Math.Pow((double)(x - 1f), 2.0));

			public static object InOutCirc(float x) => Interaction.IIf((double)x < 0.5, (1.0 - Math.Sqrt(1.0 - Math.Pow((double)(2f * x), 2.0))) / 2.0, (Math.Sqrt(1.0 - Math.Pow((double)(-2f * x + 2f), 2.0)) + 1.0) / 2.0);

			public static object InElastic(float x) => Interaction.IIf(x == 0f, 0, RuntimeHelpers.GetObjectValue(Interaction.IIf(x == 1f, 1, -Math.Pow(2.0, (double)(10f * x - 10f)) * Math.Sin(((double)(x * 10f) - 10.75) * 2.0943951023931953))));

			public static object OutElastic(float x) => Interaction.IIf(x == 0f, 0, RuntimeHelpers.GetObjectValue(Interaction.IIf(x == 1f, 1, Math.Pow(2.0, (double)(-10f * x)) * Math.Sin(((double)(x * 10f) - 0.75) * 2.0943951023931953) + 1.0)));

			public static object InOutElastic(float x) => Interaction.IIf(x == 0f, 0, RuntimeHelpers.GetObjectValue(Interaction.IIf(x == 1f, 1, RuntimeHelpers.GetObjectValue(Interaction.IIf((double)x < 0.5, -(Math.Pow(2.0, (double)(20f * x - 10f)) * Math.Sin(((double)(20f * x) - 11.125) * 1.3962634015954636)) / 2.0, Math.Pow(2.0, (double)(-20f * x + 10f)) * Math.Sin(((double)(20f * x) - 11.125) * 1.3962634015954636) / 2.0 + 1.0)))));

			public static object InBack(float x) => 2.70158 * Math.Pow((double)x, 3.0) - 1.70158 * Math.Pow((double)x, 2.0);

			public static object OutBack(float x) => 1.0 + 2.70158 * Math.Pow((double)(x - 1f), 3.0) + 1.70158 * Math.Pow((double)(x - 1f), 2.0);

			public static object InOutBack(float x) => Interaction.IIf((double)x < 0.5, Math.Pow((double)(2f * x), 2.0) * (7.189819 * (double)x - 2.5949095) / 2.0, (Math.Pow((double)(2f * x - 2f), 2.0) * (3.5949095 * (double)(x * 2f - 2f) + 2.5949095) + 2.0) / 2.0);

			public static object InBounce(float x) => Operators.SubtractObject(1, OutBounce(1f - x));

			public static object OutBounce(float x)
			{
				bool flag = (double)x < 0.36363636363636365;
				object OutBounce;
				if (flag)
				{
					OutBounce = 7.5625 * Math.Pow((double)x, 2.0);
				}
				else
				{
					bool flag2 = (double)x < 0.7272727272727273;
					if (flag2)
					{
						OutBounce = 7.5625 * Math.Pow((double)x - 0.5454545454545454, 2.0) + 0.75;
					}
					else
					{
						bool flag3 = (double)x < 0.9090909090909091;
						if (flag3)
						{
							OutBounce = 7.5625 * Math.Pow((double)x - 0.8181818181818182, 2.0) + 0.9375;
						}
						else
						{
							OutBounce = 7.5625 * Math.Pow((double)x - 0.9545454545454546, 2.0) + 0.984375;
						}
					}
				}
				return OutBounce;
			}

			public static object InOutBounce(float x) => Interaction.IIf((double)x < 0.5, Operators.DivideObject(Operators.SubtractObject(1, OutBounce(1f - 2f * x)), 2), Operators.DivideObject(Operators.AddObject(1, OutBounce(2f * x - 1f)), 2));

			public static object SmoothStep(float x) => (double)(3f - 2f * x) * Math.Pow((double)x, 2.0);
		}
	}
}
