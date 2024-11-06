using Newtonsoft.Json.Linq;
using RhythmBase.Events;
namespace RhythmBase.Settings
{
	/// <summary>
	/// Level import settings.
	/// </summary>
	public class LevelReadOrWriteSettings
	{
		/// <summary>
		/// Initialize.
		/// </summary>
		public LevelReadOrWriteSettings() { }
		/// <summary>
		/// Enable resource preloading. This may grow read times. 
		/// Defaults to <see langword="false" />.
		/// </summary>
		public bool PreloadAssets { get; set; } = false;
		/// <summary>
		/// Action on inactive items on reads or writes.
		/// Defaults to <see cref="F:RhythmBase.Settings.InactiveEventsHandling.Retain" />.
		/// </summary>
		public InactiveEventsHandling InactiveEventsHandling { get; set; } = InactiveEventsHandling.Retain;
		/// <summary>
		/// Stores unreadable event data when the <see cref="P:RhythmBase.Settings.LevelReadOrWriteSettings.InactiveEventsHandling" /> is <see cref="F:RhythmBase.Settings.InactiveEventsHandling.Store" />.
		/// </summary>
		public List<BaseEvent> InactiveEvents { get; set; } = [];
		/// <summary>
		/// Action on unreadable events.
		/// Defaults to <see cref="F:RhythmBase.Settings.UnreadableEventHandling.ThrowException" />.
		/// </summary>
		public UnreadableEventHandling UnreadableEventsHandling { get; set; } = UnreadableEventHandling.ThrowException;
		/// <summary>
		/// Stores unreadable event data when the <see cref="P:RhythmBase.Settings.LevelReadOrWriteSettings.UnreadableEventsHandling" /> is <see cref="F:RhythmBase.Settings.UnreadableEventHandling.Store" />.
		/// </summary>
		/// <returns></returns>
		public List<JObject> UnreadableEvents { get; set; } = [];
		/// <summary>
		/// Use indentation. 
		/// Defaults to <see langword="true" />.
		/// </summary>
		public bool Indented { get; set; } = true;
	}
}
