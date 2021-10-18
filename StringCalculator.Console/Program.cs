using System;
using StringCalculator.Application.Actions;
using StringCalculator.Infraestructure;

namespace StringCalculator.Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            var stringCalculator = new GetStringCalculator(new CSharpLog("./log.txt"));
            stringCalculator.Execute(parseInput(printReader.Read()));
        }

        private static string parseInput(string input)
        {
            return input.Replace("\\n", "\n");
        }
    }
}