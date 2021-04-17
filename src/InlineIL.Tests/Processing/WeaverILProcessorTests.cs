using System.Collections.Generic;
using System.Linq;
using InlineIL.Fody.Extensions;
using InlineIL.Fody.Processing;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Xunit;

namespace InlineIL.Tests.Processing
{
    public class WeaverILProcessorTests
    {
        [Fact]
        public void should_split_instructions_into_basic_blocks()
        {
            var targetA = Instruction.Create(OpCodes.Nop);
            var instructions = new (bool sameBasicBlock, Instruction instruction)[]
            {
                (true, Instruction.Create(OpCodes.Nop)),
                (true, Instruction.Create(OpCodes.Ldarg_0)),
                (true, Instruction.Create(OpCodes.Ldarg_1)),
                (true, Instruction.Create(OpCodes.Beq, targetA)),
                (false, Instruction.Create(OpCodes.Nop)),
                (true, Instruction.Create(OpCodes.Nop)),
                (false, targetA),
                (true, Instruction.Create(OpCodes.Ret)),
                (false, Instruction.Create(OpCodes.Ldnull)),
                (true, Instruction.Create(OpCodes.Throw)),
                (false, Instruction.Create(OpCodes.Nop))
            };

            using var module = ModuleDefinition.CreateModule("test", ModuleKind.Dll);
            var il = CreateProcessor(module, instructions.Select(i => i.instruction));

            for (var i = 0; i < instructions.Length; ++i)
            {
                var isSameBasicBlock = i == 0 || il.GetBasicBlock(instructions[i].instruction) == il.GetBasicBlock(instructions[i - 1].instruction);

                if (instructions[i].sameBasicBlock)
                    Assert.True(isSameBasicBlock, $"Unexpected basic block boundary at index {i}");
                else
                    Assert.False(isSameBasicBlock, $"Expected basic block boundary at index {i}");
            }
        }

        private static WeaverILProcessor CreateProcessor(ModuleDefinition module, IEnumerable<Instruction> instructions)
        {
            var method = new MethodDefinition("Test", MethodAttributes.Static, module.TypeSystem.Void);
            method.Body.Instructions.AddRange(instructions);
            return new WeaverILProcessor(method);
        }
    }
}
