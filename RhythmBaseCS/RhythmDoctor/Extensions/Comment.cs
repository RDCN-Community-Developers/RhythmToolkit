using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using static RhythmBase.RhythmDoctor.Extensions.Extensions;

namespace RhythmBase.RhythmDoctor.Events
{
	public partial class Comment
	{
		/// <summary>
		/// Contains a series of default custom method implementations.
		/// </summary>
		public static class Shared
		{
#pragma warning disable CS1591
			private static Comment FunctionCalling(string name, params object[] @params) => new() { Text = $"()=>{name.ToLowerCamelCase()}({string.Join(',', @params.Select(i => i.ToString()))})" };
			public static Comment TrueCameraMove(int RoomID, RDPointN p, float AnimationDuration, EaseType Ease) => FunctionCalling("TrueCameraMove", (byte)RoomID, p.X, p.Y, AnimationDuration, Ease);
			public static Comment Create(Particle particleName, RDPointN p) => FunctionCalling("Create", $"CustomParticles/{particleName}", p.X, p.Y);
			public static Comment Shockwave(ShockWaveType type, float value) => FunctionCalling("Shockwave", type, value);
			public static Comment WavyRowsAmplitude(byte roomID, float amplitude, float duration) => FunctionCalling("WavyRowsAmplitude", roomID, amplitude, duration);
		}
	}
}
