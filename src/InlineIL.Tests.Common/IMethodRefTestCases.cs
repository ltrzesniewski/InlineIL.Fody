using System;
using System.Diagnostics.CodeAnalysis;

namespace InlineIL.Tests.Common;

[SuppressMessage("ReSharper", "UnusedParameter.Global")]
public interface IMethodRefTestCases
{
    event Action Event;
    void AddEvent(Action callback);
    void RemoveEvent(Action callback);
    void RaiseEvent();
}
