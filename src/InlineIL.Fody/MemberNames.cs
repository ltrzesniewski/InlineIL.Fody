namespace InlineIL.Fody
{
    internal static class MemberNames
    {
        public const string IlType = "InlineIL.IL";

        public const string OpNoArgMethod = "System.Void InlineIL.IL::Op(System.Reflection.Emit.OpCode)";
        public const string PushMethod = "System.Void InlineIL.IL::Push(!!0)";
        public const string UnreachableMethod = "System.Exception InlineIL.IL::Unreachable()";
    }
}
