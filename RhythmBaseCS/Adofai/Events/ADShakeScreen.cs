using System;
using RhythmBase.Components;
using RhythmBase.Components.Easing;
using RhythmBase.Events;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents a screen shake event in the Adofai event system.  
	/// </summary>  
	public class ADShakeScreen : ADBaseTaggedTileAction, IEaseEvent
	{		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.ShakeScreen;		/// <summary>  
		/// Gets or sets the duration of the screen shake effect.  
		/// </summary>  
		public float Duration { get; set; }		/// <summary>  
		/// Gets or sets the strength of the screen shake effect.  
		/// </summary>  
		public float Strength { get; set; }		/// <summary>  
		/// Gets or sets the intensity of the screen shake effect.  
		/// </summary>  
		public float Intensity { get; set; }		/// <summary>  
		/// Gets or sets the easing type for the screen shake effect.  
		/// </summary>  
		public EaseType Ease { get; set; }		/// <summary>  
		/// Gets or sets the fade-out duration for the screen shake effect.  
		/// </summary>  
		public float FadeOut { get; set; }
	}
}
