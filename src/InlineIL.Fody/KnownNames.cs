namespace InlineIL.Fody
{
    internal static class KnownNames
    {
        public static class Short
        {
            public const string IlType = "IL";

            public const string Op = "Op";
            public const string PushMethod = "Push";
            public const string UnreachableMethod = "Unreachable";
        }

        public static class Full
        {
            public const string IlType = "InlineIL.IL";
        }
    }
}
