using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using InlineIL.Fody.Extensions;
using InlineIL.Tests.Common;
using InlineIL.Tests.Support;
using JetBrains.Annotations;
using Mono.Cecil.Cil;
using Xunit;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving
{
    public class BasicTests : ClassTestsBase
    {
        public BasicTests()
            : base("BasicTestCases")
        {
        }

        [Fact]
        public void should_push_value()
        {
            var result = (int)GetInstance().MultiplyBy3(42);

            result.ShouldEqual(42 * 3);
        }

        [Fact]
        public void should_push_string_value()
        {
            var result = (string)GetInstance().PushValue();

            result.ShouldEqual("Hello");
        }

        [Fact]
        public void should_push_null_string_value()
        {
            var result = (string?)GetInstance().PushNullValue();

            result.ShouldBeNull();
        }

        [Fact]
        public void should_push_ref()
        {
            var i = 0;
            GetInstance().PushRef(ref i);
            i.ShouldEqual(42);
        }

        [Fact]
        [SuppressMessage("ReSharper", "ConvertToConstant.Local")]
        public void should_push_in_ref()
        {
            var i = 0;
            ((IBasicTestCases)GetInstance()).PushInRef(i);
            i.ShouldEqual(42);
        }

        [Fact]
        public void should_push_out_ref()
        {
            GetInstance().PushOutRef(out int i);
            i.ShouldEqual(42);
        }

        [Fact]
        public unsafe void should_push_pointer()
        {
            int i;
            ((IUnverifiableBasicTestCases)GetUnverifiableInstance()).PushPointer(&i);
            i.ShouldEqual(42);
        }

        [Fact]
        public void should_push_value_by_ref()
        {
            var a = 42;
            GetInstance().AddAssign(ref a, 8);
            a.ShouldEqual(50);
        }

        [Fact]
        public void should_handle_const_operand_int()
        {
            var result = (int)GetInstance().TwoPlusTwo();
            result.ShouldEqual(4);
        }

        [Fact]
        public void should_handle_const_operand_float()
        {
            var result = (double)GetInstance().TwoPlusTwoFloat();
            result.ShouldEqual(4.0);
        }

        [Fact]
        public void should_handle_const_operand_byte()
        {
            var result = (int)GetInstance().TwoPlusTwoByte();
            result.ShouldEqual(4);
        }

        [Fact]
        public void should_handle_const_operand_string()
        {
            var result = (string)GetInstance().SayHi();
            result.ShouldEqual("Hello!");
        }

        [Fact]
        public void should_handle_const_operand_on_arg()
        {
            var result = (int)GetInstance().ReturnArg(42);
            result.ShouldEqual(42);
        }

        [Fact]
        public void should_pop_to_locals()
        {
            var result = (int)GetInstance().PopLocals();
            result.ShouldEqual(42);
        }

        [Fact]
        public void should_pop_to_arguments()
        {
            var result = (int)GetInstance().PopArgs(21);
            result.ShouldEqual(42);
        }

        [Fact]
        public void should_pop_to_static_field()
        {
            var result = (int)GetInstance().PopStaticField(21);
            result.ShouldEqual(42);
        }

        [Fact]
        public unsafe void should_pop_pointer_to_local()
        {
            var values = stackalloc int[2] { 24, 42 };
            var result = ((IUnverifiableBasicTestCases)GetUnverifiableInstance()).PopPointerLocal(values, 1);
            result.ShouldEqual(42);
        }

        [Fact]
        public unsafe void should_pop_pointer_to_arg()
        {
            var values = stackalloc int[2] { 24, 42 };
            var result = ((IUnverifiableBasicTestCases)GetUnverifiableInstance()).PopPointerArg(values, 1);
            result.ShouldEqual(42);
        }

        [Fact]
        public unsafe void should_pop_pointer_to_static()
        {
            var values = stackalloc int[2] { 24, 42 };
            var result = ((IUnverifiableBasicTestCases)GetUnverifiableInstance()).PopPointerStatic(values, 1);
            result.ShouldEqual(42);
        }

        [Fact]
        public unsafe void should_pop_void_pointer_to_local()
        {
            var values = stackalloc int[2] { 24, 42 };
            var result = ((IUnverifiableBasicTestCases)GetUnverifiableInstance()).PopVoidPointerLocal(values, 1);
            result.ShouldEqual(42);
        }

        [Fact]
        public unsafe void should_pop_void_pointer_to_arg()
        {
            var values = stackalloc int[2] { 24, 42 };
            var result = ((IUnverifiableBasicTestCases)GetUnverifiableInstance()).PopVoidPointerArg(values, 1);
            result.ShouldEqual(42);
        }

        [Fact]
        public unsafe void should_pop_void_pointer_to_static()
        {
            var values = stackalloc int[2] { 24, 42 };
            var result = ((IUnverifiableBasicTestCases)GetUnverifiableInstance()).PopVoidPointerStatic(values, 1);
            result.ShouldEqual(42);
        }

        [Fact]
        public void should_report_invalid_use_of_Unreachable()
        {
            ShouldHaveError("InvalidUnreachable");
        }

        [Fact]
        public void should_report_invalid_use_of_Return()
        {
            ShouldHaveError("InvalidReturn");
        }

        [Fact]
        public void should_report_unconsumed_reference()
        {
            ShouldHaveError("UnusedInstance");
        }

        [Fact]
        public void should_report_invalid_push_usage()
        {
            if (((InvalidAssemblyToProcessFixture.TestResult.Assembly.GetCustomAttribute<DebuggableAttribute>()?.DebuggingFlags ?? DebuggableAttribute.DebuggingModes.Default) & DebuggableAttribute.DebuggingModes.DisableOptimizations) != 0)
                return; // Inconclusive in debug builds

            ShouldHaveError("InvalidPushUsage").ShouldContain("IL.Push cannot be used in this context");
        }

        [Fact]
        public void should_handle_exception_blocks()
        {
            var result = (int)GetInstance().HandleExceptionBlocks();
            result.ShouldEqual(19);
        }

        [Fact]
        public void should_handle_prefix_instructions_in_debug_mode()
        {
            var guid = Guid.NewGuid();
            GetUnverifiableInstance().HandlePrefixesInDebugMode(ref guid);
            guid.ShouldEqual(Guid.Empty);
        }

        [Fact]
        public void should_process_nested_classes()
        {
            var result = (int)GetInstance().NestedClass();
            result.ShouldEqual(3);
        }

        [Fact]
        public void should_handle_return_with_conversions()
        {
            ((float)GetInstance().ReturnWithConversion1()).ShouldEqual(42.0f);
            ((int?)GetInstance().ReturnWithConversion2()).ShouldEqual(42);
        }

        [Fact]
        public void should_handle_return_ref()
        {
            var array = new int[2];
            var instance = (IUnverifiableBasicTestCases)GetUnverifiableInstance();
            ref var valueRef = ref instance.ReturnRef(array, 1);
            valueRef = 42;
            array[1].ShouldEqual(42);
        }

        [Fact]
        public unsafe void should_handle_return_pointer()
        {
            var array = new[] { 24, 42 };
            fixed (int* _ = &array[0])
            {
                var instance = (IUnverifiableBasicTestCases)GetUnverifiableInstance();
                var valuePtr = instance.ReturnPointer(array, 1);
                (*valuePtr).ShouldEqual(42);
            }
        }

        [Fact]
        public unsafe void should_handle_return_void_pointer()
        {
            var array = new[] { 24, 42 };
            fixed (int* _ = &array[0])
            {
                var instance = (IUnverifiableBasicTestCases)GetUnverifiableInstance();
                var valuePtr = instance.ReturnVoidPointer(array, 1);
                (*(int*)valuePtr).ShouldEqual(42);
            }
        }

        [Fact]
        public void should_handle_return_ref_with_dereference()
        {
            var array = new[] { 24, 42 };
            var value = (int)GetUnverifiableInstance().ReturnRefWithDereference(array, 1);
            value.ShouldEqual(42);
        }

        [Fact]
        public void should_handle_return_ref_with_dereference_and_conversion()
        {
            var array = new[] { 24, 42 };
            var value = (double)GetUnverifiableInstance().ReturnRefWithDereferenceAndConversion(array, 1);
            value.ShouldEqual(42.0);
        }

        [Fact]
        public unsafe void should_handle_return_pointer_with_dereference()
        {
            var array = new[] { 24, 42 };
            fixed (int* _ = &array[0])
            {
                var value = (int)(GetUnverifiableInstance().ReturnPointerWithDereference(array, 1));
                value.ShouldEqual(42);
            }
        }

        [Fact]
        public unsafe void should_handle_return_pointer_with_conversion()
        {
            var array = new[] { 24, 42 };
            fixed (int* _ = &array[0])
            {
                var instance = (IUnverifiableBasicTestCases)GetUnverifiableInstance();
                var valuePtr = instance.ReturnPointerWithConversion(array, 1);
                (*(int*)valuePtr).ShouldEqual(42);
            }
        }

        [Fact]
        public void should_handle_explicit_ret()
        {
            GetInstance().ExplicitRet();
            GetMethodDefinition("ExplicitRet").Body.Instructions.Count(i => i.OpCode == OpCodes.Ret).ShouldEqual(1);
        }

        [Fact]
        public void should_handle_explicit_endfinally()
        {
            GetInstance().ExplicitEndFinally();
            GetMethodDefinition("ExplicitEndFinally").Body.Instructions.Count(i => i.OpCode == OpCodes.Endfinally).ShouldEqual(1);
        }

        [Fact]
        public void should_handle_explicit_leave()
        {
            GetInstance().ExplicitLeave();
            GetMethodDefinition("ExplicitLeave").Body.Instructions.Count(i => i.OpCode == OpCodes.Leave_S).ShouldEqual(2);
        }

        [Fact]
        public void should_remove_leave_after_throw_or_rethrow()
        {
            Assert.Throws<InvalidOperationException>(new Action(() => GetInstance().NoLeaveAfterThrowOrRethrow()));
            GetOriginalMethodDefinition("NoLeaveAfterThrowOrRethrow").Body.Instructions.Count(i => i.OpCode == OpCodes.Leave_S).ShouldEqual(1);
            GetMethodDefinition("NoLeaveAfterThrowOrRethrow").Body.Instructions.Count(i => i.OpCode == OpCodes.Leave_S).ShouldEqual(0);
        }

        [Fact]
        public void should_support_ldarga_s()
        {
            GetInstance().LdargaS(new object());
        }

        [Fact]
        public void should_support_ldc_i4_s()
        {
            var result = (int)(sbyte)GetInstance().LdcI4S();
            result.ShouldEqual(-42);
        }

        [Fact]
        public void should_shorten_instructions()
        {
            var instructions = GetMethodDefinition("ShortenInstructions").Body.Instructions;

            instructions.Where(i => i.OpCode != OpCodes.Pop && i.OpCode != OpCodes.Ret && i.OpCode != OpCodes.Nop)
                        .ShouldAll(i => i.OpCode == OpCodes.Ldarg_1);
        }

        [Fact]
        public void should_report_non_existing_parameter_reference()
        {
            ShouldHaveError("NonExistingParameter").ShouldContain("foo");
        }

        [Fact]
        public void should_report_pop_to_field()
        {
            ShouldHaveError("PopToField").ShouldContain("IL.Pop");
        }

        [Fact]
        public void should_report_pop_to_array()
        {
            ShouldHaveError("PopToArray").ShouldContain("IL.Pop");
        }

        [Fact]
        public void should_add_sequence_points()
        {
            var method = GetMethodDefinition("MultiplyBy3");

            var expectedCount = method.Module.IsDebugBuild() ? 7 : 0;

            method.DebugInformation.SequencePoints.Count.ShouldEqual(expectedCount);
        }

        [Fact]
        public void should_report_not_same_basic_block()
        {
            ShouldHaveError("NotSameBasicBlock").ShouldContain("An unconditional expression was expected");
        }

        [Fact]
        public void should_report_not_same_basic_block_2()
        {
            ShouldHaveError("NotSameBasicBlock2").ShouldContain("An unconditional expression was expected");
        }

        [Fact]
        public void should_report_not_same_basic_block_array()
        {
            ShouldHaveError("NotSameBasicBlockArray").ShouldContain("An unconditional expression was expected");
        }

        [Fact]
        public void should_report_not_same_basic_block_array_2()
        {
            ShouldHaveError("NotSameBasicBlockArray2").ShouldContain("Could not locate call argument");
        }

        [Fact]
        public void should_not_add_reference_to_private_core_lib()
        {
            AssemblyToProcessFixture.ResultModule.AssemblyReferences.ShouldNotContain(i => i.Name == "System.Private.CoreLib");
            StandardAssemblyToProcessFixture.ResultModule.AssemblyReferences.ShouldNotContain(i => i.Name == "System.Private.CoreLib");
            UnverifiableAssemblyToProcessFixture.ResultModule.AssemblyReferences.ShouldNotContain(i => i.Name == "System.Private.CoreLib");
            InvalidAssemblyToProcessFixture.ResultModule.AssemblyReferences.ShouldNotContain(i => i.Name == "System.Private.CoreLib");
        }

        [Fact]
        public void should_remove_compiler_generated_nop_between_emitted_instructions()
        {
            var method = GetMethodDefinition("HandleNop");
            var firstPop = method.Body.Instructions.First(i => i.OpCode == OpCodes.Pop);
            firstPop.Next.OpCode.ShouldEqual(OpCodes.Pop);
        }

        [Fact]
        public void should_not_remove_user_emitted_nop_instructions()
        {
            var method = GetMethodDefinition("HandleNop");
            var firstPop = method.Body.Instructions.First(i => i.OpCode == OpCodes.Ldnull);
            firstPop.Next.OpCode.ShouldEqual(OpCodes.Nop);
        }

        [Fact]
        public void should_preserve_sequence_points_between_emitted_instructions()
        {
            var method = GetMethodDefinition("HandleNop");
            if (!method.Module.IsDebugBuild())
                return;

            var firstPop = method.Body.Instructions.First(i => i.OpCode == OpCodes.Pop);
            var firstPopSequencePoint = method.DebugInformation.GetSequencePoint(firstPop);
            var secondPopSequencePoint = method.DebugInformation.GetSequencePoint(firstPop.Next);

            firstPopSequencePoint.ShouldNotBeNull();
            secondPopSequencePoint.ShouldNotBeNull();
            secondPopSequencePoint.ShouldNotEqual(firstPopSequencePoint);
        }

        [Fact]
        public void should_support_no_opcode_with_byte_arg()
        {
            var method = GetUnverifiableMethodDefinition("NoOpCodeByte");
            var instruction = method.Body.Instructions.ShouldContainSingle(i => i.OpCode == OpCodes.No);
            instruction.Operand.ShouldBe<byte>().ShouldEqual((byte)(0x01 | 0x04));
        }

        [Fact]
        public void should_support_no_opcode_with_enum_arg()
        {
            var method = GetUnverifiableMethodDefinition("NoOpCodeEnum");
            var instruction = method.Body.Instructions.ShouldContainSingle(i => i.OpCode == OpCodes.No);
            instruction.Operand.ShouldBe<byte>().ShouldEqual((byte)(0x01 | 0x02 | 0x04));
        }
    }

    [UsedImplicitly]
    public class BasicTestsStandard : BasicTests
    {
        public BasicTestsStandard()
            => NetStandard = true;
    }
}
