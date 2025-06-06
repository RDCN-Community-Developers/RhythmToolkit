﻿using System.Diagnostics;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to add a one-shot beat.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
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
		public byte Subdivisions { get; set; } = 1;
		/// <summary>
		/// Gets or sets a value indicating whether the subdivision sound is enabled.
		/// </summary>
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
		public float Interval { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether to skip the shot.
		/// </summary>
		public bool Skipshot { get; set; }
		/// <summary>
		/// Gets or sets the freeze burn mode.
		/// </summary>
		public OneshotTypes? FreezeBurnMode { get; set; }
		/// <summary>
		/// Gets or sets the delay value.
		/// </summary>
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
