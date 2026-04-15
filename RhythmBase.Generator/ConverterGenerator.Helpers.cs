using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RhythmBase.Generator;

public partial class ConverterGenerator
{
	private static bool InheritsOrImplements(INamedTypeSymbol type, INamedTypeSymbol target)
	{
		static IEnumerable<INamedTypeSymbol> GetBaseTypes(INamedTypeSymbol type)
		{
			for (var current = type.BaseType;
				current != null && current.SpecialType != SpecialType.System_Object;
				current = current.BaseType)
			{
				yield return current;
			}
		}

		if (SymbolEqualityComparer.Default.Equals(type, target))
			return true;

		return target.TypeKind is TypeKind.Interface
			? type.AllInterfaces.Any(i => SymbolEqualityComparer.Default.Equals(i, target))
			: GetBaseTypes(type).Any(i => SymbolEqualityComparer.Default.Equals(i, target));
    }

}