using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            string[] delimiters = { "\n", "," };
            if (IsValidInput)
            {
                if (numbers.StartsWith("//"))
                {
                    int EndOfDelimter = numbers.IndexOf("\n");
                    if (EndOfDelimter == -1)
                    {
                        throw new ArgumentException("Invalid input format for custom delimiter.");
                    }
                    string delimiterSection = numbers.Substring(2, EndOfDelimter - 2);
                    string stringnumber = numbers.Substring(EndOfDelimter + 1);
                    delimiters = delimiterSection.ToCharArray().Select(c => c.ToString()).ToArray();

                }
                string[] splittedInput = DataSplit(numbers, delimiters);
                if (splittedInput.Length >= 1)
                {
                    int[] arrayOfNumbers = InputParsing(splittedInput);
                    var negativeNumbers = ContainsNegativeNumbers(arrayOfNumbers);
                    if (negativeNumbers.Any())
                    {
                        throw new ArgumentException("Negatives not allowed: " + string.Join(", ", negativeNumbers));
                    }
                    foreach (int number in arrayOfNumbers)
                    {
                        if (number > 1000)
                            continue;
                        
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
        private string[] DataSplit(string input, string[] delimiters)
        {
            string[] splitedInput = input.Split(delimiters, StringSplitOptions.None);
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
            if (numbers.StartsWith(",") || numbers.EndsWith(",") || numbers.StartsWith("\n") || numbers.EndsWith("\n") || (!numbers.StartsWith("//") && numbers.Contains("\n,")) || !numbers.StartsWith("//") && (numbers.Contains(",\n")))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private List<int> ContainsNegativeNumbers(int[] numbers)
        {
            List<int> negativeNumbers = new List<int>(); ;
            foreach (int num in numbers)
            {
                if (num < 0)
                {
                    negativeNumbers.Add(num);
                }
            }
            return negativeNumbers;
        }
    }
}