using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

[Generator(LanguageNames.CSharp)]
public class Generator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context) { }

    public void Execute(GeneratorExecutionContext context)
    {
        var builder = new StringBuilder();
        builder.AppendLine(
            """
            public static class StructInterop
            {
            """
        );

        const string __ = "    ";
        const string ____ = __ + __;

        foreach (var typeSymbol in context.Compilation.GetSymbolsWithName("UserInfo").OfType<ITypeSymbol>())
        {
            var propertySymbols = typeSymbol
                .GetMembers().OfType<IPropertySymbol>()
                .Where(x => !x.IsReadOnly && !x.IsWriteOnly)
                .ToImmutableArray();

            var typeName = typeSymbol.Name;

            var createFromBuilder = new StringBuilder(
                $$"""
                  {{__}}public static unsafe Variant CreateFrom{{typeName}}({{typeSymbol}} value)
                  {{__}}{
                  {{____}}NativeFuncs.godotsharp_struct_new(out godot_struct godotStruct);
                  {{____}}NativeFuncs.godotsharp_struct_resize(ref godotStruct, {{propertySymbols.Length}});

                  """
            );

            var convertToBuilder = new StringBuilder(
                $$"""
                  {{__}}public static unsafe {{typeSymbol}} ConvertTo{{typeName}}(Variant value)
                  {{__}}{
                  {{____}}{{typeName}} ret = new {{typeName}}();
                  {{____}}godot_struct godotStruct = NativeFuncs.godotsharp_variant_as_struct(value);

                  """
            );

            foreach (var propertySymbol in propertySymbols)
            {
                createFromBuilder.AppendLine($"{____}NativeFuncs.godotsharp_struct_add(ref godotStruct, VariantUtils.CreateFrom<{propertySymbol.Type}>(in value.{propertySymbol.Name}));");
                convertToBuilder.AppendLine($"{____}ret.{propertySymbol.Name} = VariantUtils.ConvertTo<{propertySymbol.Type}>(godotStruct.Elements[0]);");
            }

            createFromBuilder.AppendLine(
                $$"""
                  {{____}}NativeFuncs.godotsharp_variant_new_struct(out godot_variant ret, godotStruct);
                  {{____}}return Variant.CreateCopyingBorrowed(ret);
                  {{__}}}
                  """
            );

            convertToBuilder.AppendLine(
                $$"""
                  {{____}}return ret;
                  {{__}}}
                  """
            );

            builder.AppendLine(createFromBuilder.ToString());
            builder.AppendLine(convertToBuilder.ToString());
        }

        builder.AppendLine("}");

        context.AddSource("StructInterop.generated.cs", builder.ToString());
    }
}