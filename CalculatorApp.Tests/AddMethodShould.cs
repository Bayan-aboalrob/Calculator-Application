using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CalculatorApp;


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
        public void Returns_Argument_Exception(string numbers)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => _sut.add(numbers));

        }
        [Theory]
        [InlineData("700,100,300",1100)]
        [InlineData("100,300",400)]
        [InlineData("100,500,900,1000",2500)]
        public void Returns_Correct_Sum_When_More_Than_Two_Numbers_Inside_A_String(string numbers,int expectedSum)
        {
            //Assert
            Assert.Equal(_sut.add(numbers), expectedSum);

        }
    }
}