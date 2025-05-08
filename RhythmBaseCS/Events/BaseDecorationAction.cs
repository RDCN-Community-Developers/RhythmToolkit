using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents the base class for decoration actions in the rhythm base.
	/// </summary>
	public abstract class BaseDecorationAction : BaseEvent, IBaseEvent
	{
		/// <summary>
		/// Gets the parent decoration event collection.
		/// </summary>
		[JsonIgnore]
		public Decoration? Parent => _parent;		/// <summary>
		/// Gets or sets the Y coordinate.
		/// </summary>
		[JsonIgnore]
		public override int Y { get => base.Y; set => base.Y = value; }		/// <summary>
		/// Gets the target identifier.
		/// </summary>
		public virtual string Target => Parent?.Id ?? _decoId;		/// <summary>
		/// Gets or sets the beat associated with this action.
		/// </summary>
		/// <inheritdoc/>
		[JsonIgnore]
		public override RDBeat Beat
		{
			get => _beat;
			set => _beat = _beat.BaseLevel == null ?
							value.BaseLevel == null ?
								value :
								value.WithoutLink() :
							new(_beat.BaseLevel.Calculator, value);
		}		/// <summary>
		/// Clones this event and its basic properties. The clone will be added to the level.
		/// </summary>
		/// <typeparam name="TEvent">The type of event that will be generated.</typeparam>
		/// <returns>A new instance of <typeparamref name="TEvent"/>.</returns>
		public new TEvent Clone<TEvent>() where TEvent : BaseDecorationAction, new()
		{
			TEvent Temp = base.Clone<TEvent>();
			Temp._parent = Parent;
			return Temp;
		}		/// <summary>
		/// Clones this event and its basic properties, associating it with a specific decoration event collection.
		/// </summary>
		/// <typeparam name="TEvent">The type of event that will be generated.</typeparam>
		/// <param name="decoration">The decoration event collection to associate with the clone.</param>
		/// <returns>A new instance of <typeparamref name="TEvent"/>.</returns>
		internal TEvent Clone<TEvent>(Decoration decoration) where TEvent : BaseDecorationAction, new()
		{
			TEvent Temp = base.Clone<TEvent>(decoration.Parent ?? throw new Exceptions.RhythmBaseException());
			Temp._parent = decoration;
			return Temp;
		}		/// <summary>
		/// Gets the room associated with this action.
		/// </summary>
		[JsonIgnore]
		public RDSingleRoom Room => Parent?.Room ?? RDSingleRoom.Default;		/// <summary>
		/// The parent decoration event collection.
		/// </summary>
		internal Decoration? _parent;
		internal string _decoId = "";
	}
}
