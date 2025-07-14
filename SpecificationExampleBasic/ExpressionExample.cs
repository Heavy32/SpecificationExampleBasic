using SpecificationExampleBasic.Models;
using System.Linq.Expressions;

namespace SpecificationExampleBasic
{
    public class ExpressionExample
    {
        public static void DemonstrateParameterReplacement()
        {
            Console.WriteLine("=== Expression Parameter Replacement Demo ===\n");

            // Original expressions
            Expression<Func<Flight, bool>> expensiveFlights = f => f.Coast >= 1000;
            Expression<Func<Flight, bool>> usFlights = f => f.CountryFrom.Name == "United States";

            Console.WriteLine("Original Expressions:");
            Console.WriteLine($"Expensive: {expensiveFlights}");
            Console.WriteLine($"US Flights: {usFlights}");
            Console.WriteLine();

            // Step 1: Create a new parameter
            var newParameter = Expression.Parameter(typeof(Flight), "x");
            Console.WriteLine($"New parameter: {newParameter}");
            Console.WriteLine();

            // Step 2: Get the bodies of both expressions
            var leftBody = expensiveFlights.Body;
            var rightBody = usFlights.Body;
            Console.WriteLine($"Left body: {leftBody}");
            Console.WriteLine($"Right body: {rightBody}");
            Console.WriteLine();

            // Step 3: Replace parameters in the right expression
            var visitor = new Specifications.ParameterReplacer(usFlights.Parameters[0], newParameter);
            var replacedRightBody = visitor.Visit(rightBody);
            Console.WriteLine($"After parameter replacement:");
            Console.WriteLine($"Left body: {leftBody}");
            Console.WriteLine($"Right body: {replacedRightBody}");
            Console.WriteLine();

            // Step 4: Combine with AND
            var combinedBody = Expression.AndAlso(leftBody, replacedRightBody);
            Console.WriteLine($"Combined body: {combinedBody}");
            Console.WriteLine();

            // Step 5: Create final lambda
            var finalLambda = Expression.Lambda<Func<Flight, bool>>(combinedBody, newParameter);
            Console.WriteLine($"Final combined expression: {finalLambda}");
            Console.WriteLine();

            // Step 6: Test the combined expression
            var testFlight = new Flight 
            { 
                Name = "Test Flight", 
                Coast = 1500, 
                CountryFrom = new Country { Name = "United States" } 
            };

            var compiledFunc = finalLambda.Compile();
            var result = compiledFunc(testFlight);
            Console.WriteLine($"Test result for flight '{testFlight.Name}' (Cost: ${testFlight.Coast}, From: {testFlight.CountryFrom.Name}): {result}");
        }
    }
} 