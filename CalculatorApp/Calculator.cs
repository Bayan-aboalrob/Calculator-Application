using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    public class Calculator
    {
        public int add(string numbers)
        {
            int sum = 0;
            if (string.IsNullOrEmpty(numbers) || numbers.Length == 0)
            {
                return sum;
            }
            bool IsValidInput = ValidateInput(numbers);
            if (IsValidInput)
            {
                string[] splittedInput = DataSplit(numbers);
                if (splittedInput.Length >=1)
                {
                    int[] arrayOfNumbers = InputParsing(splittedInput);
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
                throw new ArgumentException("Invalid Input!");
            }

            return sum;
        }
        private string[] DataSplit(string input)
        {
            string[] splitedInput = input.Split(',');
            return splitedInput;
        }

        private int[] InputParsing(string[] numbers)
        {
            int[] parsedNumbers = new int[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                int.TryParse(numbers[i], out parsedNumbers[i]);

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