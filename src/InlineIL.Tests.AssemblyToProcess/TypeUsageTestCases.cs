using System;
using System.Diagnostics.CodeAnalysis;
using static InlineIL.IL.Emit;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedTypeParameter")]
public static class TypeUsageTestCases
{
    public class SelfReferencingConstraint<T>
        where T : SelfReferencingConstraint<T>
    {
        public SelfReferencingConstraint()
            => Nop();
    }

    public class SelfReferencingConstraintIndirectBase<T>
    {
    }

    public class SelfReferencingConstraintIndirect<T> : SelfReferencingConstraintIndirectBase<T>
        where T : SelfReferencingConstraintIndirectBase<T>
    {
        public SelfReferencingConstraintIndirect()
            => Nop();
    }

    [SelfReferencing]
    public class SelfReferencingAttribute : Attribute
    {
        [SelfReferencing]
        public SelfReferencingAttribute()
            => Nop();
    }
}
