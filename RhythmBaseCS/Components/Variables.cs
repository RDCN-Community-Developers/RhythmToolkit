using Microsoft.VisualBasic.CompilerServices;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
namespace RhythmBase.Components
{
	/// <summary>
	/// Variables.
	/// </summary>
	public sealed class Variables
	{
		public Variables()
		{
			i = new LimitedList<int>(10U, 0);
			f = new LimitedList<float>(10U, 0f);
			b = new LimitedList<bool>(10U, false);
		}

		public int Rand(int @int) => Random.Shared.Next(1, @int);

		public bool atLeastRank(char @char) => throw new NotImplementedException();

		public bool atLeastNPerfects(int hitsToCheck, int numberOfPerfects) => false;

		public object this[string variableName]
		{
			get
			{
				Match match = Regex.Match(variableName, "^([ifb])(\\d{2})$");
				if (match.Success)
					return match.Groups[1].Value switch
					{
						"i" => i[Conversions.ToInteger(match.Groups[2].Value)],
						"f" => f[Conversions.ToInteger(match.Groups[2].Value)],
						"b" => b[Conversions.ToInteger(match.Groups[2].Value)],
						_ => throw new NotImplementedException(),
					};
				return GetType().GetField(variableName)?.GetValue(this)!;
			}
			set
			{
				Match match = Regex.Match(variableName, "^([ifb])(\\d{2})$");
				if (match.Success)
				{
					string value2 = match.Groups[1].Value;
					if (value2 != "i")
					{
						if (value2 != "f")
						{
							if (value2 == "b")
							{
								b[Conversions.ToInteger(match.Groups[2].Value)] = Conversions.ToBoolean(value);
							}
						}
						else
						{
							f[Conversions.ToInteger(match.Groups[2].Value)] = Conversions.ToSingle(value);
						}
					}
					else
					{
						i[Conversions.ToInteger(match.Groups[2].Value)] = Conversions.ToInteger(value);
					}
				}
				else
				{
					FieldInfo field = GetType().GetField(variableName);
					if (field != null)
					{
						field.SetValue(this, RuntimeHelpers.GetObjectValue(value));
					}
				}
			}
		}
		/// <summary>
		/// Integer variables.
		/// </summary>
		public readonly LimitedList<int> i;
		/// <summary>
		/// Float variables.
		/// </summary>
		public readonly LimitedList<float> f;
		/// <summary>
		/// Boolean variables.
		/// </summary>
		public readonly LimitedList<bool> b;

		public int barNumber;

		public int buttonPressCount;

		public int missesToCrackHeart;

		public int numEarlyHits;

		public int numLateHits;

		public int numMisses;

		public int numPerfectHits;

		public float bpm;

		public float deltaTime;

		public float levelSpeed;

		public float numMistakes;

		public float numMistakesP1;

		public float numMistakesP2;

		public float shockwaveDistortionMultiplier;

		public float shockwaveDurationMultiplier;

		public float shockwaveSizeMultiplier;

		public float statusSignWidth;

		public bool activeDialogues;

		public bool activeDialoguesImmediately;

		public bool alternativeMatrix;

		public bool anyPlayerPress;

		public bool autoplay;

		public bool booleansDefaultToTrue;

		public bool charsOnlyOnStart;

		public bool cpuIsP2On2P;

		public bool disableRowChangeWarningFlashes;

		public bool downPress;

		public bool hideHandsOnStart;

		public bool invisibleChars;

		public bool invisibleHeart;

		public bool leftPress;

		public bool noBananaBeats;

		public bool noHands;

		public bool noHitFlashBorder;

		public bool noHitStrips;

		public bool noOneshotShadows;

		public bool noRowAnimsOnStart;

		public bool noSmartJudgment;

		public bool p1IsPressed;

		public bool p1Press;

		public bool p1Release;

		public bool p2IsPressed;

		public bool p2Press;

		public bool p2Release;

		public bool rightPress;

		public bool rotateShake;

		public bool rowReflectionsJumping;

		public bool skippableRankScreen;

		public bool skipRankText;

		public bool smoothShake;

		public bool upPress;

		public bool useFlashFontForFloatingText;

		public bool wobblyLines;
	}
}
