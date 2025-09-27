namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to add a decoration to a tile in the game.  
	/// </summary>  
	public class AddDecoration : BaseEvent, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.AddDecoration;
		/// <summary>  
		/// Gets or sets the image of the decoration.  
		/// </summary>  
		public string DecorationImage { get; set; } = string.Empty;
		/// <summary>  
		/// Gets or sets the position of the decoration.  
		/// </summary>  
		public RDPointN Position { get; set; }
		/// <summary>  
		/// Gets or sets the reference point for the decoration's position.  
		/// </summary>  
		public DecorationRelativeTo RelativeTo { get; set; }
		/// <summary>  
		/// Gets or sets the pivot offset of the decoration.  
		/// </summary>  
		public RDSizeN PivotOffset { get; set; }
		/// <summary>  
		/// Gets or sets the rotation of the decoration in degrees.  
		/// </summary>  
		public float Rotation { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether the rotation is locked.  
		/// </summary>  
		public bool LockRotation { get; set; }
		/// <summary>  
		/// Gets or sets the scale of the decoration.  
		/// </summary>  
		public RDSizeN Scale { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether the scale is locked.  
		/// </summary>  
		public bool LockScale { get; set; }
		/// <summary>  
		/// Gets or sets the tile size of the decoration.  
		/// </summary>  
		public RDSizeN Tile { get; set; }
		/// <summary>  
		/// Gets or sets the color of the decoration.  
		/// </summary>  
		public RDColor Color { get; set; }
		/// <summary>  
		/// Gets or sets the opacity of the decoration.  
		/// </summary>  
		public float Opacity { get; set; }
		/// <summary>  
		/// Gets or sets the depth of the decoration.  
		/// </summary>  
		public int Depth { get; set; }
		/// <summary>  
		/// Gets or sets the parallax effect of the decoration.  
		/// </summary>  
		public RDSizeN Parallax { get; set; }
		/// <summary>  
		/// Gets or sets the parallax offset of the decoration.  
		/// </summary>  
		public RDSizeN ParallaxOffset { get; set; }
		/// <summary>  
		/// Gets or sets the tag associated with the decoration.  
		/// </summary>  
		public string Tag { get; set; } = string.Empty;
		/// <summary>  
		/// Gets or sets a value indicating whether image smoothing is applied.  
		/// </summary>  
		public bool ImageSmoothing { get; set; }
		/// <summary>  
		/// Gets or sets the blend mode of the decoration.  
		/// </summary>  
		public BlendModes BlendMode { get; set; }
		/// <summary>  
		/// Gets or sets the masking type of the decoration.  
		/// </summary>  
		public MaskingTypes MaskingType { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether masking depth is used.  
		/// </summary>  
		public bool UseMaskingDepth { get; set; }
		/// <summary>  
		/// Gets or sets the front depth for masking.  
		/// </summary>  
		public int MaskingFrontDepth { get; set; }
		/// <summary>  
		/// Gets or sets the back depth for masking.  
		/// </summary>  
		public int MaskingBackDepth { get; set; }
		/// <summary>  
		/// Gets or sets the type of hitbox for the decoration.  
		/// </summary>  
		public HitboxTypes Hitbox { get; set; }
		/// <summary>  
		/// Gets or sets the event tag associated with the hitbox.  
		/// </summary>  
		public string HitboxEventTag { get; set; } = string.Empty;
		/// <summary>  
		/// Gets or sets the type of fail hitbox for the decoration.  
		/// </summary>  
		public FailHitboxTypes FailHitboxType { get; set; }
		/// <summary>  
		/// Gets or sets the scale of the fail hitbox.  
		/// </summary>  
		public RDSizeN FailHitboxScale { get; set; }
		/// <summary>  
		/// Gets or sets the offset of the fail hitbox.  
		/// </summary>  
		public RDSizeN FailHitboxOffset { get; set; }
		/// <summary>  
		/// Gets or sets the rotation of the fail hitbox in degrees.  
		/// </summary>  
		public int FailHitboxRotation { get; set; }
		/// <summary>  
		/// Gets or sets the components associated with the decoration.  
		/// </summary>  
		public string Components { get; set; } = string.Empty;
		/// <summary>  
		/// Specifies the blend modes available for the decoration.  
		/// </summary>  
		[RDJsonEnumSerializable]
		public enum BlendModes
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
		/// Specifies the masking types available for the decoration.  
		/// </summary>  
		[RDJsonEnumSerializable]
		public enum MaskingTypes
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
	}
}
