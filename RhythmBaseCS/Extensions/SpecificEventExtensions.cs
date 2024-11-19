using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Exceptions;
using RhythmBase.Extensions;
using System.Text.RegularExpressions;

namespace RhythmBase.Extensions
{
	public static partial class Extensions
	{
		/// <summary>
		/// Get the pulse sound effect of row beat event.
		/// </summary>
		/// <returns>The sound effect of row beat event.</returns>
		public static RDAudio BeatSound(this BaseBeat e)
		{
			SetBeatSound? setBeatSound = e.Parent?.LastOrDefault((SetBeatSound i) => i.Beat < e.Beat && i.Active);
			return (setBeatSound?.Sound) ?? e.Parent?.Sound ?? throw new NotImplementedException();
		}
		/// <summary>
		/// Getting controlled events.
		/// </summary>
		public static IEnumerable<IGrouping<string, IBaseEvent>> ControllingEvents(this TagAction e) => e.Beat.BaseLevel?.GetTaggedEvents(e.ActionTag, e.Action.HasFlag(TagAction.Actions.All)) ?? [];
		/// <summary>
		/// Creates a new <see cref="T:RhythmBase.Events.AdvanceText" /> subordinate to <see cref="T:RhythmBase.Events.FloatingText" /> at the specified beat. The new event created will be attempted to be added to the <see cref="T:RhythmBase.Events.FloatingText" />'s source level.
		/// </summary>
		/// <param name="e">RDLevel</param>
		/// <param name="beat">Specified beat.</param>
		public static AdvanceText CreateAdvanceText(this FloatingText e, RDBeat beat)
		{
			AdvanceText A = new()
			{
				Parent = e,
				Beat = beat.WithoutBinding()
			};
			e.Children.Add(A);
			return A;
		}
		/// <summary>
		/// Get the end beat of the duration.
		/// </summary>
		/// <param name="beat"></param>
		/// <param name="duration"></param>
		/// <returns></returns>
		/// <exception cref="InvalidRDBeatException"></exception>
		public static RDBeat DurationOffset(this RDBeat beat, float duration)
		{
			SetBeatsPerMinute setBPM = beat.BaseLevel?.First((SetBeatsPerMinute i) => i.Beat > beat) ?? throw new InvalidRDBeatException();
			RDBeat DurationOffset =
				beat.BarBeat.bar == setBPM.Beat.BarBeat.bar
				? beat + duration
				: beat + TimeSpan.FromMinutes(duration / beat.BPM);
			return DurationOffset;
		}
		/// <summary>
		/// Returns the pulse beat of the specified 0-based index.
		/// </summary>
		/// <exception cref="T:RhythmBase.Exceptions.RhythmBaseException">THIS IS 7TH BEAT GAMES!</exception>
		public static RDBeat GetBeat(this AddClassicBeat e, byte index)
		{
			SetRowXs x = e.Parent?.LastOrDefault((SetRowXs i) => i.Active && e.IsBehind(i)) ?? new();
			float Synco = 0 <= x.SyncoBeat && x.SyncoBeat < (sbyte)index ? (float)((x.SyncoSwing == 0f) ? 0.5 : ((double)x.SyncoSwing)) : 0f;
			if (index >= 7)
				throw new RhythmBaseException("THIS IS 7TH BEAT GAMES!");
			return e.Beat.DurationOffset(e.Tick * ((float)index - Synco));
		}
		/// <summary>
		/// Converts Xs patterns to string form.
		/// </summary>
		public static string GetPatternString(this SetRowXs e) => Utils.Utils.GetPatternString(e.Pattern);
		/// <summary>
		/// Get the sequence of <see cref="T:RhythmBase.Events.PulseFreeTimeBeat" /> belonging to this <see cref="T:RhythmBase.Events.AddFreeTimeBeat" />, return all of the <see cref="T:RhythmBase.Events.PulseFreeTimeBeat" /> from the time the pulse was created to the time it was removed or hit.
		/// </summary>
		public static IEnumerable<PulseFreeTimeBeat> GetPulses(this AddFreeTimeBeat e)
		{
			List<PulseFreeTimeBeat> Result = [];
			byte pulse = e.Pulse;
			if (e.Parent == null)
				yield break;
			foreach (PulseFreeTimeBeat item in e.Parent.Where<PulseFreeTimeBeat>(i => i.Active && e.IsInFrontOf(i)))
			{
				switch (item.Action)
				{
					case PulseFreeTimeBeat.ActionType.Increment:
						pulse += 1;
						yield return item;
						break;
					case PulseFreeTimeBeat.ActionType.Decrement:
						pulse = (byte)((pulse > 0b1) ? (pulse - 0b1) : 0b1);
						yield return item;
						break;
					case PulseFreeTimeBeat.ActionType.Custom:
						pulse = (byte)item.CustomPulse;
						yield return item;
						break;
					case PulseFreeTimeBeat.ActionType.Remove:
						yield return item;
						break;
				}
				if (pulse == 6)
					break;
			}
		}
		/// <summary>
		/// Get the hit sound effect of row beat event.
		/// </summary>
		/// <returns>The sound effect of row beat event.</returns>
		public static RDAudio HitSound(this BaseBeat e)
		{
			RDAudio DefaultAudio = new()
			{
				Filename = "sndClapHit",
				Offset = TimeSpan.Zero,
				Pan = 100,
				Pitch = 100,
				Volume = 100
			};
			RDAudio? HitSound;
			switch (e.Player())
			{
				case PlayerType.P1:
					{
						SetClapSounds? setClapSounds = e.Beat.BaseLevel?.LastOrDefault((SetClapSounds i) => i.Active && i.P1Sound != null);
						HitSound = (setClapSounds?.P1Sound) ?? DefaultAudio;
						break;
					}
				case PlayerType.P2:
					{
						SetClapSounds? setClapSounds2 = e.Beat.BaseLevel?.LastOrDefault((SetClapSounds i) => i.Active && i.P2Sound != null);
						HitSound = (setClapSounds2?.P2Sound) ?? DefaultAudio;
						break;
					}
				case PlayerType.CPU:
					{
						SetClapSounds? setClapSounds3 = e.Beat.BaseLevel?.LastOrDefault((SetClapSounds i) => i.Active && i.CpuSound != null);
						HitSound = (setClapSounds3?.CpuSound) ?? DefaultAudio;
						break;
					}
				default:
					HitSound = DefaultAudio;
					break;
			}
			return HitSound;
		}
		/// <summary>
		/// Get all hits.
		/// </summary>
		public static IEnumerable<RDHit> HitTimes(this AddClassicBeat e) =>
			new List<RDHit> { new(e, e.GetBeat(6), e.Hold) }
			.AsEnumerable();
		/// <summary>
		/// Get all hits.
		/// </summary>
		public static IEnumerable<RDHit> HitTimes(this AddOneshotBeat e)
		{
			e._beat.IfNullThrowException();
			List<RDHit> L = [];
			uint loops = e.Loops;
			for (uint i = 0U; i <= loops; i += 1U)
			{
				sbyte b = (sbyte)(e.Subdivisions - 1);
				for (sbyte j = 0; j <= b; j += 1)
					L.Add(new RDHit(e, new RDBeat(e._beat._calculator, e._beat.BeatOnly + i * e.Interval + e.Tick + e.Delay + (float)j * (e.Tick / (float)e.Subdivisions)), 0f));
			}
			return L.AsEnumerable();
		}
		/// <summary>
		/// Get all hits.
		/// </summary>
		public static IEnumerable<RDHit> HitTimes(this AddFreeTimeBeat e) =>
			e.Pulse == 6
				? [new(e, e.Beat, e.Hold)]
				: ([]);
		/// <summary>
		/// Get all hits.
		/// </summary>
		public static IEnumerable<RDHit> HitTimes(this PulseFreeTimeBeat e) =>
e.IsHitable()
				? [new(e, e.Beat, e.Hold)]
				: ([]);
		/// <summary>
		/// Get all hits.
		/// </summary>
		public static IEnumerable<RDHit> HitTimes(this BaseBeat e) => e.Type switch
		{
			EventType.AddClassicBeat => ((AddClassicBeat)e).HitTimes(),
			EventType.AddFreeTimeBeat => ((AddFreeTimeBeat)e).HitTimes(),
			EventType.AddOneshotBeat => ((AddOneshotBeat)e).HitTimes(),
			_ => e.Type != EventType.PulseFreeTimeBeat
				? Array.Empty<RDHit>().AsEnumerable()
				: ((PulseFreeTimeBeat)e).HitTimes(),
		};
		/// <summary>
		/// Determine if <paramref name="item1" /> is after <paramref name="item2" />
		/// </summary>
		/// <returns><list type="table">
		/// <item>If <paramref name="item1" /> is after <paramref name="item2" />, <see langword="true" /></item>
		/// <item>Else, <see langword="false" /></item>
		/// </list></returns>
		public static bool IsBehind(this OrderedEventCollection e, IBaseEvent item1, IBaseEvent item2) => (item1.Beat > item2.Beat || (item1.Beat.BeatOnly == item2.Beat.BeatOnly && (e.eventsBeatOrder[item1.Beat].BeforeThan(item2, item1))));
		/// <summary>
		/// Check if another event is after itself, including events of the same beat but executed after itself.
		/// </summary>
		public static bool IsBehind(this IBaseEvent e, IBaseEvent item) => e.Beat.BaseLevel?.IsBehind(e, item) ?? throw new InvalidRDBeatException();
		/// <summary>
		/// Check if it can be hit by player.
		/// </summary>
		public static bool IsHitable(this PulseFreeTimeBeat e)
		{
			int PulseIndexMin = 6;
			int PulseIndexMax = 6;
			if (e.Parent is null)
				return false;
			foreach (BaseBeat item in ((IEnumerable<BaseBeat>)e.Parent
			.Where(e.IsBehind, new RDRange(e.Beat, null)))
			.Reverse())
			{
				EventType type = item.Type;
				switch (type)
				{
					case EventType.AddFreeTimeBeat:
						{
							AddFreeTimeBeat Temp2 = (AddFreeTimeBeat)item;
							if (PulseIndexMin <= (int)Temp2.Pulse & (int)Temp2.Pulse <= PulseIndexMax)
								return true;
							break;
						}
					case EventType.PulseFreeTimeBeat:
						{
							PulseFreeTimeBeat Temp = (PulseFreeTimeBeat)item;
							switch (Temp.Action)
							{
								case PulseFreeTimeBeat.ActionType.Increment:
									if (PulseIndexMin > 0)
										PulseIndexMin--;
									if (!(PulseIndexMax > 0))
										return false;
									PulseIndexMax--;
									break;
								case PulseFreeTimeBeat.ActionType.Decrement:
									if (PulseIndexMin > 0)
										PulseIndexMin++;
									if (!(PulseIndexMax < 6))
										return false;
									PulseIndexMax++;
									break;
								case PulseFreeTimeBeat.ActionType.Custom:
									if (!(PulseIndexMin <= Temp.CustomPulse & Temp.CustomPulse <= PulseIndexMax))
										return false;
									PulseIndexMin = 0;
									PulseIndexMax = 5;
									break;
								case PulseFreeTimeBeat.ActionType.Remove:
									return false;
							}
							if (PulseIndexMin > PulseIndexMax)
								return false;
							break;
						}
				}
			}
			return false;
		}
		/// <summary>
		/// Check if it can be hit by player.
		/// </summary>
		public static bool IsHitable(this AddFreeTimeBeat e) => e.Pulse == 6;
		/// <summary>
		/// Check if it can be hit by player.
		/// </summary>
		public static bool IsHitable(this BaseBeat e) => e.Type switch
		{
			EventType.AddClassicBeat or EventType.AddOneshotBeat => true,
			EventType.AddFreeTimeBeat => ((AddFreeTimeBeat)e).IsHitable(),
			_ => e.Type == EventType.PulseFreeTimeBeat && ((PulseFreeTimeBeat)e).IsHitable(),
		};
		/// <summary>
		/// Determine if <paramref name="item1" /> is in front of <paramref name="item2" />
		/// </summary>
		/// <returns><list type="table">
		/// <item>If <paramref name="item1" /> is in front of <paramref name="item2" />, <see langword="true" /></item>
		/// <item>Else, <see langword="false" /></item>
		/// </list></returns>
		public static bool IsInFrontOf(this OrderedEventCollection e, IBaseEvent item1, IBaseEvent item2) => (item1.Beat < item2.Beat || (item1.Beat.BeatOnly == item2.Beat.BeatOnly && (e.eventsBeatOrder[item1.Beat].BeforeThan(item1, item2))));
		/// <summary>
		/// Check if another event is in front of itself, including events of the same beat but executed before itself.
		/// </summary>
		public static bool IsInFrontOf(this IBaseEvent e, IBaseEvent item) => e.Beat.BaseLevel?.IsInFrontOf(e, item) ?? throw new InvalidRDBeatException();
		/// <summary>
		/// Get the total length of the oneshot.
		/// </summary>
		/// <returns></returns>
		public static float Length(this AddOneshotBeat e) => e.Tick * e.Loops + e.Interval * e.Loops - 1f;
		/// <summary>
		/// Get the total length of the classic beat.
		/// </summary>
		/// <returns></returns>
		public static float Length(this AddClassicBeat e)
		{
			float SyncoSwing = e.Parent?.LastOrDefault((SetRowXs i) => i.Active && e.IsBehind(i))?.SyncoSwing ?? 0;
			return (float)((double)(e.Tick * 6f) - ((SyncoSwing == 0f) ? 0.5 : ((double)SyncoSwing)) * (double)e.Tick);
		}
		/// <summary>
		/// Specifies the position of the image. This method changes both the pivot and the angle to keep the image visually in its original position.
		/// </summary>
		/// <param name="spriteSize">Sprite size. </param>
		/// <param name="e">RDLevel</param>
		/// <param name="target">Specified position. </param>
		public static void MovePositionMaintainVisual(this Move e, RDSizeE spriteSize, RDPointE target)
		{
			if (e.Position != null && e.Pivot != null && e.Angle != null && e.Angle.Value.IsNumeric)
			{
				e.Position = new RDPointE?(target);
				e.Pivot = new RDPointE?((e.VisualPosition(spriteSize) - new RDSizeE(target)).Rotate(e.Angle.Value.NumericValue));
			}
		}
		/// <summary>
		/// Specifies the position of the image. This method changes both the pivot and the angle to keep the image visually in its original position.
		/// </summary>
		/// <param name="e">RDLevel</param>
		/// <param name="target">Specified position. </param>
		public static void MovePositionMaintainVisual(this MoveRoom e, RDSizeE target)
		{
			if (e.RoomPosition != null && e.Pivot != null && e.Angle != null && e.Angle.Value.IsNumeric)
			{
				e.RoomPosition = new RDPointE?((RDPointE)target);
				e.Pivot = new RDPointE?((e.VisualPosition() - new RDSizeE((RDPointE)target)).Rotate(e.Angle.Value.NumericValue));
			}
		}
		/// <summary>
		/// Convert beat pattern to string.
		/// </summary>
		/// <returns>The pattern string.</returns>
		public static string Pattern(this AddClassicBeat e) => Utils.Utils.GetPatternString(e.RowXs());
		/// <summary>
		/// Get current player of the beat event.
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public static PlayerType Player(this BaseBeat e)
		{
			ChangePlayersRows? changePlayersRows = e.Beat.BaseLevel?.LastOrDefault((ChangePlayersRows i) => i.Active && i.Players[e.Index] != PlayerType.NoChange);
			return (changePlayersRows != null)
					? changePlayersRows.Players[e.Index]
					: (PlayerType)(e.Parent?.Player ?? throw new NotImplementedException());
		}
		/// <summary>
		/// Get the actual beat pattern.
		/// </summary>
		/// <returns>The actual beat pattern.</returns>
		public static Patterns[] RowXs(this AddClassicBeat e)
		{
			if (e.SetXs == null)
			{
				SetRowXs X = e.Parent?.LastOrDefault((SetRowXs i) => i.Active && e.IsBehind(i)) ?? new();
				return X.Pattern;
			}
			else
			{
				Patterns[] T = new Patterns[6];
				AddClassicBeat.ClassicBeatPatterns? setXs = e.SetXs;
				int? num = (setXs != null) ? new int?((int)setXs.GetValueOrDefault()) : null;
				if (((num != null) ? new bool?(num.GetValueOrDefault() == 0) : null).GetValueOrDefault())
				{
					T[1] = Patterns.X;
					T[2] = Patterns.X;
					T[4] = Patterns.X;
					T[5] = Patterns.X;
				}
				else
				{
					num = (setXs != null) ? new int?((int)setXs.GetValueOrDefault()) : null;
					if (!(((num != null) ? new bool?(num.GetValueOrDefault() == 1) : null).GetValueOrDefault()))
					{
						throw new RhythmBaseException("How?");
					}
					T[1] = Patterns.X;
					T[3] = Patterns.X;
					T[5] = Patterns.X;
				}
				return T;
			}
		}
		/// <summary>
		/// Get the special tag of the tag event.
		/// </summary>
		/// <returns>special tags.</returns>
		public static TagAction.SpecialTag[] SpetialTags(this TagAction e) => (TagAction.SpecialTag[])(from i in Enum.GetValues<TagAction.SpecialTag>()
																									   where e.ActionTag.Contains(string.Format("[{0}]", i))
																									   select i);
		/// <summary>
		/// Generate split event instances.
		/// </summary>
		public static IEnumerable<SayReadyGetSetGo> Split(this SayReadyGetSetGo e) => e.PhraseToSay switch
		{
			SayReadyGetSetGo.Words.SayReaDyGetSetGoNew => [
e.SplitCopy(0f, SayReadyGetSetGo.Words.JustSayRea),
e.SplitCopy(e.Tick, SayReadyGetSetGo.Words.JustSayDy),
e.SplitCopy(e.Tick * 2f, SayReadyGetSetGo.Words.JustSayGet),
e.SplitCopy(e.Tick * 3f, SayReadyGetSetGo.Words.JustSaySet),
e.SplitCopy(e.Tick * 4f, SayReadyGetSetGo.Words.JustSayGo)
								],
			SayReadyGetSetGo.Words.SayGetSetGo => [
e.SplitCopy(0f, SayReadyGetSetGo.Words.JustSayGet),
e.SplitCopy(e.Tick, SayReadyGetSetGo.Words.JustSaySet),
e.SplitCopy(e.Tick * 2f, SayReadyGetSetGo.Words.JustSayGo)
								],
			SayReadyGetSetGo.Words.SayReaDyGetSetOne => [
e.SplitCopy(0f, SayReadyGetSetGo.Words.JustSayRea),
e.SplitCopy(e.Tick, SayReadyGetSetGo.Words.JustSayDy),
e.SplitCopy(e.Tick * 2f, SayReadyGetSetGo.Words.JustSayGet),
e.SplitCopy(e.Tick * 3f, SayReadyGetSetGo.Words.JustSaySet),
e.SplitCopy(e.Tick * 4f, SayReadyGetSetGo.Words.Count1)
								],
			SayReadyGetSetGo.Words.SayGetSetOne => [
e.SplitCopy(0f, SayReadyGetSetGo.Words.JustSayGet),
e.SplitCopy(e.Tick, SayReadyGetSetGo.Words.JustSaySet),
e.SplitCopy(e.Tick * 2f, SayReadyGetSetGo.Words.Count1)
								],
			SayReadyGetSetGo.Words.SayReadyGetSetGo => [
e.SplitCopy(0f, SayReadyGetSetGo.Words.JustSayReady),
e.SplitCopy(e.Tick * 2f, SayReadyGetSetGo.Words.JustSayGet),
e.SplitCopy(e.Tick * 3f, SayReadyGetSetGo.Words.JustSaySet),
e.SplitCopy(e.Tick * 4f, SayReadyGetSetGo.Words.JustSayGo)
								],
			_ => [e],
		};
		/// <summary>
		/// Generate split event instances.
		/// </summary>
		public static IEnumerable<AddOneshotBeat> Split(this AddOneshotBeat e)
		{
			e._beat.IfNullThrowException();
			List<AddOneshotBeat> L = [];
			uint loops = e.Loops;
			for (uint i = 0U; i <= loops; i += 1U)
			{
				AddOneshotBeat T = e.MemberwiseClone();
				T.Loops = 0U;
				T.Interval = 0f;
				T.Beat = new RDBeat(e._beat._calculator, unchecked(e.Beat.BeatOnly + i * e.Interval));
				L.Add(T);
			}
			return L.AsEnumerable();
		}
		/// <summary>
		/// Generate split event instances. Follow the most recently activated Xs.
		/// </summary>
		public static IEnumerable<BaseBeat> Split(this AddClassicBeat e)
		{
			SetRowXs x = e.Parent?.LastOrDefault((SetRowXs i) => i.Active && e.IsBehind(i)) ?? new();
			return e.Split(x);
		}
		/// <summary>
		/// Generate split event instances.
		/// </summary>
		public static IEnumerable<BaseBeat> Split(this AddClassicBeat e, SetRowXs Xs)
		{
			List<BaseBeat> L = [];
			AddFreeTimeBeat Head = e.Clone<AddFreeTimeBeat>();
			Head.Pulse = 0;
			Head.Hold = e.Hold;
			L.Add(Head);
			int i = 1;
			do
			{
				if (!(i < 6 && Xs.Pattern[i] == Patterns.X))
				{
					PulseFreeTimeBeat Pulse = e.Clone<PulseFreeTimeBeat>();
					PulseFreeTimeBeat pulseFreeTimeBeat;
					(pulseFreeTimeBeat = Pulse).Beat = pulseFreeTimeBeat.Beat + e.Tick * (float)i;
					if (i >= (int)Xs.SyncoBeat)
						(pulseFreeTimeBeat = Pulse).Beat = pulseFreeTimeBeat.Beat - Xs.SyncoSwing;
					if (i % 2 == 1)
						(pulseFreeTimeBeat = Pulse).Beat = pulseFreeTimeBeat.Beat + (e.Tick - ((e.Swing == 0f) ? e.Tick : e.Swing));
					Pulse.Hold = e.Hold;
					Pulse.Action = PulseFreeTimeBeat.ActionType.Increment;
					L.Add(Pulse);
				}
				i++;
			}
			while (i <= 6);
			return L.AsEnumerable();
		}
		/// <summary>
		/// Calculates the duration of the VFX effect for the given preset.
		/// </summary>
		/// <param name="e">The SetVFXPreset event.</param>
		/// <returns>An RDRange representing the duration of the VFX effect.</returns>
		public static RDRange VFXDuration(this SetVFXPreset e)
		{
			if (e.Preset != SetVFXPreset.Presets.DisableAll && e.Enable)
			{
				SetVFXPreset? close = e.After().FirstOrDefault(i =>
					i.Rooms.Contains(e.Rooms) && (
						i.Preset == e.Preset ||
						i.Preset == SetVFXPreset.Presets.DisableAll
				));
				return new(e.Beat, close?.Beat);
			}
			return new(e.Beat, e.Beat);
		}
		/// <summary>
		/// Remove auxiliary symbols.
		/// </summary>
		public static string TextOnly(this ShowDialogue e)
		{
			string result = e.Text;
			foreach (string item in new string[]
			{
				"shake",
				"shakeRadius=\\d+",
				"wave",
				"waveHeight=\\d+",
				"waveSpeed=\\d+",
				"swirl",
				"swirlRadius=\\d+",
				"swirlSpeed=\\d+",
				"static"
			})
				result = Regex.Replace(result, string.Format("\\[{0}\\]", item), "");
			return result;
		}
		/// <summary>
		/// The visual position of the lower left corner of the image.
		/// </summary>
		public static RDPointE VisualPosition(this Move e, RDSizeE spriteSize)
		{
			RDPointE VisualPosition = default;
			if (e.Position != null && e.Pivot != null && e.Angle != null && e.Angle.Value.IsNumeric && e.Scale != null)
			{
				RDPointE previousPosition = e.Position.Value;
				RDExpression? x = e.Pivot?.X * (e.Scale?.Width) * spriteSize.Width / 100f;
				RDPointE previousPivot = new(x, e.Pivot?.Y * (e.Scale?.Height) * spriteSize.Height / 100f);
				VisualPosition = previousPosition + new RDSizeE(previousPivot.Rotate(e.Angle.Value.NumericValue));
			}
			return VisualPosition;
		}
		/// <summary>
		/// The visual position of the lower left corner of the image.
		/// </summary>
		public static RDPointE VisualPosition(this MoveRoom e)
		{
			RDPointE VisualPosition = default;
			if (e.RoomPosition != null && e.Pivot != null && e.Angle != null)
			{
				RDPointE previousPosition = e.RoomPosition.Value;
				RDPointE previousPivot = new((e.Pivot?.X) * (e.Scale?.Width), (e.Pivot?.Y) * (e.Scale?.Height));
				VisualPosition = previousPosition + new RDSizeE(previousPivot.Rotate(e.Angle.Value.NumericValue));
			}
			return VisualPosition;
		}
		/// <summary>
		/// Creates a rotated rectangle for the MoveCamera event.
		/// </summary>
		/// <param name="e">The MoveCamera event.</param>
		/// <returns>A rotated rectangle representing the camera's position, zoom, and angle.</returns>
		public static RDRotatedRectE RotatedRect(this MoveCamera e) => new(e.CameraPosition, new(e.Zoom, e.Zoom), null, e.Angle);
		/// <summary>
		/// Creates a rotated rectangle for the MoveRow event.
		/// </summary>
		/// <param name="e">The MoveRow event.</param>
		/// <returns>A rotated rectangle representing the row's position, scale, pivot, and angle.</returns>
		public static RDRotatedRectE RotatedRect(this MoveRow e) => new(e.RowPosition, e.Scale, new(e.Pivot, e.Pivot), e.Angle);
		/// <summary>
		/// Creates a rotated rectangle for the MoveRoom event.
		/// </summary>
		/// <param name="e">The MoveRoom event.</param>
		/// <returns>A rotated rectangle representing the room's position, scale, pivot, and angle.</returns>
		public static RDRotatedRectE RotatedRect(this MoveRoom e) => new(e.RoomPosition, e.Scale, e.Pivot, e.Angle);
		/// <summary>
		/// Creates a rotated rectangle for the Move event.
		/// </summary>
		/// <param name="e">The Move event.</param>
		/// <returns>A rotated rectangle representing the position, scale, pivot, and angle.</returns>
		public static RDRotatedRectE RotatedRect(this Move e) => new(e.Position, e.Scale, e.Pivot, e.Angle);
		private static SayReadyGetSetGo SplitCopy(this SayReadyGetSetGo e, float extraBeat, SayReadyGetSetGo.Words word)
		{
			SayReadyGetSetGo Temp = e.Clone<SayReadyGetSetGo>();
			Temp.Beat += extraBeat;
			Temp.PhraseToSay = word;
			Temp.Volume = e.Volume;
			return Temp;
		}
	}
}