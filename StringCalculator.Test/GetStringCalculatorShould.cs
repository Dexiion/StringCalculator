using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using StringCalculator.Application;
using StringCalculator.Application.Actions;

namespace StringCalculator.Test
{
    public class GetStringCalculatorShould
    {
        private ICSharpConsole console;
        private ICSharpLogger logger;
        private GetStringCalculator stringCalculator;

        [SetUp]
        public void setUp()
        {
            console = Substitute.For<ICSharpConsole>();
            logger = Substitute.For<ICSharpLogger>();
            stringCalculator = new GetStringCalculator(logger);
        }

        [TestCase("//;\n1;2", "El resultado es: 3")]
        [TestCase("//;\n1;-2", "negatives not allowed: -2")]
        public void read_input_from_user(string input, string output)
        {

            stringCalculator.Execute(input);

            console.Received(1).Write(output);
        }
    }
}