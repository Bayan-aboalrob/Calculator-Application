using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CalculatorApp;
using FluentAssertions;
namespace CalculatorApp.Tests
{
    public class AddMethodShould
    {
        private Calculator _sut;
        public AddMethodShould()
        {
            _sut = new Calculator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Return_Zero_When_Empty_Or_Null_String(string numbers)
        {
            //Act
            int actualSum=_sut.add(numbers);
            //Assert
            Assert.Equal(actualSum, 0);

        }
        [InlineData("1", 1)]
        [InlineData("100,200", 300)]
        [InlineData("", 0)]
        public void Returns_Correct_Sum_When_Valid_Input_Passed(string numbers, int expectedSum)
        {
            //Assert
            Assert.Equal(_sut.add(numbers), expectedSum);
        }
        [Theory]
        [InlineData(",700")]
        [InlineData(",300,")]
        [InlineData("100,")]
        [InlineData("\n700")]
        [InlineData("100\n,300,")]
        [InlineData("100,300\n")]
        public void Returns_Argument_Exception(string numbers)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => _sut.add(numbers));

        }
        [InlineData("700,100,300", 1100)]
        [InlineData("100,300", 400)]
        [InlineData("100,500,900,1000", 2500)]
        public void Returns_Correct_Sum_When_More_Than_Two_Numbers_Inside_A_String(string numbers, int expectedSum)
        {
            //Assert
            Assert.Equal(_sut.add(numbers), expectedSum);

        }
        [Theory]
        [InlineData("700\n100,300",1100)]
        [InlineData("700\n100,300", 1100)]
        [InlineData("700\n100,300", 1100)]
        [InlineData("100,300", 400)]
        [InlineData("100\n500\n900,1000", 2500)]
        public void Handle_different_delimiters(string numbers, int expectedSum)
        {
            //Assert
            Assert.Equal(_sut.add(numbers), expectedSum);

        }
        [Theory]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//|\n1|2|3", 6)]
        [InlineData("//,\n1,2,3", 6)]
        [InlineData("//;\n1;2;3", 6)]
        [InlineData("1,2,3", 6)]
        [InlineData("1\n2,3", 6)]
        [InlineData("", 0)]
        public void SupportDifferentDelimiters(string input, int expectedSum)
        {
            var result = _sut.add(input);

            result.Should().Be(expectedSum);
        }
        [Theory]
        [InlineData("//;\n1;-2", new[] { -2 })]
        [InlineData("//|\n1|-2|3", new[] { -2 })]
        [InlineData("//,\n1,-2,3", new[] { -2 })]
        [InlineData("//;\n1;-2;-3", new[] { -2, -3 })]
        [InlineData("1,-2,-3", new[] { -2, -3 })]
        [InlineData("1\n-2,3", new[] { -2 })]
        [InlineData("-1", new[] { -1 })]
        public void Returns_Exception_When_String_Contains_Negative_Numbers(string input, int[] expectedNegatives)
        {
            // Act
            var exception = Assert.Throws<ArgumentException>(() => _sut.add(input));

            // Assert
            exception.Message.Should().Be($"Negatives not allowed: {string.Join(", ", expectedNegatives)}");
        }
        [Theory]
        [InlineData("700\n100,1001", 800)]
        [InlineData("700\n10000,300", 1000)]
        [InlineData("5555,30", 30)]
        [InlineData("//;\n11111;2", 2)]
        [InlineData("//|\n1|2|33333", 3)]
        public void IgnoreNumbersLargerThanOneThousand(string numbers, int expectedSum)
        {
            //Assert
            Assert.Equal(_sut.add(numbers), expectedSum);

        }
    }
}