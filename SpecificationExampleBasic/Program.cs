using SpecificationExampleBasic.Models;
using SpecificationExampleBasic.Specifications;

namespace SpecificationExampleBasic
{
    class Program
    {
        static void Main(string[] args) 
        {
            #region test
            Console.WriteLine("=== Specification Pattern Example ===\n");

            // Начальные данные
            var countries = TestData.GetCountries();
            var passengers = TestData.GetPassengers(countries);
            var flights = TestData.GetFlights(countries, passengers);

            // Создаём репозиторий
            var flightRepository = new BasicRepository<Flight>(flights);
            var passengerRepository = new BasicRepository<Passenger>(passengers);

            Console.WriteLine("1. Basic Specifications:");
            Console.WriteLine("=======================");

            // Example 1: Дорогой полёт
            var expensiveFlights = new ExpensiveFlightSpecification(2000);
            var expensiveFlightResults = flightRepository.GetBySpecification(expensiveFlights);
            Console.WriteLine($"\nExpensive flights (>= $2000): {expensiveFlightResults.Count}");
            foreach (var flight in expensiveFlightResults)
            {
                Console.WriteLine($"  - {flight.Name}: ${flight.Coast}");
            }

            // Example 2: Полёт из определенной страны
            var flightsFromUS = new FlightFromCountrySpecification("United States");
            var usFlights = flightRepository.GetBySpecification(flightsFromUS);
            Console.WriteLine($"\nFlights from United States: {usFlights.Count}");
            foreach (var flight in usFlights)
            {
                Console.WriteLine($"  - {flight.Name}");
            }

            // Example 3: Полёт с определенным количеством пассажиров
            var flightsWithPassengers = new FlightWithPassengersSpecification(2);
            var multiPassengerFlights = flightRepository.GetBySpecification(flightsWithPassengers);
            Console.WriteLine($"\nFlights with 2+ passengers: {multiPassengerFlights.Count}");
            foreach (var flight in multiPassengerFlights)
            {
                Console.WriteLine($"  - {flight.Name}: {flight.Passengers.Count()} passengers");
            }

            Console.WriteLine("\n\n2. Combining Specifications:");
            Console.WriteLine("============================");

            // Example 4: Комбинация с И
            var expensiveFromUS = expensiveFlights.And(flightsFromUS);
            var expensiveUSFlights = flightRepository.GetBySpecification(expensiveFromUS);
            Console.WriteLine($"\nExpensive flights from US: {expensiveUSFlights.Count}");
            foreach (var flight in expensiveUSFlights)
            {
                Console.WriteLine($"  - {flight.Name}: ${flight.Coast}");
            }

            // Example 5: Комбинация с ИЛИ
            var flightsToJapanOrFrance = new FlightToCountrySpecification("Japan")
                .Or(new FlightToCountrySpecification("France"));
            var japanFranceFlights = flightRepository.GetBySpecification(flightsToJapanOrFrance);
            Console.WriteLine($"\nFlights to Japan OR France: {japanFranceFlights.Count}");
            foreach (var flight in japanFranceFlights)
            {
                Console.WriteLine($"  - {flight.Name}: {flight.CountryFrom.Name} → {flight.CountryTo.Name}");
            }

            // Example 6: Несколько подряд "И"
            var complexSpec = new FlightFromCountrySpecification("United Kingdom")
                .And(new ExpensiveFlightSpecification(1000))
                .And(new FlightWithPassengersSpecification(2));
            var complexResults = flightRepository.GetBySpecification(complexSpec);
            Console.WriteLine($"\nComplex query (UK + Expensive + 2+ passengers): {complexResults.Count}");
            foreach (var flight in complexResults)
            {
                Console.WriteLine($"  - {flight.Name}: ${flight.Coast}, {flight.Passengers.Count()} passengers");
            }

            Console.WriteLine("\n\n3. Passenger Specifications:");
            Console.WriteLine("============================");

            // Example 7: Только женщины 
            var femalePassengers = new PassengerByGenderSpecification(Gender.Female);
            var females = passengerRepository.GetBySpecification(femalePassengers);
            Console.WriteLine($"\nFemale passengers: {females.Count}");
            foreach (var passenger in females)
            {
                Console.WriteLine($"  - {passenger.Name} from {passenger.OriginCountry.Name}");
            }

            // Example 8: Пассажиры из определенной страны
            var brazilPassengers = new PassengerFromCountrySpecification("Brazil");
            var brazilians = passengerRepository.GetBySpecification(brazilPassengers);
            Console.WriteLine($"\nPassengers from Brazil: {brazilians.Count}");
            foreach (var passenger in brazilians)
            {
                Console.WriteLine($"  - {passenger.Name} ({passenger.Gender})");
            }

            Console.WriteLine("\n\n4. NOT Specification:");
            Console.WriteLine("=====================");

            // Example 9: Не дорогие полёты
            var notExpensive = expensiveFlights.Not();
            var cheapFlights = flightRepository.GetBySpecification(notExpensive);
            Console.WriteLine($"\nCheap flights (< $2000): {cheapFlights.Count}");
            foreach (var flight in cheapFlights)
            {
                Console.WriteLine($"  - {flight.Name}: ${flight.Coast}");
            }

            Console.WriteLine("\n\n5. Expression Translation:");
            Console.WriteLine("=========================");

            // Example 10: То, во что экспрешены превращаются
            Console.WriteLine("\nExpression for expensive flights:");
            Console.WriteLine($"  Criteria: {expensiveFlights.Criteria}");

            Console.WriteLine("\nExpression for complex specification:");
            Console.WriteLine($"  Criteria: {complexSpec.Criteria}");

            ExpressionExample.DemonstrateParameterReplacement();

            Console.WriteLine("\n\n6. Passenger Count Specifications:");
            Console.WriteLine("==================================");

            // Example 11: Полёты с точным количеством пассажиров (тестирование COUNT)
            var flightsWith2Passengers = new FlightPassengerCountSpecification(2);
            var twoPassengerFlights = flightRepository.GetBySpecification(flightsWith2Passengers);
            Console.WriteLine($"\nFlights with exactly 2 passengers: {twoPassengerFlights.Count}");
            foreach (var flight in twoPassengerFlights)
            {
                Console.WriteLine($"  - {flight.Name}: {flight.Passengers.Count()} passengers");
            }
            #endregion
        }
    }
}
