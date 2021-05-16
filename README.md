# InlineIL.Fody

[![Build](https://github.com/ltrzesniewski/InlineIL.Fody/workflows/Build/badge.svg)](https://github.com/ltrzesniewski/InlineIL.Fody/actions?query=workflow%3ABuild)
[![NuGet package](https://img.shields.io/nuget/v/InlineIL.Fody.svg?logo=NuGet)](https://www.nuget.org/packages/InlineIL.Fody)
[![GitHub release](https://img.shields.io/github/release/ltrzesniewski/InlineIL.Fody.svg?logo=GitHub)](https://github.com/ltrzesniewski/InlineIL.Fody/releases)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/ltrzesniewski/InlineIL.Fody/blob/master/LICENSE)

![Icon](https://github.com/ltrzesniewski/InlineIL.Fody/raw/master/icon.png)

This is an add-in for [Fody](https://github.com/Fody/Fody) which lets you inject arbitrary IL into your assembly at compile time.

---

 - [Installation](#installation)
 - [Usage](#usage)
 - [Configuration](#configuration)
 - [Examples](#examples)

---

## Installation

- Install the NuGet packages [`Fody`](https://www.nuget.org/packages/Fody) and [`InlineIL.Fody`](https://www.nuget.org/packages/InlineIL.Fody). Installing `Fody` explicitly is needed to enable weaving.

  ```
  PM> Install-Package Fody
  PM> Install-Package InlineIL.Fody
  ```

- Add the `PrivateAssets="all"` metadata attribute to the `<PackageReference />` items of `Fody` and `InlineIL.Fody` in your project file, so they won't be listed as dependencies.

- If you already have a `FodyWeavers.xml` file in the root directory of your project, add the `<InlineIL />` tag there. This file will be created on the first build if it doesn't exist:

  ```XML
  <?xml version="1.0" encoding="utf-8" ?>
  <Weavers>
    <InlineIL />
  </Weavers>
  ```

See [Fody usage](https://github.com/Fody/Home/blob/master/pages/usage.md) for general guidelines, and [Fody Configuration](https://github.com/Fody/Home/blob/master/pages/configuration.md) for additional options.

## Usage

Call static methods of the `InlineIL.IL.Emit` class to emit IL instructions. That's it. :wink:

A few more things which are good to know:

 - The [`InlineIL.IL`](src/InlineIL/IL.cs) class exposes methods for handling labels and local variables, and some utilities (see below).

 - `System.Type` is implicitly convertible to `InlineIL.TypeRef`: when you see a `TypeRef` parameter, you can use the `typeof` keyword in most cases.

 - You can add the `using static InlineIL.IL.Emit;` directive to get rid of the `IL.Emit` prefix.

 - You don't *have* to emit instructions in their short form, they will be shortened automatically (*e.g.* `ldarg.0` will be emitted instead of `ldarg 0`).

 - Most types used as operands declare instance methods which change their meaning and can be chained, for instance: `new TypeRef(...).MakeArrayType().MakeByRefType()`.

 - You can combine InlineIL instructions with existing C# code: a given method doesn't have to be *entirely* written in IL. After weaving, the reference to the `InlineIL` assembly is removed from your project.

### Methods

 - `IL.Emit.*`  
   Every method call on the `IL.Emit` class will be replaced by the IL instruction it represents.  
   Note that all arguments to these methods need to be constructed in place (i.e. the instruction needs to be representable in IL).

 - `IL.DeclareLocals`  
   Declares local variables. Supports changing the [`init`](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.methodbuilder.initlocals) flag and pinned variables. Local variables can be referenced by name or by index.

 - `IL.MarkLabel`  
   Marks a label at a given position in the code.
   
 - `IL.Push`, `IL.PushInRef`, `IL.PushOutRef`  
   Pushes the provided argument onto the evaluation stack. Does not require a constant argument, any expression should work. If an error is raised in optimized builds, try to use `IL.EnsureLocal` to change the layout of the IL emitted by the compiler, so it becomes compatible.

 - `IL.Pop`  
   Pops a value from the evaluation stack into a local variable or static field.

 - `IL.Unreachable`  
   Marks an unreachable code region, for instance just after a `ret` instruction. Necessary when the compiler's control flow analysis requires a code statement.  
   Usage: `throw IL.Unreachable();`

 - `IL.Return`, `IL.ReturnRef`, `IL.ReturnPointer`  
   Helper methods to return the value from the top of the evaluation stack.  
   Example usage: `return IL.Return<int>();`

 - `IL.EnsureLocal`  
   Helper method that forces the compiler to emit an IL local for the supplied local variable, instead of keeping its value on the evaluation stack only. This can let a local variable be used with `IL.Push` in optimized builds, where the IL layout wouldn't allow it otherwise.

### Types

 - [`TypeRef`](src/InlineIL/TypeRef.cs)  
   A class which represents a type. Note that `System.Type` is implicitly convertible to `TypeRef`, so you can directly write `typeof(int)` for instance where a `TypeRef` parameter is expected.

 - [`MethodRef`](src/InlineIL/MethodRef.cs)  
   A method reference. Exposes a simple constructor for methods without overloads, and some more detailed ones for overload disambiguation. Additional static factory methods for referencing underlying methods of properties and events are provided for convenience.  
   Use `TypeRef.TypeGenericParameters[N]` and `TypeRef.MethodGenericParameters[N]` to represent the generic parameter of index `N` in `MethodRef` calls which involve overload resolution.

 - [`FieldRef`](src/InlineIL/FieldRef.cs)  
   A field reference.

 - [`StandAloneMethodSig`](src/InlineIL/StandAloneMethodSig.cs)  
   A method signature for use as an operand to the `calli` instruction.

 - [`LocalVar`](src/InlineIL/LocalVar.cs)  
   Declares a local variable (with an optional name), for use with `IL.DeclareLocals`. Implicitly convertible from `System.Type` if you don't want to use named locals or pinning.

## Configuration

The `<InlineIL />` element in `FodyWeavers.xml` accepts the following attributes:

 - `SequencePoints="True|False|Debug|Release"`, default value: `Debug`  
   Defines if sequence points should be generated for each emitted IL instruction. The default `Debug` value improves the debugging experience in Debug builds without impacting the JIT codegen in Release builds.

- `Warnings="Warnings|Ignore|Errors"`, default value: `Warnings`  
  Defines how warnings should be handled. `Ignore` hides them, while `Errors` turns them into errors.

## Examples

- A [reimplementation of the `System.Runtime.CompilerServices.Unsafe` class](src/InlineIL.Examples/Unsafe.cs) using InlineIL is provided as an example (compare to [the original IL code](https://github.com/dotnet/runtime/blob/master/src/libraries/System.Runtime.CompilerServices.Unsafe/src/System.Runtime.CompilerServices.Unsafe.il)).

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
      .maxstack 8

      IL_0000: ldarg.0
      IL_0001: ldc.i4.0
      IL_0002: sizeof !!T
      IL_0008: unaligned. 1
      IL_000b: initblk
      IL_000d: ret
    }
    ```
