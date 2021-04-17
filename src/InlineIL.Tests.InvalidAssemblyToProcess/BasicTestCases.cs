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
