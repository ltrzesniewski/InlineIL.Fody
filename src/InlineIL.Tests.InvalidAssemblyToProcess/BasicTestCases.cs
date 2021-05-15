using System;
using System.Diagnostics.CodeAnalysis;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.InvalidAssemblyToProcess
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    public class BasicTestCases
    {
        private int _intField;

        public void InvalidUnreachable()
        {
            IL.Unreachable();
        }

        public void InvalidReturn()
        {
            IL.Return<int>();
        }

        public void UnusedInstance()
        {
            GC.KeepAlive(typeof(IL));
        }

        public void InvalidPushUsage()
        {
            var guid = Guid.NewGuid();

            IL.Push(42);
            IL.Push(guid);
        }

        public void InvalidPushUsage2()
        {
            // Issue #25

            ref var a = ref GetRefToStruct(-1);
            ref var b = ref GetRefToStruct(0);

            IL.Push(a.foo);
            IL.Push(b.foo);

            Add();
            IL.Pop(out int result);
            a.foo = result;

            static ref (int foo, int bar) GetRefToStruct(int _) => throw new InvalidOperationException();
        }

        public void InvalidPushUsage3()
        {
            var result = new int[4];
            result[0] = 42;

            IL.Push(result);
            IL.Push(1);

            IL.Push(result);
            IL.Push(2);

            result[0] = 42;
        }

        public void InvalidPushUsage4()
        {
            var result = new int[4];
            result[0] = 42;

            IL.Push(result);
            IL.Push(1);

            IL.Push(result);
            IL.Push(2);
        }

        public void NonExistingParameter()
        {
            Ldarg("foo");
        }

        public void PopToField()
        {
            IL.Pop(out _intField);
        }

        public void PopToArray()
        {
            var array = new int[1];
            IL.Pop(out array[0]);
        }

        public void NotSameBasicBlock(bool a)
        {
            Ldc_I4(a ? 42 : 10);
        }

        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public void NotSameBasicBlock2()
        {
            Ldtoken(MethodRef.Constructor(typeof(BasicTestCases)) ?? MethodRef.Constructor(typeof(BasicTestCases)));
        }

        public void NotSameBasicBlockArray(bool a)
        {
            Switch(new string[a ? 1 : 2]);
        }

        public void NotSameBasicBlockArray2(bool a)
        {
            Switch(a ? "foo" : "bar");
        }
    }
}
