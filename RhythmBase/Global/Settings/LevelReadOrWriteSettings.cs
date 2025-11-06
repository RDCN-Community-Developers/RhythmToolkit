using System.Text.Json;
namespace RhythmBase.Global.Settings
{
	/// <summary>
	/// Specifies the method used to process files within a ZIP archive.
	/// </summary>
	/// <remarks>This enumeration defines the scope of file processing when working with ZIP archives.  Use <see
	/// cref="LevelFileOnly"/> to process only the top-level file, or <see cref="AllFiles"/>  to process all files within
	/// the archive, including nested files.</remarks>
	public enum ZipFileProcessMethod
	{
		/// <summary>
		/// Specifies that logging should be restricted to level-specific files only,  without writing to a general 
		/// file.
		/// </summary>
		LevelFileOnly,
		/// <summary>
		/// Represents a collection of all files in the current context.
		/// </summary>
		AllFiles,
	}
	/// <summary>
	/// Level import settings.
	/// </summary>
	public class LevelReadOrWriteSettings
	{
		/// <summary>
		/// Event triggered before reading.
		/// </summary>
		public event EventHandler BeforeReading;
		/// <summary>
		/// Event triggered after reading.
		/// </summary>
		public event EventHandler AfterReading;
		/// <summary>
		/// Event triggered before writing.
		/// </summary>
		public event EventHandler BeforeWriting;
		/// <summary>
		/// Event triggered after writing.
		/// </summary>
		public event EventHandler AfterWriting;
		/// <summary>
		/// Initialize.
		/// </summary>
		public LevelReadOrWriteSettings()
		{
			BeforeReading = delegate { };
			AfterReading = delegate { };
			BeforeWriting = delegate { };
			AfterWriting = delegate { };
		}
		/// <summary>
		/// Enable resource preloading. This may grow read times. 
		/// Defaults to <see langword="false" />.
		/// </summary>
		public bool PreloadAssets { get; set; } = false;
		/// <summary>
		/// Action on inactive items on reads or writes.
		/// Defaults to <see cref="F:RhythmBase.Global.Settings.InactiveEventsHandling.Retain" />.
		/// </summary>
		public InactiveEventsHandling InactiveEventsHandling { get; set; } = InactiveEventsHandling.Retain;
		/// <summary>
		/// Stores unreadable event data when the <see cref="P:RhythmBase.Global.Settings.LevelReadOrWriteSettings.InactiveEventsHandling" /> is <see cref="F:RhythmBase.Global.Settings.InactiveEventsHandling.Store" />.
		/// </summary>
		public List<IEvent> InactiveEvents { get; set; } = [];
		/// <summary>
		/// Action on unreadable events.
		/// Defaults to <see cref="F:RhythmBase.Global.Settings.UnreadableEventHandling.ThrowException" />.
		/// </summary>
		public UnreadableEventHandling UnreadableEventsHandling { get; set; } = UnreadableEventHandling.ThrowException;
		/// <summary>
		/// Gets or sets the method used to process zip files.
		/// </summary>
		public ZipFileProcessMethod ZipFileProcessMethod { get; set; } = ZipFileProcessMethod.LevelFileOnly;
		/// <summary>
		/// Stores unreadable event data when the <see cref="P:RhythmBase.Global.Settings.LevelReadOrWriteSettings.UnreadableEventsHandling" /> is <see cref="F:RhythmBase.Global.Settings.UnreadableEventHandling.Store" />.
		/// </summary>
		/// <returns></returns>
		public List<(JsonElement item, string reason)> UnreadableEvents { get; set; } = [];
		internal bool HandleInactiveEvent(IEvent item)
		{
			switch (InactiveEventsHandling)
			{
				case InactiveEventsHandling.Store:
					InactiveEvents.Add(item);
					break;
				case InactiveEventsHandling.Retain:
					return false;
			}
			return true;
		}
		internal void HandleUnreadableEvent(JsonElement item, string reason)
		{
			switch (UnreadableEventsHandling)
			{
				case UnreadableEventHandling.ThrowException:
					throw new InvalidOperationException($"Unreadable event: {reason}");
				case UnreadableEventHandling.Store:
					UnreadableEvents.Add((item, reason));
					break;
			}
		}
		/// <summary>  
		/// Indicates whether the level elements' associated information are interlinked.  
		/// Defaults to <see langword="true" />.  
		/// </summary>  
		public bool Linked { get; set; } = true;
		/// <summary>
		/// Use indentation. 
		/// Defaults to <see langword="true" />.
		/// </summary>
		public bool Indented { get; set; } = true;
		/// <summary>  
		/// Indicates whether group events are enabled.  
		/// Defaults to <see langword="false" />.  
		/// </summary>  
		public bool EnableMacroEvent { get; set; } = false;
		internal void OnBeforeReading()
		{
			BeforeReading?.Invoke(this, EventArgs.Empty);
		}
		internal void OnAfterReading()
		{
			AfterReading?.Invoke(this, EventArgs.Empty);
		}
		internal void OnBeforeWriting()
		{
			BeforeWriting?.Invoke(this, EventArgs.Empty);
		}
		internal void OnAfterWriting()
		{
			AfterWriting?.Invoke(this, EventArgs.Empty);
		}
	}
}
