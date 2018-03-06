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
        }

        public static class Full
        {
            public const string IlType = "InlineIL.IL";
            public const string TypeReferenceType = "InlineIL.TypeReference";
            public const string MethodReferenceType = "InlineIL.MethodReference";
        }
    }
}
