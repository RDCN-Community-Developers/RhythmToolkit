using System.Reflection;
namespace RhythmBase.Components
{
	/// <summary>
	/// Variables.
	/// </summary>
	public sealed class RDVariables
	{
		/// <summary>
		/// Enumeration representing different ranks.
		/// </summary>
		public enum RDRank
		{
			/// <summary>
			/// S+ rank.
			/// </summary>
			SPlus,
			/// <summary>
			/// S rank.
			/// </summary>
			S,
			/// <summary>
			/// A rank.
			/// </summary>
			A,
			/// <summary>
			/// B rank.
			/// </summary>
			B,
			/// <summary>
			/// C rank.
			/// </summary>
			C,
			/// <summary>
			/// D rank.
			/// </summary>
			D,
			/// <summary>
			/// F rank.
			/// </summary>
			F,
		}
#pragma warning disable CS1591
#pragma warning disable IDE1006
		public RDVariables()
		{
			i = new int[10];
			f = new float[10];
			b = new bool[10];
		}
		public static int Rand(int value) =>
			(value > 0
				? Random.Shared.Next(1, value)
				: -Random.Shared.Next(1, -value)
			) + 1;
		public static bool atLeastRank(string rank) => rank switch
		{
			"S+" => SimulateCurrentRank <= RDRank.SPlus,
			"S" => SimulateCurrentRank <= RDRank.S,
			"A" => SimulateCurrentRank <= RDRank.A,
			"B" => SimulateCurrentRank <= RDRank.B,
			"C" => SimulateCurrentRank <= RDRank.C,
			"D" => SimulateCurrentRank <= RDRank.D,
			"F" => SimulateCurrentRank <= RDRank.F,
			_ => throw new ArgumentException("Invalid rank"),
		};
		public static bool atLeastNPerfects(int hitsToCheck, int numberOfPerfects) => numberOfPerfects / (float)hitsToCheck > SimulateAtLeastNPerfectsSuccessRate;
		public object this[string variableName]
		{
			get => variableName switch
			{
			['i', char ii] => i[ii - '0'],
			['f', char fi] => f[fi - '0'],
			['b', char bi] => b[bi - '0'],
				_ => GetType().GetField(variableName)?.GetValue(this)!,
			};
			set
			{
				switch (variableName)
				{
					case ['i', char ii]:
						i[ii - '0'] = value is int v1 ? v1 : throw new ArgumentException("Value is not an integer.");
						break;
					case ['f', char fi]:
						f[fi] = value is float v2 ? v2 : throw new ArgumentException("Value is not a float.");
						break;
					case ['b', char bi]:
						b[bi] = value is bool v3 ? v3 : throw new ArgumentException("Value is not a boolean.");
						break;
					default:
						FieldInfo? field = GetType().GetField(variableName);
						field?.SetValue(this, value);
						break;				}
			}
		}
		/// <summary>
		/// Integer variables.
		/// </summary>
		public readonly int[] i;
		/// <summary>
		/// Float variables.
		/// </summary>
		public readonly float[] f;
		/// <summary>
		/// Boolean variables.
		/// </summary>
		public readonly bool[] b;		public int barNumber;		public int buttonPressCount;		public int missesToCrackHeart;		public int numEarlyHits;		public int numLateHits;		public int numMisses;		public int numPerfectHits;		public float bpm;		public float deltaTime;		public float levelSpeed;		public float numMistakes;		public float numMistakesP1;		public float numMistakesP2;		public float shockwaveDistortionMultiplier;		public float shockwaveDurationMultiplier;		public float shockwaveSizeMultiplier;		public float statusSignWidth;		public bool activeDialogues;		public bool activeDialoguesImmediately;		public bool alternativeMatrix;		public bool anyPlayerPress;		public bool autoplay;		public bool booleansDefaultToTrue;		public bool charsOnlyOnStart;		public bool cpuIsP2On2P;		public bool disableRowChangeWarningFlashes;		public bool downPress;		public bool hideHandsOnStart;		public bool invisibleChars;		public bool invisibleHeart;		public bool leftPress;		public bool noBananaBeats;		public bool noHands;		public bool noHitFlashBorder;		public bool noHitStrips;		public bool noOneshotShadows;		public bool noRowAnimsOnStart;		public bool noSmartJudgment;		public bool p1IsPressed;		public bool p1Press;		public bool p1Release;		public bool p2IsPressed;		public bool p2Press;		public bool p2Release;		public bool rightPress;		public bool rotateShake;		public bool rowReflectionsJumping;		public bool skippableRankScreen;		public bool skipRankText;		public bool smoothShake;		public bool upPress;		public bool useFlashFontForFloatingText;		public bool wobblyLines;
		public static float SimulateAtLeastNPerfectsSuccessRate { get; set; } = 0.9f;
		public static RDRank SimulateCurrentRank { get; set; } = RDRank.S;
	}
}
