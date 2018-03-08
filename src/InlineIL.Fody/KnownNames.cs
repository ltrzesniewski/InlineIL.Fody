namespace InlineIL.Fody
{
    internal static class KnownNames
    {
        public static class Short
        {
            public const string IlType = "IL";

            public const string OpMethod = "Op";
            public const string PushMethod = "Push";
            public const string UnreachableMethod = "Unreachable";
            public const string ReturnMethod = "Return";
            public const string LabelMethod = "Label";
        }

        public static class Full
        {
            public const string IlType = "InlineIL.IL";
            public const string TypeRefType = "InlineIL.TypeRef";
            public const string MethodRefType = "InlineIL.MethodRef";
            public const string LabelRefType = "InlineIL.LabelRef";
        }
    }
}
