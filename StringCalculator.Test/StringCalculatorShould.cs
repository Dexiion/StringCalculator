using System;
using FluentAssertions;
using NUnit.Framework;

namespace StringCalculator.Test {
    public class StringCalculatorShould
    {

        [Test]
        public void return_0_when_string_is_empty()
        {
            // Given
            const string input = "";
            // When
            var result = StringCalculator.Add(input);
            // Then
            result.Should().Be(0);
        }

        [Test]
        public void return_1_when_input_is_1()
        {
            var input = "1";

            var result = StringCalculator.Add(input);

            result.Should().Be(1);
        }

        [Test]
        public void return_a_number_when_input_is_that_number()
        {
            var input = "2";

            var result = StringCalculator.Add(input);

            result.Should().Be(2);
        }

        [Test]
        public void return_3_when_input_is_1_and_2()
        {
            var input = "1,2";

            var result = StringCalculator.Add(input);

            result.Should().Be(3);
        }

        [Test]
        public void return_addition_when_input_is_two_numbers()
        {
            var input = "4,2";

            var result = StringCalculator.Add(input);

            result.Should().Be(6);
        }

        [Test]
        public void return_addition_when_input_is_more_than_two_numbers()
        {
            var input = "4,2,1";

            var result = StringCalculator.Add(input);

            result.Should().Be(7);
        }
        
        [Test]
        public void return_addition_when_separator_is_new_line()
        {
            var input = "4\n2,1";

            var result = StringCalculator.Add(input);

            result.Should().Be(7);
        }

        [Test]
        public void return_addition_when_separator_is_defined_by_input()
        {
            var input = "//;\n1;2";

            var result = StringCalculator.Add(input);

            result.Should().Be(3);
        }
        
        [Test]
        public void throw_exception_when_input_has_negative_number()
        {
            var input = "1,4,-1";

            Action act = () => StringCalculator.Add(input);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("negatives not allowed: -1");
        }

        [Test]
        public void should_ignore_numbers_bigger_than_1000()
        {
            var input = "2,1001";

            var result = StringCalculator.Add(input);

            result.Should().Be(2);
        }
    }
}