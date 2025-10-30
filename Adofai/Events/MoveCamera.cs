﻿using RhythmBase.Global.Components.Easing;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to move the camera in the Adofai game.  
	/// </summary>  
	public class MoveCamera : BaseTaggedTileEvent, IEaseEvent, IBeginningEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.MoveCamera;
		/// <summary>  
		/// Gets or sets the duration of the camera movement.  
		/// </summary>  
		public float Duration { get; set; } = 1f;
		/// <summary>  
		/// Gets or sets the easing type for the camera movement.  
		/// </summary>  
		public EaseType Ease { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether the camera should not be disabled after the movement.  
		/// </summary>  
		public bool DontDisable { get; set; }
		/// <summary>  
		/// Gets or sets a value indicating whether only minimal visual effects should be applied.  
		/// </summary>  
		public bool MinVfxOnly { get; set; }
		/// <summary>  
		/// Gets or sets the reference point for the camera movement.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(RelativeTo)} is not null")]
		public CameraRelativeTo? RelativeTo { get; set; }
		/// <summary>  
		/// Gets or sets the target position of the camera.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(Position)} is not null")]
		public RDPoint? Position { get; set; }
		/// <summary>  
		/// Gets or sets the rotation angle of the camera.  
		/// </summary>
		[RDJsonCondition($"$&.{nameof(Rotation)} is not null")]
		public float? Rotation { get; set; }
		/// <summary>  
		/// Gets or sets the zoom level of the camera.  
		/// </summary>  
		[RDJsonCondition($"$&.{nameof(Zoom)} is not null")]
		public float? Zoom { get; set; }
	}
}
