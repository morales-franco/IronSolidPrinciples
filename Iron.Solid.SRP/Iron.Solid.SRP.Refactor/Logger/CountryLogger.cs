using System;

namespace Iron.Solid.SRP.Refactor.Logger
{
    public class CountryLogger : ILogger
    {
        public void LogError(string message, params object[] arg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message, arg);
            Console.ResetColor();
        }

        public void LogWarning(string message, params object[] arg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message, arg);
            Console.ResetColor();
        }
    }
}
