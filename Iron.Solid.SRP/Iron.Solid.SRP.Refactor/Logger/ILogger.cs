using System;
using System.Collections.Generic;
using System.Text;

namespace Iron.Solid.SRP.Refactor.Logger
{
    interface ILogger
    {
        void LogWarning(string message, params object[] arg);
        void LogError(string message, params object[] arg);
    }
}
