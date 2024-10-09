This Repo won't compile, it's just a demo showing an approach to create Source Generator on a recently discussed topic.

See the [Generator.cs](https://github.com/Delsin-Yu/StructGeneratorExample/blob/main/StructInteropGenerator/Generator.cs) for implementation.

The generated `StructInterop.generated.cs`:

```csharp
public static class StructInterop
{
    public static unsafe Variant CreateFromUserInfo(UserInfo value)
    {
        NativeFuncs.godotsharp_struct_new(out godot_struct godotStruct);
        NativeFuncs.godotsharp_struct_resize(ref godotStruct, 5);
        NativeFuncs.godotsharp_struct_add(ref godotStruct, VariantUtils.CreateFrom<int>(in value.UserId));
        NativeFuncs.godotsharp_struct_add(ref godotStruct, VariantUtils.CreateFrom<string>(in value.UserName));
        NativeFuncs.godotsharp_struct_add(ref godotStruct, VariantUtils.CreateFrom<string>(in value.UserPassword));
        NativeFuncs.godotsharp_struct_add(ref godotStruct, VariantUtils.CreateFrom<int[]>(in value.UserToken));
        NativeFuncs.godotsharp_struct_add(ref godotStruct, VariantUtils.CreateFrom<double[]>(in value.UserRecord));
        NativeFuncs.godotsharp_variant_new_struct(out godot_variant ret, godotStruct);
        return Variant.CreateCopyingBorrowed(ret);
    }

    public static unsafe UserInfo ConvertToUserInfo(Variant value)
    {
        UserInfo ret = new UserInfo();
        godot_struct godotStruct = NativeFuncs.godotsharp_variant_as_struct(value);
        ret.UserId = VariantUtils.ConvertTo<int>(godotStruct.Elements[0]);
        ret.UserName = VariantUtils.ConvertTo<string>(godotStruct.Elements[0]);
        ret.UserPassword = VariantUtils.ConvertTo<string>(godotStruct.Elements[0]);
        ret.UserToken = VariantUtils.ConvertTo<int[]>(godotStruct.Elements[0]);
        ret.UserRecord = VariantUtils.ConvertTo<double[]>(godotStruct.Elements[0]);
        return ret;
    }

}
```
