Imports Newtonsoft.Json
Imports RhythmBase.Adofai.Converters
Imports RhythmBase.Adofai.Events
Imports RhythmBase.Adofai.Utils
Imports SkiaSharp
Namespace Adofai
	Namespace Components
		Public Structure ADBeat
			Implements IComparable(Of ADBeat)
			Implements IEquatable(Of ADBeat)
			Friend _calculator As ADBeatCalculator
			Private _isBeatLoaded As Boolean
			Private _isTimeSpanLoaded As Boolean
#Disable Warning IDE0052
			Private _isBpmLoaded As Boolean
#Enable Warning
			Private _beat As Single
			Private _timeSpan As TimeSpan
			Private _bpm As Single
			Friend ReadOnly Property baseLevel As ADLevel
				Get
					Return _calculator.Collection
				End Get
			End Property
			Public Property BeatOnly As Single
				Get
					Return _beat + 1
				End Get
				Set(value As Single)

				End Set
			End Property
			Public Property TimeSpan As TimeSpan
				Get
					Return _timeSpan
				End Get
				Set(value As TimeSpan)

				End Set
			End Property
			Public Sub New(beat As Single)
				_beat = beat
				_isBeatLoaded = True
			End Sub
			Public Sub New(timeSpan As TimeSpan)
				_timeSpan = timeSpan
				_isTimeSpanLoaded = True
			End Sub
			Public Sub New(calculator As ADBeatCalculator, beat As Single)
				_calculator = calculator
				_beat = beat
				_isBeatLoaded = True
			End Sub
			Public Sub New(calculator As ADBeatCalculator, timeSpan As TimeSpan)
				If timeSpan < TimeSpan.Zero Then
					Throw New OverflowException($"The time must not be less than zero, but {timeSpan} is given")
				End If
				_calculator = calculator
				_timeSpan = timeSpan
				_isTimeSpanLoaded = True
			End Sub
			''' <summary>
			''' Construct a beat of the 1st beat from the calculator
			''' </summary>
			''' <param name="calculator">Specified calculator.</param>
			''' <returns>The first beat tied to the level.</returns>
			Public Shared Function [Default](calculator As ADBeatCalculator) As ADBeat
				Return New ADBeat(calculator, 1)
			End Function
			''' <summary>
			''' Determine if two beats come from the same level
			''' </summary>
			''' <param name="a">A beat.</param>
			''' <param name="b">Another beat.</param>
			''' <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
			''' <returns></returns>
			Public Shared Function FromSameLevel(a As ADBeat, b As ADBeat, Optional [throw] As Boolean = False) As Boolean
				If a.baseLevel.Equals(b.baseLevel) Then
					Return True
				Else
					If [throw] Then
						Throw New RhythmBaseException("Beats must come from the same ADLevel.")
					End If
					Return False
				End If
			End Function
			''' <summary>
			''' Determine if two beats are from the same level.
			''' <br/>
			''' If any of them does not come from any level, it will also return true.
			''' </summary>
			''' <param name="a">A beat.</param>
			''' <param name="b">Another beat.</param>
			''' <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
			''' <returns></returns>
			Public Shared Function FromSameLevelOrNull(a As ADBeat, b As ADBeat, Optional [throw] As Boolean = False) As Boolean
				Return a.baseLevel Is Nothing OrElse b.baseLevel Is Nothing OrElse FromSameLevel(a, b, [throw])
			End Function
			Public Function FromSameLevel(b As ADBeat, Optional [throw] As Boolean = False) As Boolean
				Return FromSameLevel(Me, b, [throw])
			End Function
			''' <summary>
			''' Determine if two beats are from the same level.
			''' <br/>
			''' If any of them does not come from any level, it will also return true.
			''' </summary>
			''' <param name="b">Another beat.</param>
			''' <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
			''' <returns></returns>	
			Public Function FromSameLevelOrNull(b As ADBeat, Optional [throw] As Boolean = False) As Boolean
				Return baseLevel Is Nothing OrElse b.baseLevel Is Nothing OrElse FromSameLevel(b, [throw])
			End Function
			''' <summary>
			''' Returns a new instance of unbinding the level.
			''' </summary>
			''' <returns>A new instance of unbinding the level.</returns>
			Public Function WithoutBinding() As ADBeat
				Dim result = Me
				result._calculator = Nothing
				Return result
			End Function
			Private Sub IfNullThrowException()
				If IsEmpty Then
					Throw New InvalidRDBeatException
				End If
			End Sub
			''' <summary>
			''' Refresh the cache.
			''' </summary>
			Public Sub ResetCache()
				Dim m = BeatOnly
				_isTimeSpanLoaded = False
			End Sub
			Friend Sub ResetBPM()
				'If Not _isBeatLoaded Then
				'	_beat = _calculator.TimeSpanToBeatOnly(_TimeSpan) - 1
				'End If
				_isBeatLoaded = True
				_isTimeSpanLoaded = False
				_isBpmLoaded = False
			End Sub
			Friend Sub ResetCPB()
				'If Not _isBeatLoaded Then
				'	_beat = _calculator.BarBeatToBeatOnly(_BarBeat.Bar, _BarBeat.Beat) - 1
				'End If
				_isBeatLoaded = True
			End Sub
			Public ReadOnly Property IsEmpty As Boolean
				Get
					Return _calculator Is Nothing OrElse Not (_isBeatLoaded OrElse _isTimeSpanLoaded)
				End Get
			End Property
			Public Shared Operator +(a As ADBeat, b As Single) As ADBeat
				Return New ADBeat(a._calculator, a.BeatOnly + b)
			End Operator
			Public Shared Operator +(a As ADBeat, b As TimeSpan) As ADBeat
				Return New ADBeat(a._calculator, a.TimeSpan + b)
			End Operator
			Public Shared Operator -(a As ADBeat, b As Single) As ADBeat
				Return New ADBeat(a._calculator, a.BeatOnly - b)
			End Operator
			Public Shared Operator -(a As ADBeat, b As TimeSpan) As ADBeat
				Return New ADBeat(a._calculator, a.TimeSpan - b)
			End Operator
			Public Shared Operator >(a As ADBeat, b As ADBeat) As Boolean
				Return FromSameLevel(a, b, True) AndAlso a.BeatOnly > b.BeatOnly
			End Operator
			Public Shared Operator <(a As ADBeat, b As ADBeat) As Boolean
				Return FromSameLevel(a, b, True) AndAlso a.BeatOnly < b.BeatOnly
			End Operator
			Public Shared Operator >=(a As ADBeat, b As ADBeat) As Boolean
				Return FromSameLevel(a, b, True) AndAlso a.BeatOnly >= b.BeatOnly
			End Operator
			Public Shared Operator <=(a As ADBeat, b As ADBeat) As Boolean
				Return FromSameLevel(a, b, True) AndAlso a.BeatOnly <= b.BeatOnly
			End Operator
			Public Shared Operator =(a As ADBeat, b As ADBeat) As Boolean
				Return FromSameLevel(a, b, True) AndAlso
							(a._beat = b._beat) OrElse
							(a._isTimeSpanLoaded AndAlso b._isTimeSpanLoaded AndAlso a._timeSpan = b._timeSpan) OrElse
							(a.BeatOnly = b.BeatOnly)
			End Operator
			Public Shared Operator <>(a As ADBeat, b As ADBeat) As Boolean
				Return Not a = b
			End Operator
			Public Function CompareTo(other As ADBeat) As Integer Implements IComparable(Of ADBeat).CompareTo
				Return _beat - other._beat
			End Function
			Public Overrides Function ToString() As String
				Return $"[{BeatOnly}]"
			End Function
			Public Overrides Function Equals(<CodeAnalysis.NotNull> obj As Object) As Boolean
				Return obj.GetType = GetType(ADBeat) AndAlso Equals(CType(obj, ADBeat))
			End Function
			Public Overloads Function Equals(other As ADBeat) As Boolean Implements IEquatable(Of ADBeat).Equals
				Return Me = other
			End Function
			Public Overrides Function GetHashCode() As Integer
				Return HashCode.Combine(Me.BeatOnly, Me.baseLevel)
			End Function
		End Structure
		Public Class ADTypedList(Of TEvent As ADBaseEvent)
			Implements IEnumerable(Of TEvent)

			Private ReadOnly list As New List(Of TEvent)
			Protected Friend _types As New HashSet(Of ADEventType)
			Public Overloads Sub Add(item As TEvent)
				list.Add(item)
				_types.Add(item.Type)
			End Sub
			Public Overloads Function Remove(item As TEvent)
				_types.Remove(item.Type)
				Return list.Remove(item)
			End Function
			Public Overrides Function ToString() As String
				'Return $"{If(_types.Contains(ADEventType.SetBeatsPerMinute) OrElse _types.Contains(RDEventType.PlaySong),
				'	"BPM, ", If(_types.Contains(ADEventType.SetCrotchetsPerBar),
				'	"CPB, ", ""))}Count = {list.Count}"
				Return $"Count = {list.Count}"
			End Function
			Public Function GetEnumerator() As IEnumerator(Of TEvent) Implements IEnumerable(Of TEvent).GetEnumerator
				Return list.GetEnumerator
			End Function
			Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
				Return list.GetEnumerator
			End Function
		End Class
		Public MustInherit Class ADTileCollection
			Implements ICollection(Of ADTile)
			Friend tileOrder As New List(Of ADTile)
			Public ReadOnly Property Count As Integer Implements ICollection(Of ADTile).Count
				Get
					Return tileOrder.Count
				End Get
			End Property
			Public ReadOnly Property IsReadOnly As Boolean = False Implements ICollection(Of ADTile).IsReadOnly
			Public ReadOnly Property EndTile As New ADTile
			Default Public ReadOnly Property item(index As Integer) As ADTile
				Get
					If index = tileOrder.Count Then
						Return EndTile
					End If
					Return tileOrder(index)
				End Get
			End Property
			Public Overridable ReadOnly Iterator Property Events As IEnumerable(Of ADBaseEvent)
				Get
					For Each tile In tileOrder
						For Each action In tile
							Yield action
						Next
					Next
					For Each action In EndTile
						Yield action
					Next
				End Get
			End Property
			Public Sub Add(item As ADTile) Implements ICollection(Of ADTile).Add
				tileOrder.Add(item)
			End Sub
			Public Sub Clear() Implements ICollection(Of ADTile).Clear
				tileOrder.Clear()
			End Sub
			Public Sub CopyTo(array() As ADTile, arrayIndex As Integer) Implements ICollection(Of ADTile).CopyTo
				tileOrder.CopyTo(array, arrayIndex)
			End Sub
			Public Function Contains(item As ADTile) As Boolean Implements ICollection(Of ADTile).Contains
				Return tileOrder.Contains(item)
			End Function
			Public Function Remove(item As ADTile) As Boolean Implements ICollection(Of ADTile).Remove
				Return tileOrder.Remove(item)
			End Function
			Public Function GetEnumerator() As IEnumerator(Of ADTile) Implements IEnumerable(Of ADTile).GetEnumerator
				Return tileOrder.GetEnumerator
			End Function
			''' <summary>
			''' Get the index of tile.
			''' </summary>
			''' <param name="item">The index of tile.</param>
			''' <returns></returns>
			Public Function IndexOf(item As ADTile) As Integer
				If item Is EndTile Then
					Return Count
				End If
				Return tileOrder.IndexOf(item)
			End Function
			Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
				Return GetEnumerator()
			End Function
		End Class
		Public Enum ADTrackAnimationTypes
			None
			Assemble
			Assemble_Far
			Extend
			Grow
			Grow_Spin
			Fade
			Drop
			Rise
		End Enum
		Public Enum ADTrackDisappearAnimationTypes
			None
			Scatter
			Scatter_Far
			Retract
			Shrink
			Shrink_Spin
			Fade
		End Enum
		Public Enum BgDisplayModes
			FitToScreen
			Unscaled
			Tiled
		End Enum
		''' <summary>
		''' Adofal level.
		''' </summary>
		Public Class ADLevel
			Inherits ADTileCollection
			Friend _path As String
			''' <summary>
			''' Level settings.
			''' </summary>
			Public Property Settings As New ADSettings
			''' <summary>
			''' Level decoration collection.
			''' </summary>
			Public Property Decorations As New List(Of ADBaseEvent)
			''' <summary>
			''' Level file path.
			''' </summary>
			<JsonIgnore> Public ReadOnly Property Path As String
				Get
					Return _path
				End Get
			End Property
			''' <summary>
			''' Level directory path.
			''' </summary>
			<JsonIgnore> Public ReadOnly Property Directory As String
				Get
					Return IO.Path.GetDirectoryName(_path)
				End Get
			End Property
			''' <summary>
			''' Get all the events of the level.
			''' </summary>
			Public Overrides ReadOnly Iterator Property Events As IEnumerable(Of ADBaseEvent)
				Get
					For Each tile In MyBase.Events
						Yield tile
					Next
					For Each tile In Decorations
						Yield tile
					Next
				End Get
			End Property
			''' <summary>
			''' The calculator that comes with the level.
			''' </summary>
			<JsonIgnore> Public ReadOnly Property Calculator As New ADBeatCalculator(Me)
			Public Sub New()
			End Sub
			Public Sub New(items As IEnumerable(Of ADTile))
				For Each tile In items
					Me.Add(tile)
				Next
			End Sub
#If DEBUG Then
			''' <summary>
			''' The default level within the game.
			''' </summary>
			Public Shared ReadOnly Property [Default] As ADLevel
				Get
					Return New ADLevel
				End Get
			End Property
#End If
			''' <summary>
			''' Read from file as level.
			''' Use default input settings.
			''' Supports .rdlevel, .rdzip, .zip file extension.
			''' </summary>
			''' <param name="filepath">File path.</param>
			''' <returns>An instance of a level that reads from a file.</returns>
			Public Shared Function LoadFile(filepath As String) As ADLevel
				Return LoadFile(filepath, New LevelReadOrWriteSettings)
			End Function
			''' <summary>
			''' Read from file as level.
			''' Supports .rdlevel, .rdzip, .zip file extension.
			''' </summary>
			''' <param name="filepath">File path.</param>
			''' <param name="settings">Input settings.</param>
			''' <returns>An instance of a level that reads from a file.</returns>
			Public Shared Function LoadFile(filepath As String, settings As LevelReadOrWriteSettings) As ADLevel
				Dim LevelSerializer = New JsonSerializer()
				LevelSerializer.Converters.Add(New ADLevelConverter(filepath, settings))
				Select Case IO.Path.GetExtension(filepath)
					Case ".adofai"
						Return LevelSerializer.Deserialize(Of ADLevel)(New JsonTextReader(IO.File.OpenText(filepath)))
					Case Else
						Throw New RhythmBaseException("File not supported.")
				End Select
			End Function
		End Class
		Public Class ADSettings
			Public Property Version As Integer '13
			Public Property Artist As String
			Public Property SpecialArtistType As SpecialArtistTypes
			Public Property ArtistPermission As String
			Public Property Song As String
			Public Property Author As String
			Public Property SeparateCountdownTime As Boolean
			Public Property PreviewImage As String
			Public Property PreviewIcon As String
			Public Property PreviewIconColor As SKColor
			Public Property PreviewSongStart As Single
			Public Property PreviewSongDuration As Single
			Public Property SeizureWarning As Boolean
			Public Property LevelDesc As String
			Public Property LevelTags As String
			Public Property ArtistLinks As String
			Public Property Difficulty As Integer
			Public Property RequiredMods As New List(Of String)
			Public Property SongFilename As String
			Public Property Bpm As Single
			Public Property Volume As Single
			Public Property Offset As Single
			Public Property Pitch As Single
			Public Property Hitsound As String
			Public Property HitsoundVolume As Single
			Public Property CountdownTicks As Single
			Public Property TrackColorType As ADTrackColorTypes
			Public Property TrackColor As SKColor
			Public Property SecondaryTrackColor As SKColor
			Public Property TrackColorAnimDuration As Single
			Public Property TrackColorPulse As ADTrackColorPulses
			Public Property TrackPulseLength As Single
			Public Property TrackStyle As ADTrackStyles
			Public Property TrackAnimation As ADTrackAnimationTypes
			Public Property BeatsAhead As Integer
			Public Property TrackDisappearAnimation As ADTrackDisappearAnimationTypes
			Public Property BeatsBehind As Integer
			Public Property BackgroundColor As SKColor
			Public Property ShowDefaultBGIfNoImage As Boolean
			Public Property BgImage As String
			Public Property BgImageColor As SKColor
			Public Property Parallax As RDPointI
			Public Property BgDisplayMode As BgDisplayModes
			Public Property LockRot As Boolean
			Public Property LoopBG As Boolean
			Public Property ScalingRatio As Single
			Public Property RelativeTo As ADCameraRelativeTo
			Public Property Position As RDPointI
			Public Property Rotation As Single
			Public Property Zoom As Single
			Public Property BgVideo As String
			Public Property LoopVideo As Boolean
			Public Property VidOffset As Integer
			Public Property FloorIconOutlines As Boolean
			Public Property StickToFloors As Boolean
			Public Property PlanetEase As EaseType
			Public Property PlanetEaseParts As Integer
			Public Property PlanetEasePartBehavior As ADEasePartBehaviors
			Public Property DefaultTextColor As SKColor
			Public Property DefaultTextShadowColor As SKColor
			Public Property CongratsText As String
			Public Property PerfectText As String
			'Public Property customClass As String
			Public Property LegacyFlash As Boolean
			Public Property LegacyCamRelativeTo As Boolean
			Public Property LegacySpriteTiles As Boolean
		End Class
	End Namespace
End Namespace
