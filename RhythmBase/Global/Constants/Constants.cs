namespace RhythmBase.Global.Constants;
/// <summary>
/// Provides a collection of constants used throughout the application for versioning and compatibility checks.
/// </summary>
/// <remarks>These constants define the minimum and default supported versions for A Dance of Fire and Ice
/// (Adofai) and Rhythm Doctor, ensuring that the application can validate and manage compatibility with these formats
/// effectively.</remarks>
public static partial class Constants
{
	/// <summary>
	/// Represents the minimum supported version of the A Dance of Fire and Ice (ADOFAI) game required by this application.
	/// </summary>
	public const int MinimumSupportedVersionAdofai = 15;

	/// <summary>
	/// The default Adofai format version assumed when creating or exporting content.
	/// </summary>
	public const int DefaultVersionAdofai = 15;

	/// <summary>
	/// Represents the minimum supported version of the Rhythm Doctor application required for compatibility.
	/// </summary>
	public const int MinimumSupportedVersionRhythmDoctor = 54;

	/// <summary>
	/// The default Rhythm Doctor version used when no explicit target version is specified.
	/// </summary>
	public const int DefaultVersionRhythmDoctor = 66;

	/// <summary>
	/// Represents the tolerance level used for floating-point comparisons.
	/// </summary>
	public const float Tolerance = 0.00001f;
}
