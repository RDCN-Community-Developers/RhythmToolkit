using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents the base class for decoration actions in the rhythm base.
	/// </summary>
	public abstract class BaseDecorationAction : BaseEvent, IBaseEvent
	{
		/// <summary>
		/// Gets the parent decoration event collection.
		/// </summary>
		public Decoration? Parent => _parent;
		/// <summary>
		/// Gets or sets the Y coordinate.
		/// </summary>
		public override int Y { get => base.Y; set => base.Y = value; }
		/// <inheritdoc/>
		public override Tabs Tab => Tabs.Decorations;
		/// <summary>
		/// Gets the target identifier.
		/// </summary>
		public virtual string Target => Parent?.Id ?? _decoId;
		/// <summary>
		/// Clones this event and its basic properties. The clone will be added to the level.
		/// </summary>
		/// <typeparam name="TEvent">The type of event that will be generated.</typeparam>
		/// <returns>A new instance of <typeparamref name="TEvent"/>.</returns>
		public override TEvent Clone<TEvent>()
		{
			TEvent Temp = base.Clone<TEvent>();
			if (Temp is BaseDecorationAction TempAction)
				TempAction._parent = Parent;
			return Temp;
		}
		/// <summary>
		/// Clones this event and its basic properties, associating it with a specific decoration event collection.
		/// </summary>
		/// <typeparam name="TEvent">The type of event that will be generated.</typeparam>
		/// <param name="decoration">The decoration event collection to associate with the clone.</param>
		/// <returns>A new instance of <typeparamref name="TEvent"/>.</returns>
		internal TEvent Clone<TEvent>(Decoration decoration) where TEvent : BaseDecorationAction, new()
		{
			TEvent Temp = base.Clone<TEvent>(decoration.Parent ?? throw new RhythmBaseException());
			Temp._parent = decoration;
			return Temp;
		}
		/// <summary>
		/// Gets the room associated with this action.
		/// </summary>
		public RDSingleRoom Room => Parent?.Room ?? RDSingleRoom.Default;
		/// <summary>
		/// The parent decoration event collection.
		/// </summary>
		internal Decoration? _parent;
		internal string _decoId = "";
	}
}
