using System.Diagnostics;
using Xunit;

namespace InlineIL.Tests.Support
{
    public class DebugTestAttribute : FactAttribute
    {
        private string _skip;

        public override string Skip
        {
            get
            {
                if (_skip != null)
                    return _skip;

                if (!Debugger.IsAttached)
                    return "Debug test";

                return null;
            }
            set => _skip = value;
        }
    }
}
