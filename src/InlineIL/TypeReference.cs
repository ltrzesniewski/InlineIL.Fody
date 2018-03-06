using System;
using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class TypeReference
    {
        public TypeReference(Type type)
            => IL.Throw();

        public TypeReference(string assemblyName, string typeName)
            => IL.Throw();

        public static implicit operator TypeReference(Type type)
            => new TypeReference(type);
    }
}
