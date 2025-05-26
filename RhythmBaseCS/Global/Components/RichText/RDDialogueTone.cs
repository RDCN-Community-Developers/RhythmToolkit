using System.Diagnostics.CodeAnalysis;

namespace RhythmBase.Global.Components.RichText
{
	/// <summary>
	/// Enum representing the different types of rich string events.
	/// </summary>
	public enum RDDialogueToneType
	{
		/// <summary>
		/// Static event type.
		/// </summary>
		Static,
		/// <summary>
		/// Flash event type.
		/// </summary>
		Flash,
		/// <summary>
		/// Very slow event type.
		/// </summary>
		VerySlow,
		/// <summary>
		/// Slow event type.
		/// </summary>
		Slow,
		/// <summary>
		/// Normal event type.
		/// </summary>
		Normal,
		/// <summary>
		/// Fast event type.
		/// </summary>
		Fast,
		/// <summary>
		/// Very fast event type.
		/// </summary>
		VeryFast,
		/// <summary>
		/// Very very fast event type.
		/// </summary>
		VeryVeryFast,
		/// <summary>
		/// Excited event type.
		/// </summary>
		Excited,
		/// <summary>
		/// Shout event type.
		/// </summary>
		Shout,
		/// <summary>
		/// Shake event type.
		/// </summary>
		Shake,
		/// <summary>
		/// Pause event type.
		/// </summary>
		Pause,
	}
	/// <summary>
	/// Class representing a rich string event.
	/// </summary>
	/// <param name="Type">Rich string event type.</param>
	/// <param name="Index">Rich string event index.</param>
	public record struct RDDialogueTone(RDDialogueToneType Type, int Index)
	{
		/// <summary>
		/// Gets the pause duration for the dialogue event.
		/// </summary>
		public int? Pause
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
		/// <summary>
		/// Serializes the rich string event type to its corresponding string representation.
		/// </summary>
		/// <returns>A string representation of the rich string event type.</returns>
		/// <exception cref="NotImplementedException">Thrown when the event type is not implemented.</exception>
		public string Serialize() => "[" + Type switch
		{
			RDDialogueToneType.Static => "static",
			RDDialogueToneType.Flash => "flash",
			RDDialogueToneType.VerySlow => "vslow",
			RDDialogueToneType.Slow => "slow",
			RDDialogueToneType.Normal => "normal",
			RDDialogueToneType.Fast => "fast",
			RDDialogueToneType.VeryFast => "vfast",
			RDDialogueToneType.VeryVeryFast => "vvvfast",
			RDDialogueToneType.Excited => "excited",
			RDDialogueToneType.Shout => "shout",
			RDDialogueToneType.Shake => "shake",
			RDDialogueToneType.Pause => Pause?.ToString(),
			_ => throw new NotImplementedException(),
		} + "]";
		/// <summary>
		/// Creates a new instance of <see cref="RDDialogueTone"/> based on the provided type and index.
		/// </summary>
		/// <param name="type">The string representation of the event type.</param>
		/// <param name="index">The index of the event.</param>
		/// <param name="result">The created <see cref="RDDialogueTone"/> instance if successful, otherwise null.</param>
		/// <returns>True if the event was successfully created, otherwise false.</returns>
#if NETSTANDARD
		public static bool Create(string type, int index, out RDDialogueTone? result)
#else
		public static bool Create(string type, int index, [NotNullWhen(true)] out RDDialogueTone? result)
#endif
		{
			RDDialogueToneType? eventType = type switch
			{
				"static" => RDDialogueToneType.Static,
				"flash" => RDDialogueToneType.Flash,
				"vslow" => RDDialogueToneType.VerySlow,
				"slow" => RDDialogueToneType.Slow,
				"normal" => RDDialogueToneType.Normal,
				"fast" => RDDialogueToneType.Fast,
				"vfast" => RDDialogueToneType.VeryFast,
				"vvvfast" => RDDialogueToneType.VeryVeryFast,
				"excited" => RDDialogueToneType.Excited,
				"shout" => RDDialogueToneType.Shout,
				"shake" => RDDialogueToneType.Shake,
				_ => null,
			};
			if (eventType is null)
			{
				if (int.TryParse(type, out int pause))
				{
					result = new(RDDialogueToneType.Pause, index) { Pause = pause };
					return true;
				}
				result = null;
				return false;
			}
			result = new(eventType.Value, index);
			return true;
		}
	}
}