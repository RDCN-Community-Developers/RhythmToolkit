namespace RhythmBase.Adofai.Events;
/// <summary>
/// Represents an event that adds a particle effect to the level.
/// </summary>
public class AddParticle : BaseEvent, IBeginningEvent
{
	/// <inheritdoc/>
	public override EventType Type { get; } = EventType.AddParticle;

	/// <summary>
	/// Gets or sets the tag associated with the particle effect.
	/// </summary>
	public string Tag { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the decoration image used for the particle effect.
	/// </summary>
	public string DecorationImage { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the random texture tiling for the particle effect.
	/// </summary>
	public RDPointNI RandomTextureTiling { get; set; } = new RDPointNI(1, 1);

	/// <summary>
	/// Gets or sets the simulation space for the particle effect.
	/// </summary>
	public SimulationSpace SimulationSpace { get; set; } = SimulationSpace.Local;

	/// <summary>
	/// Gets or sets the start rotation of the particle effect.
	/// </summary>
	public RDPointN StartRotation { get; set; } = new RDPointN(0, 0);

	/// <summary>
	/// Gets or sets the color gradient of the particle effect.
	/// </summary>
	public ColorGradient Color { get; set; } = new ColorGradient();

	/// <summary>
	/// Gets or sets the velocity limit over the lifetime of the particles.
	/// </summary>
	public ValueRange VelocityLimitOverLifetime { get; set; } = new ValueRange(0);

	/// <summary>
	/// Gets or sets the color gradient over the lifetime of the particles.
	/// </summary>
	public ColorGradient ColorOverLifetime { get; set; } = new ColorGradient();
	public ValueRange SizeOverLifetime { get; set; } = new ValueRange(100);
	/// <summary>
	/// Gets or sets the maximum number of particles.
	/// </summary>
	public int MaxParticles { get; set; } = 1000;

	/// <summary>
	/// Gets or sets a value indicating whether the particle effect should autoplay.
	/// </summary>
	public bool AutoPlay { get; set; } = true;

	/// <summary>
	/// Gets or sets the duration of the particle effect playback in seconds.
	/// </summary>
	public int PlayDuration { get; set; } = 5;

	/// <summary>
	/// Gets or sets a value indicating whether the particle effect should loop.
	/// </summary>
	public bool Loop { get; set; } = false;

	/// <summary>
	/// Gets or sets the lifetime range of the particles.
	/// </summary>
	public ValueRange ParticleLifetime { get; set; } = new ValueRange(10);

	/// <summary>
	/// Gets or sets the size of the particles.
	/// </summary>
	public RDSizeN ParticleSize { get; set; } = new RDSizeN(100, 100);

	/// <summary>
	/// Gets or sets the velocity range of the particles in the X and Y directions.
	/// </summary>
	public ValueRangePair Velocity { get; set; } = new ValueRangePair(new(0),new(0));

	/// <summary>
	/// Gets or sets the rotation of the particles over time.
	/// </summary>
	public ValueRange RotationOverTime { get; set; } = new ValueRange(0);

	/// <summary>
	/// Gets or sets the shape type of the particle emission.
	/// </summary>
	public EmissionShapeType ShapeType { get; set; } = EmissionShapeType.Circle;

	/// <summary>
	/// Gets or sets the radius of the emission shape.
	/// </summary>
	public float ShapeRadius { get; set; } = 1f;

	/// <summary>
	/// Gets or sets the arc of the emission shape in degrees.
	/// </summary>
	public float Arc { get; set; } = 360f;

	/// <summary>
	/// Gets or sets the mode of the arc emission.
	/// </summary>
	public ArcMode ArcMode { get; set; } = ArcMode.Random;

	/// <summary>
	/// Gets or sets the emission rate of the particles.
	/// </summary>
	public ValueRange EmissionRate { get; set; } = new ValueRange(10);

	/// <summary>
	/// Gets or sets the simulation speed of the particle effect.
	/// </summary>
	public float SimulationSpeed { get; set; } = 100f;

	/// <summary>
	/// Gets or sets the random seed for the particle effect.
	/// </summary>
	public int RandomSeed { get; set; } = 0;

	/// <summary>
	/// Gets or sets the position of the particle effect.
	/// </summary>
	public RDPointN Position { get; set; } = new RDPointN(0, 0);

	/// <summary>
	/// Gets or sets the reference point for decoration placement.
	/// </summary>
	public DecorationRelativeTo RelativeTo { get; set; } = DecorationRelativeTo.Global;

	/// <summary>
	/// Gets or sets the pivot offset of the particle effect.
	/// </summary>
	public RDPointN PivotOffset { get; set; } = new RDPointN(0, 0);

	/// <summary>
	/// Gets or sets the rotation of the particle effect.
	/// </summary>
	public float Rotation { get; set; } = 0;

	/// <summary>
	/// Gets or sets the scale of the particle effect.
	/// </summary>
	public RDPointN Scale { get; set; } = new RDPointN(100, 100);

	/// <summary>
	/// Gets or sets the depth of the particle effect.
	/// </summary>
	public int Depth { get; set; } = -1;

	/// <summary>
	/// Gets or sets the parallax effect of the particle effect.
	/// </summary>
	public RDPointN Parallax { get; set; } = new RDPointN(0, 0);

	/// <summary>
	/// Gets or sets the parallax offset of the particle effect.
	/// </summary>
	public RDPointN ParallaxOffset { get; set; } = new RDPointN(0, 0);

	/// <summary>
	/// Gets or sets a value indicating whether the rotation of the particle effect is locked.
	/// </summary>
	public bool LockRotation { get; set; } = false;

	/// <summary>
	/// Gets or sets a value indicating whether the scale of the particle effect is locked.
	/// </summary>
	public bool LockScale { get; set; } = false;
}
/// <summary>
/// Specifies the mode of the arc emission.
/// </summary>
[RDJsonEnumSerializable]
public enum ArcMode
{
	/// <summary>
	/// Emission occurs at random angles within the arc.
	/// </summary>
	Random,

	/// <summary>
	/// Emission occurs sequentially in a loop within the arc.
	/// </summary>
	Loop,

	/// <summary>
	/// Emission alternates back and forth within the arc.
	/// </summary>
	PingPong,

	/// <summary>
	/// Emission occurs based on the burst speed within the arc.
	/// </summary>
	BurstSpeed,
}
/// <summary>
/// Specifies the simulation space for the particle effect.
/// </summary>
[RDJsonEnumSerializable]
public enum SimulationSpace
{
	/// <summary>
	/// The particle effect is simulated in the local coordinate space.
	/// </summary>
	Local,

	/// <summary>
	/// The particle effect is simulated in the world coordinate space.
	/// </summary>
	World
}
/// <summary>
/// Represents a color gradient used for particle effects.
/// </summary>
public class ColorGradient
{
	/// <summary>
	/// Gets or sets the first color in the gradient.
	/// </summary>
	public RDColor Color1 { get; set; } = RDColor.White;

	/// <summary>
	/// Gets or sets the second color in the gradient.
	/// </summary>
	public RDColor Color2 { get; set; } = RDColor.White;

	/// <summary>
	/// Gets or sets the first gradient configuration.
	/// </summary>
	public Gradient Gradient1 { get; set; } = new Gradient();

	/// <summary>
	/// Gets or sets the second gradient configuration.
	/// </summary>
	public Gradient Gradient2 { get; set; } = new Gradient();

	/// <summary>
	/// Gets or sets the mode of the color gradient.
	/// </summary>
	public ColorMode Mode { get; set; } = ColorMode.Color;
}
/// <summary>
/// Represents a gradient configuration for particle effects.
/// </summary>
public class Gradient
{
	/// <summary>
	/// Gets or sets the mode of the gradient.
	/// </summary>
	public GradientMode Mode { get; set; } = GradientMode.Blend;

	/// <summary>
	/// Gets or sets the alpha keys for the gradient.
	/// </summary>
	public List<AlphaKey> AlphaKeys { get; set; } = [];

	/// <summary>
	/// Gets or sets the color keys for the gradient.
	/// </summary>
	public List<ColorKey> ColorKeys { get; set; } = [];
}
/// <summary>
/// Represents an alpha key in a gradient.
/// </summary>
public struct AlphaKey(float alpha, float time)
{
	/// <summary>
	/// Gets or sets the alpha value of the key.
	/// </summary>
	public float Alpha { get; set; } = alpha;

	/// <summary>
	/// Gets or sets the time value of the key.
	/// </summary>
	public float Time { get; set; } = time;
}
/// <summary>
/// Represents a color key in a gradient.
/// </summary>
public struct ColorKey(float time, RDColor color)
{
	/// <summary>
	/// Gets or sets the time value of the key.
	/// </summary>
	public float Time { get; set; } = time;

	/// <summary>
	/// Gets or sets the color value of the key.
	/// </summary>
	public RDColor Color { get; set; } = color;
}
/// <summary>
/// Specifies the mode of the color gradient.
/// </summary>
[RDJsonEnumSerializable]
public enum ColorMode
{
	/// <summary>
	/// A single color is used.
	/// </summary>
	Color,

	/// <summary>
	/// A gradient is used.
	/// </summary>
	Gradient,

	/// <summary>
	/// Two colors are used.
	/// </summary>
	TwoColors,

	/// <summary>
	/// Two gradients are used.
	/// </summary>
	TwoGradients,

	/// <summary>
	/// A random color is used.
	/// </summary>
	RandomColor,
}
/// <summary>
/// Specifies the mode of the gradient.
/// </summary>
[RDJsonEnumSerializable]
public enum GradientMode
{
	/// <summary>
	/// The gradient blends smoothly between colors.
	/// </summary>
	Blend,

	/// <summary>
	/// The gradient uses fixed color transitions.
	/// </summary>
	Fixed,

	/// <summary>
	/// The gradient uses perceptual blending for smoother transitions.
	/// </summary>
	PerceptualBlend,
}
/// <summary>
/// Represents a range of values with a minimum and maximum.
/// </summary>
public struct ValueRange
{
	/// <summary>
	/// Gets or sets the minimum value of the range.
	/// </summary>
	public float Min { get; set; }

	/// <summary>
	/// Gets or sets the maximum value of the range.
	/// </summary>
	public float Max { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ValueRange"/> struct with the specified minimum and maximum values.
	/// </summary>
	/// <param name="min">The minimum value of the range.</param>
	/// <param name="max">The maximum value of the range.</param>
	public ValueRange(float min, float max)
	{
		this.Min = min;
		this.Max = max;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ValueRange"/> struct with a single value for both minimum and maximum.
	/// </summary>
	/// <param name="value">The value to set for both minimum and maximum.</param>
	public ValueRange(float value)
	{
		this.Min = value;
		this.Max = value;
	}
}
/// <summary>
/// Initializes a new instance of the <see cref="ValueRangePair"/> struct with the specified X and Y value ranges.
/// </summary>
public struct ValueRangePair(ValueRange x, ValueRange y)
{
	/// <summary>
	/// Gets or sets the X value range.
	/// </summary>
	public ValueRange X { get; set; } = x;
	/// <summary>
	/// Gets or sets the Y value range.
	/// </summary>
	public ValueRange Y { get; set; } = y;
}
/// <summary>
/// Specifies the shape type for particle emission.
/// </summary>
[RDJsonEnumSerializable]
public enum EmissionShapeType
{
	/// <summary>
	/// Particles are emitted in a circular shape.
	/// </summary>
	Circle,

	/// <summary>
	/// Particles are emitted in a rectangular shape.
	/// </summary>
	Rectangle,
}
