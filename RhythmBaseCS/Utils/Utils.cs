using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RhythmBase.Assets;
using RhythmBase.Components;
using RhythmBase.Converters;
using RhythmBase.Events;
using RhythmBase.Exceptions;
using RhythmBase.Settings;
using System.Collections.ObjectModel;
namespace RhythmBase.Utils
{
	public static class Utils
	{
		/// <summary>
		/// Converts Xs patterns to string form.
		/// </summary>
		/// <param name="list">String pattern.</param>
		/// <returns></returns>
		public static string GetPatternString(LimitedList<Patterns> list)
		{
			string @out = "";
			foreach (Patterns item in list)
			{
				switch (item)
				{
					case Patterns.None:
						@out += "-";
						break;
					case Patterns.X:
						@out += "x";
						break;
					case Patterns.Up:
						@out += "u";
						break;
					case Patterns.Down:
						@out += "d";
						break;
					case Patterns.Banana:
						@out += "b";
						break;
					case Patterns.Return:
						@out += "r";
						break;
				}
			}
			return @out;
		}

		public static JsonSerializerSettings GetSerializer(this RDLevel rdlevel, LevelReadOrWriteSettings settings)
		{
			JsonSerializerSettings EventsSerializer = new()
			{
				ContractResolver = new RDContractResolver()
			};
			IList<JsonConverter> converters = EventsSerializer.Converters;
			converters.Add(new AssetConverter<SpriteFile>(rdlevel));
			converters.Add(new AssetConverter<ImageFile>(rdlevel));
			converters.Add(new AssetConverter<AudioFile>(rdlevel));
			converters.Add(new AssetConverter<BuiltInAudio>(rdlevel));
			converters.Add(new AudioConverter(rdlevel.Manager));
			converters.Add(new PanelColorConverter(rdlevel.ColorPalette));
			converters.Add(new ColorConverter());
			converters.Add(new ConditionalConverter());
			converters.Add(new CharacterConverter(rdlevel, rdlevel.Assets));
			converters.Add(new ConditionConverter(rdlevel.Conditionals));
			converters.Add(new TagActionConverter(rdlevel, settings));
			converters.Add(new CustomDecorationEventConverter(rdlevel, settings));
			converters.Add(new CustomRowEventConverter(rdlevel, settings));
			converters.Add(new CustomEventConverter(rdlevel, settings));
			converters.Add(new BaseRowActionConverter<BaseRowAction>(rdlevel, settings));
			converters.Add(new BaseDecorationActionConverter<BaseDecorationAction>(rdlevel, settings));
			converters.Add(new BaseEventConverter<IBaseEvent>(rdlevel, settings));
			converters.Add(new BookmarkConverter(rdlevel.Calculator));
			converters.Add(new StringEnumConverter());
			return EventsSerializer;
		}

	}
}
