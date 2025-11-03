using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.RichText;
using RhythmBase.Global.Settings;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Components.RDLang;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Utils;
using System.Text.Json;

namespace RhythmBase.Test
{
	[TestClass]
	public sealed class Tutorial
	{
		private static RDLevel rdlevel = RDLevel.Default;
		[TestMethod]
		public void CreateAnEmptyLevel()
		{
			using RDLevel emptyLevel = [];
			Console.WriteLine(emptyLevel); // "" Count = 0
		}
		[TestMethod]
		public void CreateADefaultLevel()
		{
			using RDLevel defaultLevel = RDLevel.Default;
			Console.WriteLine(defaultLevel); // "" Count = 3
		}
		[TestMethod]
		public void ReadOrWriteLevel()
		{
			// Directly read a level file
			using RDLevel rdlevel1 = RDLevel.FromFile(@"your\level.rdlevel");

			// Read a level pack file
			using RDLevel rdlevel2 = RDLevel.FromFile(@"your\level.rdzip");

			// Read a compressed level pack
			using RDLevel rdlevel3 = RDLevel.FromFile(@"your\level.zip");

			// Write a level file
			rdlevel1.SaveToFile(@"your\outLevel.rdlevel");
		}
		[TestMethod]
		public void ReadOrWriteLevelWithSettings()
		{
			// Create custom read/write settings
			LevelReadOrWriteSettings settings = new()
			{
				// Handling of inactive events
				InactiveEventsHandling = InactiveEventsHandling.Store,
				// Handling of unreadable events
				// Common when sprite events are not bound to sprite tracks, etc.
				UnreadableEventsHandling = UnreadableEventHandling.Store,
				// Enable indentation
				Indented = true,
			};

			using RDLevel rdlevel1 = RDLevel.FromFile(@"your\level.rdlevel", settings);
		}
		[TestMethod]
		public void ConvertLevelToJsonDocument()
		{
			LevelReadOrWriteSettings settings = new();

			JsonDocument jdoc = rdlevel.ToJsonDocument();
			string json = rdlevel.ToJsonString(settings);
			Console.WriteLine(jdoc);
			Console.WriteLine(json);
		}
		[TestMethod]
		public void ReadOrWriteEventHandling()
		{
			LevelReadOrWriteSettings settings = new();
			settings.AfterWriting += Settings_AfterReading;

			// This will be triggered after writing is finished
			void Settings_AfterReading(object? sender, EventArgs e)
			{
				Console.WriteLine("After writing");
			}

			rdlevel.SaveToFile(@"your\outLevel.rdlevel", settings);
		}
#if NETCOREAPP
		[TestMethod]
		public void FindEventsInLevel()
		{
			// Find MoveRow events between bar 3 and 5, and in event rows 0 to 2
			var list = rdlevel
				.OfEvent<MoveRow>()
				.InRange(new(3, 1), new(5, 1))// From Bar 3 to 5
				.Where(i => 0 <= i.Y && i.Y < 3);  // In event rows 0 to 2
		}
#endif
		[TestMethod]
		public void FindEventsInDecoration()
		{
			rdlevel.Decorations.Add([]);
			// Find the AddClassicBeat event in the row decoration between beat (11,1) and (13,1)
			var list = rdlevel.Decorations[0]
				.OfEvent<AddClassicBeat>()
				.InRange(
					new RDBeat(11, 1), // Start searching from bar 11, beat 1
					new RDBeat(13, 1)  // End searching at bar 13, beat 1
				);
		}
		[TestMethod]
		public void CreateBeatWithoutBinding()
		{
			// Create a beat not associated with a level
			RDBeat beat1 = new(11);
			RDBeat beat2 = new(2, 3);
			RDBeat beat3 = new(TimeSpan.FromSeconds(11.45));

			Console.WriteLine(beat1); // [10,?,?]
			Console.WriteLine(beat2); // [?,(2, 3),?]
			Console.WriteLine(beat3); // [?,?,00:00:11.4500000]
		}
		[TestMethod]
		public void CreateBeatWithBinding()
		{
			// Create a beat associated with a level
			RDBeat beat1 = rdlevel.BeatOf(11);
			RDBeat beat2 = rdlevel.Calculator.BeatOf(2, 3);
			RDBeat beat3 = beat1 - 10 + TimeSpan.FromSeconds(11.45);

			Console.WriteLine(beat1); // [2,3]
			Console.WriteLine(beat2); // [2,3]
			Console.WriteLine(beat3); // [3,4.083334]
		}
		[TestMethod]
		public void LinkBeats()
		{
			RDBeat beat1 = rdlevel.BeatOf(1);
			RDBeat beat2 = beat1.WithoutLink();

			Console.WriteLine(beat1.FromSameLevel(beat2));       // False
			Console.WriteLine(beat1.FromSameLevelOrNull(beat2)); // True
		}
		[TestMethod]
		public void ConvertTimeUnit()
		{
			(float, float) barbeat = rdlevel.Calculator.TimeSpanToBarBeat(TimeSpan.FromSeconds(19.19));
			Console.WriteLine(barbeat); // (4, 8.983334)
		}
		[TestMethod]
		public void RangeUsage()
		{
			var result = rdlevel.InRange(new RDRange(rdlevel.DefaultBeat + 10, null));
		}
		[TestMethod]
		public void ExpressionUsage()
		{

			RDExpression exp1 = new("i2+1");
			RDExpression exp2 = new(30);
			RDExpression exp3 = new("25.5");

			RDExpression result = exp1 - exp2 * exp3;

			Console.WriteLine(result.ExpressionValue); // i2+1-765
		}
		[TestMethod]
		public void AddAndRemoveEvent()
		{
			Comment comment = new() { Beat = new(12), Text = "My_comment." };
			Console.WriteLine(comment); // [11,?,?] Comment My_comment.

			rdlevel.Add(comment);
			Console.WriteLine(comment); // [2,4] Comment My_comment.

			rdlevel.Remove(comment);
			Console.WriteLine(comment); // [11,?,?] Comment My_comment.
		}
		[TestMethod]
		public void AddForwardEvent()
		{
			MyEvent myEvent = new();

			myEvent.MyProperty = new(2, "i3+1");

			rdlevel.Add(myEvent);

			myEvent.Beat = new(8);

			Console.WriteLine(myEvent.Type);        // ForwardEvent  
			Console.WriteLine(myEvent.ActualType);  // MyEvent  
		}
		[TestMethod]
		public void EventTypeUtilsConvert()
		{
			Console.WriteLine(EventType.Tint.ToType());                                                // RhythmBase.Events.Tint
			Console.WriteLine(EventTypeUtils.ToType("Tint"));                                          // RhythmBase.Events.Tint
			Console.WriteLine(EventTypeUtils.ToEnum(typeof(Tint)));                                    // Tint
			Console.WriteLine(EventTypeUtils.ToEnum<Tint>());                                          // Tint
			Console.WriteLine(string.Join(", ", EventTypeUtils.ToEnums(typeof(IBarBeginningEvent))));  // PlaySong,SetCrotchetsPerBar, SetHeartExplodeVolume
			Console.WriteLine(string.Join(", ", EventTypeUtils.ToEnums<IBarBeginningEvent>()));        // PlaySong,SetCrotchetsPerBar, SetHeartExplodeVolume
		}
		[TestMethod]
		public void EventTypeUtilsStatic()
		{
			Console.WriteLine(string.Join(",\n", EventTypeUtils.DecorationTypes));
			// Comment,
			// CustomDecorationEvent,
			// Move,
			// PlayAnimation,
			// SetVisible,
			// Tile,
			// Tint

			Console.WriteLine(string.Join(",\n", EventTypeUtils.EventTypeEnumsForCameraFX));
			// MoveCamera,
			// ShakeScreen,
			// FlipScreen,
			// PulseCamera

			Console.WriteLine(string.Join(",\n", EventTypeUtils.EventTypeEnumsForUtility));
			// Comment,
			// TagAction,
			// CallCustomMethod
		}
		[TestMethod]
		public void RichTextUsage()
		{
#if NETFRAMEWORK
			RDLine<RDRichStringStyle> line = new RDLine<RDRichStringStyle>().Deserialize("Hel<color=#00FF00>lo");
#elif NETCOREAPP
			RDLine<RDRichStringStyle> line = RDLine<RDRichStringStyle>.Deserialize("Hel<color=#00FF00>lo");
#endif

			Console.WriteLine(line.ToString()); // Hello
			Console.WriteLine(line.Serialize()); // Hel<color=lime>lo</color>

			line +=
				new RDPhrase<RDRichStringStyle>(" Rhythm")
				{
					Style = new()
					{
						Color = RDColor.Lime
					}
				};

			line += " Doctor!";

			Console.WriteLine(line.ToString()); // Hello Rhythm Doctor!
			Console.WriteLine(line.Serialize()); // Hel<color=lime>lo Rhythm</color> Doctor!
		}
		[TestMethod]
		public void RichTextModify()
		{
#if NETFRAMEWORK
			RDLine<RDRichStringStyle> line = new RDLine<RDRichStringStyle>().Deserialize("Hel<color=#00FF00>lo Rhythm</color> Doctor!");
#elif NETCOREAPP
			RDLine<RDRichStringStyle> line = RDLine<RDRichStringStyle>.Deserialize("Hel<color=#00FF00>lo Rhythm</color> Doctor!");
#endif

#if NETCOREAPP
			Console.WriteLine(line[6..].ToString()); // Rhythm Doctor!
			Console.WriteLine(line[6..].Serialize()); // <color=lime>Rhythm</color> Doctor!

			line[5] = " and Welcome to ";
#endif

			Console.WriteLine(line.ToString()); // Hello and Welcome to Rhythm Doctor!
			Console.WriteLine(line.Serialize()); // Hel<color=lime>lo</color> and Welcome to <color=lime>Rhythm</color> Doctor!

			return;
		}
		[TestMethod]
		public void RichTextBuild()
		{
			RDDialogueExchange exchange =
			[
				new RDDialogueBlock()
				{
					Character = "Paige",
					Expression = "neutral",
#if NETFRAMEWORK
					Content = new RDLine<RDDialoguePhraseStyle>().Deserialize("Hel<color=#00FF00>lo [2]<shake>Rhythm</color> Doctor</shake>!"),
#elif NETCOREAPP
					Content = RDLine<RDDialoguePhraseStyle>.Deserialize("Hel<color=#00FF00>lo [2]<shake>Rhythm</color> Doctor</shake>!"),
#endif
				},
				new RDDialogueBlock()
				{
					Character = "Ian",
					Content = "Hello Paige!",
				},
				new RDDialogueBlock()
				{
					Character = "Paige",
					Expression = "happy",
					Content = new RDPhrase<RDDialoguePhraseStyle>("What a good day!")
					{
						Events =
						[
							new RDDialogueTone(RDDialogueToneType.VerySlow,6),
							new RDDialogueTone(RDDialogueToneType.Static,11),
						],
						Style = new RDDialoguePhraseStyle()
						{
							Volume = 0.5f,
							Bold = true,
						},
					}
				}
			];

			Console.WriteLine(exchange.Serialize());
			// Paige_neutral:Hel<color=lime>lo [2]<shake>Rhythm</color> Doctor</shake>!
			// Ian:Hello Paige!
			// Paige_happy:<volume=0.5><bold>What a[vslow] good[static] day!</volume></bold>
		}
		[TestMethod]
		public void EasingCalculate()
		{
			double var1 = EaseType.InSine.Calculate(0.25);
			double var2 = EaseType.Linear.Calculate(0.5, 4, 9);

			Console.WriteLine(var1); // 0.07612046748871326
			Console.WriteLine(var2); // 6.5
		}
		[TestMethod]
		public void RDCode()
		{
			RDLang.Variables.i[1] = 9;

			RDLang.TryRun("numMistakesP2 = 3", out float result); // 3
			Console.WriteLine(result);
			RDLang.TryRun("numMistakesP2+i1", out result); // 12
			Console.WriteLine(result);
			RDLang.TryRun("atLeastRank(A)", out result); // 1
			Console.WriteLine(result);
		}
		[TestMethod]
		public void MacroEvents()
		{

			LevelReadOrWriteSettings settings = new()
			{
				EnableMacroEvent = true,
				InactiveEventsHandling = InactiveEventsHandling.Retain,
				Indented = true
			};

			using RDLevel level = RDLevel.Default;
			level.Decorations.Add(new Decoration() { Room = RDRoomIndex.Room1 });
			var re1 = new MoveCameraRectangle() { Beat = new(4), Size = new RDSize(80, 80) };
			var re2 = new MoveCameraRectangle() { Beat = new(9), Y = 2, Size = new RDSize(20, 20) };
			level.Add(re1);
			level.Add(re2);
			string levelJson = level.ToJsonString(settings);
			Console.WriteLine(levelJson);
			using RDLevel level2 = RDLevel.FromJsonString(levelJson, settings);

			/* The following events will be generated:
			 * {"bar":1,"beat":1,"type":"MoveCamera","rooms":[0],"cameraPosition":[10,10],"duration":1,"ease":"Linear","y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000000"},
			 * {"bar":1,"beat":1,"type":"MoveRow","customPosition":true,"target":"WholeRow","rowPosition":[50,50],"duration":0,"ease":"Linear","row":0,"y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000000"},
			 * {"bar":1,"beat":1.001,"type":"MoveRow","customPosition":true,"target":"WholeRow","rowPosition":[10,10],"duration":1,"ease":"Linear","row":0,"y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000000"},
			 * {"bar":1,"beat":2,"type":"MoveCamera","rooms":[0],"cameraPosition":[90,10],"duration":1,"ease":"Linear","y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000000"},
			 * {"bar":1,"beat":2,"type":"MoveRow","customPosition":true,"target":"WholeRow","rowPosition":[90,10],"duration":1,"ease":"Linear","row":0,"y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000000"},
			 * {"bar":1,"beat":3,"type":"MoveCamera","rooms":[0],"cameraPosition":[90,90],"duration":1,"ease":"Linear","y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000000"},
			 * {"bar":1,"beat":3,"type":"MoveRow","customPosition":true,"target":"WholeRow","rowPosition":[90,90],"duration":1,"ease":"Linear","row":0,"y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000000"},
			 * {"bar":1,"beat":4,"type":"MoveCamera","rooms":[0],"cameraPosition":[10,90],"duration":1,"ease":"Linear","y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000000"},
			 * {"bar":1,"beat":4,"type":"MoveRow","customPosition":true,"target":"WholeRow","rowPosition":[10,90],"duration":1,"ease":"Linear","row":0,"y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000000"},
			 * {"bar":1,"beat":1,"type":"MoveCamera","rooms":[0],"cameraPosition":[40,40],"duration":1,"ease":"Linear","y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000001"},
			 * {"bar":1,"beat":1,"type":"MoveRow","customPosition":true,"target":"WholeRow","rowPosition":[50,50],"duration":0,"ease":"Linear","row":0,"y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000001"},
			 * {"bar":1,"beat":1.001,"type":"MoveRow","customPosition":true,"target":"WholeRow","rowPosition":[40,40],"duration":1,"ease":"Linear","row":0,"y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000001"},
			 * {"bar":1,"beat":2,"type":"MoveCamera","rooms":[0],"cameraPosition":[60,40],"duration":1,"ease":"Linear","y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000001"},
			 * {"bar":1,"beat":2,"type":"MoveRow","customPosition":true,"target":"WholeRow","rowPosition":[60,40],"duration":1,"ease":"Linear","row":0,"y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000001"},
			 * {"bar":1,"beat":3,"type":"MoveCamera","rooms":[0],"cameraPosition":[60,60],"duration":1,"ease":"Linear","y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000001"},
			 * {"bar":1,"beat":3,"type":"MoveRow","customPosition":true,"target":"WholeRow","rowPosition":[60,60],"duration":1,"ease":"Linear","row":0,"y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000001"},
			 * {"bar":1,"beat":4,"type":"MoveCamera","rooms":[0],"cameraPosition":[40,60],"duration":1,"ease":"Linear","y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000001"},
			 * {"bar":1,"beat":4,"type":"MoveRow","customPosition":true,"target":"WholeRow","rowPosition":[40,60],"duration":1,"ease":"Linear","row":0,"y":-1,"tag":"$RhythmBase_GroupEvent$0000000000000001"},
			 * {"bar":1,"beat":1,"type":"Comment","tab":"Song","show":false,"text":"$RhythmBase_GroupData$\r\n\/* Generated by RhythmBase /\r\n@MoveCameraRectangle\r\n@MoveCameraRectangle2\r\n@RhythmBase.RhythmDoctor.Events.Group\r\n@RhythmBase.RhythmDoctor.Events.Group`1\r\n{\"size\":[80.0,80.0],\"rowIndex\":0}\r\n{\"size\":[20.0,20.0],\"rowIndex\":0}\r\n","color":"F2E644","y":-1},
			 * {"bar":1,"beat":4,"type":"TagAction","Tag":"$RhythmBase_GroupEvent$0000000000000000","y":0,"tag":"","Action":"Run"},
			 * {"bar":2,"beat":1,"type":"TagAction","Tag":"$RhythmBase_GroupEvent$0000000000000001","y":2,"tag":"","Action":"Run"},
			*/
		}
		public void Example_01()
		{

			// Read the visual effects level file
			using RDLevel vfxLevel = RDLevel.FromFile(@"vfx.rdlevel");
			// Read the audio level file
			using RDLevel audioLevel = RDLevel.FromFile(@"beat.rdlevel");

			// Remove all rows from the visual effects level
			Row[] vfxrows = [.. vfxLevel.Rows];
			foreach (var row in vfxrows)
				vfxLevel.Rows.Remove(row);

			// Copy all rows from the audio level into the new level
			foreach (var row in audioLevel.Rows)
			{
				// Copy row information
				Row row2 = new()
				{
					Rooms = row.Rooms,
					Character = row.Character,
					Sound = row.Sound,
					RowType = row.RowType
				};
				vfxLevel.Rows.Add(row2);

				// Copy events within the row
				BaseBeat[] evts = [.. row.OfEvent<BaseBeat>()];
				foreach (var evt in evts)
					row2.Add(evt);
			}

			// Copy necessary sound events
			foreach (var sound in audioLevel.Where(e =>
				e.Tab == Tabs.Sounds &&       // Event is in the Sounds tab
				e is not BaseRowAction &&     // Sound events contain row events; adding row events here would cause reference errors
				e is not PlaySong &&          // No need to copy PlaySong if the music is the same
				e is not SetCrotchetsPerBar)) // The timing of these events is independent of the number of crotchets per bar, so they don't need to be added
			{
				vfxLevel.Add(sound);
			}

			// Write to a new level file
			vfxLevel.SaveToFile(@"result.rdlevel");
		}
		[TestClass]
		// Create a MyEvent type  
		//   Inherit from CustomEvent  
		public class MyEvent : ForwardEvent
		{
			// Override property  
			public override Tabs Tab => Tabs.Actions;

			// All implemented properties need to be bound to and checked for null in the CustomEvent.Data field.  

			// Implement an RDPointE type property  
			public RDPointE? MyProperty
			{
				get
				{
					// Get the required content from the Data field and check for null  
					return ExtraData.TryGetValue("myProperty", out var jsonElement)
						? jsonElement.Deserialize<RDPointE>()
						: null;
				}
				set
				{
					// Save the content in the Data field  
					ExtraData["myProperty"] =
						value.HasValue ?
						JsonSerializer.SerializeToElement(value, Utils.GetJsonSerializerOptions()) :
						default;
				}
			}

			// Initialize the type in the constructor  
			public MyEvent()
			{
				// Initialize the ActureType property.
				ActualType = nameof(MyEvent);
			}
		}
		public class GroupData1
		{
			public RDSize Size { get; set; }
			public int RowIndex { get; set; }
		}
		public class MoveCameraRectangle : MacroEvent<GroupData1>
		{
			public RDSize Size
			{
				get => Data.Size;
				set => Data.Size = value;
			}
			public Row Row
			{
				get => Rows?[Data.RowIndex] ?? [];
				set => Data.RowIndex = value.Index;
			}
			public MoveCameraRectangle() { }
			public override IEnumerable<BaseEvent> GenerateEvents()
			{
				yield return new MoveCamera() { Beat = new(1), Rooms = new(0), CameraPosition = new(50 - Size.Width / 2, 50 - Size.Height / 2), Duration = 1 };
				yield return new MoveCamera() { Beat = new(2), Rooms = new(0), CameraPosition = new(50 + Size.Width / 2, 50 - Size.Height / 2), Duration = 1 };
				yield return new MoveCamera() { Beat = new(3), Rooms = new(0), CameraPosition = new(50 + Size.Width / 2, 50 + Size.Height / 2), Duration = 1 };
				yield return new MoveCamera() { Beat = new(4), Rooms = new(0), CameraPosition = new(50 - Size.Width / 2, 50 + Size.Height / 2), Duration = 1 };
				yield return SetParent(new MoveRow() { Beat = new(1), Position = new(50, 50), EnableCustomPosition = true, Duration = 0 }, Row);
				yield return SetParent(new MoveRow() { Beat = new(1.001f), Position = new(50 - Size.Width / 2, 50 - Size.Height / 2), EnableCustomPosition = true, Duration = 1 }, Row);
				yield return SetParent(new MoveRow() { Beat = new(2), Position = new(50 + Size.Width / 2, 50 - Size.Height / 2), EnableCustomPosition = true, Duration = 1 }, Row);
				yield return SetParent(new MoveRow() { Beat = new(3), Position = new(50 + Size.Width / 2, 50 + Size.Height / 2), EnableCustomPosition = true, Duration = 1 }, Row);
				yield return SetParent(new MoveRow() { Beat = new(4), Position = new(50 - Size.Width / 2, 50 + Size.Height / 2), EnableCustomPosition = true, Duration = 1 }, Row);
			}
		}
	}
}
