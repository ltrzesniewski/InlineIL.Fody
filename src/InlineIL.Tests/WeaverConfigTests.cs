using System.Xml.Linq;
using Fody;
using InlineIL.Fody.Support;
using InlineIL.Tests.Support;
using Xunit;

namespace InlineIL.Tests
{
    public class WeaverConfigOptionsTests
    {
        [Fact]
        public void should_parse_empty_config()
        {
            Parse("<InlineIL />");
        }

        [Fact]
        public void should_throw_on_unknown_attribute()
        {
            AssertInvalid(@"<InlineIL Foo=""Bar""/>");
        }

        [Fact]
        public void should_throw_on_unknown_element()
        {
            AssertInvalid(@"<InlineIL><Foo/></InlineIL>");
        }

        [Fact]
        public void should_parse_SequencePoints()
        {
            var config = Parse(@"<InlineIL SequencePoints=""True""/>");
            config.SequencePoints.ShouldEqual(WeaverConfigOptions.SequencePointsBehavior.True);
        }

        [Fact]
        public void should_throw_on_invalid_enum_value()
        {
            AssertInvalid(@"<InlineIL SequencePoints=""Foo""/>");
        }

        private static WeaverConfigOptions Parse(string xml)
            => new WeaverConfigOptions(XElement.Parse(xml));

        private static void AssertInvalid(string xml)
            => Assert.ThrowsAny<WeavingException>(() => new WeaverConfigOptions(XElement.Parse(xml)));
    }
}
