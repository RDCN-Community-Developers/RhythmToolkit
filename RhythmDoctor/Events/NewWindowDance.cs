﻿using Newtonsoft.Json;
using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Events;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents the presets.
	/// </summary>
	public enum WindowDancePresets
	{
		/// <summary>
		/// Move preset.
		/// </summary>
		Move,
		/// <summary>
		/// Sway preset.
		/// </summary>
		Sway,
		/// <summary>
		/// Wrap preset.
		/// </summary>
		Wrap,
		/// <summary>
		/// Ellipse preset.
		/// </summary>
		Ellipse,
		/// <summary>
		/// Shake per preset.
		/// </summary>
		ShakePer
	}
	/// <summary>
	/// Represents a new window dance event.
	/// </summary>
	public class NewWindowDance : BaseEvent, IEaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NewWindowDance"/> class.
		/// </summary>
		public NewWindowDance()
		{
			Amplitude = 0f;
			Type = EventType.NewWindowDance;
			Tab = Tabs.Actions;
		}
		/// <summary>
		/// Gets or sets the preset.
		/// </summary>
		public WindowDancePresets Preset { get; set; }
		/// <summary>
		/// Gets or sets the same preset behavior.
		/// </summary>
		public SamePresetBehaviors SamePresetBehavior { get; set; }
		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		[EaseProperty]
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RDPointE Position { get; set; }
		/// <summary>
		/// Gets or sets the reference.
		/// </summary>
		public WindowDanceReferences Reference { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether to use a circle.
		/// </summary>
		public bool UseCircle { get; set; }
		/// <summary>
		/// Gets or sets the speed.
		/// </summary>
		[EaseProperty]
		public float Speed { get; set; }
		/// <summary>
		/// Gets or sets the amplitude.
		/// </summary>
		[EaseProperty]
		public float Amplitude { get; set; }
		/// <summary>
		/// Gets or sets the amplitude vector.
		/// </summary>
		[EaseProperty]
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RDPointE AmplitudeVector { get; set; }
		/// <summary>
		/// Gets or sets the angle.
		/// </summary>
		[EaseProperty]
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float? Angle { get; set; }
		/// <summary>
		/// Gets or sets the frequency.
		/// </summary>
		[EaseProperty]
		public float Frequency { get; set; }
		/// <summary>
		/// Gets or sets the period.
		/// </summary>
		[EaseProperty]
		public float Period { get; set; }
		/// <summary>
		/// Gets or sets the ease type.
		/// </summary>
		public WindowDanceEaseTypes EaseType { get; set; }
		/// <summary>
		/// Gets or sets the sub ease type.
		/// </summary>
		public EaseType SubEase { get; set; }
		/// <summary>
		/// Gets or sets the duration.
		/// </summary>
		[JsonProperty("easingDuration")]
		public float Duration { get; set; }
		/// <summary>
		/// Gets or sets the ease.
		/// </summary>
		public EaseType Ease { get; set; }
		/// <summary>
		/// Gets the event type.
		/// </summary>
		public override EventType Type { get; }
		/// <summary>
		/// Gets the tab.
		/// </summary>
		public override Tabs Tab { get; }
	}
	/// <summary>
	/// Represents the same preset behaviors.
	/// </summary>
	public enum SamePresetBehaviors
	{
		/// <summary>
		/// Reset behavior.
		/// </summary>
		Reset,
		/// <summary>
		/// Keep behavior.
		/// </summary>
		Keep
	}
	/// <summary>
	/// Represents the references.
	/// </summary>
	public enum WindowDanceReferences
	{
		/// <summary>
		/// Center reference.
		/// </summary>
		Center,
		/// <summary>
		/// Edge reference.
		/// </summary>
		Edge
	}
	/// <summary>
	/// Represents the ease types.
	/// </summary>
	public enum WindowDanceEaseTypes
	{
		/// <summary>
		/// Repeat ease type.
		/// </summary>
		Repeat,
		/// <summary>
		/// Mirror ease type.
		/// </summary>
		Mirror,
	}
}
