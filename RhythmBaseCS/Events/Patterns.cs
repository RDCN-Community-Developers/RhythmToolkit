using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Events
{
	/// <summary>  
	/// Enum representing different rhythm patterns.  
	/// </summary>  
	[JsonConverter(typeof(PatternConverter))]
	public enum Patterns
	{
		/// <summary>  
		/// No pattern.  
		/// </summary>  
		None,
		/// <summary>  
		/// Pattern X.  
		/// </summary>  
		X,
		/// <summary>  
		/// Pattern Up.  
		/// </summary>  
		Up,
		/// <summary>  
		/// Pattern Down.  
		/// </summary>  
		Down,
		/// <summary>  
		/// Pattern Banana.  
		/// </summary>  
		Banana,
		/// <summary>  
		/// Pattern Return.  
		/// </summary>  
		Return
	}
}
