namespace RhythmBase.Adofai.Events
{
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
}
