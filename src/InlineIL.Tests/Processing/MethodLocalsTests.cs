using System;
using System.Collections.Generic;
using InlineIL.Fody.Model;
using InlineIL.Fody.Processing;
using InlineIL.Tests.Support;
using Mono.Cecil;
using Xunit;

namespace InlineIL.Tests.Processing;

public class MethodLocalsTests : IDisposable
{
    private readonly TestLogger _logger = new();
    private readonly ModuleDefinition _module = ModuleDefinition.CreateModule("test", ModuleKind.Dll);

    public void Dispose()
        => _module.Dispose();

    [Fact]
    public void should_log_warning_for_unused_unnamed_locals()
    {
        var methodLocals = CreateLocals(new LocalVarBuilder(_module.TypeSystem.Int32));
        methodLocals.PostProcess();
        _logger.LoggedWarnings.ShouldContain(i => i.message == "Unused local at index 0");
    }

    [Fact]
    public void should_not_log_warning_for_used_unnamed_locals()
    {
        var methodLocals = CreateLocals(new LocalVarBuilder(_module.TypeSystem.Int32));
        methodLocals.GetLocalByIndex(0);
        methodLocals.PostProcess();
        _logger.LoggedWarnings.ShouldBeEmpty();
    }

    [Fact]
    public void should_log_warning_for_unused_named_locals()
    {
        var methodLocals = CreateLocals(new LocalVarBuilder(_module.TypeSystem.Int32, "foo"));
        methodLocals.PostProcess();
        _logger.LoggedWarnings.ShouldContain(i => i.message == "Unused local: 'foo'");
    }

    [Fact]
    public void should_not_log_warning_for_used_named_locals()
    {
        var methodLocals = CreateLocals(new LocalVarBuilder(_module.TypeSystem.Int32, "foo"));
        methodLocals.GetLocalByName("foo");
        methodLocals.PostProcess();
        _logger.LoggedWarnings.ShouldBeEmpty();
    }

    private MethodLocals CreateLocals(params IEnumerable<LocalVarBuilder> localVarBuilders)
    {
        var methodLocals = new MethodLocals(new MethodDefinition("test", default, _module.TypeSystem.Void), _logger);
        methodLocals.DeclareLocals(localVarBuilders, null);
        return methodLocals;
    }
}
