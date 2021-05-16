# InlineIL.Fody

This is an add-in for [Fody](https://github.com/Fody/Fody) which lets you inject arbitrary IL into your assembly at compile time.

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

Call static methods of the [`InlineIL.IL.Emit` class](https://github.com/ltrzesniewski/InlineIL.Fody/blob/master/src/InlineIL/IL.Emit.cs) to emit IL instructions.

See the [GitHub repository](https://github.com/ltrzesniewski/InlineIL.Fody#usage) for more information.
