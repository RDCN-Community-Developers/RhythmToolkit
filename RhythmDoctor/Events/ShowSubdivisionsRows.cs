using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Specifies the display mode for subdivision rows.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum ShowSubdivisionsRowsMode
	{
		/// <summary>
		/// Mini mode for displaying subdivision rows.
		/// </summary>
		Mini,
		/// <summary>
		/// Normal mode for displaying subdivision rows.
		/// </summary>
		Normal,
	}
	/// <summary>
	/// Represents an event that shows subdivision rows in Rhythm Doctor.
	/// </summary>
	public class ShowSubdivisionsRows : BaseEvent
	{
		/// <summary>
		/// Gets the event type for this event.
		/// </summary>
		public override EventType Type => EventType.ShowSubdivisionsRows;

		/// <summary>
		/// Gets the tab to which this event belongs.
		/// </summary>
		public override Tabs Tab => Tabs.Actions;

		/// <summary>
		/// Gets or sets the number of subdivisions to display.
		/// </summary>
		public int Subdivisions { get; set; } = 1;

		/// <summary>
		/// Gets or sets the arc angle for the subdivision rows.
		/// Only used if not null.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(ArcAngle)} is not null")]
		public int? ArcAngle { get; set; } = null;

		/// <summary>
		/// Gets or sets the spin speed per second for the subdivision rows.
		/// </summary>
		public float SpinPerSecond { get; set; } = -100f;

		/// <summary>
		/// Gets or sets the display mode for the subdivision rows.
		/// </summary>
		public ShowSubdivisionsRowsMode Mode { get; set; } = ShowSubdivisionsRowsMode.Mini;
	}
}
