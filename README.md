# InlineIL.Fody

[![NuGet package](https://img.shields.io/nuget/v/InlineIL.Fody.svg)](https://www.nuget.org/packages/InlineIL.Fody)
[![Build status](https://ci.appveyor.com/api/projects/status/qs6051y6i3228afn/branch/master?svg=true)](https://ci.appveyor.com/project/ltrzesniewski/inlineil-fody/branch/master)
[![GitHub release](https://img.shields.io/github/release/ltrzesniewski/InlineIL.Fody.svg)](https://github.com/ltrzesniewski/InlineIL.Fody/releases)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/ltrzesniewski/InlineIL.Fody/blob/master/LICENSE)

![Icon](https://github.com/ltrzesniewski/InlineIL.Fody/raw/master/icon.png)

This is an add-in for [Fody](https://github.com/Fody/Fody) which lets you inject arbitrary IL into your assembly at compile time.

## Installation

Install the NuGet package [`InlineIL.Fody`](https://www.nuget.org/packages/InlineIL.Fody), and ensure Fody is up to date:

```
PM> Install-Package InlineIL.Fody
PM> Update-Package Fody
```

Add the `<InlineIL />` tag to the [`FodyWeavers.xml`](https://github.com/Fody/Fody#add-fodyweaversxml) file in the root directory of your project. Create the file with the following contents if it doesn't exist:

```XML
<?xml version="1.0" encoding="utf-8" ?>
<Weavers>
  <InlineIL />
</Weavers>
```

See [Fody usage](https://github.com/Fody/Fody#usage) for general guidelines.

## Usage

Call static methods of the [`InlineIL.IL.Emit` class](src/InlineIL/IL.Emit.cs) to emit IL instructions. That's it. :wink:

A few more things which are good to know:

 - The [`InlineIL.IL`](src/InlineIL/IL.cs) class exposes methods for handling labels and local variables, and some utilities (see below).

 - `System.Type` is implicitly convertible to `InlineIL.TypeRef`: when you see a `TypeRef` parameter, you can use the `typeof` keyword.

 - You can add the `using static InlineIL.IL.Emit;` directive to get rid of the `IL.Emit` prefix.

 - You don't *have* to emit instructions in their short form, they will be shortened automatically (*e.g.* `ldarg.0` will be emitted instead of `ldarg 0`).

 - Most types used as operands declare instance methods which change their meaning and can be chained, for instance: `new TypeRef(...).MakeArrayType().MakeByRefType()`.

 - You can combine InlineIL instructions with existing C# code: a given method doesn't have to be *entirely* written in IL. After weaving, the reference to the `InlineIL` assembly is removed from your project.

### Methods

 - `IL.Emit.*`  
   Every method call on the `IL.Emit` class will be replaced by the IL instruction it represents.

 - `IL.DeclareLocals`  
   Declares local variables. Supports changing the [`init`](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.methodbuilder.initlocals) flag and pinned variables. Local variables can be referenced by name or by index.

 - `IL.MarkLabel`  
   Marks a label at a given poisition in the code.
   
 - `IL.Push`  
   A helper method to push a value onto the evaluation stack.

 - `IL.Pop`  
   A helper method to pop a value from the evaluation stack into a local variable.

 - `IL.Unreachable`  
   A method used to mark an unreachable code region, for instance just after a `ret` instruction. Necessary when the compiler's control flow analysis requires a code statement.  
   Usage: `throw IL.Unreachable();`

 - `IL.Return`  
   A helper method to return the value from the top of the evaluation stack.  
   Example usage: `return IL.Return<int>();`

### Types

 - [`TypeRef`](src/InlineIL/TypeRef.cs)  
   A class which represents a type. Note that `System.Type` is implicitly convertible to `TypeRef`, so you can directly write `typeof(int)` for instance where a `TypeRef` parameter is expected.

 - [`MethodRef`](src/InlineIL/MethodRef.cs)  
   A method reference. Exposes a simple constructor for methods without overloads, and a more detailed one for overload disambiguation. Additional static factory methods for referencing underlying methods of properties and events are provided for convenience.

 - [`FieldRef`](src/InlineIL/FieldRef.cs)  
   A field reference.

 - [`StandAloneMethodSig`](src/InlineIL/StandAloneMethodSig.cs)  
   Method signature for use as an operand for the `calli` opcode.

 - [`LocalVar`](src/InlineIL/LocalVar.cs)  
   Declares a local variable (with an optional name), for use with `IL.DeclareLocals`. Implicitly convertible from `System.Type` if you don't want to use named locals.

## Configuration

The `<InlineIL />` element in `FodyWeavers.xml` accepts the following attribute:

 - `SequencePoints="True|False|Debug|Release"`, default value: `Debug`  
   Defines if sequence points should be generated for each emitted IL instruction.

## Examples

- A [reimplementation of the `System.Runtime.CompilerServices.Unsafe` class](src/InlineIL.Examples/Unsafe.cs) using InlineIL is provided as an example (compare to [the original IL code](https://github.com/dotnet/corefx/blob/master/src/System.Runtime.CompilerServices.Unsafe/src/System.Runtime.CompilerServices.Unsafe.il)).

- Unit tests can also serve as examples of API usage. See [verifiable](https://github.com/ltrzesniewski/InlineIL.Fody/tree/master/src/InlineIL.Tests.AssemblyToProcess) and [unverifiable](https://github.com/ltrzesniewski/InlineIL.Fody/tree/master/src/InlineIL.Tests.UnverifiableAssemblyToProcess) test cases.

 - [Simple example](src/InlineIL.Examples/Examples.cs):

    ```C#
    public static void ZeroInit<T>(ref T value)
        where T : struct
    {
        Ldarg(nameof(value));
        Ldc_I4_0();
        Sizeof(typeof(T));
        Unaligned(1);
        Initblk();
    }
    ```

    What gets compiled:

    ```
    .method public hidebysig static 
      void ZeroInit<valuetype .ctor ([mscorlib]System.ValueType) T> (
        !!T& 'value'
      ) cil managed 
    {
      .maxstack 3

      IL_0000: ldarg.0
      IL_0001: ldc.i4.0
      IL_0002: sizeof !!T
      IL_0008: unaligned. 1
      IL_000b: initblk
      IL_000d: ret
    }
    ```
