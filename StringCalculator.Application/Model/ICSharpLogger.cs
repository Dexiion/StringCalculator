using System.Threading.Tasks;

namespace StringCalculator.Application
{
    public interface ICSharpLogger
    {
        public Task Write(string input);
    }
}