using System.Diagnostics;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to add a one-shot beat.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	//[RDJsonObjectNotSerializable]
	public class AddOneshotBeat : BaseBeat
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AddOneshotBeat"/> class.
		/// </summary>
		public AddOneshotBeat() { }
		/// <summary>
		/// Gets or sets the type of pulse.
		/// </summary>
		public OneshotPulseShapeTypes PulseType { get; set; }
		/// <summary>
		/// Gets or sets the number of subdivisions.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(PulseType)} is not RhythmBase.RhythmDoctor.Events.{nameof(OneshotPulseShapeTypes)}.{nameof(OneshotPulseShapeTypes.Wave)}")]
		public byte Subdivisions { get; set; } = 1;
		/// <summary>
		/// Gets or sets a value indicating whether the subdivision sound is enabled.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(PulseType)} is not RhythmBase.RhythmDoctor.Events.{nameof(OneshotPulseShapeTypes)}.{nameof(OneshotPulseShapeTypes.Wave)}")]
		public bool SubdivSound { get; set; }
		/// <summary>
		/// Gets or sets the tick value.
		/// </summary>
		public float Tick { get; set; } = 1f;
		/// <summary>
		/// Gets or sets the number of loops.
		/// </summary>
		public uint Loops { get; set; }
		/// <summary>
		/// Gets or sets the interval value.
		/// </summary>
		[RDJsonCondition($"""
			$&.{nameof(Skipshot)} ||
			$&.{nameof(FreezeBurnMode)} is not RhythmBase.RhythmDoctor.Events.{nameof(OneshotTypes)}.{nameof(OneshotTypes.Wave)}
			""")]
		public float Interval { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether to skip the shot.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(Skipshot)}")]
		public bool Skipshot { get; set; }

		/// <summary>  
		/// Gets or sets a value indicating whether the hold action is enabled.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(Hold)}")]
		public bool Hold { get; set; } = false;

		/// <summary>  
		/// Gets or sets a value indicating whether a sudden hold cue is triggered.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(Hold)}")]
		public bool SuddenHoldCue { get; set; } = false;

		/// <summary>
		/// Gets or sets the freeze burn mode.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(FreezeBurnMode)} is not null")]
		public OneshotTypes? FreezeBurnMode { get; set; }
		/// <summary>
		/// Gets or sets the delay value.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(FreezeBurnMode)} == RhythmBase.RhythmDoctor.Events.OneshotTypes.Freezeshot")]
		public float Delay
		{
			get => _delay;
			set => _delay = FreezeBurnMode != OneshotTypes.Freezeshot
					? 0f : value <= 0f
					? 0.5f : value;
		}
		/// <inheritdoc/>
		public override EventType Type => EventType.AddOneshotBeat;
		/// <inheritdoc/>
		public override string ToString() => base.ToString() + $" {FreezeBurnMode} {PulseType}";
		private float _delay = 0f;
		private string GetDebuggerDisplay() => ToString();
	}
	/// <summary>
	/// Represents the freeze burn mode.
	/// </summary>
	/// <remarks>
	/// The freeze burn mode determines the effect applied to the beat.
	/// </remarks>
	[RDJsonEnumSerializable]
	public enum OneshotTypes
	{
		/// <summary>
		/// A wave freeze burn mode.
		/// </summary>
		Wave,
		/// <summary>
		/// A freeze shot mode.
		/// </summary>
		Freezeshot,
		/// <summary>
		/// A burn shot mode.
		/// </summary>
		Burnshot
	}
	/// <summary>
	/// Represents the type of pulse.
	/// </summary>
	/// <remarks>
	/// The pulse type determines the shape of the beat's waveform.
	/// </remarks>
	[RDJsonEnumSerializable]
	public enum OneshotPulseShapeTypes
	{
		/// <summary>
		/// A wave pulse.
		/// </summary>
		Wave,
		/// <summary>
		/// A square pulse.
		/// </summary>
		Square,
		/// <summary>
		/// A triangle pulse.
		/// </summary>
		Triangle,
		/// <summary>
		/// A heart-shaped pulse.
		/// </summary>
		Heart
	}
}
