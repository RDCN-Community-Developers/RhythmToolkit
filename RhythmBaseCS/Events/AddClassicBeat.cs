using Newtonsoft.Json;
using RhythmBase.Extensions;
using System.Diagnostics;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event to add a classic beat.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public class AddClassicBeat : BaseBeat
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AddClassicBeat"/> class.
		/// </summary>
		public AddClassicBeat() { }

		/// <summary>
		/// Gets or sets the tick value.
		/// </summary>
		public float Tick { get; set; } = 1f;

		/// <summary>
		/// Gets or sets the swing value.
		/// </summary>
		public float Swing { get; set; }

		/// <summary>
		/// Gets or sets the hold value.
		/// </summary>
		public float Hold { get; set; }

		/// <summary>
		/// Gets or sets the classic beat pattern.
		/// </summary>
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public ClassicBeatPatterns? SetXs { get; set; }
		/// <inheritdoc/>
		public override EventType Type { get; } = EventType.AddClassicBeat;
		/// <inheritdoc/>
		public override string ToString() => base.ToString() +
			$" {Utils.Utils.GetPatternString(this.RowXs())} {((Swing is 0.5f or 0f) ? "" : " Swing")}";

		/// <summary>
		/// Defines the classic beat patterns.
		/// </summary>
		public enum ClassicBeatPatterns
		{
			/// <summary>
			/// No change in the beat pattern.
			/// </summary>
			NoChange,

			/// <summary>
			/// Three beat pattern.
			/// </summary>
			ThreeBeat,

			/// <summary>
			/// Four beat pattern.
			/// </summary>
			FourBeat
		}
		private string GetDebuggerDisplay() => ToString();
	}
}
