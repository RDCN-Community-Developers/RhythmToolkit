using RhythmBase.Adofai.Converters;
using RhythmBase.Global.Components.Easing;
using System.Text.Json;

namespace RhythmBase.Adofai.Events;

/// <summary>
/// Represents an advanced filter event in the Adofai event system.
/// </summary>
public class SetFilterAdvanced : BaseTaggedTileEvent, IDurationEvent, IBeginningEvent
{
#pragma warning disable CS8618
	/// <inheritdoc/>
	public override EventType Type => EventType.SetFilterAdvanced;

	/// <summary>
	/// Gets or sets the name of the filter to apply.
	/// </summary>
	public string Filter { get; set; } = "CameraFilterPack_AAA_SuperComputer";
	/// <summary>
	/// Gets or sets a value indicating whether the filter is enabled.
	/// </summary>
	public bool Enabled { get; set; } = true;
	/// <summary>
	/// Gets or sets a value indicating whether other filters should be disabled.
	/// </summary>
	public bool DisableOthers { get; set; }
	/// <summary>
	/// Gets or sets the duration of the filter effect in seconds.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Enabled)}")]
	public float Duration { get; set; }
	/// <summary>
	/// Gets or sets the easing type for the filter effect.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Enabled)}")]
	public EaseType Ease { get; set; }
	/// <summary>
	/// Gets or sets the target type for the filter effect.
	/// </summary>
	public TargetType TargetType { get; set; }
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
	[RDJsonConverter(typeof(JsonContentConverter))]
	public JsonElement FilterProperties { get; set; } = new();
}
/// <summary>
/// Represents the target type for the filter effect.
/// </summary>
[RDJsonEnumSerializable]
public enum TargetType
{
	/// <summary>
	/// The target is the camera.
	/// </summary>
	Camera,
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
