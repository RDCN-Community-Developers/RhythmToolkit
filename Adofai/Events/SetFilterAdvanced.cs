using RhythmBase.Global.Components.Easing;
using System.Text.Json;

namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents an advanced filter event in the Adofai event system.
	/// </summary>
	public class SetFilterAdvanced : BaseTaggedTileAction, IEaseEvent, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.SetFilterAdvanced;

		/// <summary>
		/// Gets or sets the name of the filter to apply.
		/// </summary>
		public string Filter { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the filter is enabled.
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether other filters should be disabled.
		/// </summary>
		public bool DisableOthers { get; set; }

		/// <summary>
		/// Gets or sets the duration of the filter effect in seconds.
		/// </summary>
		public float Duration { get; set; }

		/// <summary>
		/// Gets or sets the easing type for the filter effect.
		/// </summary>
		public EaseType Ease { get; set; }

		/// <summary>
		/// Gets or sets the target type for the filter effect.
		/// </summary>
		public TargetTypes TargetType { get; set; }

		/// <summary>
		/// Gets or sets the plane (background or foreground) where the filter is applied.
		/// </summary>
		public Plane Plane { get; set; } = Plane.Background;

		/// <summary>
		/// Gets or sets the target tag for the filter effect.
		/// </summary>
		public string TargetTag { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the properties of the filter as a JSON object.
		/// </summary>
		public JsonDocument[] FilterProperties { get; set; } = [];
		/// <summary>
		/// Represents the target type for the filter effect.
		/// </summary>
		[RDJsonEnumSerializable]
		public enum TargetTypes
		{
			/// <summary>
			/// The target is the camera.
			/// </summary>
			Camera,
		}
	}
	/// <summary>
	/// Represents the plane where the filter is applied.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum Plane
	{
		/// <summary>
		/// The background plane.
		/// </summary>
		Background,

		/// <summary>
		/// The foreground plane.
		/// </summary>
		Foreground,
	}
}
