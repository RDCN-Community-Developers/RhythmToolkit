using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.Adofai;

/// <summary>
/// Represents the types of events available in the Adofai editor.
/// </summary>
[RDJsonEnumSerializable]
public enum EventType

{
	/// <summary>
	/// Adds a decoration to the level.
	/// </summary>
	/// <summary>  
	/// Adds a decoration to the level.  
	/// </summary>  
	AddDecoration,
	/// <summary>  
	/// Adds a particle effect to the level.  
	/// </summary>  
	AddParticle,
	/// <summary>
	/// Adds an object to the level.
	/// </summary>
	AddObject,
	/// <summary>
	/// Adds text to the level.
	/// </summary>
	AddText,
	/// <summary>
	/// Animates a track in the level.
	/// </summary>
	AnimateTrack,
	/// <summary>
	/// Enables autoplay for specific tiles.
	/// </summary>
	AutoPlayTiles,
	/// <summary>
	/// Adjusts the bloom effect in the level.
	/// </summary>
	Bloom,
	/// <summary>
	/// Adds a bookmark to the level.
	/// </summary>
	Bookmark,
	/// <summary>
	/// Represents a change track for monitoring modifications to a collection of items.
	/// </summary>
	ChangeTrack,
	/// <summary>
	/// Creates a checkpoint in the level.
	/// </summary>
	Checkpoint,
	/// <summary>
	/// Changes the color of a track.
	/// </summary>
	ColorTrack,
	/// <summary>
	/// Sets a custom background for the level.
	/// </summary>
	CustomBackground,
	/// <summary>
	/// Defines a custom event in the level.
	/// </summary>
	ForwardEvent,
	/// <summary>
	/// Defines a custom tile event in the level.
	/// </summary>
	ForwardTileEvent,
	/// <summary>
	/// Adds a comment in the editor.
	/// </summary>
	EditorComment,
	/// <summary>  
	/// Emits a particle effect in the level.  
	/// </summary>  
	EmitParticle,
	/// <summary>
	/// Creates a flash effect in the level.
	/// </summary>
	Flash,
	/// <summary>
	/// Enables free roam mode.
	/// </summary>
	FreeRoam,
	/// <summary>
	/// Removes free roam mode.
	/// </summary>
	FreeRoamRemove,
	/// <summary>
	/// Adds a twirl effect in free roam mode.
	/// </summary>
	FreeRoamTwirl,
	/// <summary>
	/// Represents a warning message displayed when the user is in free roam mode.
	/// </summary>
	FreeRoamWarning,
	/// <summary>
	/// Applies a hall of mirrors effect.
	/// </summary>
	HallOfMirrors,
	/// <summary>
	/// Hides an object or element in the level.
	/// </summary>
	Hide,
	/// <summary>
	/// Holds an action or event.
	/// </summary>
	Hold,
	/// <summary>
	/// Moves the camera to a specific position.
	/// </summary>
	MoveCamera,
	/// <summary>
	/// Moves decorations in the level.
	/// </summary>
	MoveDecorations,
	/// <summary>
	/// Moves a track to a specific position.
	/// </summary>
	MoveTrack,
	/// <summary>
	/// Enables multi-planet mode.
	/// </summary>
	MultiPlanet,
	/// <summary>
	/// Pauses the level.
	/// </summary>
	Pause,
	/// <summary>
	/// Plays a sound in the level.
	/// </summary>
	PlaySound,
	/// <summary>
	/// Sets the position of a track.
	/// </summary>
	PositionTrack,
	/// <summary>
	/// Recolors a track in the level.
	/// </summary>
	RecolorTrack,
	/// <summary>
	/// Repeats specific events in the level.
	/// </summary>
	RepeatEvents,
	/// <summary>
	/// Scales the margin of the level.
	/// </summary>
	ScaleMargin,
	/// <summary>
	/// Scales the planets in the level.
	/// </summary>
	ScalePlanets,
	/// <summary>
	/// Scales the radius of the level.
	/// </summary>
	ScaleRadius,
	/// <summary>
	/// Scrolls the screen in the level.
	/// </summary>
	ScreenScroll,
	/// <summary>
	/// Sets the screen tile effect.
	/// </summary>
	ScreenTile,
	/// <summary>
	/// Sets the background of the level.
	/// </summary>
	SetBackground,
	/// <summary>
	/// Sets conditional events in the level.
	/// </summary>
	SetConditionalEvents,
	/// <summary>
	/// Sets the default text for the level.
	/// </summary>
	SetDefaultText,
	/// <summary>
	/// Applies a filter effect in the level.
	/// </summary>
	SetFilter,
	/// <summary>
	/// Sets an advanced filter effect in the level.
	/// </summary>
	SetFilterAdvanced,
	/// <summary>
	/// Sets the frame rate for the level.
	/// </summary>
	SetFrameRate,
	/// <summary>
	/// Sets the hitsound for the level.
	/// </summary>
	SetHitsound,
	/// <summary>
	/// Sets the hold sound for the level.
	/// </summary>
	SetHoldSound,
	/// <summary>
	/// Sets the input event for the level.
	/// </summary>
	SetInputEvent,
	/// <summary>
	/// Sets an object in the level.
	/// </summary>
	SetObject,
	/// <summary>
	/// Sets the particle for the level.
	/// </summary>
	SetParticle,
	/// <summary>
	/// Sets the rotation of a planet.
	/// </summary>
	SetPlanetRotation,
	/// <summary>
	/// Sets the speed of the level.
	/// </summary>
	SetSpeed,
	/// <summary>
	/// Sets text in the level.
	/// </summary>
	SetText,
	/// <summary>
	/// Creates a screen shake effect.
	/// </summary>
	ShakeScreen,
	/// <summary>
	/// Adds a twirl effect to the level.
	/// </summary>
	Twirl
}/// <summary>
 /// Specifies how a decoration's hitbox trigger should behave when activated.
 /// </summary>
[RDJsonEnumSerializable]
public enum HitboxTriggerType
{
	/// <summary>
	/// The hitbox triggers a single time and then stops until reactivated.
	/// </summary>
	Once,
	/// <summary>
	/// The hitbox triggers each time a distinct touch or contact occurs.
	/// </summary>
	PerTouch,
	/// <summary>
	/// The hitbox triggers repeatedly at the configured repeat interval while active.
	/// </summary>
	Repeat,
}
/// <summary>
/// Defines what kinds of objects the hitbox detection system should consider as valid targets.
/// </summary>
[RDJsonEnumSerializable]
public enum HitboxDetectTarget
{
	/// <summary>
	/// The hitbox detects collisions with planets.
	/// </summary>
	Planet,
	/// <summary>
	/// The hitbox detects collisions with decorations.
	/// </summary>
	Decoration,
}
/// <summary>
/// Specifies which planets are considered by the hitbox when the detect target is a planet.
/// </summary>
[RDJsonEnumSerializable]
public enum HitboxTargetPlanet
{
	/// <summary>
	/// The hitbox can target any planet (no restriction).
	/// </summary>
	Any,
	/// <summary>
	/// The hitbox targets the central planet.
	/// </summary>
	Center,
	/// <summary>
	/// The hitbox targets orbiting planets.
	/// </summary>
	Orbiting,
}
/// <summary>
/// Specifies the coordinate space or reference frame used to interpret an object's position and pivot offsets.
/// </summary>
/// <remarks>
/// Use this enum to indicate whether object coordinates are evaluated relative to a tile, the global level
/// coordinate system, a specific planet, the camera, or the last used object position. This affects how
/// Position, PivotOffset and related properties are applied when the object is placed or rendered.
/// </remarks>
[RDJsonEnumSerializable]
public enum ObjectRelativeTo
{
	/// <summary>
	/// Position is relative to the current tile. The object's coordinates are interpreted in the local tile space.
	/// </summary>
	Tile,
	/// <summary>
	/// Position is in global (level) coordinates. The object's coordinates are independent of individual tiles.
	/// </summary>
	Global,
	/// <summary>
	/// Position is relative to the red planet. Coordinates are interpreted in the red planet's local space.
	/// </summary>
	RedPlanet,
	/// <summary>
	/// Position is relative to the blue planet. Coordinates are interpreted in the blue planet's local space.
	/// </summary>
	BluePlanet,
	/// <summary>
	/// Position is relative to the green planet. Coordinates are interpreted in the green planet's local space.
	/// </summary>
	GreenPlanet,
	/// <summary>
	/// Position is relative to the camera. Use this to place objects in camera space (camera-local coordinates).
	/// </summary>
	Camera,
	/// <summary>
	/// Position is relative to the camera's aspect or viewport space. Useful for screen-aligned placements that depend on aspect ratio.
	/// </summary>
	CameraAspect,
	/// <summary>
	/// Position uses the last known position used by a previously placed object (e.g. for repeated placements).
	/// </summary>
	LastPosition,
}

/// <summary>  
/// Represents the types of objects that can be added.  
/// </summary>  
[RDJsonEnumSerializable]
public enum ObjectType
{
	/// <summary>  
	/// Represents a floor object.  
	/// </summary>  
	Floor,
	/// <summary>  
	/// Represents a planet object.  
	/// </summary>  
	Planet
}
/// <summary>  
/// Represents the types of planet colors.  
/// </summary>  
[RDJsonEnumSerializable]
public enum PlanetColorType
{
	/// <summary>  
	/// Default red color.  
	/// </summary>  
	DefaultRed,
	/// <summary>  
	/// Custom planet color type.  
	/// </summary>  
	planetColorType,
	/// <summary>  
	/// Gold color.  
	/// </summary>  
	Gold,
	/// <summary>  
	/// Overseer color.  
	/// </summary>  
	Overseer,
	/// <summary>  
	/// Custom color.  
	/// </summary>  
	Custom
}
/// <summary>  
/// Represents the types of tracks.  
/// </summary>  
[RDJsonEnumSerializable]
public enum TrackType
{
	/// <summary>  
	/// Normal track type.  
	/// </summary>  
	Normal,
	/// <summary>  
	/// Midspin track type.  
	/// </summary>  
	Midspin
}
/// <summary>
/// Specifies the type of icon used to represent a track element in the application.
/// </summary>
/// <remarks>The <see cref="TrackIconType"/> enumeration provides various icon types that can be used to visually
/// represent different track elements. Each value corresponds to a specific icon style, such as a swirl, snail, or
/// rabbit, which can be used to convey different meanings or statuses within the application's user
/// interface.</remarks>
[RDJsonEnumSerializable]
public enum TrackIconType
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
	None,
	Swirl,
	Snail,
	DoubleSnail,
	Rabbit,
	DoubleRabbit,
	Checkpoint,
	HoldArrowShort,
	HoldArrowLong,
	HoldReleaseShort,
	HoldReleaseLong,
	MultiPlanetTwo,
	MultiPlanetThreeMore,
	Portal,
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
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
/// Specifies the hitbox types available for the decoration.  
/// </summary>  
[RDJsonEnumSerializable]
public enum HitboxTypes
{
	/// <summary>  
	/// No hitbox is applied.  
	/// </summary>  
	None,
	/// <summary>  
	/// A hitbox that causes the player to fail when touched.  
	/// </summary>  
	Kill,
	/// <summary>  
	/// A hitbox that triggers an event when touched.  
	/// </summary>  
	Event
}
/// <summary>  
/// Specifies the blend modes available for the decoration.  
/// </summary>  
[RDJsonEnumSerializable]
public enum BlendMode
{
	/// <summary>  
	/// No blending is applied.  
	/// </summary>  
	None,
	/// <summary>  
	/// Darkens the image by selecting the darker of the base or blend colors.  
	/// </summary>  
	Darken,
	/// <summary>  
	/// Multiplies the base color by the blend color.  
	/// </summary>  
	Multiply,
	/// <summary>  
	/// Darkens the base color to reflect the blend color by increasing contrast.  
	/// </summary>  
	ColorBurn,
	/// <summary>  
	/// Darkens the base color by decreasing brightness.  
	/// </summary>  
	LinearBurn,
	/// <summary>  
	/// Darkens the image by selecting the darkest color.  
	/// </summary> 
	DarkerColor,
	/// <summary>  
	/// Lightens the image by selecting the lighter of the base or blend colors.  
	/// </summary>  
	Lighten,
	/// <summary>  
	/// Multiplies the inverse of the base and blend colors.  
	/// </summary>  
	Screen,
	/// <summary>  
	/// Brightens the base color to reflect the blend color by decreasing contrast.  
	/// </summary>  
	ColorDodge,
	/// <summary>  
	/// Brightens the base color by increasing brightness.  
	/// </summary>  
	LinearDodge,
	/// <summary>  
	/// Lightens the image by selecting the lightest color.  
	/// </summary>  
	LighterColor,
	/// <summary>  
	/// Combines the base and blend colors to create a soft overlay effect.  
	/// </summary>  
	Overlay,
	/// <summary>  
	/// Applies a soft light effect to the base color.  
	/// </summary>  
	SoftLight,
	/// <summary>  
	/// Applies a hard light effect to the base color.  
	/// </summary>  
	HardLight,
	/// <summary>  
	/// Increases the contrast of the base color using the blend color.  
	/// </summary>  
	VividLight,
	/// <summary>  
	/// Adjusts the brightness of the base color using the blend color.  
	/// </summary>  
	LinearLight,
	/// <summary>  
	/// Replaces the base color with the blend color where the blend color is darker.  
	/// </summary>  
	PinLight,
	/// <summary>  
	/// Creates a high-contrast effect by combining the base and blend colors.  
	/// </summary>  
	HardMix,
	/// <summary>  
	/// Subtracts the darker color from the lighter color.  
	/// </summary>  
	Difference,
	/// <summary>  
	/// Subtracts the base color from the blend color or vice versa to create an exclusion effect.  
	/// </summary>  
	Exclusion,
	/// <summary>  
	/// Subtracts the blend color from the base color.  
	/// </summary>  
	Subtract,
	/// <summary>  
	/// Divides the base color by the blend color.  
	/// </summary>  
	Divide,
	/// <summary>  
	/// Applies the hue of the blend color to the base color.  
	/// </summary>  
	Hue,
	/// <summary>  
	/// Applies the saturation of the blend color to the base color.  
	/// </summary>  
	Saturation,
	/// <summary>  
	/// Applies the color of the blend color to the base color.  
	/// </summary>  
	Color,
	/// <summary>  
	/// Applies the luminosity of the blend color to the base color.  
	/// </summary>  
	Luminosity
}
/// <summary>
/// Specifies the reference point for the camera in the game.
/// </summary>
[RDJsonEnumSerializable]
public enum CameraRelativeTo
{
	/// <summary>
	/// The camera is relative to the player.
	/// </summary>
	Player,
	/// <summary>
	/// The camera is relative to the tile.
	/// </summary>
	Tile,
	/// <summary>
	/// The camera is relative to a global position.
	/// </summary>
	Global,
	/// <summary>
	/// The camera is relative to the last position.
	/// </summary>
	LastPosition,
	/// <summary>
	/// The camera is relative to the last position without considering rotation.
	/// </summary>
	LastPositionNoRotation
}
/// <summary>
/// Represents the different types of track colors available in the application.
/// </summary>
[RDJsonEnumSerializable]
public enum TrackColorType
{
	/// <summary>
	/// Represents a state where no specific value or option is selected.
	/// </summary>
	None,
	/// <summary>
	/// A single solid color for the track.
	/// </summary>
	Single,
	/// <summary>
	/// Alternating stripes of colors for the track.
	/// </summary>
	Stripes,
	/// <summary>
	/// A glowing effect for the track.
	/// </summary>
	Glow,
	/// <summary>
	/// A blinking effect for the track.
	/// </summary>
	Blink,
	/// <summary>
	/// A switching effect between different colors for the track.
	/// </summary>
	Switch,
	/// <summary>
	/// A rainbow gradient effect for the track.
	/// </summary>
	Rainbow,
	/// <summary>
	/// A color effect based on the volume of the track.
	/// </summary>
	Volume,
}
/// <summary>
/// Represents the types of track animations available in the ADTrack system.
/// </summary>
[RDJsonEnumSerializable]
public enum TrackAnimationType
{
	/// <summary>
	/// No animation.
	/// </summary>
	None,
	/// <summary>
	/// Assemble animation type.
	/// </summary>
	Assemble,
	/// <summary>
	/// Assemble animation type with a far effect.
	/// </summary>
	Assemble_Far,
	/// <summary>
	/// Extend animation type.
	/// </summary>
	Extend,
	/// <summary>
	/// Grow animation type.
	/// </summary>
	Grow,
	/// <summary>
	/// Grow animation type with a spinning effect.
	/// </summary>
	Grow_Spin,
	/// <summary>
	/// Fade animation type.
	/// </summary>
	Fade,
	/// <summary>
	/// Drop animation type.
	/// </summary>
	Drop,
	/// <summary>
	/// Rise animation type.
	/// </summary>
	Rise
}
/// <summary>
/// Represents the types of animations used for track disappearance.
/// </summary>
[RDJsonEnumSerializable]
public enum TrackDisappearAnimationType
{
	/// <summary>
	/// No animation is applied.
	/// </summary>
	None,
	/// <summary>
	/// The track disappears with a scatter effect.
	/// </summary>
	Scatter,
	/// <summary>
	/// The track disappears with a far scatter effect.
	/// </summary>
	Scatter_Far,
	/// <summary>
	/// The track retracts before disappearing.
	/// </summary>
	Retract,
	/// <summary>
	/// The track shrinks before disappearing.
	/// </summary>
	Shrink,
	/// <summary>
	/// The track shrinks and spins before disappearing.
	/// </summary>
	Shrink_Spin,
	/// <summary>
	/// The track fades out before disappearing.
	/// </summary>
	Fade
}
/// <summary>
/// Represents the different styles of tracks available in the game.
/// </summary>
[RDJsonEnumSerializable]
public enum TrackStyle
{
	/// <summary>
	/// The standard track style.
	/// </summary>
	Standard,
	/// <summary>
	/// A neon-themed track style.
	/// </summary>
	Neon,
	/// <summary>
	/// A neon light-themed track style.
	/// </summary>
	NeonLight,
	/// <summary>
	/// A basic and simple track style.
	/// </summary>
	Basic,
	/// <summary>
	/// A track style featuring gem-like visuals.
	/// </summary>
	Gems,
	/// <summary>
	/// A minimalistic track style.
	/// </summary>
	Minimal
}
/// <summary>  
/// Defines the types of speed adjustments available.  
/// </summary>  
[RDJsonEnumSerializable]
public enum SpeedType
{
	/// <summary>  
	/// Adjust speed using beats per minute (BPM).  
	/// </summary>  
	Bpm,
	/// <summary>  
	/// Adjust speed using a multiplier.  
	/// </summary>  
	Multiplier
}
/// <summary>  
/// Specifies the relative type of a tile reference.  
/// </summary>  
[RDJsonEnumSerializable]
public enum RelativeType
{
	/// <summary>  
	/// Refers to the current tile.  
	/// </summary>  
	ThisTile,
	/// <summary>  
	/// Refers to the starting tile.  
	/// </summary>  
	Start,
	/// <summary>  
	/// Refers to the ending tile.  
	/// </summary>  
	End,
}
/// <summary>
/// Represents the types of default background tile shapes.
/// </summary>
[RDJsonEnumSerializable]
public enum DefaultBGTileShapeType
{
	/// <summary>
	/// The default background tile shape.
	/// </summary>
	Default,

	/// <summary>
	/// A single color background tile shape.
	/// </summary>
	SingleColor,

	/// <summary>
	/// A disabled background tile shape.
	/// </summary>
	Disabled,
}
/// <summary>
/// Represents the different modes for displaying a background.
/// </summary>
[RDJsonEnumSerializable]
public enum BgDisplayMode
{
	/// <summary>
	/// Scales the background to fit the screen dimensions.
	/// </summary>
	FitToScreen,
	/// <summary>
	/// Displays the background without scaling.
	/// </summary>
	Unscaled,
	/// <summary>
	/// Tiles the background to fill the screen.
	/// </summary>
	Tiled
}

/// <summary>  
/// Represents the display modes for the background image.  
/// </summary>  
[RDJsonEnumSerializable]
public enum BackgroundDisplayMode
{
	/// <summary>  
	/// Fits the background image to the screen.  
	/// </summary>  
	FitToScreen,
	/// <summary>  
	/// Displays the background image without scaling.  
	/// </summary>  
	Unscaled,
	/// <summary>  
	/// Tiles the background image.  
	/// </summary>  
	Tiled
}
/// <summary>
/// Specifies the reference point for decoration placement in the game.
/// </summary>
[RDJsonEnumSerializable]
public enum DecorationRelativeTo
{
	/// <summary>
	/// Decoration is positioned relative to the tile.
	/// </summary>
	Tile,
	/// <summary>
	/// Decoration is positioned relative to the global coordinate system.
	/// </summary>
	Global,
	/// <summary>
	/// Decoration is positioned relative to the red planet.
	/// </summary>
	RedPlanet,
	/// <summary>
	/// Decoration is positioned relative to the blue planet.
	/// </summary>
	BluePlanet,
	/// <summary>
	/// Decoration is positioned relative to the green planet.
	/// </summary>
	GreenPlanet,
	/// <summary>
	/// Decoration is positioned relative to the camera.
	/// </summary>
	Camera,
	/// <summary>
	/// Decoration is positioned relative to the camera's aspect ratio.
	/// </summary>
	CameraAspect,
	/// <summary>
	/// Decoration is positioned relative to the last known position.
	/// </summary>
	LastPosition
}
/// <summary>
/// Represents the behaviors for easing parts in the ADEase system.
/// </summary>
[RDJsonEnumSerializable]
public enum EasePartBehaviors
{
	/// <summary>
	/// Repeats the easing behavior.
	/// </summary>
	Repeat,
	/// <summary>
	/// Mirrors the easing behavior.
	/// </summary>
	Mirror
}
/// <summary>  
/// Specifies the fail hitbox types available for the decoration.  
/// </summary>  
[RDJsonEnumSerializable]
public enum FailHitboxTypes
{
	/// <summary>  
	/// A rectangular fail hitbox.  
	/// </summary>  
	Box,
	/// <summary>  
	/// A circular fail hitbox.  
	/// </summary>  
	Circle,
	/// <summary>  
	/// A capsule-shaped fail hitbox.  
	/// </summary>  
	Capsule
}
/// <summary>
/// Specifies the plane on which a flash effect is rendered.
/// Use this enum to select whether the flash should appear behind or in front of foreground elements.
/// </summary>
[RDJsonEnumSerializable]
public enum FlashPlane
{
	/// <summary>
	/// Render the flash on the background plane (behind most foreground elements).
	/// </summary>
	Background,
	/// <summary>
	/// Render the flash on the foreground plane (in front of most foreground elements).
	/// </summary>
	Foreground,
}
/// <summary>
/// Specifies the direction used for angle correction when Free Roam mode adjusts or normalizes angles.
/// </summary>
/// <remarks>
/// Angle correction controls how the editor corrects or resolves angular discontinuities
/// when exiting or interacting with free roam. Use <see cref="None"/> to disable correction,
/// <see cref="Forward"/> to prefer increasing angle adjustments, or <see cref="Backward"/> to prefer decreasing adjustments.
/// </remarks>
[RDJsonEnumSerializable]
public enum AngleCorrectionDirection
{
	/// <summary>
	/// Do not perform any angle correction.
	/// </summary>
	None,
	/// <summary>
	/// Correct angles by choosing the forward (increasing) rotation direction.
	/// </summary>
	Forward,
	/// <summary>
	/// Correct angles by choosing the backward (decreasing) rotation direction.
	/// </summary>
	Backward,
}
/// <summary>  
/// Specifies the masking types available for the decoration.  
/// </summary>  
[RDJsonEnumSerializable]
public enum MaskingType
{
	/// <summary>  
	/// No masking is applied.  
	/// </summary>  
	None,
	/// <summary>  
	/// Applies a mask to the decoration.  
	/// </summary>  
	Mask,
	/// <summary>  
	/// Makes the decoration visible only inside the mask.  
	/// </summary>  
	VisibleInsideMask,
	/// <summary>  
	/// Makes the decoration visible only outside the mask.  
	/// </summary>  
	VisibleOutsideMask
}
/// <summary>  
/// Represents the number of planets associated with the multi-planet event.  
/// </summary>  
[RDJsonEnumSerializable]
public enum Planets
{
	/// <summary>  
	/// Two planets are active in the event.  
	/// </summary>  
	TwoPlanets,
	/// <summary>  
	/// Three planets are active in the event.  
	/// </summary>  
	ThreePlanets,
}

/// <summary>  
/// Defines the types of repetition available for the event.  
/// </summary>  
[RDJsonEnumSerializable]
public enum RepeatType
{
	/// <summary>  
	/// Repeats the event based on beats.  
	/// </summary>  
	Beat,
	/// <summary>  
	/// Repeats the event based on floors.  
	/// </summary>  
	Floor
}

/// <summary>  
/// Represents the target planets that can be scaled.  
/// </summary>  
[RDJsonEnumSerializable]
public enum TargetPlanets
{
	/// <summary>  
	/// The fire planet.  
	/// </summary>  
	FirePlanet,
	/// <summary>  
	/// The ice planet.  
	/// </summary>  
	IcePlanet,
	/// <summary>  
	/// The green planet.  
	/// </summary>  
	GreenPlanet,
	/// <summary>  
	/// All planets.  
	/// </summary>  
	All
}
/// <summary>  
/// Represents the available filter types.  
/// </summary>  
[RDJsonEnumSerializable]
public enum Filter
{
	/// <summary>  
	/// Grayscale filter.  
	/// </summary>  
	Grayscale,
	/// <summary>  
	/// Sepia filter.  
	/// </summary>  
	Sepia,
	/// <summary>  
	/// Invert colors filter.  
	/// </summary>  
	Invert,
	/// <summary>  
	/// VHS effect filter.  
	/// </summary>  
	VHS,
	/// <summary>  
	/// 1980s TV effect filter.  
	/// </summary>  
	EightiesTV,
	/// <summary>  
	/// 1950s TV effect filter.  
	/// </summary>  
	FiftiesTV,
	/// <summary>  
	/// Arcade effect filter.  
	/// </summary>  
	Arcade,
	/// <summary>  
	/// LED effect filter.  
	/// </summary>  
	LED,
	/// <summary>  
	/// Rain effect filter.  
	/// </summary>  
	Rain,
	/// <summary>  
	/// Blizzard effect filter.  
	/// </summary>  
	Blizzard,
	/// <summary>  
	/// Pixel snow effect filter.  
	/// </summary>  
	PixelSnow,
	/// <summary>  
	/// Compression effect filter.  
	/// </summary>  
	Compression,
	/// <summary>  
	/// Glitch effect filter.  
	/// </summary>  
	Glitch,
	/// <summary>  
	/// Pixelate effect filter.  
	/// </summary>  
	Pixelate,
	/// <summary>  
	/// Waves effect filter.  
	/// </summary>  
	Waves,
	/// <summary>  
	/// Static effect filter.  
	/// </summary>  
	Static,
	/// <summary>  
	/// Grain effect filter.  
	/// </summary>  
	Grain,
	/// <summary>  
	/// Motion blur effect filter.  
	/// </summary>  
	MotionBlur,
	/// <summary>  
	/// Fisheye effect filter.  
	/// </summary>  
	Fisheye,
	/// <summary>  
	/// Chromatic aberration effect filter.  
	/// </summary>  
	Aberration,
	/// <summary>  
	/// Drawing effect filter.  
	/// </summary>  
	Drawing,
	/// <summary>  
	/// Neon effect filter.  
	/// </summary>  
	Neon,
	/// <summary>  
	/// Handheld camera effect filter.  
	/// </summary>  
	Handheld,
	/// <summary>  
	/// Night vision effect filter.  
	/// </summary>  
	NightVision,
	/// <summary>  
	/// Funk effect filter.  
	/// </summary>  
	Funk,
	/// <summary>  
	/// Tunnel effect filter.  
	/// </summary>  
	Tunnel,
	/// <summary>  
	/// Weird 3D effect filter.  
	/// </summary>  
	Weird3D,
	/// <summary>  
	/// Blur effect filter.  
	/// </summary>  
	Blur,
	/// <summary>  
	/// Blur focus effect filter.  
	/// </summary>  
	BlurFocus,
	/// <summary>  
	/// Gaussian blur effect filter.  
	/// </summary>  
	GaussianBlur,
	/// <summary>  
	/// Hexagon black effect filter.  
	/// </summary>  
	HexagonBlack,
	/// <summary>  
	/// Posterize effect filter.  
	/// </summary>  
	Posterize,
	/// <summary>  
	/// Sharpen effect filter.  
	/// </summary>  
	Sharpen,
	/// <summary>  
	/// Contrast effect filter.  
	/// </summary>  
	Contrast,
	/// <summary>  
	/// Edge black line effect filter.  
	/// </summary>  
	EdgeBlackLine,
	/// <summary>  
	/// Oil paint effect filter.  
	/// </summary>  
	OilPaint,
	/// <summary>  
	/// Super dot effect filter.  
	/// </summary>  
	SuperDot,
	/// <summary>  
	/// Water drop effect filter.  
	/// </summary>  
	WaterDrop,
	/// <summary>  
	/// Light water effect filter.  
	/// </summary>  
	LightWater,
	/// <summary>  
	/// Petals effect filter.  
	/// </summary>  
	Petals,
	/// <summary>  
	/// Instant petals effect filter.  
	/// </summary>  
	PetalsInstant
}
/// <summary>
/// Represents the target type for the filter effect.
/// </summary>
[RDJsonEnumSerializable]
public enum TargetType
{
	/// <summary>
	/// The target is the camera.
	/// </summary>
	Camera,
	/// <summary>
	/// The target is the decoration.
	/// </summary>
	Decoration,
}
/// <summary>
/// Represents the plane where the filter is applied.
/// </summary>
[RDJsonEnumSerializable]
public enum Plane
{
	/// <summary>
	/// The background plane.
	/// </summary>
	Background,

	/// <summary>
	/// The foreground plane.
	/// </summary>
	Foreground,
}

/// <summary>  
/// Represents the predefined game sounds available for hitsounds.  
/// </summary>  
[RDJsonEnumSerializable]
public enum GameSound
{
	/// <summary>  
	/// The default hitsound.  
	/// </summary>  
	Hitsound,
	/// <summary>  
	/// The sound used for midspin events.  
	/// </summary>  
	Midspin
}
/// <summary>
/// 
/// </summary>
[RDJsonEnumSerializable]
public enum HitSound
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
	Hat,
	Kick,
	Shaker,
	Sizzle,
	Chuck,
	ShakerLoud,
	None,
	Hammer,
	KickChroma,
	SnareAcoustic2,
	Sidestick,
	Stick,
	ReverbClack,
	Squareshot,
	PowerDown,
	PowerUp,
	KickHouse,
	KickRupture,
	HatHouse,
	SnareHouse,
	SnareVapor,
	ClapHit,
	ClapHitEcho,
	ReverbClap,
	FireTile,
	IceTile,
	VehiclePositive,
	VehicleNegative
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
/// <summary>  
/// Represents the types of sounds that can be played at the start or end of a hold.  
/// </summary>  
[RDJsonEnumSerializable]
public enum HoldSound
{
	/// <summary>  
	/// A fuse sound effect.  
	/// </summary>  
	Fuse,
	/// <summary>  
	/// No sound effect.  
	/// </summary>  
	None,
}
/// <summary>  
/// Represents the types of sounds that can be played in the middle of a hold.  
/// </summary>  
[RDJsonEnumSerializable]
public enum HoldMidSound
{
	/// <summary>  
	/// A fuse sound effect.  
	/// </summary>  
	Fuse,
	/// <summary>  
	/// A "SingSing" sound effect.  
	/// </summary>  
	SingSing,
	/// <summary>  
	/// No sound effect.  
	/// </summary>  
	None,
}
/// <summary>  
/// Represents the types of mid-hold sound playback behaviors.  
/// </summary>  
[RDJsonEnumSerializable]
public enum HoldMidSoundType
{
	/// <summary>  
	/// The sound is played once.  
	/// </summary>  
	Once,
	/// <summary>  
	/// The sound is repeated.  
	/// </summary>  
	Repeat,
}
/// <summary>
/// Specifies the possible input targets for the event.
/// </summary>
[RDJsonEnumSerializable]
public enum Target
{
	/// <summary>
	/// Any input target.
	/// </summary>
	Any,
	/// <summary>
	/// The first action button.
	/// </summary>
	Action1,
	/// <summary>
	/// The second action button.
	/// </summary>
	Action2,
	/// <summary>
	/// The confirm button.
	/// </summary>
	Confirm,
	/// <summary>
	/// The up direction.
	/// </summary>
	Up,
	/// <summary>
	/// The down direction.
	/// </summary>
	Down,
	/// <summary>
	/// The left direction.
	/// </summary>
	Left,
	/// <summary>
	/// The right direction.
	/// </summary>
	Right,
}

/// <summary>
/// Specifies the possible states of a key.
/// </summary>
[RDJsonEnumSerializable]
public enum KeyState
{
	/// <summary>
	/// The key is pressed down.
	/// </summary>
	Down,
	/// <summary>
	/// The key is released.
	/// </summary>
	Up,
}
/// <summary>  
/// Represents the various track icons that can be used in the Adofai editor.  
/// </summary>  
[RDJsonEnumSerializable]
public enum TrackIcon
{
	/// <summary>  
	/// No icon.  
	/// </summary>  
	None,
	/// <summary>  
	/// A single snail icon.  
	/// </summary>  
	Snall,
	/// <summary>  
	/// A double snail icon.  
	/// </summary>  
	DoubleSnall,
	/// <summary>  
	/// A rabbit icon.  
	/// </summary>  
	Rabbit,
	/// <summary>  
	/// A double rabbit icon.  
	/// </summary>  
	DoubleRabbit,
	/// <summary>  
	/// A swirl icon.  
	/// </summary>  
	Swirl,
	/// <summary>  
	/// A checkpoint icon.  
	/// </summary>  
	Checkpoint,
	/// <summary>  
	/// A short hold arrow icon.  
	/// </summary>  
	HoldArrowShort,
	/// <summary>  
	/// A long hold arrow icon.  
	/// </summary>  
	HoldArrowLong,
	/// <summary>  
	/// A short hold release icon.  
	/// </summary>  
	HoldReleaseShort,
	/// <summary>  
	/// A long hold release icon.  
	/// </summary>  
	HoldReleaseLong,
	/// <summary>  
	/// A two-planet multi-planet icon.  
	/// </summary>  
	MultiPlanetTwo,
	/// <summary>  
	/// A three-planet multi-planet icon.  
	/// </summary>  
	MultiPlanetThree,
	/// <summary>  
	/// A portal icon.  
	/// </summary>  
	Portal,
}

/// <summary>
/// Specifies the target mode for the particle effect.
/// </summary>
[RDJsonEnumSerializable]
public enum TargetMode
{
	/// <summary>
	/// Starts the particle effect.
	/// </summary>
	Start,

	/// <summary>
	/// Stops the particle effect.
	/// </summary>
	Stop,

	/// <summary>
	/// Clears the particle effect.
	/// </summary>
	Clear,
}

/// <summary>
/// Represents the direction of track color pulses in the Adofai rhythm game.
/// </summary>
[RDJsonEnumSerializable]
public enum TrackColorPulse
{
	/// <summary>
	/// No track color pulse.
	/// </summary>
	None,
	/// <summary>
	/// Track color pulses move forward.
	/// </summary>
	Forward,
	/// <summary>
	/// Track color pulses move backward.
	/// </summary>
	Backward,
}

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
[RDJsonEnumSerializable]
public enum FilterType	
{
		AaaSuperComputer,
		AaaSuperHexagon,
		AaaWaterDrop,
		AlienVision,
		AntialiasingFxaa,
		AtmosphereRain,
		AtmosphereRainPro,
		AtmosphereSnow8Bits,
		BlendToCameraBlend,
		BlendToCameraBlueScreen,
		BlendToCameraColor,
		BlendToCameraColorBurn,
		BlendToCameraColorDodge,
		BlendToCameraDarken,
		BlendToCameraDarkerColor,
		BlendToCameraDifference,
		BlendToCameraDivide,
		BlendToCameraExclusion,
		BlendToCameraGreenScreen,
		BlendToCameraHardLight,
		BlendToCameraHardMix,
		BlendToCameraHue,
		BlendToCameraLighten,
		BlendToCameraLighterColor,
		BlendToCameraLinearBurn,
		BlendToCameraLinearDodge,
		BlendToCameraLinearLight,
		BlendToCameraLuminosity,
		BlendToCameraMultiply,
		BlendToCameraOverlay,
		BlendToCameraPhotoshopFilters,
		BlendToCameraPinLight,
		BlendToCameraSaturation,
		BlendToCameraScreen,
		BlendToCameraSoftLight,
		BlendToCameraSplitScreen,
		BlendToCameraSubtract,
		BlendToCameraVividLight,
		Blizzard,
		BlurBloom,
		BlurBlurHole,
		BlurBlurry,
		BlurDitheringToxTo,
		BlurDitherOffset,
		BlurFocus,
		BlurGaussianBlur,
		BlurMovie,
		BlurNoise,
		BlurRadial,
		BlurRadialFast,
		BlurRegular,
		BlurSteam,
		BlurTiltShift,
		BlurTiltShiftHole,
		BlurTiltShiftV,
		ColorBrightContrastSaturation,
		ColorChromaticAberration,
		ColorContrast,
		ColorGrayScale,
		ColorInvert,
		ColorNoise,
		ColorRgb,
		ColorsAdjustColorRGB,
		ColorsAdjustFullColors,
		ColorsAdjustPreFilters,
		ColorsBleachBypass,
		ColorsBrightness,
		ColorsDarkColor,
		ColorSepia,
		ColorsHsv,
		ColorsHueRotate,
		ColorsNewPosterize,
		ColorsRgbClamp,
		ColorsThreshold,
		ColorSwitching,
		ColorYuv,
		DistortionAspiration,
		DistortionBigFace,
		DistortionBlackHole,
		DistortionDissipation,
		DistortionDream,
		DistortionDreamTo,
		DistortionFishEye,
		DistortionFlag,
		DistortionFlush,
		DistortionHalfSphere,
		DistortionHeat,
		DistortionLens,
		DistortionNoise,
		DistortionShockWave,
		DistortionTwist,
		DistortionTwistSquare,
		DistortionWaterDrop,
		DistortionWaveHorizontal,
		DrawingBluePrint,
		DrawingCellShading,
		DrawingCellShadingTo,
		DrawingComics,
		DrawingCrosshatch,
		DrawingCurve,
		DrawingEnhancedComics,
		DrawingHalftone,
		DrawingLaplacian,
		DrawingLines,
		DrawingManga,
		DrawingManga3,
		DrawingManga4,
		DrawingManga5,
		DrawingMangaColor,
		DrawingMangaFlash,
		DrawingMangaFlashColor,
		DrawingMangaFlashWhite,
		DrawingMangaTo,
		DrawingNewCellShading,
		DrawingPaper,
		DrawingPaper3,
		DrawingPaperTo,
		DrawingToon,
		EdgeBlackLine,
		EdgeEdgeFilter,
		EdgeGolden,
		EdgeNeon,
		EdgeSigmoid,
		EdgeSobel,
		ExtraRotation,
		EyesVision1,
		EyesVisionTo,
		FilmColorPerfection,
		FilmGrain,
		FlipScreen,
		FlyVision,
		Fx8Bits,
		Fx8BitsGb,
		FxAscii,
		FxDarkMatter,
		FxDigitalMatrix,
		FxDigitalMatrixDistortion,
		FxDotCircle,
		FxDrunk,
		FxDrunkTo,
		FxEarthQuake,
		FxFunk,
		FxGlitch1,
		FxGlitch3,
		FxGlitchTo,
		FxGrid,
		FxHexagon,
		FxHexagonBlack,
		FxHypno,
		FxInverChromiLum,
		FxMirror,
		FxPlasma,
		FxPsycho,
		FxScan,
		FxScreens,
		FxSpot,
		FxSuperDot,
		FxZebraColor,
		GlitchMozaic,
		GlowGlow,
		GlowGlowColor,
		GradientsAnsi,
		GradientsDesert,
		GradientsElectricGradient,
		GradientsFireGradient,
		GradientsHue,
		GradientsNeonGradient,
		GradientsRainbow,
		GradientsStripe,
		GradientsTech,
		GradientsTherma,
		LightRainbow,
		LightRainbowTo,
		LightWater,
		LightWaterTo,
		NightVision4,
		NightVisionFX,
		NoiseTv,
		NoiseTv3,
		NoiseTvTo,
		OculusNightVision1,
		OculusNightVision3,
		OculusNightVision5,
		OculusNightVisionTo,
		OculusThermaVision,
		OldFilmCutting1,
		OldFilmCuttingTo,
		PixelisationDot,
		PixelisationOilPaint,
		PixelisationOilPaintHQ,
		PixelPixelisation,
		RealVhs,
		RetroLoading,
		SharpenSharpen,
		SpecialBubble,
		Tv50,
		Tv80,
		TvArcade,
		TvArcade3,
		TvArcadeFast,
		TvArcadeTo,
		TvArtefact,
		TvBrokenGlass,
		TvBrokenGlassTo,
		TvChromatical,
		TvChromaticalTo,
		TvCompressionFX,
		TvDistorted,
		TvHorror,
		TvLed,
		TvNoise,
		TvOld,
		TvOldMovie,
		TvOldMovieTo,
		TvPlanetMars,
		TvPosterize,
		TvRgb,
		TvTiles,
		TvVcr,
		TvVhs,
		TvVhsRewind,
		TvVideo3D,
		TvVideoflip,
		TvVintage,
		TvWideScreenCircle,
		TvWideScreenHorizontal,
		TvWideScreenHV,
		TvWideScreenVertical,
		VhsTracking,
		VisionAura,
		VisionAuraDistortion,
		VisionCrystal,
		VisionDrost,
		VisionPlasma,
		VisionPsycho,
		VisionRainbow,
		VisionTunnel,
		VisionWarp,
		VisionWarpTo,
	}
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释