namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a swirl radius component in a dialogue.
	/// </summary>
	/// <param name="radius">The radius value of the component.</param>
	public struct SwirlRadiusComponent(float radius) : ITextDialogueComponent
	{
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the height of the component.
		/// </summary>
		public float Radius { get; set; } = radius;
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("swirlRadius", Radius.ToString());
	}
}
