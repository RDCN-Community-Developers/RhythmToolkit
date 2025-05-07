using Newtonsoft.Json;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an action to play an animation.
	/// </summary>
	public class PlayAnimation : BaseDecorationAction
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PlayAnimation"/> class.
		/// </summary>
		public PlayAnimation()
		{
			Type = EventType.PlayAnimation;
			Tab = Tabs.Decorations;
		}		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }		/// <summary>
		/// Gets or sets the expression for the animation.
		/// </summary>
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
		public string Expression { get; set; } = "";		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" Expression:{0}", Expression);
	}
}
