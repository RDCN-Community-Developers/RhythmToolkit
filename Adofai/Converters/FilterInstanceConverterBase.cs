using RhythmBase.Adofai.Components.Filters;
using RhythmBase.Adofai.Utils;
using System.Text;
using System.Text.Json;

namespace RhythmBase.Adofai.Converters
{
	internal abstract class FilterInstanceConverterBase
	{
		public abstract IFilter ReadProperties(ref Utf8JsonReader reader, JsonSerializerOptions options);
		public abstract void WriteProperties(Utf8JsonWriter writer, IFilter value, JsonSerializerOptions options);
	}
	internal abstract class FilterInstanceConverterBase<TFilter> : FilterInstanceConverterBase where TFilter : struct, IFilter
	{
		public sealed override IFilter ReadProperties(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			TFilter value = new();
			while (reader.TokenType is JsonTokenType.PropertyName)
			{
				ReadOnlySpan<byte> propertyName = reader.ValueSpan;
				if (propertyName.IsEmpty)
					throw new JsonException("Property name cannot be null");
				reader.Read();
				if (!Read(ref reader, propertyName, ref value, options))
				{
#if DEBUG
					if (!(false
						))
						Console.WriteLine($"The key {Encoding.UTF8.GetString([.. propertyName])} of {value.GetType().Name} not found.");
#endif
				}
				var c = FilterTypeUtils.converters;
			}
			return value;
		}
		public sealed override void WriteProperties(Utf8JsonWriter writer, IFilter value, JsonSerializerOptions options)
		{
			var f = (TFilter)value;
			Write(writer, ref f, options);
		}
		protected virtual bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref TFilter value, JsonSerializerOptions options) { return false; }
		protected virtual void Write(Utf8JsonWriter writer, ref TFilter value, JsonSerializerOptions options) { }
	}
}
