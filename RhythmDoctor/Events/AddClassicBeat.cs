﻿using RhythmBase.RhythmDoctor.Extensions;
using System.Diagnostics;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to add a classic beat.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	//[RDJsonObjectNotSerializable]
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
		[RDJsonCondition("$&.Swing is not 0.5f and not 0f")]
		public float Swing { get; set; }
		/// <summary>
		/// Gets or sets the hold value.
		/// </summary>
		[RDJsonCondition("$&.Hold != 0f")]
		public float Hold { get; set; }

		/// <summary>  
		/// Gets or sets the number of beats in a classic beat pattern.  
		/// </summary>   
		[RDJsonCondition("false")]
		public ushort Length { get; set; } = 7;

		/// <summary>
		/// Gets or sets the classic beat pattern.
		/// </summary>
		//[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		[RDJsonCondition("$&.SetXs is not null")]
		public ClassicBeatPatterns? SetXs { get; set; }
		/// <inheritdoc/>
		public override EventType Type => EventType.AddClassicBeat;
		/// <inheritdoc/>
		public override string ToString() => base.ToString() +
			$" {Utils.Utils.GetPatternString(this.RowXs())} {((Swing is 0.5f or 0f) ? "" : " Swing")}";
		private string GetDebuggerDisplay() => ToString();
	}
	/// <summary>
	/// Defines the classic beat patterns.
	/// </summary>
	[RDJsonEnumSerializable]
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
}
