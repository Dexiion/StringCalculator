using System;

namespace StringCalculator.Application.Actions
{
    public class GetStringCalculator
    {
        private readonly ICSharpConsole console;
        private readonly ICSharpLogger logPrinter;

        public GetStringCalculator(ICSharpConsole console, ICSharpLogger logPrinter)
        {
            this.console = console;
            this.logPrinter = logPrinter;
        }

        public void Execute(string input)
        {
            try
            {
                var result = StringCalculator.Add(input);
                var log = ParseLog(input, result.ToString());
                logPrinter.Write(log);
                console.Write("El resultado es: " + result);
            }
            catch (InvalidOperationException e)
            {
                var log = ParseLog(input, e.Message);
                logPrinter.Write(log);
                console.Write(e.Message);
            }
        }

        private string ParseLog(string input, string output)
        {
            return input + " = " + output;
        }
    }
}