using System;
using System.IO;
using System.Threading.Tasks;
using StringCalculator.Application;

namespace StringCalculator.Infraestructure
{
    public class CSharpLog : ICSharpLogger
    {
        private string filePath;

        public CSharpLog(string filePath)
        {
            this.filePath = filePath;
        }

        public async Task Write(string input)
        {
            if (!File.Exists(filePath))
            {
                File.CreateText(filePath);
            }
            
            await File.AppendAllTextAsync(filePath, DateTime.UtcNow + " - " + input + "\n");
        }
    }
}