using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Text;

// 这坨写得太史了

namespace RhythmBase.Generator;

[Generator(LanguageNames.CSharp)]
public class ConverterGenerator : IIncrementalGenerator
{
	// Attribute 名称常量
	private const string JsonEnumAttrName = "RhythmBase.Global.Converters.RDJsonEnumSerializableAttribute";
	private const string JsonObjectNotSerializableAttrName = "RhythmBase.Global.Converters.RDJsonObjectNotSerializableAttribute";
	private const string JsonAliasAttrName = "RhythmBase.Global.Converters.RDJsonAliasAttribute";
	private const string JsonIgnoreAttrName = "RhythmBase.Global.Converters.RDJsonIgnoreAttribute";
	private const string JsonNotIgnoreAttrName = "RhythmBase.Global.Converters.RDJsonNotIgnoreAttribute";
	private const string JsonDefaultSerializerAttrName = "RhythmBase.Global.Converters.RDJsonDefaultSerializerAttribute";
	private const string JsonConditionAttrName = "RhythmBase.Global.Converters.RDJsonConditionAttribute";
	private const string JsonTimeAttrName = "RhythmBase.Global.Converters.RDJsonTimeAttribute";
	private const string JsonConverterAttrName = "RhythmBase.Global.Converters.RDJsonConverterAttribute";
	private const string JsonSpecialIDAttrName = "RhythmBase.Global.Converters.RDJsonSpecialIDAttribute";

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		GenerateAttribute(context);
		GenerateEnumConverter(context);
		GenerateEventConverterForRDLevel(context);
		GenerateEventConverterForADLevel(context);
		GenerateFilterConverter(context);

		// 预留：类处理
		// var classes = context.SyntaxProvider.CreateSyntaxProvider(
		// 	predicate: (s, _) => s is ClassDeclarationSyntax,
		// 	transform: (ctx, _) => { ... })
		// 	.Where(x => x.Item2)
		// 	.Select((x, _) => x.symbol)
		// 	.Collect();
	}
	#region Attribute 生成

	private static void GenerateAttribute(IncrementalGeneratorInitializationContext context)
	{
		context.RegisterPostInitializationOutput(ctx =>
		{
			ctx.AddSource("JsonSerializableAttribute.g.cs",
				"""
				using System;
				#pragma warning disable CS9113
				namespace RhythmBase.Global.Converters
				{
					internal sealed class RDJsonEnumSerializableAttribute : Attribute { }
					internal sealed class RDJsonObjectNotSerializableAttribute : Attribute { }
					internal sealed class RDJsonIgnoreAttribute : Attribute { }
					internal sealed class RDJsonNotIgnoreAttribute : Attribute { }
					internal sealed class RDJsonConditionAttribute(string condition) : Attribute { }
					internal sealed class RDJsonAliasAttribute(string name) : Attribute { }
					internal sealed class RDJsonDefaultSerializerAttribute : Attribute { }
					internal sealed class RDJsonTimeAttribute(string type) : Attribute { }
					internal sealed class RDJsonConverterAttribute(Type converterType) : Attribute { }
					internal sealed class RDJsonSpecialIDAttribute(string id) : Attribute { }
				}
				"""
			);
		});
	}

	#endregion

	private static bool HasAttribute(SyntaxList<AttributeListSyntax> list, string attributeFullName)
	{
		string attributeFullNameWithoutPostfix = attributeFullName.Remove(attributeFullName.Length - "Attribute".Length);
		string attributeShortName = attributeFullName.Split('.').Last();
		string attributeShortNameWithoutPostfix = attributeShortName.Remove(attributeShortName.Length - "Attribute".Length);
		return list.Any(i => i.Attributes.Any(j =>
		{
			string name = j.Name.ToString();
			return
				name == attributeShortName ||
				name == attributeFullName ||
				name == attributeShortNameWithoutPostfix ||
				name == attributeFullNameWithoutPostfix;
		}));
	}

	#region Enum Converter 生成

	private record struct FieldName(string Name, string FullName, string? Alias = null);
	private struct EnumInfo
	{
		public FieldName Symbol;
		public FieldName[] Fields;
	}
	private static void GenerateEnumConverter(IncrementalGeneratorInitializationContext context)
	{
		IncrementalValueProvider<ImmutableArray<EnumInfo>> enums = context.SyntaxProvider.CreateSyntaxProvider(
		predicate: (syntaxNode, e) =>
		{
			return syntaxNode is EnumDeclarationSyntax enumDeclaration &&
			 HasAttribute(enumDeclaration.AttributeLists,JsonEnumAttrName);
		},
		transform: (ctx, e) =>
		{
			EnumDeclarationSyntax enumDeclaration = (EnumDeclarationSyntax)ctx.Node;
			EnumMemberDeclarationSyntax[] enumMemberDeclaration = [.. enumDeclaration.ChildNodes().OfType<EnumMemberDeclarationSyntax>()];
			EnumInfo info = new();
			INamedTypeSymbol? symbol = ctx.SemanticModel.GetDeclaredSymbol(enumDeclaration);
			info.Symbol = new(symbol.Name, symbol.ToDisplayString());
			info.Fields = [.. enumMemberDeclaration.Select(i =>
			{
				IFieldSymbol? symbol = ctx.SemanticModel.GetDeclaredSymbol(i);
				return new FieldName(i.Identifier.Text, symbol.ToDisplayString());
			})];
			return info;
		}
		).Collect();

		context.RegisterSourceOutput(enums, (spc, enumSymbols) =>
		{
			StringBuilder sb = new();
			sb.AppendLine("""
				
					// <auto-generated/>
					#nullable enable
					using System;
					using System.Runtime.CompilerServices;
				
					namespace RhythmBase.Global.Extensions
					{
						/// <summary>
						/// Provides extension methods for converting enums to and from string representations.
						/// </summary>
						public static class EnumConverter
						{
					""");

			foreach (EnumInfo e in enumSymbols.OrderBy(i => i.Symbol.FullName))
			{
				string fullName = e.Symbol.FullName;
				sb.AppendLine($"/* {fullName} */");

				// TryParse(string)
				sb.AppendLine($$"""
							/// <summary>
							/// Attempts to parse the specified string value to a <see cref="{{fullName}}"/> enum value.
							/// </summary>
							/// <param name="value">The string representation of the enum value.</param>
							/// <param name="result">When this method returns, contains the parsed <see cref="{{fullName}}"/> value if parsing succeeded; otherwise, the default value.</param>
							/// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
							[MethodImpl(MethodImplOptions.AggressiveInlining)]
							public static bool TryParse(string? value, out {{fullName}} result)
							{
								switch(value) {
					""");
				foreach (FieldName field in e.Fields)
				{
					sb.AppendLine($$"""
									case "{{field.Name}}":
										result = {{field.FullName}};
										return true;
					""");
				}
				sb.AppendLine("""
					
									default:
										result = default;
										return false;
								}
							}
					
					""");
				sb.AppendLine();

				// TryParse(ReadOnlySpan<byte>)
				sb.AppendLine($$"""
							/// <summary>
							/// Attempts to parse the specified UTF-8 byte span to a <see cref="{{fullName}}"/> enum value.
							/// </summary>
							/// <param name="value">The UTF-8 byte span representing the enum value.</param>
							/// <param name="result">When this method returns, contains the parsed <see cref="{{fullName}}"/> value if parsing succeeded; otherwise, the default value.</param>
							/// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
							[MethodImpl(MethodImplOptions.AggressiveInlining)]
							public static bool TryParse(ReadOnlySpan<byte> value, out {{fullName}} result)
							{
					""");
				bool isFirst = true;
				foreach (FieldName field in e.Fields.OrderBy(i => i.FullName))
				{
					sb.AppendLine($$"""
								{{(isFirst ? "": "else ")}}if (value.SequenceEqual("{{field.Name}}"u8))
								{
									result = {{field.FullName}};
									return true;
								}
					""");
					isFirst = false;
				}
				sb.AppendLine("""
								else
								{
									result = default;
									return false;
								}
							}

					""");

				// ToEnumString
				sb.AppendLine($$"""
							/// <summary>
							/// Converts the <see cref="{{fullName}}"/> enum value to its string representation.
							/// </summary>
							/// <param name="value">The enum value to convert.</param>
							/// <returns>The string representation of the enum value.</returns>
							[MethodImpl(MethodImplOptions.AggressiveInlining)]
							public static string ToEnumString(this {{fullName}} value) => value switch
							{
					""");
				foreach (FieldName field in e.Fields)
				{
					sb.AppendLine($"""
								{field.FullName} => "{field.Name}",
					""");
				}
				sb.AppendLine("""
								_ => value.ToString(),
							};
					""");
			}
			sb.AppendLine("""
						}
					}
					""");

			spc.AddSource("EnumConverters.g.cs", sb.ToString());
		});
	}

	#endregion

	#region Event Converter 生成

	private struct PropertyInfo
	{
		public string Name;
		public string? Alias;
		public string Type;
		public bool IsNullable;
		public string? Condition;
		public string? Converter;
	}
	private struct ClassInfo
	{
		public string Name;
		public string? Alias;
		public PropertyInfo[] Properties;
	}
	private	static AttributeSyntax? GetAttribute(SyntaxList<AttributeListSyntax> list, string attributeFullName)
		{
			string attributeFullNameWithoutPostfix = attributeFullName.Remove(attributeFullName.Length - "Attribute".Length);
			string attributeShortName = attributeFullName.Split('.').Last();
			string attributeShortNameWithoutPostfix = attributeShortName.Remove(attributeShortName.Length - "Attribute".Length);
			foreach (var attrList in list)
			{
				foreach (var attr in attrList.Attributes)
				{
					string name = attr.Name.ToString();
					if (name == attributeShortName ||
						name == attributeFullName ||
						name == attributeShortNameWithoutPostfix ||
						name == attributeFullNameWithoutPostfix)
					{
						return attr;
					}
				}
			}
			return null;
		}

	private static void GenerateEventConverterForRDLevel(IncrementalGeneratorInitializationContext context)
	{
		HashSet<string> names = [];
		//var classes = context.SyntaxProvider.CreateSyntaxProvider(
		//	predicate: (s, e) => { return
		//		s is ClassDeclarationSyntax classDeclaration &&
		//		classDeclaration.BaseList is not null &&
		//		!HasAttribute(classDeclaration.AttributeLists, JsonIgnoreAttrName); },
		//	transform: (ctx, e) => { 
		//		ClassDeclarationSyntax classDeclaration = (ClassDeclarationSyntax)ctx.Node;
		//		PropertyDeclarationSyntax[] propertyDeclarations = [.. classDeclaration.ChildNodes().OfType<PropertyDeclarationSyntax>()];
		//		INamedTypeSymbol classDeclarationSymbol = ctx.SemanticModel.GetDeclaredSymbol(classDeclaration);
		//		if (classDeclarationSymbol is null)
		//			return null;
		//		bool isTargetEvent = 
		//			(!classDeclarationSymbol.IsAbstract) &&
		//			(classDeclarationSymbol.ContainingNamespace?.ToDisplayString().Contains("RhythmBase.RhythmDoctor.Events") ?? false) &&
		//			classDeclarationSymbol.AllInterfaces.Any(i => i.ToDisplayString().Contains("IBaseEvent"));
		//		if (!isTargetEvent)
		//			return null;
		//		PropertyInfo[] props = propertyDeclarations
		//			.Where(i => HasAttribute(i.AttributeLists, JsonNotIgnoreAttrName) && !HasAttribute(i.AttributeLists, JsonIgnoreAttrName))
		//			.Select(i =>
		//			{
		//				PropertyInfo prop = new();
		//				var conditionAttr = GetAttribute(i.AttributeLists, JsonConditionAttrName);
		//				var aliasAttr = GetAttribute(i.AttributeLists, JsonAliasAttrName);
		//				var converterAttr = GetAttribute(i.AttributeLists, JsonConverterAttrName);
		//				var timeAttr = GetAttribute(i.AttributeLists, JsonTimeAttrName);
		//			});
		//	}
		//	);
		IncrementalValueProvider <ImmutableArray<(INamedTypeSymbol? symbol, INamedTypeSymbol? baseType)>> eventClasses = context.SyntaxProvider.CreateSyntaxProvider(
			predicate: (s, _) => s is ClassDeclarationSyntax,
			transform: (ctx, _) =>
			{
				ClassDeclarationSyntax classDeclaration = (ClassDeclarationSyntax)ctx.Node;
				INamedTypeSymbol? symbol = ctx.SemanticModel.GetDeclaredSymbol(classDeclaration);
				if (symbol is INamedTypeSymbol namedTypeSymbol
				&& (namedTypeSymbol.AllInterfaces.Any(i => i.ToDisplayString().Contains("IBaseEvent")))//namedTypeSymbol.AllInterfaces.Any(i => i.Name.Contains("IBaseEvent"))
				&& (namedTypeSymbol.ContainingNamespace?.ToDisplayString().Contains("RhythmBase.RhythmDoctor.Events") ?? false)
				&& !namedTypeSymbol.IsAbstract
				)
				{
					INamedTypeSymbol? baseType = namedTypeSymbol.BaseType;
					if (names.Add(symbol.Name))
						return (symbol, baseType);
				}
#pragma warning disable CS8619 // 值中的引用类型的为 Null 性与目标类型不匹配。
				return (null, null);
#pragma warning restore CS8619 // 值中的引用类型的为 Null 性与目标类型不匹配。
			})
			.Where(i => i.symbol is not null)
			.Collect();

		context.RegisterSourceOutput(eventClasses, (ctx, symbols) =>
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("// <auto-generated/>");
			sb.AppendLine("#nullable enable");
			sb.AppendLine();
			sb.AppendLine("using System;");
			sb.AppendLine("using RhythmBase.Global.Extensions;");
			sb.AppendLine("using RhythmBase.Global.Components;");
			sb.AppendLine("using RhythmBase.Global.Components.Vector;");
			sb.AppendLine("using RhythmBase.Global.Components.Easing;");
			sb.AppendLine("using RhythmBase.Global.Components.RichText;");
			sb.AppendLine("using RhythmBase.RhythmDoctor.Events;");
			sb.AppendLine("using RhythmBase.RhythmDoctor.Components;");
			sb.AppendLine("using System.Text.Json;");
			sb.AppendLine("using static RhythmBase.Global.Extensions.EnumConverter;");
			sb.AppendLine();
			sb.AppendLine("namespace RhythmBase.RhythmDoctor.Converters;");
			sb.AppendLine();

			foreach ((INamedTypeSymbol? symbol, INamedTypeSymbol? baseType) in symbols
				.Where(i => !i.symbol.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == JsonObjectNotSerializableAttrName))
				.OrderBy(i => i.symbol.Name))
			{
				if (symbol is null)
					continue;
				INamedTypeSymbol e = symbol;
				List<IPropertySymbol> properties = e.GetMembers()
					.OfType<IPropertySymbol>()
					.Where(p =>
						p.DeclaredAccessibility.HasFlag(Accessibility.Public) && p.SetMethod != null ||
						p.GetAttributes().Any(i => i.AttributeClass?.ToDisplayString() == JsonNotIgnoreAttrName))
					.ToList();

				sb.AppendLine($"internal class EventInstanceConverter{e.Name} : EventInstanceConverter{baseType?.Name}<{e.ToDisplayString()}>");
				sb.AppendLine("{");
				// Read
				sb.AppendLine($"	protected override bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref {e.ToDisplayString()} value, JsonSerializerOptions options)");
				sb.AppendLine("	{");
				if (properties.Count > 0)
				{
					sb.AppendLine("		if(base.Read(ref reader, propertyName, ref value, options))");
					sb.AppendLine("			return true;");
					AppendEventReadBody(sb, properties, e);
				}
				else
				{
					sb.AppendLine("		return base.Read(ref reader, propertyName, ref value, options);");
				}
				sb.AppendLine("	}");

				// Write
				sb.AppendLine($"	protected override void Write(Utf8JsonWriter writer, ref {e.Name} value, JsonSerializerOptions options)");
				sb.AppendLine("	{");
				sb.AppendLine("		base.Write(writer, ref value, options);");
				AppendEventWriteBody(sb, properties, e);
				sb.AppendLine("	}");
				sb.AppendLine("}");
			}
			ctx.AddSource("EventInstanceConvertersRDLevel.g.cs", sb.ToString());

			// EventTypeUtils
			sb.Clear();
			sb.AppendLine("// <auto-generated/>");
			sb.AppendLine("#nullable enable");
			sb.AppendLine();
			sb.AppendLine("using System;");
			sb.AppendLine();
			sb.AppendLine("namespace RhythmBase.RhythmDoctor.Utils;");
			sb.AppendLine();
			sb.AppendLine("partial class EventTypeUtils");
			sb.AppendLine("{");
			sb.AppendLine("	internal static System.Collections.ObjectModel.ReadOnlyDictionary<RhythmBase.RhythmDoctor.Events.EventType, RhythmBase.RhythmDoctor.Converters.EventInstanceConverterBase> converters = new(new Dictionary<RhythmBase.RhythmDoctor.Events.EventType, RhythmBase.RhythmDoctor.Converters.EventInstanceConverterBase>()");
			sb.AppendLine("	{");
			foreach ((INamedTypeSymbol? symbol, INamedTypeSymbol? _) in symbols
				.Where(i => !(i.symbol.Name.StartsWith("Forward") && i.symbol.Name.EndsWith("Event")))
				.OrderBy(i => i.symbol.Name))
			{
				sb.AppendLine($"			[RhythmBase.RhythmDoctor.Events.EventType.{symbol.Name}] = new RhythmBase.RhythmDoctor.Converters.EventInstanceConverter{symbol.Name}(),");
			}
			sb.AppendLine("	});");
			sb.AppendLine("}");
			ctx.AddSource("EventTypeUtilsRDLevel.g.cs", sb.ToString());
		});
	}

	private static void GenerateEventConverterForADLevel(IncrementalGeneratorInitializationContext context)
	{
		HashSet<string> names = [];
		IncrementalValueProvider<ImmutableArray<(INamedTypeSymbol? symbol, INamedTypeSymbol? baseType)>> eventClasses = context.SyntaxProvider.CreateSyntaxProvider(
			predicate: (s, _) => s is ClassDeclarationSyntax,
			transform: (ctx, _) =>
			{
				ClassDeclarationSyntax classDeclaration = (ClassDeclarationSyntax)ctx.Node;
				INamedTypeSymbol? symbol = ctx.SemanticModel.GetDeclaredSymbol(classDeclaration);
				if (symbol is INamedTypeSymbol namedTypeSymbol &&
					namedTypeSymbol.AllInterfaces.Any(i => i.Name == "IBaseEvent") &&
				(namedTypeSymbol.ContainingNamespace?.ToDisplayString() == "RhythmBase.Adofai.Events") &&
					!namedTypeSymbol.IsAbstract)
				{
					INamedTypeSymbol? baseType = namedTypeSymbol.BaseType;
					if (names.Add(symbol.Name))
						return (symbol, baseType);
				}
#pragma warning disable CS8619 // 值中的引用类型的为 Null 性与目标类型不匹配。
				return (null, null);
#pragma warning restore CS8619 // 值中的引用类型的为 Null 性与目标类型不匹配。
			})
			.Where(i => i.symbol is not null)
			.Collect();

		context.RegisterSourceOutput(eventClasses, (ctx, symbols) =>
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("// <auto-generated/>");
			sb.AppendLine("#nullable enable");
			sb.AppendLine();
			sb.AppendLine("using System;");
			sb.AppendLine("using RhythmBase.Global.Extensions;");
			sb.AppendLine("using RhythmBase.Global.Components;");
			sb.AppendLine("using RhythmBase.Global.Components.Vector;");
			sb.AppendLine("using RhythmBase.Global.Components.Easing;");
			sb.AppendLine("using RhythmBase.Global.Components.RichText;");
			sb.AppendLine("using RhythmBase.Adofai.Events;");
			sb.AppendLine("using RhythmBase.Adofai.Components;");
			sb.AppendLine("using System.Text.Json;");
			sb.AppendLine("using static RhythmBase.Global.Extensions.EnumConverter;");
			sb.AppendLine();
			sb.AppendLine("namespace RhythmBase.Adofai.Converters;");
			sb.AppendLine();

			foreach ((INamedTypeSymbol? symbol, INamedTypeSymbol? baseType) in symbols
				.Where(i => !i.symbol.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == JsonObjectNotSerializableAttrName))
				.OrderBy(i => i.symbol.Name))
			{
				if (symbol is null)
					continue;
				INamedTypeSymbol e = symbol;
				List<IPropertySymbol> properties = e.GetMembers()
					.OfType<IPropertySymbol>()
					.Where(p =>
						p.DeclaredAccessibility.HasFlag(Accessibility.Public) && p.SetMethod != null ||
						p.GetAttributes().Any(i => i.AttributeClass?.ToDisplayString() == JsonNotIgnoreAttrName))
					.ToList();

				sb.AppendLine($"internal class EventInstanceConverter{e.Name} : EventInstanceConverter{baseType?.Name}<{e.ToDisplayString()}>");
				sb.AppendLine("{");
				// Read
				sb.AppendLine($"	protected override bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref {e.ToDisplayString()} value, JsonSerializerOptions options)");
				sb.AppendLine("	{");
				if (properties.Count > 0)
				{
					sb.AppendLine("		if(base.Read(ref reader, propertyName, ref value, options))");
					sb.AppendLine("			return true;");
					AppendEventReadBody(sb, properties, e);
				}
				else
				{
					sb.AppendLine("		return base.Read(ref reader, propertyName, ref value, options);");
				}
				sb.AppendLine("	}");

				// Write
				sb.AppendLine($"	protected override void Write(Utf8JsonWriter writer, ref {e.Name} value, JsonSerializerOptions options)");
				sb.AppendLine("	{");
				sb.AppendLine("		base.Write(writer, ref value, options);");
				AppendEventWriteBody(sb, properties, e);
				sb.AppendLine("	}");
				sb.AppendLine("}");
			}
			ctx.AddSource("EventInstanceConvertersADLevel.g.cs", sb.ToString());

			// EventTypeUtils
			sb.Clear();
			sb.AppendLine("// <auto-generated/>");
			sb.AppendLine("#nullable enable");
			sb.AppendLine();
			sb.AppendLine("using System;");
			sb.AppendLine();
			sb.AppendLine("namespace RhythmBase.Adofai.Utils;");
			sb.AppendLine();
			sb.AppendLine("partial class EventTypeUtils");
			sb.AppendLine("{");
			sb.AppendLine("	internal static System.Collections.ObjectModel.ReadOnlyDictionary<RhythmBase.Adofai.Events.EventType, RhythmBase.Adofai.Converters.EventInstanceConverterBase> converters = new(new Dictionary<RhythmBase.Adofai.Events.EventType, RhythmBase.Adofai.Converters.EventInstanceConverterBase>()");
			sb.AppendLine("	{");
			foreach ((INamedTypeSymbol? symbol, INamedTypeSymbol? _) in symbols
				.Where(i => !(i.symbol.Name.StartsWith("Forward") && i.symbol.Name.EndsWith("Event")))
				.OrderBy(i => i.symbol.Name))
			{
				sb.AppendLine($"			[RhythmBase.Adofai.Events.EventType.{symbol.Name}] = new RhythmBase.Adofai.Converters.EventInstanceConverter{symbol.Name}(),");
			}
			sb.AppendLine("	});");
			sb.AppendLine("}");
			ctx.AddSource("EventTypeUtilsADLevel.g.cs", sb.ToString());
		});
	}
	#endregion

	#region Filter Converter 生成

	private static void GenerateFilterConverter(IncrementalGeneratorInitializationContext context)
	{
		HashSet<string> names = [];
		IncrementalValueProvider<ImmutableArray<(INamedTypeSymbol? symbol, INamedTypeSymbol? baseType)>> eventClasses = context.SyntaxProvider.CreateSyntaxProvider(
			predicate: (s, _) =>
			{
				if (s is not StructDeclarationSyntax structDeclarationSyntax)
					return false;
				return true;
			},
			transform: (ctx, _) =>
			{
				StructDeclarationSyntax classDeclaration = (StructDeclarationSyntax)ctx.Node;
				INamedTypeSymbol? symbol = ctx.SemanticModel.GetDeclaredSymbol(classDeclaration);
				if (symbol is INamedTypeSymbol namedTypeSymbol
				&& (namedTypeSymbol.AllInterfaces.Any(i => i.ToDisplayString().Contains("IFilter")))//namedTypeSymbol.AllInterfaces.Any(i => i.Name.Contains("IBaseEvent"))
				&& (namedTypeSymbol.ContainingNamespace?.ToDisplayString().Contains("RhythmBase.Adofai.Components.Filters") ?? false)
				&& !namedTypeSymbol.IsAbstract
				)
				{
					INamedTypeSymbol? baseType = namedTypeSymbol.BaseType;
					if (names.Add(symbol.Name))
						return (symbol, baseType);
				}
#pragma warning disable CS8619 // 值中的引用类型的为 Null 性与目标类型不匹配。
				return (null, null);
#pragma warning restore CS8619 // 值中的引用类型的为 Null 性与目标类型不匹配。
			})
			.Where(i => i.symbol is not null)
			.Collect();

		context.RegisterSourceOutput(eventClasses, (ctx, symbols) =>
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("// <auto-generated/>");
			sb.AppendLine("#nullable enable");
			sb.AppendLine();
			sb.AppendLine("using System;");
			sb.AppendLine("using RhythmBase.Global.Extensions;");
			sb.AppendLine("using RhythmBase.Global.Components;");
			sb.AppendLine("using RhythmBase.Global.Components.Vector;");
			sb.AppendLine("using RhythmBase.Global.Components.Easing;");
			sb.AppendLine("using RhythmBase.Global.Components.RichText;");
			sb.AppendLine("using RhythmBase.Adofai.Components.Filters;");
			sb.AppendLine("using System.Text.Json;");
			sb.AppendLine();
			sb.AppendLine("namespace RhythmBase.Adofai.Converters;");
			sb.AppendLine();

			foreach ((INamedTypeSymbol? symbol, INamedTypeSymbol? baseType) in symbols
				.Where(i => !i.symbol.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == JsonObjectNotSerializableAttrName))
				.OrderBy(i => i.symbol.Name))
			{
				if (symbol is null)
					continue;
				INamedTypeSymbol e = symbol;
				List<IPropertySymbol> properties = e.GetMembers()
					.OfType<IPropertySymbol>()
					.Where(p =>
						p.DeclaredAccessibility.HasFlag(Accessibility.Public) && p.SetMethod != null ||
						p.GetAttributes().Any(i => i.AttributeClass?.ToDisplayString() == JsonNotIgnoreAttrName))
					.ToList();


				sb.AppendLine($"internal class FilterInstanceConverter{e.Name} : FilterInstanceConverterBase<{e.ToDisplayString()}>");
				sb.AppendLine("{");
				// Read
				sb.AppendLine($"	protected override bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref {e.ToDisplayString()} value, JsonSerializerOptions options)");
				sb.AppendLine("	{");
				if (properties.Count > 0)
				{
					sb.AppendLine("		if(base.Read(ref reader, propertyName, ref value, options))");
					sb.AppendLine("			return true;");
					AppendEventReadBody(sb, properties, e);
				}
				else
				{
					sb.AppendLine("		return base.Read(ref reader, propertyName, ref value, options);");
				}
				sb.AppendLine("	}");

				// Write
				sb.AppendLine($"	protected override void Write(Utf8JsonWriter writer, ref {e.Name} value, JsonSerializerOptions options)");
				sb.AppendLine("	{");
				sb.AppendLine("		base.Write(writer, ref value, options);");
				AppendEventWriteBody(sb, properties, e);
				sb.AppendLine("	}");
				sb.AppendLine("}");
			}
			ctx.AddSource("FilterInstanceConverter.g.cs", sb.ToString());

			// EventTypeUtils
			sb.Clear();
			sb.AppendLine("// <auto-generated/>");
			sb.AppendLine("#nullable enable");
			sb.AppendLine();
			sb.AppendLine("using System;");
			sb.AppendLine();
			sb.AppendLine("namespace RhythmBase.Adofai.Utils;");
			sb.AppendLine();
			sb.AppendLine("internal partial class FilterTypeUtils");
			sb.AppendLine("{");
			sb.AppendLine("	internal static System.Collections.ObjectModel.ReadOnlyDictionary<string, RhythmBase.Adofai.Converters.FilterInstanceConverterBase> converters = new(new Dictionary<string, RhythmBase.Adofai.Converters.FilterInstanceConverterBase>()");
			sb.AppendLine("	{");

			foreach ((INamedTypeSymbol? symbol, INamedTypeSymbol? _) in symbols
				.OrderBy(i => i.symbol.Name))
			{
				string specialID = "";

				AttributeData? specialIDAttr = symbol.GetAttributes().FirstOrDefault(i => i.AttributeClass?.ToDisplayString() == JsonSpecialIDAttrName);
				if (specialIDAttr is not null)
				{
					specialID = specialIDAttr.ConstructorArguments.Length > 0 ? specialIDAttr.ConstructorArguments[0].Value?.ToString() ?? "" : "";
				}

				//sb.AppendLine($"			//{filterName.}");
				sb.AppendLine($"			[\"{specialID}\"] = new RhythmBase.Adofai.Converters.FilterInstanceConverter{symbol.Name}(),");
			}
			sb.AppendLine("	});");
			sb.AppendLine("}");
			ctx.AddSource("FilterTypeUtilsADLevel.g.cs", sb.ToString());
		});
	}
	#endregion

	#region 工具方法

	private static string TypeNameOf(ITypeSymbol symbol)
	{
		if (symbol is INamedTypeSymbol namedTypeSymbol)
		{
			if (namedTypeSymbol.TypeArguments.Length > 0)
			{
				string typeArgs = string.Join(", ", namedTypeSymbol.TypeArguments.Select(t => t.ToDisplayString()));
				return $"{namedTypeSymbol.Name}<{typeArgs}>";
			}
			else
			{
				return namedTypeSymbol.Name;
			}
		}
		else if (symbol is IArrayTypeSymbol)
		{
			return symbol.ToDisplayString();
		}
		return "";
	}

	private static string LastPartOf(string str) => str.Split('.').Last();

	private static string ToLowerCamelCase(string str)
	{
		if (string.IsNullOrEmpty(str) || str.Length < 2)
			return str.ToLower();
		return char.ToLower(str[0]) + str.Substring(1);
	}
	private static string GetFullNamespace(INamedTypeSymbol type)
	{
		if (type == null) return string.Empty;

		INamespaceSymbol? ns = type.ContainingNamespace;
		if (ns == null || ns.IsGlobalNamespace)   // 顶级命名空间
			return string.Empty;

		return ns.ToDisplayString();   // 自带“.”连接
	}
	#endregion

#pragma warning disable IDE0060

	// 分离的 Read 代码生成
	private static void AppendEventReadBody(StringBuilder sb, List<IPropertySymbol> properties, INamedTypeSymbol e)
	{
		bool isFirst = true;
		int enumIndex = 0;
		foreach (IPropertySymbol? p in properties)
		{
			string propertyName = p.Name;
			string jsonName = p.Name;
			ITypeSymbol type = p.Type;

			if (p.GetAttributes().Any(i => i.AttributeClass?.ToDisplayString() == JsonIgnoreAttrName))
				continue;
			if (p.SetMethod == null)
				continue;
			AttributeData? nameAttr = p.GetAttributes().FirstOrDefault(i => i.AttributeClass?.ToDisplayString() == JsonAliasAttrName);
			AttributeData? timeAttr = p.GetAttributes().FirstOrDefault(i => i.AttributeClass?.ToDisplayString() == JsonTimeAttrName);
			AttributeData? converterAttr = p.GetAttributes().FirstOrDefault(i => i.AttributeClass?.ToDisplayString() == JsonConverterAttrName);
			if (nameAttr is not null)
			{
				ImmutableArray<TypedConstant> args = nameAttr.ConstructorArguments;
				if (args != null)
					foreach (TypedConstant arg in args)
						jsonName = args[0].Value?.ToString() ?? propertyName;
			}
			else
			{
				jsonName = ToLowerCamelCase(LastPartOf(propertyName));
			}

			string name2 = propertyName;
			ITypeSymbol type2 = type;
			bool isNullable = false;
			if (converterAttr != null)
			{
				isNullable = type.NullableAnnotation == NullableAnnotation.Annotated;
				string converterTypeName = converterAttr.ConstructorArguments[0].Value?.ToString() ?? "?";
				sb.AppendLine($"		{(isFirst ? "" : "else ")}if (propertyName.SequenceEqual(\"{jsonName}\"u8))");
				if (isNullable)
				{
					sb.AppendLine($"""
									if(reader.TokenType is JsonTokenType.Null)
										value.{propertyName} = null;
									else
						""");
				}
				sb.AppendLine($"			value.{propertyName} = new {converterTypeName}().Read(ref reader, typeof({TypeNameOf(type)}), options);");
				isFirst = false;
				continue;
			}
			else if (type is IArrayTypeSymbol || p.GetAttributes().Any(i => i.AttributeClass?.ToDisplayString() == JsonDefaultSerializerAttrName))
			{
				sb.AppendLine($"		{(isFirst ? "" : "else ")}if (propertyName.SequenceEqual(\"{jsonName}\"u8))");
				sb.AppendLine($"			value.{propertyName} = JsonSerializer.Deserialize<{TypeNameOf(type)}>(ref reader, options) ?? {(type is IArrayTypeSymbol ? "[]" : "new()")};");
				isFirst = false;
				continue;
			}
			else if (timeAttr != null)
			{
				string timeType = timeAttr.ConstructorArguments.Length > 0 ? timeAttr.ConstructorArguments[0].Value?.ToString() ?? "" : "";
				sb.AppendLine($"		{(isFirst ? "" : "else ")}if (propertyName.SequenceEqual(\"{jsonName}\"u8))");
				switch (timeType)
				{
					case "seconds":
						sb.AppendLine($"			value.{propertyName} = TimeSpan.FromSeconds(reader.GetDouble());");
						break;
					case "milliseconds":
						sb.AppendLine($"			value.{propertyName} = TimeSpan.FromMilliseconds(reader.GetDouble());");
						break;
				}
				isFirst = false;
				continue;
			}
			else
			{
				if (type.NullableAnnotation == NullableAnnotation.Annotated)
				{
					name2 = propertyName + "?";
					if (((INamedTypeSymbol)type).TypeArguments.Length > 0)
					{
						type2 = ((INamedTypeSymbol)type).TypeArguments[0];
						sb.AppendLine($"// Found GenericType: {type2.Name}, {type2.MetadataName}");
					}
					isNullable = true;
				}
				if (type2.TypeKind == TypeKind.Enum)
				{
					sb.AppendLine($"		{(isFirst ? "" : "else ")}if (propertyName.SequenceEqual(\"{jsonName}\"u8){(isNullable ? $" && reader.TokenType is not JsonTokenType.Null)" : ")")}");
					sb.AppendLine($$"""
									if(reader.TokenType is JsonTokenType.String && TryParse(reader.ValueSpan, out {{type.ToDisplayString().TrimEnd('?')}} enumValue{{enumIndex}}))
										value.{{propertyName}} = enumValue{{enumIndex}};
									else if(reader.TokenType is JsonTokenType.Number && reader.TryGetInt32(out int intValue{{enumIndex}}))
										value.{{propertyName}} = ({{type.ToDisplayString().TrimEnd('?')}})intValue{{enumIndex}};
									else
										value.{{propertyName}} = default;
						""");
					enumIndex++;
				}
				else
				{
					sb.AppendLine($"		{(isFirst ? "" : "else ")}if (propertyName.SequenceEqual(\"{jsonName}\"u8))");
					if (isNullable)
					{
						sb.AppendLine($"""
										if(reader.TokenType is JsonTokenType.Null)
											value.{propertyName} = null;
										else
							""");
					}

					switch (type2.SpecialType)
					{
						case SpecialType.System_Byte or
							SpecialType.System_Int16 or
							SpecialType.System_Int32 or
							SpecialType.System_Int64 or
							SpecialType.System_UInt16 or
							SpecialType.System_UInt32 or
							SpecialType.System_UInt64 or
							SpecialType.System_Single or
							SpecialType.System_Double or
							SpecialType.System_Decimal:
							sb.AppendLine($"			value.{propertyName} = reader.Get{type2.SpecialType.ToString().Replace("System_", "")}();");
							break;
						case SpecialType.System_Boolean:
							sb.AppendLine($"""
											if (reader.TokenType is JsonTokenType.True or JsonTokenType.False)
												value.{propertyName} = reader.GetBoolean();
											else if (reader.TokenType is JsonTokenType.String)
												value.{propertyName} = "Enabled" == reader.GetString();
											else
												value.{propertyName} = false;
								""");
							break;
						case SpecialType.System_String:
							sb.AppendLine($"			value.{propertyName} = reader.GetString() ?? \"\";");
							break;
						default:
							sb.AppendLine($"			value.{propertyName} = JsonSerializer.Deserialize<{TypeNameOf(type)}>(ref reader, options){(
								type.Name.Contains("List") ? " ?? []" :
								 "")};");
							break;
					}
				}
				isFirst = false;
			}
		}
		sb.AppendLine("		else");
		sb.AppendLine("			return false;");
		sb.AppendLine("		return true;");
	}
	// 分离的 Write 代码生成
	private static void AppendEventWriteBody(StringBuilder sb, List<IPropertySymbol> properties, INamedTypeSymbol e)
	{
		foreach (IPropertySymbol? p in properties)
		{
			string propertyName = p.Name;
			string jsonName = p.Name;
			ITypeSymbol type = p.Type;
			if (p.GetAttributes().Any(i => i.AttributeClass?.ToDisplayString() == JsonIgnoreAttrName))
				continue;
			if (p.GetMethod == null && !p.GetAttributes().Any(i => i.AttributeClass?.ToDisplayString() == JsonNotIgnoreAttrName))
				continue;
			AttributeData? nameAttr = p.GetAttributes().FirstOrDefault(i => i.AttributeClass?.ToDisplayString() == JsonAliasAttrName);
			AttributeData? timeAttr = p.GetAttributes().FirstOrDefault(i => i.AttributeClass?.ToDisplayString() == JsonTimeAttrName);
			AttributeData? converterAttr = p.GetAttributes().FirstOrDefault(i => i.AttributeClass?.ToDisplayString() == JsonConverterAttrName);
			if (nameAttr is not null)
			{
				ImmutableArray<TypedConstant> args = nameAttr.ConstructorArguments;
				if (args != null)
					foreach (TypedConstant arg in args)
						jsonName = args[0].Value?.ToString() ?? propertyName;
			}
			else
			{
				jsonName = ToLowerCamelCase(LastPartOf(propertyName));
			}

			AttributeData? conditionAttr = p.GetAttributes().FirstOrDefault(i => i.AttributeClass?.ToDisplayString() == JsonConditionAttrName);
			string condition = "";
			if (conditionAttr is not null)
			{
				ImmutableArray<TypedConstant> args = conditionAttr.ConstructorArguments;
				if (args != null)
					foreach (TypedConstant arg in args)
						condition = args[0].Value?.ToString() ?? "";
			}

			string name2 = propertyName;
			ITypeSymbol type2 = type;
			bool hasCondition = !string.IsNullOrEmpty(condition);
			bool isNullable = false;
			if (hasCondition)
			{
				sb.AppendLine($"		if ({condition})".Replace("$&", "value").Replace("\n", "\n\t\t\t"));
				sb.Append('	');
			}
			if (converterAttr != null)
			{
				if (type.NullableAnnotation == NullableAnnotation.Annotated)
					isNullable = true;
				string converterTypeName = converterAttr.ConstructorArguments[0].Value?.ToString() ?? "?";
				if (hasCondition)
					sb.Append("		{\n	");
				sb.AppendLine($"		writer.WritePropertyName(\"{jsonName}\"u8);");
				if (isNullable)
				{
					sb.AppendLine($"		if (value.{propertyName} is null)");
					sb.AppendLine("		{");
					sb.AppendLine("			writer.WriteNullValue();");
					sb.AppendLine("		}");
					sb.AppendLine("		else");
					sb.AppendLine("		{");
					sb.Append("		");
				}
				sb.AppendLine($"		new {converterTypeName}().Write(writer, value.{propertyName}{(isNullable ? ".Value" : "")}, options);");
				if (isNullable)
					sb.AppendLine("		}");
				if (hasCondition)
					sb.AppendLine("		}");
				continue;
			}
			else if (type is IArrayTypeSymbol || p.GetAttributes().Any(i => i.AttributeClass?.ToDisplayString() == JsonDefaultSerializerAttrName))
			{
				if (hasCondition)
					sb.Append("		{\n	");
				sb.AppendLine($"		writer.WritePropertyName(\"{jsonName}\"u8);");
				if (hasCondition)
					sb.Append("	");
				sb.AppendLine($"		JsonSerializer.Serialize(writer, value.{propertyName}, options);");
				if (hasCondition)
					sb.AppendLine("		}");
				continue;
			}
			else if (timeAttr != null)
			{
				string timeType = timeAttr.ConstructorArguments.Length > 0 ? timeAttr.ConstructorArguments[0].Value?.ToString() ?? "" : "";
				if (hasCondition)
					sb.Append("		{\n	");
				switch (timeType)
				{
					case "seconds":
						sb.AppendLine($"		writer.WriteNumber(\"{jsonName}\"u8, value.{propertyName}.TotalSeconds);");
						break;
					case "milliseconds":
						sb.AppendLine($"		writer.WriteNumber(\"{jsonName}\"u8, value.{propertyName}.TotalMilliseconds);");
						break;
				}
				if (hasCondition)
					sb.AppendLine("		}");
				continue;
			}
			else if (type.NullableAnnotation == NullableAnnotation.Annotated)
			{

				name2 = propertyName + "?";
				if (((INamedTypeSymbol)type).TypeArguments.Length > 0)
				{
					type2 = ((INamedTypeSymbol)type).TypeArguments[0];
					sb.AppendLine($"// Found GenericType: {type2.Name}, {type2.MetadataName}");
				}
				isNullable = true;
			}
			if (type.TypeKind == TypeKind.Enum)
			{
				sb.AppendLine($"		writer.WriteString(\"{jsonName}\"u8, value.{propertyName}.ToEnumString());");
			}
			else
			{
				switch (type.SpecialType)
				{
					case SpecialType.System_Boolean:
						sb.AppendLine($"		writer.WriteBoolean(\"{jsonName}\"u8, value.{LastPartOf(propertyName)});");
						break;
					case SpecialType.System_String:
						sb.AppendLine($"		writer.WriteString(\"{jsonName}\"u8, value.{LastPartOf(propertyName)});");
						break;
					case SpecialType.System_Byte or
						SpecialType.System_Int16 or
						SpecialType.System_Int32 or
						SpecialType.System_Int64 or
						SpecialType.System_UInt16 or
						SpecialType.System_UInt32 or
						SpecialType.System_UInt64 or
						SpecialType.System_Single or
						SpecialType.System_Double or
						SpecialType.System_Decimal:
						sb.AppendLine($"		writer.WriteNumber(\"{jsonName}\"u8, value.{LastPartOf(propertyName)});");
						break;
					default:
						if (hasCondition)
							sb.Append("		{\n	");
						sb.AppendLine($"		writer.WritePropertyName(\"{jsonName}\"u8);");
						if (hasCondition)
							sb.Append("	");
						sb.AppendLine($"		JsonSerializer.Serialize(writer, value.{propertyName}, options);");
						if (hasCondition)
							sb.AppendLine("		}");
						break;
				}
			}
		}
	}
}

