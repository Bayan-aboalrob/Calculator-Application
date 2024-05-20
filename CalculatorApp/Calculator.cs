using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    internal class Calculator
    {
        public int add(string numbers)
        {
            int sum = 0;
            if (numbers.Length == 0)
            {
                return sum;
            }
            if(numbers.Length< 3)
            {
                bool IsValidInput = ValidateInput(numbers);
                if (IsValidInput)
                {
                    int[] arrayOfNumbers = InputParsing(numbers);
                    foreach (int number in arrayOfNumbers)
                    {
                        sum += number;
                    }

                }
                else
                {
                    throw new ArgumentException("Invalid Input!");
                }
            }
            else
            {
                throw new Exception("More than two numbers in the string");
            }
            
            return sum;
        }

        private int[] InputParsing(string numbers)
        {
            int[] parsedNumbers= new int[numbers.Length];
            for(int i=0; i<numbers.Length; i++)
            {
                int.TryParse(numbers, out parsedNumbers[i]);

            }
            return parsedNumbers;

        }

        private bool ValidateInput(string numbers)
        {
            if (numbers.StartsWith(",") || numbers.EndsWith(","))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
