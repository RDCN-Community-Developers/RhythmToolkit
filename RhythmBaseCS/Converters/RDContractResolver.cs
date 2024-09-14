using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Extensions;
using System.Reflection;

namespace RhythmBase.Converters
{

	internal class RDContractResolver : DefaultContractResolver
	{
		// Note: this type is marked as 'beforefieldinit'.
		public static RDContractResolver Instance { get; } = new RDContractResolver();
		public RDContractResolver() : base()
		{
			NamingStrategy = new CamelCaseNamingStrategy();
		}
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty p = base.CreateProperty(member, memberSerialization);
			Predicate<object>? f = null;
			if (p.DeclaringType == typeof(RowEventCollection))
				f = p.PropertyName!.ToUpperCamelCase() switch
				{
					"RowToMimic" => i => ((RowEventCollection)i).RowToMimic >= 0,
					_ => null
				};
			else if (p.DeclaringType == typeof(BaseEvent))
				f = p.PropertyName!.ToUpperCamelCase() switch
				{
					"Active" => i => !((BaseEvent)i).Active,
					_ => null
				};
			if (f != null)
				p.ShouldSerialize = f;
			return p;
		}
	}
}
