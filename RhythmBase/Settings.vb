﻿Namespace Settings
	''' <summary>
	''' Actions performed on items with exceptions during reads.
	''' </summary>
	Public Enum UnreadableEventHandling
		''' <summary>
		''' Stores unreadable events in <see cref="LevelReadOrWriteSettings.UnreadableEvents"/> for restoration.
		''' </summary>
		Store
		''' <summary>
		''' An exception will be thrown.
		''' </summary>
		ThrowException
	End Enum
	''' <summary>
	''' Actions performed on inactive items at read or write times.
	''' </summary>
	Public Enum InactiveEventsHandling
		''' <summary>
		''' Retaining inactivated events to the level on reads.
		''' Write inactivated events on writes.
		''' </summary>
		Retain
		''' <summary>
		''' Dumps inactivated events to <see cref="LevelReadOrWriteSettings.InactiveEvents"/> on reads and writes.
		''' </summary>
		Store
		''' <summary>
		''' Ignore inactivation events on reads and writes.
		''' </summary>
		Ignore
	End Enum
	''' <summary>
	''' Level import settings.
	''' </summary>
	Public Class LevelReadOrWriteSettings
		''' <summary>
		''' Enable resource preloading. This may grow read times. 
		''' Defaults to <see langword="false"/>.
		''' </summary>
		Public Property PreloadAssets As Boolean = False
		''' <summary>
		''' Action on inactive items on reads or writes.
		''' Defaults to <see cref="InactiveEventsHandling.Retain"/>.
		''' </summary>
		Public Property InactiveEventsHandling As InactiveEventsHandling = InactiveEventsHandling.Retain
		''' <summary>
		''' Stores unreadable event data when the <see cref="LevelReadOrWriteSettings.InactiveEventsHandling"/> is <see cref="Settings.InactiveEventsHandling.Store"/>.
		''' </summary>
		Public Property InactiveEvents As New List(Of BaseEvent)
		''' <summary>
		''' Action on unreadable events.
		''' Defaults to <see cref="Settings.UnreadableEventHandling.ThrowException"/>.
		''' </summary>
		Public Property UnreadableEventsHandling As UnreadableEventHandling = UnreadableEventHandling.ThrowException
		''' <summary>
		''' Stores unreadable event data when the <see cref="LevelReadOrWriteSettings.UnreadableEventsHandling"/> is <see cref="Settings.UnreadableEventHandling.Store"/>.
		''' </summary>
		''' <returns></returns>
		Public Property UnreadableEvents As New List(Of Newtonsoft.Json.Linq.JObject)
		''' <summary>
		''' Use indentation. 
		''' Defaults to <see langword="true"/>.
		''' </summary>
		Public Property Indented As Boolean = True
	End Class
	Public Class SpriteReadOrWriteSettings
		''' <summary>
		''' Whether to overwrite the source file.
		''' If <see langword="false"/> and the export path is the same as the source path, an exception will be thrown.
		''' Defaults to <see langword="false"/>.
		''' </summary>
		Public Property OverWrite As Boolean = False
		''' <summary>
		''' Use the indent and align expressions property.
		''' Defaults to <see langword="true"/>.
		''' </summary>
		Public Property Indented As Boolean = True
		''' <summary>
		''' Ignore all null values.
		''' </summary>
		''' Defaults to <see langword="false"/>.
		Public Property IgnoreNullValue As Boolean = False
		''' <summary>
		''' Export the image file at the same time as the export.
		''' </summary>
		''' Defaults to <see langword="false"/>.
		Public Property WithImage As Boolean = False
	End Class
End Namespace