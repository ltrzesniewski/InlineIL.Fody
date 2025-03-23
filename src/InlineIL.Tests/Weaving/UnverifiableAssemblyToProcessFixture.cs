﻿using Fody;
using InlineIL.Tests.UnverifiableAssemblyToProcess;
using Mono.Cecil;

namespace InlineIL.Tests.Weaving;

public static class UnverifiableAssemblyToProcessFixture
{
    public static TestResult TestResult { get; }

    public static ModuleDefinition OriginalModule { get; }
    public static ModuleDefinition ResultModule { get; }

    static UnverifiableAssemblyToProcessFixture()
    {
        (TestResult, OriginalModule, ResultModule) = WeaverRunner.ExecuteTestRun(
            typeof(UnverifiableAssemblyToProcessReference).Assembly,
            new TestModuleWeaver(),
            false
        );
    }
}
