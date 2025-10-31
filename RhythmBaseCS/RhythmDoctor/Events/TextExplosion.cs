﻿using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a text explosion event in a room.
	/// </summary>
	public class TextExplosion : BaseEvent, IRoomEvent, IColorEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TextExplosion"/> class.
		/// </summary>
		public TextExplosion() { }
		/// <summary>
		/// Gets or sets the rooms associated with the text explosion.
		/// </summary>
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <summary>
		/// Gets or sets the color of the text explosion.
		/// </summary>
		public PaletteColor Color { get; set; } = RDColor.Black;
		/// <summary>
		/// Gets or sets the text to be displayed in the explosion.
		/// </summary>
		public string Text { get; set; } = "";
		/// <summary>
		/// Gets or sets the speed value. The value is constrained to be at least 1.0.
		/// </summary>
		public float Speed { get; set => field = value > 1f ? value : 1f; } = 100f;
		/// <summary>
		/// Gets or sets the direction of the text explosion.
		/// </summary>
		public TextExplosionDirections Direction { get; set; } = TextExplosionDirections.Left;
		/// <summary>
		/// Gets or sets the mode of the text explosion.
		/// </summary>
		public TextExplosionModes Mode { get; set; } = TextExplosionModes.OneColor;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.TextExplosion;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Actions;
		/// <summary>
		/// Gets or sets the easing type.
		/// </summary>
		public EaseType Ease { get; set; }

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}", Text);
	}
	/// <summary>
	/// Specifies the direction of the text explosion.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum TextExplosionDirections
	{
		/// <summary>
		/// The text explodes to the left.
		/// </summary>
		Left,
		/// <summary>
		/// The text explodes to the right.
		/// </summary>
		Right
	}
	/// <summary>
	/// Specifies the mode of the text explosion.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum TextExplosionModes
	{
		/// <summary>
		/// The text explosion uses one color.
		/// </summary>
		OneColor,
		/// <summary>
		/// The text explosion uses random colors.
		/// </summary>
		Random
	}
}
