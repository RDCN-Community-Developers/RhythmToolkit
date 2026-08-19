using RhythmBase.Adofai.Serialization;
using System.Text.Json;
namespace RhythmBase.Adofai.Utils;

/// <summary>
/// Useful utils.
/// </summary>
public static class Utils
{
	/// <summary>  
	/// Represents the angle used for mid-spin calculations.  
	/// </summary>  
	public const float MidSpinAngle = 999f;
	/// <summary>
	/// Creates and configures a <see cref="JsonSerializerOptions"/> instance for serializing and deserializing JSON data.
	/// </summary>
	internal static MetadataJsonSerializerOptions GetJsonSerializerOptions(string? filepath = null, LevelReadSettings? settings = null)
	{
		settings ??= new LevelReadSettings();
		MetadataJsonSerializerOptions options = new() { JsonSerializerOptions = new() };
		options.ReadSettings = settings;
		options.DirectoryName = filepath;
		return options;
	}
	/// <summary>
	/// Creates and configures a <see cref="JsonSerializerOptions"/> instance for serializing and deserializing JSON data.
	/// </summary>
	internal static MetadataJsonSerializerOptions GetJsonSerializerOptions(string? filepath = null, LevelWriteSettings? settings = null)
	{
		settings ??= new LevelWriteSettings();
		MetadataJsonSerializerOptions options = new() { JsonSerializerOptions = new() };
		options.JsonSerializerOptions.WriteIndented = settings.WriteIndented;
		options.WriteSettings = settings;
		options.DirectoryName = filepath;
		return options;
	}
}
