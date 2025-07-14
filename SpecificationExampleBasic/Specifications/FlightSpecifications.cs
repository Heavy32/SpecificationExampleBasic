using SpecificationExampleBasic.Models;
using SpecificationExampleBasic.Specifications;

namespace SpecificationExampleBasic.Specifications
{
    public class ExpensiveFlightSpecification : BaseSpecification<Flight>
    {
        public ExpensiveFlightSpecification(decimal minCost = 1000) 
            : base(f => f.Coast >= minCost)
        {
        }
    }

    public class FlightFromCountrySpecification : BaseSpecification<Flight>
    {
        public FlightFromCountrySpecification(string countryName) 
            : base(f => f.CountryFrom.Name == countryName)
        {
        }
    }

    public class FlightToCountrySpecification : BaseSpecification<Flight>
    {
        public FlightToCountrySpecification(string countryName) 
            : base(f => f.CountryTo.Name == countryName)
        {
        }
    }

    public class FlightWithPassengersSpecification : BaseSpecification<Flight>
    {
        public FlightWithPassengersSpecification(int minPassengers = 1) 
            : base(f => f.Passengers.Count() >= minPassengers)
        {
            AddInclude(f => f.Passengers);
        }
    }

    public class FlightByNameSpecification : BaseSpecification<Flight>
    {
        public FlightByNameSpecification(string name) 
            : base(f => f.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
        {
        }
    }

    public class FlightPassengerCountSpecification : BaseSpecification<Flight>
    {
        public FlightPassengerCountSpecification(int exactPassengerCount) 
            : base(f => f.Passengers.Count() == exactPassengerCount)
        {
            AddInclude(f => f.Passengers);
        }

        public FlightPassengerCountSpecification(int minPassengers, int maxPassengers) 
            : base(f => f.Passengers.Count() >= minPassengers && f.Passengers.Count() <= maxPassengers)
        {
            AddInclude(f => f.Passengers);
        }
    }

    public class FlightWithMalePassengersSpecification : BaseSpecification<Flight>
    {
        public FlightWithMalePassengersSpecification(int minMalePassengers = 1) 
            : base(f => f.Passengers.Count(p => p.Gender == Gender.Male) >= minMalePassengers)
        {
            AddInclude(f => f.Passengers);
        }
    }

    public class FlightWithFemalePassengersSpecification : BaseSpecification<Flight>
    {
        public FlightWithFemalePassengersSpecification(int minFemalePassengers = 1) 
            : base(f => f.Passengers.Count(p => p.Gender == Gender.Female) >= minFemalePassengers)
        {
            AddInclude(f => f.Passengers);
        }
    } 
} 