using System;
using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public sealed class TypeRef
    {
        public TypeRef(Type type)
            => IL.Throw();

        public TypeRef(string assemblyName, string typeName)
            => IL.Throw();

        public static implicit operator TypeRef(Type type)
            => new TypeRef(type);

        public TypeRef ToPointer()
            => throw IL.Throw();

        public TypeRef ToReference()
            => throw IL.Throw();

        public TypeRef ToArray()
            => throw IL.Throw();
    }
}
