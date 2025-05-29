using Newtonsoft.Json.Serialization;

namespace RhythmBase.Adofai.Converters
{

	internal class ContractResolver : DefaultContractResolver
	{
		public static ContractResolver Instance => new ContractResolver();
		public ContractResolver() : base()
		{
			NamingStrategy = new CamelCaseNamingStrategy();
		}
	}
}
