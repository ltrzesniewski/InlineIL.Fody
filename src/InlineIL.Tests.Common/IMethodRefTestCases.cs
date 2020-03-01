using System;

namespace InlineIL.Tests.Common
{
    public interface IMethodRefTestCases
    {
        event Action Event;
        void AddEvent(Action callback);
        void RemoveEvent(Action callback);
        void RaiseEvent();
    }
}
