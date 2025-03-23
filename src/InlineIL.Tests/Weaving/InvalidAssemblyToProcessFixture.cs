﻿using System.Linq;
using Fody;
using InlineIL.Tests.InvalidAssemblyToProcess;
using InlineIL.Tests.Support;
using Mono.Cecil;

namespace InlineIL.Tests.Weaving;

public static class InvalidAssemblyToProcessFixture
{
    public static TestResult TestResult { get; }

    public static ModuleDefinition OriginalModule { get; }
    public static ModuleDefinition ResultModule { get; }

    static InvalidAssemblyToProcessFixture()
    {
        (TestResult, OriginalModule, ResultModule) = WeaverRunner.ExecuteTestRun(
            typeof(InvalidAssemblyToProcessReference).Assembly,
            new TestModuleWeaver(),
            false
        );
    }

    public static string ShouldHaveError(string className, string methodName, bool sequencePointRequired)
    {
        var expectedMessagePart = $" {className}::{methodName}(";
        var errorMessage = TestResult.Errors.SingleOrDefault(err => err.Text.Contains(expectedMessagePart));
        errorMessage.ShouldNotBeNull();

        if (sequencePointRequired)
            errorMessage.SequencePoint.ShouldNotBeNull();

        return errorMessage.Text;
    }

    public static void ShouldHaveErrorInType(string className, string nestedTypeName)
    {
        var expectedMessagePart = $" {className}/{nestedTypeName}";
        TestResult.Errors.ShouldAny(err => err.Text.Contains(expectedMessagePart));
    }
}
