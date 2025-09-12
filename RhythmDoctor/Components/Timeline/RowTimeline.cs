using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Components.Timeline;

public class RowTimeline
{
	public Row Row { get; internal set; }
	public BeatTimeline BeatTimeline { get; internal set; }
	public Curve<RDCharacter> Character { get; internal set; }
	public VectorTweenCurve<RDPointN> RowPosition { get; internal set; }
	public VectorTweenCurve<float> RowPivot { get; internal set; }
	public TweenCurve<float> RowAngle { get; internal set; }
	public VectorTweenCurve<RDSizeN> RowSize { get; internal set; }
	public VectorTweenCurve<RDPointN> CharacterPosition { get; internal set; }
	public TweenCurve<float> CharacterAngle { get; internal set; }
	public VectorTweenCurve<RDSizeN> CharacterSize { get; internal set; }
	public VectorTweenCurve<RDPointN> HeartPosition { get; internal set; }
	public TweenCurve<float> HeartAngle { get; internal set; }
	public VectorTweenCurve<RDSizeN> HeartSize { get; internal set; }
	public Curve<bool> Visible { get; internal set; }
	public Curve<string> Expression { get; internal set; }
	public Curve<RDRoomIndex> Room { get; internal set; }
	public TweenCurve<RDColor> TintColor { get;internal set; }
	public Curve<Borders> Border { get; internal set; }
	public TweenCurve<RDColor> BorderColor { get; internal set; }
	public TweenCurve<int> Opacity { get; internal set; }
	public Curve<TintRowEffects> Effect { get; internal set; }
}
