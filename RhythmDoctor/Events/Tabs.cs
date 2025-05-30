﻿using Newtonsoft.Json;
using RhythmBase.RhythmDoctor.Converters;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Specifies the different tabs available in the RhythmBase application.  
	/// </summary>  
	[JsonConverter(typeof(TabsConverter))]
	public enum Tabs
	{
		/// <summary>  
		/// Represents the Sounds tab.  
		/// </summary>  
		Sounds,
		/// <summary>  
		/// Represents the Rows tab.  
		/// </summary>  
		Rows,
		/// <summary>  
		/// Represents the Actions tab.  
		/// </summary>  
		Actions,
		/// <summary>  
		/// Represents the Decorations tab.  
		/// </summary>  
		Decorations,
		/// <summary>  
		/// Represents the Rooms tab.  
		/// </summary>  
		Rooms,
		/// <summary>  
		/// Represents an unknown tab.  
		/// </summary>  
		Unknown
	}
}
