# InlineIL.Fody

This is an add-in for [Fody](https://github.com/Fody/Fody) which lets you inject arbitrary IL into your assembly.

:warning: **This is a work in progress, and very much still an experiment at this stage.**

## Example

What you write:

```C#
public void AddAssign(ref int a, int b)
{
    IL.Push(ref a);
    IL.Push(a);
    IL.Push(b);
    IL.Op(OpCodes.Add);
    IL.Op(OpCodes.Stind_I4);
}
```

What gets compiled:

```
.method public hidebysig instance void AddAssign (int32& a, int32 b) cil managed 
{
    .maxstack 8

    ldarg.1
    ldarg.1
    ldind.i4
    ldarg.2
    add
    stind.i4
    ret
}
```
