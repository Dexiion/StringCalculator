using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using StringCalculator.Infraestructure;

namespace StringCalculator.Test
{
    public class CSharpLogShould
    {
        private CSharpLog logger;
        private string path = "./testLog.txt";

        [SetUp]
        public void setUp()
        {
            logger = new CSharpLog(path);
        }

        [Ignore("")]
        [Test]
        public async Task write_a_given_line()
        {
            var input = "hola";

            logger.Write(input);

            var result = await File.ReadAllTextAsync(path);
            result.Should().Be(DateTime.UtcNow + " - " + "hola");
        }

        [TearDown]
        public void tearDown()
        {
            Task.Delay(1000);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}