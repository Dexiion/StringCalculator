using System;
using StringCalculator.Application.Actions;
using StringCalculator.Infraestructure;

namespace StringCalculator.Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            var printReader = new CSharpIcSharpConsole();
            var stringCalculator = new GetStringCalculator(printReader, new CSharpLog("./log.txt"));
            printReader.Write("Introduzca los valores: ");
            stringCalculator.Execute(parseInput(printReader.Read()));
        }

        private static string parseInput(string input)
        {
            return input.Replace("\\n", "\n");
        }
    }
}