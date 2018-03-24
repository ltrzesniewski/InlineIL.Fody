# InlineIL.Fody

[![NuGet package](https://img.shields.io/nuget/v/InlineIL.Fody.svg)](https://www.nuget.org/packages/InlineIL.Fody)
[![Build status](https://ci.appveyor.com/api/projects/status/qs6051y6i3228afn/branch/master?svg=true)](https://ci.appveyor.com/project/ltrzesniewski/inlineil-fody/branch/master)
[![GitHub release](https://img.shields.io/github/release/ltrzesniewski/InlineIL.Fody.svg)](https://github.com/ltrzesniewski/InlineIL.Fody/releases)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/ltrzesniewski/InlineIL.Fody/blob/master/LICENSE)

This is an add-in for [Fody](https://github.com/Fody/Fody) which lets you inject arbitrary IL into your assembly at compile time.

## Installation

See [Fody usage](https://github.com/Fody/Fody#usage) for general guidelines.

Install the NuGet package [`InlineIL.Fody`](https://www.nuget.org/packages/InlineIL.Fody), and ensure Fody is up to date:

```
PM> Install-Package InlineIL.Fody
PM> Update-Package Fody
```

Add the `<InlineIL />` tag to the [`FodyWeavers.xml`](https://github.com/Fody/Fody#add-fodyweaversxml) file of your project.

```XML
<?xml version="1.0" encoding="utf-8" ?>
<Weavers>
  <InlineIL />
</Weavers>
```

## Usage

The API is similar to the [`System.Reflection.Emit.ILGenerator`](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.ilgenerator) class, except it works by substituting calls to static methods of the `InlineIL.IL` class at compile time. After weaving, the reference to the `InlineIL` assembly is removed from your project.

### Methods:

 - `IL.Emit`  
   This is the method used to generate IL instructions.

 - `IL.DeclareLocals`  
   Declares local variables. Supports changing the [`init`](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.methodbuilder.initlocals) flag and pinned variables. Local variables can be referenced by name or by index.

 - `IL.MarkLabel`  
   Marks a label at a given poisition in the code.
   
 - `IL.Push`  
   A helper method to push a value onto the evaluation stack.

 - `IL.Unreachable`  
   A method used to mark an unreachable code region, for instance just after a `ret` instruction. Necessary when the compiler's control flow analysis requires a code statement.  
   Usage: `throw IL.Unreachable();`

 - `IL.Return`  
   A helper method to return the value from the top of the evaluation stack.  
   Example usage: `return IL.Return<int>();`

### Types

 - `TypeRef`  
   A class which represents a type. Note that `System.Type` is implicitly convertible to `TypeRef`, so you can directly write `typeof(int)` for instance where a `TypeRef` parameter is expected.

 - `MethodRef`  
   A method reference. Exposes a simple constructor for methods without overloads, and a more detailed one for overload disambiguation.

 - `FieldRef`  
   A field reference.

 - `LocalRef`  
   A reference to a named local variable, which was previously declared with `IL.DeclareLocals`. Note that local variables can also be referenced by index.

 - `LabelRef`  
   A reference to a label, for use as a branching instruction target.

 - `StandAloneMethodSig`  
   Method signature for use as an operand for the `calli` opcode.

## Examples

- A [reimplementation of the `System.Runtime.CompilerServices.Unsafe` class](https://github.com/ltrzesniewski/InlineIL.Fody/blob/master/src/InlineIL.Examples/Unsafe.cs) using InlineIL is provided as an example (compare to [the original IL code](https://github.com/dotnet/corefx/blob/master/src/System.Runtime.CompilerServices.Unsafe/src/System.Runtime.CompilerServices.Unsafe.il)).

- Unit tests can also serve as examples of API usage, which is entirely covered. See [verifiable](https://github.com/ltrzesniewski/InlineIL.Fody/tree/master/src/InlineIL.Tests.AssemblyToProcess) and [unverifiable](https://github.com/ltrzesniewski/InlineIL.Fody/tree/master/src/InlineIL.Tests.UnverifiableAssemblyToProcess) test cases.

 - Simple example:

    ```C#
    public void AddAssign(ref int a, int b)
    {
        IL.Push(ref a);
        IL.Push(a);
        IL.Push(b);
        IL.Emit(OpCodes.Add);
        IL.Emit(OpCodes.Stind_I4);
    }
    ```

    What gets compiled:

    ```
    .method public hidebysig instance void AddAssign (int32& a, int32 b) cil managed 
    {
        .maxstack 3

        ldarg.1
        ldarg.1
        ldind.i4
        ldarg.2
        add
        stind.i4
        ret
    }
    ```
