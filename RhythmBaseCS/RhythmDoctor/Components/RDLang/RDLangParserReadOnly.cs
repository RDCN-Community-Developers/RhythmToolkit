namespace RhythmBase.RhythmDoctor.Components.RDLang
{
	partial class RDLangParser
	{
		/*
Sentence ->
	| Identifier OperatorIncreasement
	| Identifier OperatorDecreasement
	| Identifier OperatorAssignment Expression
	| Function

Identifier ->
	| Variable
	| StringOrIdentifier Dot Identifier
	| StringOrIdentifier LeftBracket Integer RightBracket Dot Identifier

Variable ->
	| VariableInteger
	| VariableFloat
	| VariableBoolean
	| StringOrIdentifier
	| StringOrIdentifier LeftBracket Integer RightBracket

Expression ->
	| Number
	| Function
    | LeftParenthesis Expression RightParenthesis
	| Expression Operator Expression
	| OperatorAdd Expression
	| OperatorSubtract Expression

Number ->
	| Integer
	| Float
	| Identifier
	| True
	| False

Operator ->
	| OperatorAdd
	| OperatorSubtract
	| OperatorMultipy
	| OperatorDivide
	| OperatorAssignment
	| OperatorGreaterThan
	| OperatorGreaterThanOrEqual
	| OperatorLessThan
	| OperatorLessThanOrEqual
	| OperatorAnd
	| OperatorOr
	| OperatorNot

Args ->
	| String
	| Expression
	| Args Comma Args

Function ->
	| Identifier LeftParenthesis RightParenthesis
	| Identifier LeftParenthesis Args RightParenthesis
		 */
		private static readonly PatternGroup[] patterns = [
			new PatternGroup(GroupType.Sentence){new PatternGroup(GroupType.Identifier), new PatternValue(TokenType.OperatorIncreasement),},
			new PatternGroup(GroupType.Sentence){new PatternGroup(GroupType.Identifier), new PatternValue(TokenType.OperatorDecreasement),},
			new PatternGroup(GroupType.Sentence){new PatternGroup(GroupType.Identifier), new PatternValue(TokenType.OperatorAssignment), new PatternGroup(GroupType.Expression),},
			new PatternGroup(GroupType.Sentence){new PatternGroup(GroupType.Function),},

			new PatternGroup(GroupType.Identifier){new PatternGroup(GroupType.Variable),},
			new PatternGroup(GroupType.Identifier){new PatternValue(TokenType.StringOrIdentifier), new PatternValue(TokenType.Dot), new PatternGroup(GroupType.Identifier) },
			new PatternGroup(GroupType.Identifier){new PatternValue(TokenType.StringOrIdentifier), new PatternValue(TokenType.LeftBracket), new PatternValue(TokenType.Integer), new PatternValue(TokenType.RightBracket),},

			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.VariableInteger),},
			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.VariableFloat),},
			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.VariableBoolean),},
			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.StringOrIdentifier),},
			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.StringOrIdentifier), new PatternValue(TokenType.LeftBracket), new PatternValue(TokenType.Integer), new PatternValue(TokenType.RightBracket),},

			new PatternGroup(GroupType.Expression){new PatternGroup(GroupType.Number),},
			new PatternGroup(GroupType.Expression){new PatternGroup(GroupType.Function),},
			new PatternGroup(GroupType.Expression){new PatternValue(TokenType.LeftParenthesis), new PatternGroup(GroupType.Expression), new PatternValue(TokenType.RightParenthesis),},
			new PatternGroup(GroupType.Expression){new PatternGroup(GroupType.Expression), new PatternGroup(GroupType.Operator), new PatternGroup(GroupType.Expression),},
			new PatternGroup(GroupType.Expression){new PatternValue(TokenType.OperatorAdd), new PatternGroup(GroupType.Expression),},
			new PatternGroup(GroupType.Expression){new PatternValue(TokenType.OperatorSubtract), new PatternGroup(GroupType.Expression),},

			new PatternGroup(GroupType.Number){new PatternValue(TokenType.Integer),},
			new PatternGroup(GroupType.Number){new PatternValue(TokenType.Float),},
			new PatternGroup(GroupType.Number){new PatternGroup(GroupType.Identifier),},
			new PatternGroup(GroupType.Number){new PatternValue(TokenType.True),},
			new PatternGroup(GroupType.Number){new PatternValue(TokenType.False),},

			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorAdd),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorSubtract),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorMultipy),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorDivide),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorAssignment),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorGreaterThan),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorGreaterThanOrEqual),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorLessThan),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorLessThanOrEqual),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorAnd),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorOr),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorNot),},

			new PatternGroup(GroupType.Args){new PatternValue(TokenType.String),},
			new PatternGroup(GroupType.Args){new PatternGroup(GroupType.Expression),},
			new PatternGroup(GroupType.Args){new PatternGroup(GroupType.Args), new PatternValue(TokenType.Comma), new PatternGroup(GroupType.Args),},

			new PatternGroup(GroupType.Function){new PatternGroup(GroupType.Identifier), new PatternValue(TokenType.LeftParenthesis), new PatternValue(TokenType.RightParenthesis),},
			new PatternGroup(GroupType.Function){new PatternGroup(GroupType.Identifier), new PatternValue(TokenType.LeftParenthesis), new PatternGroup(GroupType.Args), new PatternValue(TokenType.RightParenthesis),},
					];
		private static readonly Action[,] actions = new Action[,]
				{
{"","","","","","","","","","","","","","","","","","","","","","","","","s5","","s8","s7","s6",""},
{"","","","","","","","","","","","","","","","","","","","","","","","","","","","","",new()},
{"","","","","","","s12","","","s11","s10","","","","s9","","","","","","","","","","","","","","",""},
{"","","","","","","","","","","","","","","","","","","","","","","","","","","","","","r3"},
{"r4","","","","","","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","","r4","","","","","","","r4"},
{"r10","s13","","","","s14","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","","r10","","","","","","","r10"},
{"r7","","","","","","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","","r7","","","","","","","r7"},
{"r8","","","","","","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","","r8","","","","","","","r8"},
{"r9","","","","","","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","","r9","","","","","","","r9"},
{"","","","","","","","","","","","","","","","","","","","","","","","","","","","","","r0"},
{"","","","","","","","","","","","","","","","","","","","","","","","","","","","","","r1"},
{"","","s25","s22","s21","","s18","s19","","","","","","","","","","","","","s20","","","","s5","s24","s8","s7","s6",""},
{"","","s25","s22","s21","","s18","s19","","","","","","","","","","","","","s20","","s26","s28","s5","s24","s8","s7","s6",""},
{"","","","","","","","","","","","","","","","","","","","","","","","","s5","","s8","s7","s6",""},
{"","","","","s31","","","","","","","","","","","","","","","","","","","","","","","","",""},
{"","","","","","","","s33","s42","s37","","s36","s38","s39","","s40","s41","s35","s44","s43","s34","","","","","","","","","r2"},
{"r12","","","","","","","r12","r12","r12","","r12","r12","r12","","r12","r12","r12","r12","r12","r12","","r12","","","","","","","r12"},
{"r13","","","","","","","r13","r13","r13","","r13","r13","r13","","r13","r13","r13","r13","r13","r13","","r13","","","","","","","r13"},
{"","","s25","s22","s21","","s18","s19","","","","","","","","","","","","","s20","","","","s5","s24","s8","s7","s6",""},
{"","","s25","s22","s21","","s18","s19","","","","","","","","","","","","","s20","","","","s5","s24","s8","s7","s6",""},
{"","","s25","s22","s21","","s18","s19","","","","","","","","","","","","","s20","","","","s5","s24","s8","s7","s6",""},
{"r18","","","","","","","r18","r18","r18","","r18","r18","r18","","r18","r18","r18","r18","r18","r18","","r18","","","","","","","r18"},
{"r19","","","","","","","r19","r19","r19","","r19","r19","r19","","r19","r19","r19","r19","r19","r19","","r19","","","","","","","r19"},
{"r20","","","","","","s12","r20","r20","r20","","r20","r20","r20","","r20","r20","r20","r20","r20","r20","","r20","","","","","","","r20"},
{"r21","","","","","","","r21","r21","r21","","r21","r21","r21","","r21","r21","r21","r21","r21","r21","","r21","","","","","","","r21"},
{"r22","","","","","","","r22","r22","r22","","r22","r22","r22","","r22","r22","r22","r22","r22","r22","","r22","","","","","","","r22"},
{"r38","","","","","","","r38","r38","r38","","r38","r38","r38","","r38","r38","r38","r38","r38","r38","","r38","","","","","","","r38"},
{"s49","","","","","","","","","","","","","","","","","","","","","","s48","","","","","","",""},
{"r35","","","","","","","","","","","","","","","","","","","","","","r35","","","","","","",""},
{"r36","","","","","","","s33","s42","s37","","s36","s38","s39","","s40","s41","s35","s44","s43","s34","","r36","","","","","","",""},
{"r5","","","","","","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","","r5","","","","","","","r5"},
{"","","","","","","","","","","","","","","","","","","","","","s50","","","","","","","",""},
{"","","s25","s22","s21","","s18","s19","","","","","","","","","","","","","s20","","","","s5","s24","s8","s7","s6",""},
{"","","r23","r23","r23","","r23","r23","","","","","","","","","","","","","r23","","","","r23","r23","r23","r23","r23",""},
{"","","r24","r24","r24","","r24","r24","","","","","","","","","","","","","r24","","","","r24","r24","r24","r24","r24",""},
{"","","r25","r25","r25","","r25","r25","","","","","","","","","","","","","r25","","","","r25","r25","r25","r25","r25",""},
{"","","r26","r26","r26","","r26","r26","","","","","","","","","","","","","r26","","","","r26","r26","r26","r26","r26",""},
{"","","r27","r27","r27","","r27","r27","","","","","","","","","","","","","r27","","","","r27","r27","r27","r27","r27",""},
{"","","r28","r28","r28","","r28","r28","","","","","","","","","","","","","r28","","","","r28","r28","r28","r28","r28",""},
{"","","r29","r29","r29","","r29","r29","","","","","","","","","","","","","r29","","","","r29","r29","r29","r29","r29",""},
{"","","r30","r30","r30","","r30","r30","","","","","","","","","","","","","r30","","","","r30","r30","r30","r30","r30",""},
{"","","r31","r31","r31","","r31","r31","","","","","","","","","","","","","r31","","","","r31","r31","r31","r31","r31",""},
{"","","r32","r32","r32","","r32","r32","","","","","","","","","","","","","r32","","","","r32","r32","r32","r32","r32",""},
{"","","r33","r33","r33","","r33","r33","","","","","","","","","","","","","r33","","","","r33","r33","r33","r33","r33",""},
{"","","r34","r34","r34","","r34","r34","","","","","","","","","","","","","r34","","","","r34","r34","r34","r34","r34",""},
{"","","","","","","","s33","s42","s37","","s36","s38","s39","","s40","s41","s35","s44","s43","s34","","s52","","","","","","",""},
{"r16","","","","","","","s33/r16","s42/r16","s37/r16","","s36/r16","s38/r16","s39/r16","","s40/r16","s41/r16","s35/r16","s44/r16","s43/r16","s34/r16","","r16","","","","","","","r16"},
{"r17","","","","","","","s33/r17","s42/r17","s37/r17","","s36/r17","s38/r17","s39/r17","","s40/r17","s41/r17","s35/r17","s44/r17","s43/r17","s34/r17","","r17","","","","","","","r17"},
{"r39","","","","","","","r39","r39","r39","","r39","r39","r39","","r39","r39","r39","r39","r39","r39","","r39","","","","","","","r39"},
{"","","s25","s22","s21","","s18","s19","","","","","","","","","","","","","s20","","","s28","s5","s24","s8","s7","s6",""},
{"r11","s54","","","","","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","","r11","","","","","","","r11"},
{"r15","","","","","","","s33/r15","s42/r15","s37/r15","","s36/r15","s38/r15","s39/r15","","s40/r15","s41/r15","s35/r15","s44/r15","s43/r15","s34/r15","","r15","","","","","","","r15"},
{"r14","","","","","","","r14","r14","r14","","r14","r14","r14","","r14","r14","r14","r14","r14","r14","","r14","","","","","","","r14"},
{"s49/r37","","","","","","","","","","","","","","","","","","","","","","r37","","","","","","",""},
{"","","","","","","","","","","","","","","","","","","","","","","","","s5","","s8","s7","s6",""},
{"r6","","","","","","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","","r6","","","","","","","r6"},
				};
		private static readonly Action[,] goTos = new Action[,]
		{
{1,2,4,"","","","",3},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"",23,4,15,16,"","",17},
{"",23,4,29,16,"",27,17},
{"",30,4,"","","","",""},
{"","","","","","","",""},
{"","","","","",32,"",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"",23,4,45,16,"","",17},
{"",23,4,46,16,"","",17},
{"",23,4,47,16,"","",17},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","",32,"",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"",23,4,51,16,"","",17},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","",32,"",""},
{"","","","","",32,"",""},
{"","","","","",32,"",""},
{"","","","","","","",""},
{"",23,4,29,16,"",53,17},
{"","","","","","","",""},
{"","","","","",32,"",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"",55,4,"","","","",""},
{"","","","","","","",""},
		};

		private static readonly PatternGroup[] patterns2 = [
			new PatternGroup(GroupType.Sentence){new PatternGroup(GroupType.Expression),},

			new PatternGroup(GroupType.Identifier){new PatternGroup(GroupType.Variable),},
			new PatternGroup(GroupType.Identifier){new PatternValue(TokenType.StringOrIdentifier), new PatternValue(TokenType.Dot), new PatternGroup(GroupType.Identifier) },
			new PatternGroup(GroupType.Identifier){new PatternValue(TokenType.StringOrIdentifier), new PatternValue(TokenType.LeftBracket), new PatternValue(TokenType.Integer), new PatternValue(TokenType.RightBracket),},

			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.VariableInteger),},
			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.VariableFloat),},
			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.VariableBoolean),},
			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.StringOrIdentifier),},
			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.StringOrIdentifier), new PatternValue(TokenType.LeftBracket), new PatternValue(TokenType.Integer), new PatternValue(TokenType.RightBracket),},

			new PatternGroup(GroupType.Expression){new PatternGroup(GroupType.Number),},
			new PatternGroup(GroupType.Expression){new PatternGroup(GroupType.Function),},
			new PatternGroup(GroupType.Expression){new PatternValue(TokenType.LeftParenthesis), new PatternGroup(GroupType.Expression), new PatternValue(TokenType.RightParenthesis),},
			new PatternGroup(GroupType.Expression){new PatternGroup(GroupType.Expression), new PatternGroup(GroupType.Operator), new PatternGroup(GroupType.Expression),},
			new PatternGroup(GroupType.Expression){new PatternValue(TokenType.OperatorAdd), new PatternGroup(GroupType.Expression),},
			new PatternGroup(GroupType.Expression){new PatternValue(TokenType.OperatorSubtract), new PatternGroup(GroupType.Expression),},

			new PatternGroup(GroupType.Number){new PatternValue(TokenType.Integer),},
			new PatternGroup(GroupType.Number){new PatternValue(TokenType.Float),},
			new PatternGroup(GroupType.Number){new PatternGroup(GroupType.Identifier),},
			new PatternGroup(GroupType.Number){new PatternValue(TokenType.True),},
			new PatternGroup(GroupType.Number){new PatternValue(TokenType.False),},

			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorAdd),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorSubtract),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorMultipy),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorDivide),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorAssignment),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorGreaterThan),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorGreaterThanOrEqual),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorLessThan),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorLessThanOrEqual),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorAnd),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorOr),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorNot),},

			new PatternGroup(GroupType.Args){new PatternValue(TokenType.String),},
			new PatternGroup(GroupType.Args){new PatternGroup(GroupType.Expression),},
			new PatternGroup(GroupType.Args){new PatternGroup(GroupType.Args), new PatternValue(TokenType.Comma), new PatternGroup(GroupType.Args),},

			new PatternGroup(GroupType.Function){new PatternGroup(GroupType.Identifier), new PatternValue(TokenType.LeftParenthesis), new PatternValue(TokenType.RightParenthesis),},
			new PatternGroup(GroupType.Function){new PatternGroup(GroupType.Identifier), new PatternValue(TokenType.LeftParenthesis), new PatternGroup(GroupType.Args), new PatternValue(TokenType.RightParenthesis),},
					];
		private static readonly Action[,] actions2 = new Action[,] {
			{"","","s12","s9","s8","","s5","s6","","","","","","","","","","","s7","","","","s14","s11","s17","s16","s15",""},
{"","","","","","","","","","","","","","","","","","","","","","","","","","","","new()"},
{ "","","","","","","","s19","s28","s23","s22","s24","s25","s26","s27","s21","s30","s29","s20","","","","","","","","","r0"},
{ "r9","","","","","","","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","","r9","","","","","","","r9"},
{ "r10","","","","","","","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","","r10","","","","","","","r10"},
{ "","","s12","s9","s8","","s5","s6","","","","","","","","","","","s7","","","","s14","s11","s17","s16","s15",""},
{ "","","s12","s9","s8","","s5","s6","","","","","","","","","","","s7","","","","s14","s11","s17","s16","s15",""},
{ "","","s12","s9","s8","","s5","s6","","","","","","","","","","","s7","","","","s14","s11","s17","s16","s15",""},
{ "r15","","","","","","","r15","r15","r15","r15","r15","r15","r15","r15","r15","r15","r15","r15","","r15","","","","","","","r15"},
{ "r16","","","","","","","r16","r16","r16","r16","r16","r16","r16","r16","r16","r16","r16","r16","","r16","","","","","","","r16"},
{ "r17","","","","","","s34","r17","r17","r17","r17","r17","r17","r17","r17","r17","r17","r17","r17","","r17","","","","","","","r17"},
{ "r18","","","","","","","r18","r18","r18","r18","r18","r18","r18","r18","r18","r18","r18","r18","","r18","","","","","","","r18"},
{ "r19","","","","","","","r19","r19","r19","r19","r19","r19","r19","r19","r19","r19","r19","r19","","r19","","","","","","","r19"},
{ "r1","","","","","","r1","r1","r1","r1","r1","r1","r1","r1","r1","r1","r1","r1","r1","","r1","","","","","","","r1"},
{ "r7","s35","","","","s36","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","","r7","","","","","","","r7"},
{ "r4","","","","","","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","","r4","","","","","","","r4"},
{ "r5","","","","","","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","","r5","","","","","","","r5"},
{ "r6","","","","","","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","","r6","","","","","","","r6"},
{ "","","s12","s9","s8","","s5","s6","","","","","","","","","","","s7","","","","s14","s11","s17","s16","s15",""},
{ "","","r20","r20","r20","","r20","r20","","","","","","","","","","","r20","","","","r20","r20","r20","r20","r20",""},
{ "","","r21","r21","r21","","r21","r21","","","","","","","","","","","r21","","","","r21","r21","r21","r21","r21",""},
{ "","","r22","r22","r22","","r22","r22","","","","","","","","","","","r22","","","","r22","r22","r22","r22","r22",""},
{ "","","r23","r23","r23","","r23","r23","","","","","","","","","","","r23","","","","r23","r23","r23","r23","r23",""},
{ "","","r24","r24","r24","","r24","r24","","","","","","","","","","","r24","","","","r24","r24","r24","r24","r24",""},
{ "","","r25","r25","r25","","r25","r25","","","","","","","","","","","r25","","","","r25","r25","r25","r25","r25",""},
{ "","","r26","r26","r26","","r26","r26","","","","","","","","","","","r26","","","","r26","r26","r26","r26","r26",""},
{ "","","r27","r27","r27","","r27","r27","","","","","","","","","","","r27","","","","r27","r27","r27","r27","r27",""},
{ "","","r28","r28","r28","","r28","r28","","","","","","","","","","","r28","","","","r28","r28","r28","r28","r28",""},
{ "","","r29","r29","r29","","r29","r29","","","","","","","","","","","r29","","","","r29","r29","r29","r29","r29",""},
{ "","","r30","r30","r30","","r30","r30","","","","","","","","","","","r30","","","","r30","r30","r30","r30","r30",""},
{ "","","r31","r31","r31","","r31","r31","","","","","","","","","","","r31","","","","r31","r31","r31","r31","r31",""},
{ "","","","","","","","s19","s28","s23","s22","s24","s25","s26","s27","s21","s30","s29","s20","","s38","","","","","","",""},
{ "r13","","","","","","","s19/r13","s28/r13","s23/r13","s22/r13","s24/r13","s25/r13","s26/r13","s27/r13","s21/r13","s30/r13","s29/r13","s20/r13","","r13","","","","","","","r13"},
{ "r14","","","","","","","s19/r14","s28/r14","s23/r14","s22/r14","s24/r14","s25/r14","s26/r14","s27/r14","s21/r14","s30/r14","s29/r14","s20/r14","","r14","","","","","","","r14"},
{ "","","s12","s9","s8","","s5","s6","","","","","","","","","","","s7","","s39","s41","s14","s11","s17","s16","s15",""},
{ "","","","","","","","","","","","","","","","","","","","","","","s14","","s17","s16","s15",""},
{ "","","","","s44","","","","","","","","","","","","","","","","","","","","","","",""},
{ "r12","","","","","","","s19/r12","s28/r12","s23/r12","s22/r12","s24/r12","s25/r12","s26/r12","s27/r12","s21/r12","s30/r12","s29/r12","s20/r12","","r12","","","","","","","r12"},
{ "r11","","","","","","","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","","r11","","","","","","","r11"},
{ "r35","","","","","","","r35","r35","r35","r35","r35","r35","r35","r35","r35","r35","r35","r35","","r35","","","","","","","r35"},
{ "s46","","","","","","","","","","","","","","","","","","","","s45","","","","","","",""},
{ "r32","","","","","","","","","","","","","","","","","","","","r32","","","","","","",""},
{ "r33","","","","","","","s19","s28","s23","s22","s24","s25","s26","s27","s21","s30","s29","s20","","r33","","","","","","",""},
{ "r2","","","","","","r2","r2","r2","r2","r2","r2","r2","r2","r2","r2","r2","r2","r2","","r2","","","","","","","r2"},
{ "","","","","","","","","","","","","","","","","","","","s47","","","","","","","",""},
{ "r36","","","","","","","r36","r36","r36","r36","r36","r36","r36","r36","r36","r36","r36","r36","","r36","","","","","","","r36"},
{ "","","s12","s9","s8","","s5","s6","","","","","","","","","","","s7","","","s41","s14","s11","s17","s16","s15",""},
{ "r8","s49","","","","","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","","r8","","","","","","","r8"},
{ "s46/r34","","","","","","","","","","","","","","","","","","","","r34","","","","","","",""},
{ "","","","","","","","","","","","","","","","","","","","","","","s14","","s17","s16","s15",""},
{ "r3","","","","","","r3","r3","r3","r3","r3","r3","r3","r3","r3","r3","r3","r3","r3","","r3","","","","","","","r3"},
		};
		private static readonly Action[,] goTos2 = new Action[,]
		{
{1,10,13,2,3,"","",4},
{"","","","","","","",""},
{"","","","","",18,"",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"",10,13,31,3,"","",4},
{"",10,13,32,3,"","",4},
{"",10,13,33,3,"","",4},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"",10,13,37,3,"","",4},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","",18,"",""},
{"","","","","",18,"",""},
{"","","","","",18,"",""},
{"",10,13,42,3,"",40,4},
{"",43,13,"","","","",""},
{"","","","","","","",""},
{"","","","","",18,"",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","",18,"",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"",10,13,42,3,"",48,4},
{"","","","","","","",""},
{"","","","","","","",""},
{"",50,13,"","","","",""},
{"","","","","","","",""},
		};

		private static readonly List<TokenType> actionIndexes =
			[
			TokenType.Comma,
			TokenType.Dot,
			TokenType.False,
			TokenType.Float,
			TokenType.Integer,
			TokenType.LeftBracket,
			TokenType.LeftParenthesis,
			TokenType.OperatorAdd,
			TokenType.OperatorAnd,
			TokenType.OperatorAssignment,
			TokenType.OperatorDecreasement,
			TokenType.OperatorDivide,
			TokenType.OperatorGreaterThan,
			TokenType.OperatorGreaterThanOrEqual,
			TokenType.OperatorIncreasement,
			TokenType.OperatorLessThan,
			TokenType.OperatorLessThanOrEqual,
			TokenType.OperatorMultipy,
			TokenType.OperatorNot,
			TokenType.OperatorOr,
			TokenType.OperatorSubtract,
			TokenType.RightBracket,
			TokenType.RightParenthesis,
			TokenType.String,
			TokenType.StringOrIdentifier,
			TokenType.True,
			TokenType.VariableBoolean,
			TokenType.VariableFloat,
			TokenType.VariableInteger,
		];
		private static readonly List<GroupType> goToIndexes =
			[
			GroupType.Sentence,
			GroupType.Identifier,
			GroupType.Variable,
			GroupType.Expression,
			GroupType.Number,
			GroupType.Operator,
			GroupType.Args,
			GroupType.Function,
		];
		private static readonly List<HashSet<TokenType>> priority = [
			[TokenType.OperatorAssignment],
			[TokenType.OperatorGreaterThan, TokenType.OperatorGreaterThanOrEqual, TokenType.OperatorLessThan, TokenType.OperatorLessThanOrEqual],
			[TokenType.OperatorAdd, TokenType.OperatorSubtract],
			[TokenType.OperatorMultipy, TokenType.OperatorDivide],
		];
	}
}