# InlineIL.Fody

This is an add-in for [Fody](https://github.com/Fody/Fody) which lets you inject arbitrary IL into your assembly at compile time.

## Installation

- Include the [`Fody`](https://www.nuget.org/packages/Fody) and [`InlineIL.Fody`](https://www.nuget.org/packages/InlineIL.Fody) NuGet packages with a `PrivateAssets="all"` attribute on their `<PackageReference />` items. Installing `Fody` explicitly is needed to enable weaving.

- If you have a `FodyWeavers.xml` file in the root directory of your project, add the `<InlineIL />` tag there.

See [Fody usage](https://github.com/Fody/Home/blob/master/pages/usage.md) for general guidelines, and [Fody configuration](https://github.com/Fody/Home/blob/master/pages/configuration.md) for additional options.

## Usage

Call static methods of the `InlineIL.IL.Emit` class to emit IL instructions. Additional helper methods are available in the `InlineIL.IL` class.

See the [GitHub repository](https://github.com/ltrzesniewski/InlineIL.Fody#usage) for more information.
