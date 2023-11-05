using System.Collections.Generic;
using System.Linq;
using InlineIL.Fody.Extensions;
using InlineIL.Fody.Processing;
using InlineIL.Tests.Support;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Xunit;

namespace InlineIL.Tests.Processing;

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

    [Fact]
    public void should_merge_basic_blocks()
    {
        var targetA = Instruction.Create(OpCodes.Nop);
        var instructions = new[]
        {
            Instruction.Create(OpCodes.Nop),
            Instruction.Create(OpCodes.Br, targetA),
            Instruction.Create(OpCodes.Nop),
            targetA,
            Instruction.Create(OpCodes.Ret)
        };

        using var module = ModuleDefinition.CreateModule("test", ModuleKind.Dll);
        var il = CreateProcessor(module, instructions);

        il.TryMergeBasicBlocks(instructions[0], instructions[0]).ShouldBeTrue();
        ShouldHaveBasicBlockCount(3);

        il.TryMergeBasicBlocks(instructions[0], instructions[2], instructions[3]).ShouldBeFalse();
        ShouldHaveBasicBlockCount(3);

        il.TryMergeBasicBlocks(instructions[0], instructions[2]).ShouldBeFalse();
        ShouldHaveBasicBlockCount(3);

        il.Remove(instructions[1]);

        il.TryMergeBasicBlocks(instructions[0], instructions[2], instructions[3]).ShouldBeTrue();
        ShouldHaveBasicBlockCount(1);

        var emittedInstruction = Instruction.Create(OpCodes.Nop);
        il.Replace(instructions[0], emittedInstruction);
        instructions[0] = emittedInstruction;
        il.TryMergeBasicBlocks(emittedInstruction, instructions[2]).ShouldBeFalse();
        ShouldHaveBasicBlockCount(2);

        void ShouldHaveBasicBlockCount(int count)
            => instructions.Where(i => i != null).Select(il.GetBasicBlock).Distinct().Count().ShouldEqual(count);
    }

    private static WeaverILProcessor CreateProcessor(ModuleDefinition module, IEnumerable<Instruction> instructions)
    {
        var method = new MethodDefinition("Test", MethodAttributes.Static, module.TypeSystem.Void);
        method.Body.Instructions.AddRange(instructions);
        return new WeaverILProcessor(method, new MethodLocals(method, NoOpLogger.Instance));
    }
}
