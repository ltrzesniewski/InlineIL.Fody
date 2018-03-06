using System;
using Fody;
using InlineIL.Fody;
using Xunit;

#pragma warning disable 618

namespace InlineIL.Tests
{
    public class ModuleWeaverTests
    {
        private static readonly TestResult _testResult;

        static ModuleWeaverTests()
        {
            var weavingTask = new ModuleWeaver();
            _testResult = weavingTask.ExecuteTestRun("InlineIL.Tests.AssemblyToProcess.dll");
        }

        [Fact]
        public void should_push_value()
        {
            var result = (int)_testResult.GetInstance("BasicClass").MultiplyBy3(42);

            Assert.Equal(42 * 3, result);
        }

        [Fact]
        public void should_push_value_by_ref()
        {
            var a = 42;
            _testResult.GetInstance("BasicClass").AddAssign(ref a, 8);
            Assert.Equal(50, a);
        }

        [Fact]
        public void should_handle_const_operand_int()
        {
            var result = (int)_testResult.GetInstance("BasicClass").TwoPlusTwo();
            Assert.Equal(4, result);
        }

        [Fact]
        public void should_handle_const_operand_float()
        {
            var result = (int)_testResult.GetInstance("BasicClass").TwoPlusTwoFloat();
            Assert.Equal(4.0, result);
        }

        [Fact]
        public void should_handle_const_operand_byte()
        {
            var result = (int)_testResult.GetInstance("BasicClass").TwoPlusTwoByte();
            Assert.Equal(4, result);
        }

        [Fact]
        public void should_handle_const_operand_string()
        {
            var result = (string)_testResult.GetInstance("BasicClass").SayHi();
            Assert.Equal("Hello!", result);
        }

        [Fact]
        public void should_handle_const_operand_on_arg()
        {
            var result = (int)_testResult.GetInstance("BasicClass").ReturnArg(42);
            Assert.Equal(42, result);
        }

        [Fact]
        public void should_handle_type_arg()
        {
            var result = (RuntimeTypeHandle)_testResult.GetInstance("BasicClass").ReturnTypeHandle<Guid>();
            Assert.Equal(typeof(Guid).TypeHandle, result);
        }

        [Fact]
        public void should_handle_type_arg_different_ways()
        {
            var result = (RuntimeTypeHandle[])_testResult.GetInstance("BasicClass").LoadTypeDifferentWays();
            Assert.All(result, handle => Assert.Equal(handle, result[0]));
        }

        [Fact]
        public void should_handle_pointer_types()
        {
            var result = (RuntimeTypeHandle)_testResult.GetInstance("BasicClass").LoadPointerType();
            Assert.Equal(typeof(int**), Type.GetTypeFromHandle(result));
        }

        [Fact]
        public void should_handle_reference_types()
        {
            var result = (RuntimeTypeHandle)_testResult.GetInstance("BasicClass").LoadReferenceType();
            Assert.Equal(typeof(int).MakeByRefType(), Type.GetTypeFromHandle(result));
        }

        [Fact]
        public void should_handle_array_types()
        {
            var result = (RuntimeTypeHandle)_testResult.GetInstance("BasicClass").LoadArrayType();
            Assert.Equal(typeof(int[][]), Type.GetTypeFromHandle(result));
        }
    }
}
