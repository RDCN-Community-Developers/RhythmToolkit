using System;
using Newtonsoft.Json;
namespace RhythmBase.Adofai.Events
{
	public abstract class ADBaseTileEvent : ADBaseEvent
	{
		[JsonIgnore]
		public ADTile Parent { get; set; }

		public override string ToString() => string.Format("{0}", Type);
	}
}
