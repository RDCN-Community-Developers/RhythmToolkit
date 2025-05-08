using RhythmBase.RhythmDoctor.Components;
using sly.parser;
using sly.parser.generator;

namespace RhythmBase.RhythmDoctor.Components.RDLang
{
	/// <summary>
	/// RDLang class provides functionality to parse and evaluate expressions written in a custom language.
	/// </summary>
	public static class RDLang
	{
		private static readonly RDVariables variables = new();
		private static readonly RDLangParser parserInstance;
		/// <summary>
		/// Gets the collection of variables used in the custom language.
		/// </summary>
		/// <value>
		/// The collection of variables.
		/// </value>
		public static RDVariables Variables { get => variables; }
		/// <summary>
		/// Lazy-initialized parser for the custom language expressions.
		/// </summary>
		internal static Lazy<Parser<RDExpressionToken, float>> Parser = new(() =>
		{
			var builder = new ParserBuilder<RDExpressionToken, float>();
			var build = builder.BuildParser(parserInstance, ParserType.LL_RECURSIVE_DESCENT, "expression");
			var parser = build.Result;
			return parser;
		});
		static RDLang()
		{
			parserInstance = new RDLangParser() { Variables = variables };
		}
		/// <summary>
		/// Tries to parse the given code string into a float result.
		/// </summary>
		/// <param name="code">The code string to parse.</param>
		/// <param name="result">The parsed float result if the parsing is successful.</param>
		/// <returns>True if parsing is successful; otherwise, false.</returns>
		public static bool TryRun(string code, out float result)
		{
			var parseResult = Parser.Value.Parse(code);
			result = parseResult.Result;
			return parseResult.IsOk;
		}
	}
}
