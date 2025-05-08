using System.Diagnostics;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to add a free time beat.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public class AddFreeTimeBeat : BaseBeat
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AddFreeTimeBeat"/> class.
		/// </summary>
		public AddFreeTimeBeat() { }
		/// <summary>
		/// Gets or sets the hold duration of the beat.
		/// </summary>
		public float Hold { get; set; }
		/// <summary>
		/// Gets or sets the pulse value of the beat.
		/// </summary>
		public byte Pulse { get; set; }
		/// <inheritdoc/>
		public override EventType Type => EventType.AddFreeTimeBeat;
		/// <inheritdoc/>
		public override string ToString() => base.ToString() + $" {Pulse + 1}";
		private string GetDebuggerDisplay() => ToString();
	}
}
