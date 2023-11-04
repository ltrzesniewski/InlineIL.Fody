// ReSharper disable CheckNamespace

namespace System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
internal sealed class RequiredMemberAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
public sealed class CompilerFeatureRequiredAttribute : Attribute
{
    public string FeatureName { get; }
    public bool IsOptional { get; init; }

    public CompilerFeatureRequiredAttribute(string featureName)
        => FeatureName = featureName;
}

internal static class IsExternalInit
{
}
