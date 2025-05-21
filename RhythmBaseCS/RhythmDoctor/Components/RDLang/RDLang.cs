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
		/// <summary>
		/// Gets the collection of variables used in the custom language.
		/// </summary>
		/// <value>
		/// The collection of variables.
		/// </value>
		public static RDVariables Variables { get => variables; }
		/// <summary>
		/// Attempts to parse and execute the provided code in the custom language.
		/// </summary>
		/// <param name="code">The code to execute.</param>
		/// <param name="result">
		/// When this method returns, contains the result of the execution if successful; otherwise, 0.
		/// </param>
		/// <returns>
		/// <c>true</c> if the code was successfully executed; otherwise, <c>false</c>.
		/// </returns>
		public static bool TryRun(string code, out float result)
		{
			RDLangParser.TryRun(code);

			result = 0;
			return false;
		}
		/// <summary>
		/// Attempts to parse and evaluate the provided code in the custom language.
		/// </summary>
		/// <param name="code">The code to evaluate.</param>
		/// <param name="result">
		/// When this method returns, contains the result of the evaluation if successful; otherwise, 0.
		/// </param>
		/// <returns>
		/// <c>true</c> if the code was successfully evaluated; otherwise, <c>false</c>.
		/// </returns>
		public static bool TryEvaluate(string code, out float result)
		{
			result = 0;
			return false;
		}
	}
}
