using RhythmBase.Global.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.BeatBlock.Components
{
partial class Chart
	{

		/// <summary>
		/// Loads a chart from its two documents: the <c>level</c> part (an object of shared events) and the
		/// <c>chart</c> part (an array of chart events).
		/// </summary>
		/// <param name="levelStream">The stream of the level document.</param>
		/// <param name="chartStream">The stream of the chart document.</param>
		/// <param name="settings">Optional read settings.</param>
		public static Chart FromLevelAndChart(Stream levelStream, Stream chartStream, LevelReadSettings? settings = null)
		{
			settings ??= new LevelReadSettings();
			MetadataJsonSerializerOptions options = JsonSerializerOptionsUtils.GetJsonSerializerOptionsForRead(settings);
			Chart variant = new("");
			Level.FileConverter.DeserializeLevel(new StreamDataSource(levelStream), options, variant, settings);
			Level.FileConverter.DeserializeChart(new StreamDataSource(chartStream), options, variant, settings);
			return variant;
		}
		/// <summary>
		/// Saves this chart to its two documents: the <c>level</c> part (an object of shared events) and the
		/// <c>chart</c> part (an array of chart events).
		/// </summary>
		/// <param name="levelStream">The stream to write the level document to.</param>
		/// <param name="chartStream">The stream to write the chart document to.</param>
		/// <param name="settings">Optional write settings.</param>
		public void SaveToLevelAndChart(Stream levelStream, Stream chartStream, LevelWriteSettings? settings = null)
		{
			settings ??= new LevelWriteSettings();
			MetadataJsonSerializerOptions options = JsonSerializerOptionsUtils.GetJsonSerializerOptionsForWrite(settings);
			using NoIndentScope noIndentScope = new(options.JsonSerializerOptions.Encoder, options);
			Level.FileConverter.WriteVariantLevelToStream(levelStream, noIndentScope, this, options);
			Level.FileConverter.WriteVariantChartsToStream(chartStream, noIndentScope, this, options);
		}
	}
}
