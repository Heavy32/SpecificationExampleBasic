using SpecificationExampleBasic.Models;
using SpecificationExampleBasic.Specifications;

namespace SpecificationExampleBasic.Specifications
{
    public class PassengerByGenderSpecification : BaseSpecification<Passenger>
    {
        public PassengerByGenderSpecification(Gender gender) 
            : base(p => p.Gender == gender)
        {
        }
    }

    public class PassengerFromCountrySpecification : BaseSpecification<Passenger>
    {
        public PassengerFromCountrySpecification(string countryName) 
            : base(p => p.OriginCountry.Name == countryName)
        {
        }
    }

    public class PassengerByNameSpecification : BaseSpecification<Passenger>
    {
        public PassengerByNameSpecification(string name) 
            : base(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
        {
        }
    }
} 