using Newtonsoft.Json;
using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents the presets.
	/// </summary>
	[RDJsonEnumSerializable]
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
		[RDJsonCondition($"$&.{nameof(Preset)} is not RhythmBase.RhythmDoctor.Events.WindowDancePresets.Move")]
		public SamePresetBehaviors SamePresetBehavior { get; set; }
		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		[EaseProperty]
		public RDPointE Position { get; set; }
		/// <summary>
		/// Gets or sets the reference.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Move")]
		public WindowDanceReferences Reference { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether to use a circle.
		/// </summary>
		[RDJsonCondition($"""
			$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Ellipse &&
			$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.SamePresetBehaviors.Reset
			""")]
		public bool UseCircle { get; set; }
		/// <summary>
		/// Gets or sets the speed.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"""
			$&.{nameof(Preset)}
				is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Wrap
				or RhythmBase.RhythmDoctor.Events.WindowDancePresets.Ellipse
				&&
			$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.SamePresetBehaviors.Reset
			""")]
		public float Speed { get; set; }
		/// <summary>
		/// Gets or sets the amplitude.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"""
			$&.{nameof(Preset)}
			is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Move
			or RhythmBase.RhythmDoctor.Events.WindowDancePresets.Sway
			""")]
		public float Amplitude { get; set; }
		/// <summary>
		/// Gets or sets the amplitude vector.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"""
			$&.{nameof(Preset)}
			is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Ellipse
			or RhythmBase.RhythmDoctor.Events.WindowDancePresets.ShakePer
			""")]
		public RDPointE AmplitudeVector { get; set; }
		/// <summary>
		/// Gets or sets the angle.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"$&.{nameof(Preset)} is not RhythmBase.RhythmDoctor.Events.WindowDancePresets.Move")]
		public float? Angle { get; set; }
		/// <summary>
		/// Gets or sets the frequency.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"""
			$&.{nameof(Preset)}
				is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Wrap
				or RhythmBase.RhythmDoctor.Events.WindowDancePresets.ShakePer
				&&
			$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.SamePresetBehaviors.Reset
			""")]
		public float Frequency { get; set; }
		/// <summary>
		/// Gets or sets the period.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"""
			$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.WindowDancePresets.ShakePer &&
			$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.SamePresetBehaviors.Reset
			""")]
		public float Period { get; set; }
		/// <summary>
		/// Gets or sets the ease type.
		/// </summary>
		[RDJsonCondition($"""
			$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Sway &&
			$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.SamePresetBehaviors.Reset
			""")]
		public WindowDanceEaseTypes EaseType { get; set; }
		/// <summary>
		/// Gets or sets the sub ease type.
		/// </summary>
		[RDJsonCondition($"""
			$&.{nameof(Preset)}
				is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Sway
				or RhythmBase.RhythmDoctor.Events.WindowDancePresets.ShakePer &&
			$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.SamePresetBehaviors.Reset
			""")]
		public EaseType SubEase { get; set; }
		/// <summary>
		/// Gets or sets the duration.
		/// </summary>
		[RDJsonProperty("easingDuration")]
		public float Duration { get; set; }
		/// <summary>
		/// Gets or sets the ease.
		/// </summary>
		public EaseType Ease { get; set; }
		/// <summary>
		/// Gets or sets the room.
		/// </summary>
		public RDRoom Rooms { get; set; }
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
	[RDJsonEnumSerializable]
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
	[RDJsonEnumSerializable]
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
	[RDJsonEnumSerializable]
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
