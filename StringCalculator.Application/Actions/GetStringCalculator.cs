using System;

namespace StringCalculator.Application.Actions
{
    public class GetStringCalculator
    {
        private readonly ICSharpLogger logPrinter;

        public GetStringCalculator(ICSharpLogger logPrinter)
        {
            this.logPrinter = logPrinter;
        }

        public string Execute(string input)
        {
            try
            {
                var result = StringCalculator.Add(input);
                var log = ParseLog(input, result.ToString());
                logPrinter.Write(log);
                return result.ToString();
            }
            catch (InvalidOperationException e)
            {
                var log = ParseLog(input, e.Message);
                logPrinter.Write(log);
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string ParseLog(string input, string output)
        {
            return input + " = " + output;
        }
    }
}