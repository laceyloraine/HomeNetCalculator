
using System;
using System.Collections.Generic;

namespace Arithmetic
{
	class Program
	{
		static void Main(string[] args)
		{
            ICalculator calculator = new Calculator();

			Dictionary<string, decimal> expressionsWithExpectedResults = new Dictionary<string, decimal>
				{
					{"1", 1},

					{"1 + 1", 2},
					{"1 - 1", 0},
					{"1 * 1", 1},
					{"1 / 1", 1},

					{"3 + 4 + 5", 12},
					{"3 + 4 - 5", 2},
					{"3 + 4 + -5", 2},
					{"3 + 4 * 5", 23},
					{"3 * 4 / 5", 2.4m},

					{"3 + 4 + 5 * 0", 7},
					{"0 * 3 + 4 - 5", -1},
					
					{"1 + 1 - 1 * 1 / 1", 1},
					{"1 / 1 * 1 - 1 + 1", 1},

					{"1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1 + 1", 23},
				};

            
			ConsoleColor originalForegroundColor = Console.ForegroundColor;

			bool happy = true;
			foreach (string expression in expressionsWithExpectedResults.Keys)
			{
				decimal actualResult = calculator.Calculate(expression);

				if (expressionsWithExpectedResults[expression] != actualResult) happy = false;
				string passOrFail       = expressionsWithExpectedResults[expression] == actualResult ? "pass"             : "fail";
				Console.ForegroundColor = expressionsWithExpectedResults[expression] == actualResult ? ConsoleColor.Green : ConsoleColor.Red;
				
				Console.WriteLine("{0} => expected {1}; actual {2}; {3}", expression, expressionsWithExpectedResults[expression], actualResult, passOrFail);				
			}

			Console.WriteLine();
			Console.ForegroundColor = happy ? ConsoleColor.Green : ConsoleColor.Red;
			Console.WriteLine(happy ? "All tests passed.  Good job!" : "There is at least one failed test.");
			Console.ForegroundColor = originalForegroundColor;
			Console.WriteLine("Press enter to exit...");
			Console.ReadLine();
		}    
	}


    
    class Calculator : ICalculator
    {
        
        decimal ICalculator.Calculate(string expression)
        {
            //remove spaces from expression string for easier parsing
            expression = expression.Replace(" ", string.Empty);

            //pass the whitespace-free expression to a recursive function
            return evaluate(expression);
        }


        private decimal evaluate(string expression)
        {

            //if the expression string has been simplified to a decimal, the result is that decimal
            decimal result;
            if (Decimal.TryParse(expression, out result))
            {}
            else
            {
                //use order of operations to break the expression down into parts that are recursively evaluated
                if (expression.Contains("+"))
                {
                    string[] parts = expression.Split('+');
                    result = 0;
                    foreach (string expr in parts)
                    {
                        result = result + evaluate(expr);
                    }
                }
                else if (expression.Contains("-"))
                {
                    string[] parts = expression.Split('-');
                    result = evaluate(parts[0]);
                    for( int i = 1; i < parts.Length; i++ )
                    {
                        result = result - evaluate(parts[i]);
                    }
                }
                else if (expression.Contains("*"))
                {
                    string[] parts = expression.Split('*');
                    result = 1;
                    foreach (string expr in parts)
                    {
                        result = result * evaluate(expr);
                    }
                }
                else if (expression.Contains("/"))
                {
                    string[] parts = expression.Split('/');
                    result = evaluate(parts[0]);
                    for( int i = 1; i < parts.Length; i++)
                    {
                        result = result / evaluate(parts[i]);
                    }
                }
               
            }
            return result;
        }
    }
}



