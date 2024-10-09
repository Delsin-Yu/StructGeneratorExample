public enum PropertyHint : long
{
    None = 0,
}

[System.Flags]
public enum PropertyUsageFlags : long
{
    ScriptVariable = 4096,
    Default = 6,
}

public struct Variant
{
    public enum Type : long
    {
        Nil = 0,
        Array = 28,
    }
    
    public static Variant CreateCopyingBorrowed(in godot_variant nativeValueToOwn) => default;
}

public readonly struct PropertyInfo
{
    public Variant.Type Type { get; init; }
    public StringName Name { get; init; }
    public PropertyHint Hint { get; init; }
    public string HintString { get; init; }
    public PropertyUsageFlags Usage { get; init; }
    public StringName? ClassName { get; init; }
    public bool Exported { get; init; }

    public PropertyInfo(
        Variant.Type type,
        StringName name,
        PropertyHint hint,
        string hintString,
        PropertyUsageFlags usage,
        bool exported)
        : this(type, name, hint, hintString, usage, className: null, exported) { }

    public PropertyInfo(
        Variant.Type type,
        StringName name,
        PropertyHint hint,
        string hintString,
        PropertyUsageFlags usage,
        StringName? className,
        bool exported)
    {
        Type = type;
        Name = name;
        Hint = hint;
        HintString = hintString;
        Usage = usage;
        ClassName = className;
        Exported = exported;
    }
}

public struct godot_string_name;

public class List<T>;

public class StringName;

public readonly struct MethodInfo
{
    public StringName Name { get; init; }
    public PropertyInfo ReturnVal { get; init; }
    public MethodFlags Flags { get; init; }
    public int Id { get; init; } = 0;
    public List<PropertyInfo>? Arguments { get; init; }
    public List<Variant>? DefaultArguments { get; init; }

    public MethodInfo(
        StringName name,
        PropertyInfo returnVal,
        MethodFlags flags,
        List<PropertyInfo>? arguments,
        List<Variant>? defaultArguments)
    {
        Name = name;
        ReturnVal = returnVal;
        Flags = flags;
        Arguments = arguments;
        DefaultArguments = defaultArguments;
    }
}

public enum MethodFlags : long
{
    Normal = 1,
    Editor = 2,
    Const = 4,
    Virtual = 8,
    Vararg = 16,
    Static = 32,
    ObjectCore = 64,
    Default = 1,
}

public sealed class GodotSerializationInfo
{
    public void AddProperty(StringName name, Variant value) { }

    public bool TryGetProperty(StringName name, out Variant value)
    {
        value = default;
        return default;
    }
}

public static class VariantUtils
{
    public static T ConvertTo<T>(in godot_variant variant) => default;
    public static godot_variant CreateFrom<T>(in T from) => default;
}

public static class NativeFuncs
{
    public static Error godotsharp_struct_resize(ref godot_struct p_self, int p_new_size) => default;
    public static godot_struct godotsharp_variant_as_struct(in godot_variant p_self) => default;
    public static int godotsharp_struct_add(ref godot_struct p_self, in godot_variant p_item) => default;
    public static void godotsharp_struct_new(out godot_struct r_dest) => r_dest = default;
    public static void godotsharp_variant_new_struct(out godot_variant r_dest, in godot_struct p_arr) { }
}

public ref struct godot_struct
{
    public readonly unsafe godot_variant* Elements;
}

public ref struct godot_variant;

public enum Error : long { }

partial class Main
{
    public static class PropertyName
    {
        public static StringName @_userInfo;
    }
}

public class GD
{
    public static void Print(params object[] args) { }
}