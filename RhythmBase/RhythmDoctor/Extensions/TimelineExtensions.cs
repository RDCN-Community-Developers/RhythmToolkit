using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Components.Timeline;
using RhythmBase.RhythmDoctor.Components.TimeLine;
using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Extensions
{
	partial class Extensions
	{
		private class MethodList
		{
			private List<(float time, int action)> list = [];
			public int this[float time]
			{
				set => list.Add((time, value));
			}
			public IEnumerable<(float time, int sum)> GetSums()
			{
				int sum = 0;
				float lastTime = float.NegativeInfinity;
				foreach ((float time, int action) in list.OrderBy(e => e.time))
				{
					sum += action;
					if (time == lastTime) continue;
					lastTime = time;
					yield return (time, sum);
				}
			}
		}
		/// <summary>
		/// Generates a timeline-based view of the specified level, including its decorations, rows, and other elements.
		/// </summary>
		/// <remarks>This method processes the elements of the provided level and organizes them into a structured
		/// timeline view. The returned <see cref="LevelTimeline"/> includes detailed information about decorations, rows, and
		/// other  level components, such as their positions, sizes, visibility, animations, and other properties.</remarks>
		/// <param name="level">The level to generate the timeline view for. This parameter cannot be <see langword="null"/>.</param>
		/// <returns>A <see cref="LevelTimeline"/> object representing the timeline-based view of the level,  including all
		/// decorations, rows, and their associated properties and animations.</returns>
		public static LevelTimeline GetView(this RDLevel level)
		{
			#region decorations
			int decoCount = level.Decorations.Count;
			List<TweenCurveNode<RDPointE>>[] positions = new List<TweenCurveNode<RDPointE>>[decoCount];
			List<TweenCurveNode<RDPointE>>[] pivots = new List<TweenCurveNode<RDPointE>>[decoCount];
			List<TweenCurveNode<float>>[] angles = new List<TweenCurveNode<float>>[decoCount];
			List<TweenCurveNode<RDSizeE>>[] sizes = new List<TweenCurveNode<RDSizeE>>[decoCount];

			List<CurveNode<bool>>[] visibles = new List<CurveNode<bool>>[decoCount];

			List<CurveNode<int>>[] depths = new List<CurveNode<int>>[decoCount];
			List<CurveNode<RDRoomIndex>>[] rooms = new List<CurveNode<RDRoomIndex>>[decoCount];

			List<CurveNode<BlendType>>[] blends = new List<CurveNode<BlendType>>[decoCount];

			List<CurveNode<string>>[] animations = new List<CurveNode<string>>[decoCount];

			List<CurveNode<Border>>[] borders = new List<CurveNode<Border>>[decoCount];
			List<TweenCurveNode<RDColor>>[] borderColors = new List<TweenCurveNode<RDColor>>[decoCount];
			List<TweenCurveNode<RDColor>>[] tints = new List<TweenCurveNode<RDColor>>[decoCount];
			List<TweenCurveNode<int>>[] opacities = new List<TweenCurveNode<int>>[decoCount];

			List<TweenCurveNode<RDPoint>>[] tilePositions = new List<TweenCurveNode<RDPoint>>[decoCount];
			List<TweenCurveNode<RDPoint>>[] tileTilings = new List<TweenCurveNode<RDPoint>>[decoCount];
			List<TweenCurveNode<RDPoint>>[] tileSpeeds = new List<TweenCurveNode<RDPoint>>[decoCount];
			#endregion
			#region rows
			int rowCount = level.Rows.Count;
			List<CurveNode<int>>[] hitIndices = new List<CurveNode<int>>[rowCount];
			List<CurveNode<bool>>[] isHoldings = new List<CurveNode<bool>>[rowCount];

			List<CurveNode<int>>[] beatIndices = new List<CurveNode<int>>[rowCount];
			List<CurveNode<Pattern[]>>[] patterns = new List<CurveNode<Pattern[]>>[rowCount];

			List<CurveNode<OneshotPulseShapeType>>[] pulseTypes = new List<CurveNode<OneshotPulseShapeType>>[rowCount];
			List<CurveNode<int>>[] beatCounts = new List<CurveNode<int>>[rowCount];
			List<CurveNode<bool>>[] isSkippings = new List<CurveNode<bool>>[rowCount];
			List<CurveNode<OneshotPulseState>>[] pulseStates = new List<CurveNode<OneshotPulseState>>[rowCount];
			#endregion
			#region rooms
			#endregion
			foreach (IBaseEvent e in level)
			{
				switch (e)
				{
					#region decorations
					case Blend blend:
						if (blend.Parent is null) continue;
						(blends[blend.Parent.Index] ??= []).Add(new(blend.Beat.BeatOnly, blend.BlendType));
						break;
					case Move move:
						if (move.Parent is null) continue;
						if (move.Position is not null)
							(positions[move.Parent.Index] ??= []).Add(new(move.Beat.BeatOnly, move.Position.Value, new(move.Ease, move.Duration)));
						if (move.Pivot is not null)
							(pivots[move.Parent.Index] ??= []).Add(new(move.Beat.BeatOnly, move.Pivot.Value, new(move.Ease, move.Duration)));
						if (move.Angle is not null)
							(angles[move.Parent.Index] ??= []).Add(new(move.Beat.BeatOnly, move.Angle?.NumericValue ?? 0, new(move.Ease, move.Duration)));
						if (move.Scale is not null)
							(sizes[move.Parent.Index] ??= []).Add(new(move.Beat.BeatOnly, move.Scale.Value, new(move.Ease, move.Duration)));
						break;
					case PlayAnimation playAnimation:
						if (playAnimation.Parent is null) continue;
						(animations[playAnimation.Parent.Index] ??= []).Add(new(playAnimation.Beat.BeatOnly, playAnimation.Expression));
						break;
					case ReorderSprite reorderSprite:
						if (reorderSprite.Parent is null) continue;
						(depths[reorderSprite.Parent.Index] ??= []).Add(new(reorderSprite.Beat.BeatOnly, reorderSprite.Depth));
						(rooms[reorderSprite.Parent.Index] ??= []).Add(new(reorderSprite.Beat.BeatOnly, reorderSprite.NewRoom));
						break;
					case SetVisible setVisible:
						if (setVisible.Parent is null) continue;
						(visibles[setVisible.Parent.Index] ??= []).Add(new(setVisible.Beat.BeatOnly, setVisible.Visible));
						break;
					case Tile tile:
						if (tile.Parent is null) continue;
						if (tile.Position is not null)
							(tilePositions[tile.Parent.Index] ??= []).Add(new(tile.Beat.BeatOnly, tile.Position.Value, new(tile.Ease, tile.Duration)));
						if (tile.Tiling is not null)
							(tileTilings[tile.Parent.Index] ??= []).Add(new(tile.Beat.BeatOnly, tile.Tiling.Value, new(tile.Ease, tile.Duration)));
						if (tile.Speed is not null)
							(tileSpeeds[tile.Parent.Index] ??= []).Add(new(tile.Beat.BeatOnly, tile.Speed.Value, new(tile.Ease, tile.Duration)));
						break;
					case Tint tint:
						if (tint.Parent is null) continue;
						if (tint.IsTint)
							(tints[tint.Parent.Index] ??= []).Add(new(tint.Beat.BeatOnly, tint.TintColor, new(tint.Ease, tint.Duration)));
						if (tint.Border != Border.None)
							(borderColors[tint.Parent.Index] ??= []).Add(new(tint.Beat.BeatOnly, tint.BorderColor, new(tint.Ease, tint.Duration)));
						(borders[tint.Parent.Index] ??= []).Add(new(tint.Beat.BeatOnly, tint.Border));
						(opacities[tint.Parent.Index] ??= []).Add(new(tint.Beat.BeatOnly, tint.Opacity, new(tint.Ease, tint.Duration)));
						break;
					#endregion
					#region rows
					case AddClassicBeat addClassicBeat:
						if (addClassicBeat.Parent is null || addClassicBeat.Parent.RowType is not RowType.Classic) continue;

						break;
					case AddOneshotBeat addOneshotBeat:
						if (addOneshotBeat.Parent is null || addOneshotBeat.Parent.RowType is not RowType.Oneshot) continue;

						break;
					case PulseFreeTimeBeat pulseFreeTimeBeat:
						if (pulseFreeTimeBeat.Parent is null || pulseFreeTimeBeat.Parent.RowType is not RowType.Oneshot) continue;

						break;
					case SetRowXs setRowXs:
						if (setRowXs.Parent is null || setRowXs.Parent.RowType is not RowType.Classic) continue;

						break;
					case PlayExpression playExpression:
						if (playExpression.Parent is null) continue;

						break;
					case HideRow hideRow:
						if (hideRow.Parent is null) continue;

						break;
					case MoveRow moveRow:
						if (moveRow.Parent is null) continue;

						break;
					case TintRows tintRows:
						if (tintRows.Parent is null)
						{

						}
						else
						{

						}
						break;
					case ReorderRow reorderRow:
						if (reorderRow.Parent is null) continue;

						break;
					#endregion
					#region rooms
					#endregion
					default:
						break;
				}
			}
			return new()
			{
				Decorations = [..Enumerable
				.Range(0, decoCount)
				.Select(i => new DecorationTimeline()
				{
					Decoration = level.Decorations[i],
					Position = new(
					[
						[..positions[i].Where(p=>p.Target.X is not null).Select(p=>new TweenCurveNode<float>(p.Time, p.Target.X?.NumericValue ?? 0, p.EaseInfo))],
						[..positions[i].Where(p=>p.Target.Y is not null).Select(p=>new TweenCurveNode<float>(p.Time, p.Target.Y?.NumericValue ?? 0, p.EaseInfo))],
					],
					new RDPointN(50, 50),
					(from, to, x) => (float)(from + (to - from) * x),
					p => [p.X, p.Y],
					v => new(v[0], v[1])),
					Pivot = new(
					[
						[..pivots[i].Where(p=>p.Target.X is not null).Select(p=>new TweenCurveNode<float>(p.Time, p.Target.X?.NumericValue ?? 0, p.EaseInfo))],
						[..pivots[i].Where(p=>p.Target.Y is not null).Select(p=>new TweenCurveNode<float>(p.Time, p.Target.Y?.NumericValue ?? 0, p.EaseInfo))],
					],
					new RDPointN(50, 50),
					(from, to, x) => (float)(from + (to - from) * x),
					p => [p.X, p.Y],
					v => new(v[0], v[1])),
					Angle = new([.. angles[i]], 0, (from, to, x) => from + (to - from) * (float)x),
					Size = new(
					[
						[..sizes[i].Where(p=>p.Target.Width is not null).Select(p=>new TweenCurveNode<float>(p.Time, p.Target.Width?.NumericValue ?? 0,p.EaseInfo))],
						[..sizes[i].Where(p=>p.Target.Height is not null).Select(p=>new TweenCurveNode<float>(p.Time, p.Target.Height?.NumericValue ?? 0, p.EaseInfo))],
					],
					new RDSizeN(100, 100),
					(from, to, x) => (float)(from + (to - from) * x),
					p => [p.Width, p.Height],
					v => new(v[0], v[1])),
					Visible = new([.. visibles[i]], level.Decorations[i].Visible),
					Depth = new([.. depths[i]], level.Decorations[i].Depth),
					Room = new([.. rooms[i]], level.Decorations[i].Room.Room),
					Blend = new([.. blends[i]], BlendType.None),
					Animation = new([.. animations[i]], ""),
					Border = new([.. borders[i]], Border.None),
					BorderColor = new([.. borderColors[i]], RDColor.Transparent, (from, to, x) => RDColor.Lerp(from, to, (float)x)),
					TintColor = new([.. tints[i]], RDColor.White, (from, to, x) => RDColor.Lerp(from, to, (float)x)),
					Opacity = new([.. opacities[i]], 100, (from, to, x) => (int)(from + (to - from) * x)),
					TilePosition = new(
					[
						[..tilePositions[i].Where(p=>p.Target.X is not null).Select(p=>new TweenCurveNode<float>(p.Time, p.Target.X ?? 0, p.EaseInfo))],
						[..tilePositions[i].Where(p=>p.Target.Y is not null).Select(p=>new TweenCurveNode<float>(p.Time, p.Target.Y ?? 0, p.EaseInfo))],
					],
					new RDPointN(1, 1),
					(from, to, x) => (float)(from + (to - from) * x),
					p => [p.X, p.Y],
					v => new(v[0], v[1])),
					TileSpeed = new(
					[
						[..tilePositions[i].Where(p=>p.Target.X is not null).Select(p=>new TweenCurveNode<float>(p.Time, p.Target.X ?? 0, p.EaseInfo))],
						[..tilePositions[i].Where(p=>p.Target.Y is not null).Select(p=>new TweenCurveNode<float>(p.Time, p.Target.Y ?? 0, p.EaseInfo))],
					],
					new RDPointN(0, 0),
					(from, to, x) => (float)(from + (to - from) * x),
					p => [p.X, p.Y],
					v => new(v[0], v[1])),
					TileTiling = new(
					[
						[..tilePositions[i].Where(p=>p.Target.X is not null).Select(p=>new TweenCurveNode<float>(p.Time, p.Target.X ?? 0, p.EaseInfo))],
						[..tilePositions[i].Where(p=>p.Target.Y is not null).Select(p=>new TweenCurveNode<float>(p.Time, p.Target.Y ?? 0, p.EaseInfo))],
					],
					new RDPointN(0, 0),
					(from, to, x) => (float)(from + (to - from) * x),
					p => [p.X, p.Y],
					v => new(v[0], v[1])),
				})],

			};
		}
	}
}
