using System;
using System.Linq;
using System.Reflection;
using InlineIL.Fody;
using Mono.Cecil.Cil;
using Xunit;
using Xunit.Abstractions;

namespace InlineIL.Tests
{
    public class OpCodeMapTests
    {
        private readonly ITestOutputHelper _output;

        public OpCodeMapTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(Skip = "Manual test")]
        public void PrintMap()
        {
            var cecilCodes = typeof(OpCodes)
                             .GetFields(BindingFlags.Public | BindingFlags.Static)
                             .Where(field => field.IsInitOnly && field.FieldType == typeof(OpCode))
                             .Select(field => (OpCode)field.GetValue(null))
                             .ToDictionary(i => i.Value);

            var reflectionEmitCodes = typeof(System.Reflection.Emit.OpCodes)
                                      .GetFields(BindingFlags.Public | BindingFlags.Static)
                                      .Where(field => field.IsInitOnly && field.FieldType == typeof(System.Reflection.Emit.OpCode))
                                      .Select(field => (System.Reflection.Emit.OpCode)field.GetValue(null))
                                      .ToDictionary(i => i.Value);

            var maxCode = Math.Max(cecilCodes.Keys.Max(), reflectionEmitCodes.Keys.Max());

            for (short i = 0; i < maxCode; ++i)
            {
                var reflectionEmitCode = reflectionEmitCodes.TryGetValue(i, out var reflectionEmitOpCode) ? reflectionEmitOpCode.Name : "???";
                var cecilCode = cecilCodes.TryGetValue(i, out var cecilOpCode) ? cecilOpCode.Name : "???";

                _output.WriteLine($"{i:X4}: {reflectionEmitCode,-15} {cecilCode,-15}");
            }
        }

        [Fact]
        public void should_map_opcodes()
        {
            Assert.Equal(OpCodes.Nop, OpCodeMap.FromReflectionEmit(System.Reflection.Emit.OpCodes.Nop));
            Assert.Equal(OpCodes.Dup, OpCodeMap.FromReflectionEmit(System.Reflection.Emit.OpCodes.Dup));
            Assert.Equal(OpCodes.Leave, OpCodeMap.FromReflectionEmit(System.Reflection.Emit.OpCodes.Leave));
            Assert.Equal(OpCodes.Sizeof, OpCodeMap.FromReflectionEmit(System.Reflection.Emit.OpCodes.Sizeof));
        }
    }
}
