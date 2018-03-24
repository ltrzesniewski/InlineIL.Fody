using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

namespace InlineIL
{
    /// <summary>
    /// Represents a stand-alone method signature,
    /// for use with <see cref="IL.Emit(System.Reflection.Emit.OpCode, StandAloneMethodSig)" /> to emit a calli instruction.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public sealed class StandAloneMethodSig
    {
        /// <summary>
        /// Constructs an unmanaged method signature for the calli opcode.
        /// </summary>
        /// <param name="callingConvention">The unmanaged calling convention.</param>
        /// <param name="returnType">The method return type.</param>
        /// <param name="parameterTypes">The method parameter types.</param>
        public StandAloneMethodSig(CallingConvention callingConvention, TypeRef returnType, params TypeRef[] parameterTypes)
        {
        }

        /// <summary>
        /// Constructs a managed method signature for the calli opcode.
        /// </summary>
        /// <param name="callingConvention">The managed calling convention.</param>
        /// <param name="returnType">The method return type.</param>
        /// <param name="parameterTypes">The method parameter types.</param>
        public StandAloneMethodSig(CallingConventions callingConvention, TypeRef returnType, params TypeRef[] parameterTypes)
        {
        }

        /// <summary>
        /// Specifies the optional parameter types for a managed varargs method call.
        /// </summary>
        /// <param name="optionalParameterTypes">The optional parameter types.</param>
        /// <returns>A reference to a varargs method call signature.</returns>
        public StandAloneMethodSig WithOptionalParameters(params TypeRef[] optionalParameterTypes)
            => throw IL.Throw();
    }
}
