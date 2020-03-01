using InlineIL.Fody.Support;
using InlineIL.Tests.Support;
using Mono.Cecil.Cil;
using Xunit;

namespace InlineIL.Tests
{
    public class WeaverLoggerTests
    {
        private readonly TestLogger _log = new TestLogger();

        [Fact]
        public void should_log_debug_and_info()
        {
            var log = new WeaverLogger(_log, new WeaverConfigOptions());

            log.Debug("Foo");
            log.Debug("Bar");
            log.Info("Baz");

            _log.LoggedDebug.ShouldEqual(new[] { "Foo", "Bar" });
            _log.LoggedInfos.ShouldEqual(new[] { "Baz" });
            _log.LoggedWarnings.ShouldBeEmpty();
            _log.LoggedErrors.ShouldBeEmpty();
        }

        [Fact]
        public void should_log_warnings_and_errors()
        {
            var log = new WeaverLogger(_log, new WeaverConfigOptions());
            var sequencePoint = new SequencePoint(Instruction.Create(OpCodes.Nop), new Document(""));

            log.Warning("Foo", null);
            log.Warning("Bar", sequencePoint);
            log.Error("Baz", null);
            log.Error("Hello", sequencePoint);

            _log.LoggedDebug.ShouldBeEmpty();
            _log.LoggedInfos.ShouldBeEmpty();
            _log.LoggedWarnings.ShouldEqual(new[]
            {
                ("Foo", null),
                ("Bar", sequencePoint)
            });
            _log.LoggedErrors.ShouldEqual(new[]
            {
                ("Baz", null),
                ("Hello", sequencePoint)
            });
        }

        [Fact]
        public void should_ignore_warnings()
        {
            var log = new WeaverLogger(_log, new WeaverConfigOptions { Warnings = WeaverConfigOptions.WarningsBehavior.Ignore });
            var sequencePoint = new SequencePoint(Instruction.Create(OpCodes.Nop), new Document(""));

            log.Warning("Foo", null);
            log.Warning("Bar", sequencePoint);
            log.Error("Baz", null);
            log.Error("Hello", sequencePoint);

            _log.LoggedDebug.ShouldEqual(new[]
            {
                "Ignored warning: Foo",
                "Ignored warning: Bar"
            });
            _log.LoggedInfos.ShouldBeEmpty();
            _log.LoggedWarnings.ShouldBeEmpty();
            _log.LoggedErrors.ShouldEqual(new[]
            {
                ("Baz", null),
                ("Hello", sequencePoint)
            });
        }

        [Fact]
        public void should_treat_warnings_as_errors()
        {
            var log = new WeaverLogger(_log, new WeaverConfigOptions { Warnings = WeaverConfigOptions.WarningsBehavior.Errors });
            var sequencePoint = new SequencePoint(Instruction.Create(OpCodes.Nop), new Document(""));

            log.Warning("Foo", null);
            log.Warning("Bar", sequencePoint);
            log.Error("Baz", null);
            log.Error("Hello", sequencePoint);

            _log.LoggedDebug.ShouldBeEmpty();
            _log.LoggedInfos.ShouldBeEmpty();
            _log.LoggedWarnings.ShouldBeEmpty();
            _log.LoggedErrors.ShouldEqual(new[]
            {
                ("Warning as error: Foo", null),
                ("Warning as error: Bar", sequencePoint),
                ("Baz", null),
                ("Hello", sequencePoint)
            });
        }
    }
}
