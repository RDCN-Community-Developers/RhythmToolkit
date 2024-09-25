using RhythmBase.Components;
using System.Reflection;
using static RhythmBase.Extensions.Extensions;

namespace RhythmBase.Events
{
	public partial class CallCustomMethod
	{
		public static class Shared
		{
			public static CallCustomMethod PropertyAssignment(string propertyName, bool value) => new() { MethodName = $"{propertyName.ToLowerCamelCase()} = {value}" };
			public static CallCustomMethod RoomPropertyAssignment(byte room, string propertyName, bool value) => new() { MethodName = $"room[{room}].{propertyName.ToLowerCamelCase()} = {value}" };
			public static CallCustomMethod FunctionCalling(string functionName, params object[] @params) => new() { MethodName = $"{functionName}({ArgumentCombining(@params)})" };
			private static string ArgumentCombining(params object[] @params) => $"{string.Join(", ", @params.Select(i =>
																									 i.GetType() == typeof(string) || i.GetType().IsEnum
																										 ? $"str:{i}"
																										 : i.ToString() ?? ""))}";
			public static CallCustomMethod RoomFunctionCalling(byte room, string functionName, params object[] @params) => new() { MethodName = $"room[{room}].{functionName}({ArgumentCombining(@params)})" };
			private static CallCustomMethod VfxFunctionCalling(string functionName, params object[] @params) => new() { MethodName = $"vfx.{functionName}({ArgumentCombining(@params)})" };
			public static CallCustomMethod SetScoreboardLights(bool Mode, string Text) => FunctionCalling(nameof(SetScoreboardLights), Mode, Text);
			public static CallCustomMethod InvisibleChars(bool value) => PropertyAssignment(nameof(InvisibleChars), value);
			public static CallCustomMethod InvisibleHeart(bool value) => PropertyAssignment(nameof(InvisibleHeart), value);
			public static CallCustomMethod NoHitFlashBorder(bool value) => PropertyAssignment(nameof(NoHitFlashBorder), value);
			public static CallCustomMethod NoHitStrips(bool value) => PropertyAssignment(nameof(NoHitStrips), value);
			public static CallCustomMethod SetOneshotType(int rowID, ShockWaveType wavetype) => FunctionCalling(nameof(SetOneshotType), rowID, wavetype);
			public static CallCustomMethod WobblyLines(bool value) => PropertyAssignment(nameof(WobblyLines), value);
			public static CallCustomMethod ShockwaveSizeMultiplier(bool value) => PropertyAssignment(nameof(ShockwaveSizeMultiplier), value);
			public static CallCustomMethod ShockwaveDistortionMultiplier(bool value) => PropertyAssignment(nameof(ShockwaveDistortionMultiplier), value);
			public static CallCustomMethod ShockwaveDurationMultiplier(bool value) => PropertyAssignment(nameof(ShockwaveDurationMultiplier), value);
			public static CallCustomMethod MistakeOrHeal(float damageOrHeal) => FunctionCalling(nameof(MistakeOrHeal), damageOrHeal);
			public static CallCustomMethod MistakeOrHealP1(float damageOrHeal) => FunctionCalling(nameof(MistakeOrHealP1), damageOrHeal);
			public static CallCustomMethod MistakeOrHealP2(float damageOrHeal) => FunctionCalling(nameof(MistakeOrHealP2), damageOrHeal);
			public static CallCustomMethod MistakeOrHealSilent(float damageOrHeal) => FunctionCalling(nameof(MistakeOrHealSilent), damageOrHeal);
			public static CallCustomMethod MistakeOrHealP1Silent(float damageOrHeal) => FunctionCalling(nameof(MistakeOrHealP1Silent), damageOrHeal);
			public static CallCustomMethod MistakeOrHealP2Silent(float damageOrHeal) => FunctionCalling(nameof(MistakeOrHealP2Silent), damageOrHeal);
			public static CallCustomMethod SetMistakeWeight(int rowID, float weight) => FunctionCalling(nameof(SetMistakeWeight), rowID, weight);
			public static CallCustomMethod DamageHeart(int rowID, float damage) => FunctionCalling(nameof(DamageHeart), rowID, damage);
			public static CallCustomMethod HealHeart(int rowID, float damage) => FunctionCalling(nameof(HealHeart), rowID, damage);
			public static CallCustomMethod WavyRowsAmplitude(byte roomID, float amplitude) => RoomPropertyAssignment(roomID, nameof(WavyRowsAmplitude), amplitude != 0f);
			public static CallCustomMethod WavyRowsFrequency(byte roomID, float frequency) => RoomPropertyAssignment(roomID, nameof(WavyRowsFrequency), frequency != 0f);
			public static CallCustomMethod SetShakeIntensityOnHit(byte roomID, int number, int strength) => RoomFunctionCalling(roomID, nameof(SetShakeIntensityOnHit), number, strength);
			public static CallCustomMethod ShowPlayerHand(byte roomID, bool isPlayer1, bool isShortArm, bool isInstant) => FunctionCalling(nameof(ShowPlayerHand), roomID, isPlayer1, isShortArm, isInstant);
			public static CallCustomMethod TintHandsWithInts(byte roomID, float R, float G, float B, float A) => FunctionCalling(nameof(TintHandsWithInts), roomID, R, G, B, A);
			public static CallCustomMethod SetHandsBorderColor(byte roomID, float R, float G, float B, float A, int style) => FunctionCalling(nameof(SetHandsBorderColor), roomID, R, G, B, A, style);
			public static CallCustomMethod SetAllHandsBorderColor(float R, float G, float B, float A, int style) => FunctionCalling(nameof(SetAllHandsBorderColor), R, G, B, A, style);
			public static CallCustomMethod SetHandToP1(int room, bool rightHand) => FunctionCalling(nameof(SetHandToP1), room, rightHand);
			public static CallCustomMethod SetHandToP2(int room, bool rightHand) => FunctionCalling(nameof(SetHandToP2), room, rightHand);
			public static CallCustomMethod SetHandToIan(int room, bool rightHand) => FunctionCalling(nameof(SetHandToIan), room, rightHand);
			public static CallCustomMethod SetHandToPaige(int room, bool rightHand) => FunctionCalling(nameof(SetHandToPaige), room, rightHand);
			public static CallCustomMethod SetShadowRow(int mimickerRowID, int mimickedRowID) => FunctionCalling(nameof(SetShadowRow), mimickerRowID, mimickedRowID);
			public static CallCustomMethod UnsetShadowRow(int mimickerRowID, int mimickedRowID) => FunctionCalling(nameof(UnsetShadowRow), mimickerRowID, mimickedRowID);
			public static CallCustomMethod ShakeCam(int number, int strength, int roomID) => VfxFunctionCalling(nameof(ShakeCam), number, strength, roomID);
			public static CallCustomMethod StopShakeCam(int roomID) => VfxFunctionCalling(nameof(StopShakeCam), roomID);
			public static CallCustomMethod ShakeCamSmooth(int duration, int strength, int roomID) => VfxFunctionCalling(nameof(ShakeCamSmooth), duration, strength, roomID);
			public static CallCustomMethod ShakeCamRotate(int duration, int strength, int roomID) => VfxFunctionCalling(nameof(ShakeCamRotate), duration, strength, roomID);
			public static CallCustomMethod SetKaleidoscopeColor(int roomID, float R1, float G1, float B1, float R2, float G2, float B2) => FunctionCalling(nameof(SetKaleidoscopeColor), roomID, R1, G1, B1, R2, G2, B2);
			public static CallCustomMethod SyncKaleidoscopes(int targetRoomID, int otherRoomID) => FunctionCalling(nameof(SyncKaleidoscopes), targetRoomID, otherRoomID);
			public static CallCustomMethod SetVignetteAlpha(float alpha, int roomID) => VfxFunctionCalling(nameof(SetVignetteAlpha), alpha, roomID);
			public static CallCustomMethod NoOneshotShadows(bool value) => PropertyAssignment(nameof(NoOneshotShadows), value);
			public static CallCustomMethod EnableRowReflections(int roomID) => FunctionCalling(nameof(EnableRowReflections), roomID);
			public static CallCustomMethod DisableRowReflections(int roomID) => FunctionCalling(nameof(DisableRowReflections), roomID);
			public static CallCustomMethod ChangeCharacter(string Name, int roomID) => FunctionCalling(nameof(ChangeCharacter), Name, roomID);
			public static CallCustomMethod ChangeCharacter(Characters Name, int roomID) => FunctionCalling(nameof(ChangeCharacter), Name, roomID);
			public static CallCustomMethod ChangeCharacterSmooth(string Name, int roomID) => FunctionCalling(nameof(ChangeCharacterSmooth), Name, roomID);
			public static CallCustomMethod ChangeCharacterSmooth(Characters Name, int roomID) => FunctionCalling(nameof(ChangeCharacterSmooth), Name, roomID);
			public static CallCustomMethod SmoothShake(bool value) => PropertyAssignment(nameof(SmoothShake), value);
			public static CallCustomMethod RotateShake(bool value) => PropertyAssignment(nameof(RotateShake), value);
			public static CallCustomMethod DisableRowChangeWarningFlashes(bool value) => PropertyAssignment(nameof(DisableRowChangeWarningFlashes), value);
			public static CallCustomMethod StatusSignWidth(float value) => PropertyAssignment(nameof(StatusSignWidth), value != 0f);
			public static CallCustomMethod SkippableRankScreen(bool value) => PropertyAssignment(nameof(SkippableRankScreen), value);
			public static CallCustomMethod MissesToCrackHeart(int value) => PropertyAssignment(nameof(MissesToCrackHeart), value != 0);
			public static CallCustomMethod SkipRankText(bool value) => PropertyAssignment(nameof(SkipRankText), value);
			public static CallCustomMethod AlternativeMatrix(bool value) => PropertyAssignment(nameof(AlternativeMatrix), value);
			public static CallCustomMethod ToggleSingleRowReflections(byte room, byte row, bool value) => FunctionCalling(nameof(ToggleSingleRowReflections), room, row, value);
			public static CallCustomMethod SetScrollSpeed(byte roomID, float speed, float duration, Ease.EaseType ease) => RoomFunctionCalling(roomID, nameof(SetScrollSpeed), speed, duration, ease);
			public static CallCustomMethod SetScrollOffset(byte roomID, float cameraOffset, float duration, Ease.EaseType ease) => RoomFunctionCalling(roomID, nameof(SetScrollOffset), cameraOffset, duration, ease);
			public static CallCustomMethod DarkenedRollerdisco(byte roomID, float value) => RoomFunctionCalling(roomID, nameof(DarkenedRollerdisco), value);
			public static CallCustomMethod CurrentSongVol(float targetVolume, float fadeTimeSeconds) => FunctionCalling(nameof(CurrentSongVol), targetVolume, fadeTimeSeconds);
			public static CallCustomMethod PreviousSongVol(float targetVolume, float fadeTimeSeconds) => FunctionCalling(nameof(PreviousSongVol), targetVolume, fadeTimeSeconds);
			public static CallCustomMethod EditTree(byte room, string property, float value, float beats, Ease.EaseType ease) => RoomFunctionCalling(room, nameof(EditTree), property, value, beats, ease);
			public static IEnumerable<CallCustomMethod> EditTree(byte room, ProceduralTree treeProperties, float beats, Ease.EaseType ease)
			{
				List<CallCustomMethod> L = [];
				foreach (FieldInfo p in typeof(ProceduralTree).GetFields())
				{
					float? value = (float?)p.GetValue(treeProperties);
					if (value != null)
						L.Add(EditTree(room, p.Name.ToLowerCamelCase(), (float)value, beats, ease));
				}
				return L;
			}
			public static CallCustomMethod EditTreeColor(byte room, bool location, string color, float beats, Ease.EaseType ease) => RoomFunctionCalling(room, nameof(EditTreeColor), location, color, beats, ease);
			public struct ProceduralTree
			{
				/// <summary>
				/// 过程树的属性
				/// </summary>
				public float?
					BrachedPerlteration,
					BranchesPerDivision,
					IterationsPerSecond,
					Thickness,
					TargetLength,
					MaxDeviation,
					Angle,
					CamAngle,
					CamDistance,
					CamDegreesPerSecond,
					CamSpeed,
					PulseIntensity,
					PulseRate,
					PulseWavelength;
			}
		}

	}
}
