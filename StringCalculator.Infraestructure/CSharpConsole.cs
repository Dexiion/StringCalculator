using System;

namespace StringCalculator.Infraestructure
{
    public class CSharpIcSharpConsole : ICSharpConsole
    {
        public void Write(string input)
        {
            Console.WriteLine(input);
        }

        public string Read()
        {
            return Console.ReadLine();
        }
    }
}