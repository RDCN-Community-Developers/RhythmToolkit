# [RhythmBase](../../RhythmToolkit.md).[LevelElements](../namespace/LevelElements.md).Settings
### [RhythmBase.dll](../assembly/RhythmBase.md)
关卡设置。

## 枚举

- [SpecialArtistTypes](../enum/Settings.SpecialArtistTypes.md)
- [DifficultyLevel](../enum/Settings.DifficultyLevel.md)
- [LevelPlayedMode](../enum/Settings.LevelPlayedMode.md)
- [FirstBeatBehaviors](../enum/Settings.FirstBeatBehaviors.md)
- [MultiplayerAppearances](../enum/Settings.MultiplayerAppearances.md)

## 属性和字段
修饰 | 类型 | 名称 | 说明
-|-|-|-
| | int | Version | 返回或设置此关卡的版本号。
| | string | Artist | 返回或设置此关卡的艺术家名。
| | string | Song | 返回或设置此关卡的音乐文件名。
| | [SpecialArtistTypes](../enum/Settings.SpecialArtistTypes.md) | SpecialArtistType | 返回或设置此关卡的艺术家授权类型。
| | string | ArtistPermission | 返回或设置此关卡的艺术家授权证明文件名
| | string | ArtistLinks | 返回或设置此关卡的艺术家链接。
| | string | Author | 返回或设置此关卡的作者。
| | [DifficultyLevel](../enum/Settings.DifficultyLevel.md) | Difficulty | 返回或设置此关卡的关卡难度。
| | bool | SeizureWarning | 返回或设置此关卡的癫痫警告。
| | string | PreviewImage | 返回或设置此关卡的预览图片。
| | string | SyringeIcon | 返回或设置此关卡的针管图标文件名。
| | string | PreviewSong | 返回或设置此关卡的预览音频文件名。
| | float | PreviewSongStartTime | 返回或设置此关卡的预览音频起始时间。
| | float | PreviewSongDuration | 返回或设置此关卡的预览音频时长。
| | float | SongNameHue | 返回或设置此关卡的针管色调。
| | bool | SongLabelGrayscale | 返回或设置此关卡的针管颜色是否使用灰度。
| | string | Description | 返回或设置此关卡的关卡描述。
| | string | Tags | 返回或设置此关卡的关卡标签。
| | string | Separate2PLevelFilename | 返回或设置此关卡的双人版本关卡文件。
| | [LevelPlayedMode](../enum/Settings.LevelPlayedMode.md) | CanBePlayedOn | 返回或设置此关卡的限定游玩模式。
| | [FirstBeatBehaviors](../enum/Settings.FirstBeatBehaviors.md) | FirstBeatBehavior | 返回或设置此关卡的初始节拍生效方式。
| | [MultiplayerAppearances](../enum/Settings.MultiplayerAppearances.md) | MultiplayerAppearance | (已弃用)。
| | float | LevelVolume | 返回或设置此关卡的整体音量。
| | [LimitedList](../class/LimitedList_T_.md)\<int\> | RankMaxMistakes | 返回或设置此关卡的评级容错数。
| | [LimitedList](../class/LimitedList_T_.md)\<string\> | RankDescription | 返回或设置此关卡的评级描述。
| | List\<string\> | Mods | 返回或设置此关卡激活的模组。
