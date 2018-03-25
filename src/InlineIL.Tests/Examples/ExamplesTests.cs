using System;
using Xunit;

namespace InlineIL.Tests.Examples
{
    public class ExamplesTests
    {
        [Fact]
        public void InitStruct()
        {
            var item = new MyStruct
            {
                Int = 42,
                Guid = Guid.NewGuid()
            };

            InlineIL.Examples.Examples.ZeroInit(ref item);

            Assert.Equal(0, item.Int);
            Assert.Equal(Guid.Empty, item.Guid);
        }

        private struct MyStruct
        {
            public int Int;
            public Guid Guid;
        }
    }
}
