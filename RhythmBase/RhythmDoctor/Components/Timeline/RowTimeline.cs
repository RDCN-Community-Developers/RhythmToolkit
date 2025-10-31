﻿using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Components.Timeline;

/// <summary>
/// Represents the timeline for a row, including its position, size, character, and various visual and logical properties over time.
/// </summary>
public class RowTimeline
{
#pragma warning disable CS8618
	/// <summary>
	/// Gets or sets the row associated with this timeline.
	/// </summary>
	public Row Row { get; internal set; }

	/// <summary>
	/// Gets or sets the beat timeline for this row.
	/// </summary>
	public BeatTimeline BeatTimeline { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the character for this row over time.
	/// </summary>
	public Curve<RDCharacter> Character { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the position of the row over time.
	/// </summary>
	public VectorTweenCurve<RDPointN> RowPosition { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the pivot of the row over time.
	/// </summary>
	public VectorTweenCurve<float> RowPivot { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the angle of the row over time.
	/// </summary>
	public TweenCurve<float> RowAngle { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the size of the row over time.
	/// </summary>
	public VectorTweenCurve<RDSizeN> RowSize { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the position of the character within the row over time.
	/// </summary>
	public VectorTweenCurve<RDPointN> CharacterPosition { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the angle of the character within the row over time.
	/// </summary>
	public TweenCurve<float> CharacterAngle { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the size of the character within the row over time.
	/// </summary>
	public VectorTweenCurve<RDSizeN> CharacterSize { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the position of the heart within the row over time.
	/// </summary>
	public VectorTweenCurve<RDPointN> HeartPosition { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the angle of the heart within the row over time.
	/// </summary>
	public TweenCurve<float> HeartAngle { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the size of the heart within the row over time.
	/// </summary>
	public VectorTweenCurve<RDSizeN> HeartSize { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the visibility of the row over time.
	/// </summary>
	public Curve<bool> Visible { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the expression of the row's character over time.
	/// </summary>
	public Curve<string> Expression { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the room index of the row over time.
	/// </summary>
	public Curve<RDRoomIndex> Room { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the tint color of the row over time.
	/// </summary>
	public TweenCurve<RDColor> TintColor { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the border style of the row over time.
	/// </summary>
	public Curve<Borders> Border { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the border color of the row over time.
	/// </summary>
	public TweenCurve<RDColor> BorderColor { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the opacity of the row over time.
	/// </summary>
	public TweenCurve<int> Opacity { get; internal set; }

	/// <summary>
	/// Gets or sets the curve representing the special effects applied to the row over time.
	/// </summary>
	public Curve<TintRowEffects> Effect { get; internal set; }
}
