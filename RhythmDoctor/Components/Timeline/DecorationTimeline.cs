using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.RhythmDoctor.Components.TimeLine;

public record class DecorationTimeline
{
	public Decoration Decoration { get; internal set; }
	public VectorTweenCurve<RDPointN> Position { get; internal set; }
	public VectorTweenCurve<RDPointN> Pivot { get; internal set; }
	public TweenCurve<float> Angle { get; internal set; }
	public VectorTweenCurve<RDSizeN> Size { get; internal set; }
	public Curve<bool> Visible { get; internal set; }
	public Curve<int> Depth { get; internal set; }
	public Curve<RDRoomIndex> Room { get; internal set; }
	public Curve<BlendTypes> Blend { get; internal set; }
	public Curve<string> Animation { get; internal set; }
	public Curve<Borders> Border { get; internal set; }
	public TweenCurve<RDColor> BorderColor { get; internal set; }
	public TweenCurve<RDColor> TintColor { get; internal set; }
	public TweenCurve<int> Opacity { get; internal set; }
	public VectorTweenCurve<RDPointN> TilePosition { get; internal set; }
	public VectorTweenCurve<RDPointN> TileTiling { get; internal set; }
	public VectorTweenCurve<RDPointN> TileSpeed { get; internal set; }
}
