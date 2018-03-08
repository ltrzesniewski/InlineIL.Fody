using System;
using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public sealed class TypeRef
    {
        public TypeRef(Type type)
            => IL.Throw();

        public TypeRef(string assemblyName, string typeName)
            => IL.Throw();

        public static implicit operator TypeRef(Type type)
            => new TypeRef(type);

        public TypeRef MakePointerType()
            => throw IL.Throw();

        public TypeRef MakeByRefType()
            => throw IL.Throw();

        public TypeRef MakeArrayType()
            => throw IL.Throw();

        public TypeRef MakeArrayType(int rank)
            => throw IL.Throw();

        public TypeRef MakeGenericType(params TypeRef[] typeArguments)
            => throw IL.Throw();
    }
}
