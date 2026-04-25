#### \[ English | [中文](./VERSION.zh-cn.md) \]

### 20260420 v1.3.11-alpha2
- Refactored serializer generator
- Removed MacroEvent event type
- Added .NET Standard 2.0 compatibility
- Fixed exception triggered when changing BPM under specific conditions
- Added PlayerTypeGroup constructor
- Fixed reversed index color and custom color issues in PaletteColor and PaletteColorWithAlpha

### 20260405 v1.3.11-alpha1
- Synchronized with Rhythm Doctor level version
- Added custom serialization interfaces for LevelReadSettings and LevelWriteSettings

### 20260330 v1.3.10
- Fixed event model anomalies:
    - CallCustomMethod no longer implements IRoomEvent interface
    - TagAction adapted to new enum types
    - FloatingText.Position property naming inconsistency
    - TextExplosion.Speed property default value incorrect
    - ReorderWindows.Order property default value incorrect
    - ReorderRooms.Order property default value incorrect
    - ReorderRow.Tab property value incorrect
    - SetVFXPreset property nullable
- Added RDExpressionBuilder type for auxiliary expression construction
- RDLevel now supports loading from JsonDocument
- RoomHeight can be declared directly using tuple syntax with implicit conversion
- Added legality check for cache directory names
- BaseDecorationAction retrieves its Y property value from parent Decoration
- Fixed EventEnumerator\<TEvent\> boundary issue
- Added ITintEvent interface to unify PaintHands/Tint/TintRows event properties
- Fixed AddClassicBeat Splitted extension method not splitting by Length property
- Fixed Bookmark not adjusting when changing time signature
- Fixed exported compressed packages not including decoration assets
- Added RDLine<TStyle>.Deserialize extension method adaptation for .NET Standard
- Fixed BeatCalculator calculation anomaly
- RDBeat type can now be declared using tuple syntax with implicit conversion
- Added Y property for Row events pointing to the row's index in its room
- Fixed Order type unable to represent window counts exceeding 4
- Added Corner type for SetRoomPerspective adaptation

### 20260320 v1.3.9-patch2
- Fixed BeatCalculator calculation anomaly

### 20260319 v1.3.9-patch1
- Fixed NuGet packaging error under .NET Standard 2.0

### 20260319 v1.3.9
- Moved all enum types to parent namespace
- Fixed exception where modification events were not triggered during automatic time signature repair
- Removed deprecated methods

### 20260306 v1.3.9-alpha2
- Fixed event model anomalies:
    - Move.Pivot property type incorrect
    - NarrateRowInfo.CustomRowLength property missing
    - NewWindowDance.Position property type incorrect
    - TintRows.HeartTransition property missing
    - TintRows.Rooms property default value incorrect
    - WindowResize.Pivot property type incorrect
    - FloatingText.Id property supplement
    - AdvanceText.Duration property type incorrect
- Added PaletteColorWithAlpha type and synchronized to related event models
- Modified RDCharacter default value behavior
- Removed coordinate conversion methods

### 20260306 v1.3.9-alpha1
- Fixed exception where event beats became invalid when removing events
- Fixed event model anomalies:
    - SetCrotchetsPerBar.CrotchetsPerBar lower limit adjusted to 1
    - MoveCamera.Angle property type incorrect
    - AddOneshotBeat partial properties not participating in serialization
- Rewrote BeatCalculator
- Added RDCharacter equality method
- Fixed RDBeat cached value marking error exception
- Fixed exception for missing folder resources
- Serialization logic optimization

### 20260226 v1.3.8
- Fixed Row and Decoration serialization logic
- FileReference added equality method
- Added collection expression declarations for some types

### 20260208 v1.3.7
- Fixed event model anomalies:
    - NewWindowDance.Tab defaulting to Actions instead of Windows
    - WindowResize.Tab defaulting to Actions instead of Windows
- Added LevelWriteSettings.EnableUnsafeRelaxedJsonEscaping method to enable or disable HTML unsafe character escaping
- Fixed RDBeat comparison cache invalidation exception
- Fixed RDRange range exception
- Fixed TimesExecuted condition serialization exception
- Fixed RDLevel.Add overload exception

### 20260128 v1.3.6
- All event types updated to record types
- Fixed event model anomalies:
    - PaintHands partial properties could not be disabled
    - Tint partial properties could not be disabled
    - TintRows partial properties could not be disabled
    - MoveCamera.Window property missing
- Optimized source generator logic

### 20260120 v1.3.5
- Adapted to game version v1.0.4
- Fixed event model anomalies:
    - AddOneshotBeat partial property default values incorrect
    - NewWindowDance.Tab property default value anomaly
    - ChangeCharacter partial property serialization anomaly
- Fixed RDColor color reading anomaly
- Some properties replaced with rich text types

### 20260108 v1.3.4
- Fixed event model anomalies:
    - AddClassicBeat.Sound default value
- Modified Condition usage
- RDRoom.Default default value changed to property
- Added PaletteColor.PaletteIndex property
- Fixed enum constant naming issues

### 20251227 v1.3.3
- Fixed event model anomalies:
    - NarrateRowInfo.Pattern property type change
    - AddClassicBeat.Length property type change
    - ReorderSprite missing properties:
        - LayerType
- Fixed Row/Decoration events losing parent object references when loading levels
- Moved all enum types outside of types

### 20251215 v1.3.2
- Fixed event model anomalies:
    - NewWindowDance unable to customize Tab property
    - SetRowXs missing properties:
        - SyncoPlayModifierOffSound
        - SyncoPitch
    - WindowResize missing properties:
        - ZoomMode
    - BaseWindowEvent limiting Y <= 3
    - SetGameSound.Tab property value error
- Removed Row event type restrictions; Row events can now be freely placed on Classic and Oneshot type Rows

### 20251210 v1.3.1
- Fixed event model anomalies:
    - DesktopColor inheritance relationship error
    - ReorderWindows.Order property name incorrect
    - SpinningRows.Tab property value incorrect
- Fixed window event inheritance and partial properties

### 20251209 v1.3.0
- Adapted to all official release events
- Fixed BeatCalculator calculation anomaly
- PatternCollection added Length property
- Condition added implicit creation method
