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
			return RDLangParser.TryRun(code, RDLangType.Statement, out result);
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
			return RDLangParser.TryRun(code, RDLangType.Expression, out result);
		}
		/// <summary>
		/// Executes the provided code written in the custom language as a statement.
		/// </summary>
		/// <param name="code">The code to execute.</param>
		/// <returns>The result of the execution as a float.</returns>
		public static float Run(string code)
		{
			return RDLangParser.Run(code, RDLangType.Statement);
		}
		/// <summary>
		/// Evaluates the provided code written in the custom language as an expression.
		/// </summary>
		/// <param name="code">The code to evaluate.</param>
		/// <returns>The result of the evaluation as a float.</returns>
		public static float Evaluate(string code)
		{
			return RDLangParser.Run(code, RDLangType.Expression);
		}
		public static Token[] Analyze(string code)
		{
			return RDLangParser.Analyze(code);
		}
	}
}
