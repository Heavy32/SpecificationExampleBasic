using SpecificationExampleBasic.Models;

namespace SpecificationExampleBasic
{
    public static class TestData
    {
        public static List<Country> GetCountries()
        {
            return new List<Country>
            {
                new Country { Id = 1, Name = "United States" },
                new Country { Id = 2, Name = "Canada" },
                new Country { Id = 3, Name = "United Kingdom" },
                new Country { Id = 4, Name = "France" },
                new Country { Id = 5, Name = "Germany" },
                new Country { Id = 6, Name = "Japan" },
                new Country { Id = 7, Name = "Australia" },
                new Country { Id = 8, Name = "Brazil" }
            };
        }

        public static List<Passenger> GetPassengers(List<Country> countries)
        {
            return new List<Passenger>
            {
                new Passenger { Id = 1, Name = "John Smith", OriginCountry = countries[0], Gender = Gender.Male },
                new Passenger { Id = 2, Name = "Jane Doe", OriginCountry = countries[0], Gender = Gender.Female },
                new Passenger { Id = 3, Name = "Pierre Dubois", OriginCountry = countries[3], Gender = Gender.Male },
                new Passenger { Id = 4, Name = "Maria Garcia", OriginCountry = countries[7], Gender = Gender.Female },
                new Passenger { Id = 5, Name = "Hans Mueller", OriginCountry = countries[4], Gender = Gender.Male },
                new Passenger { Id = 6, Name = "Yuki Tanaka", OriginCountry = countries[5], Gender = Gender.Female },
                new Passenger { Id = 7, Name = "Emma Wilson", OriginCountry = countries[2], Gender = Gender.Female },
                new Passenger { Id = 8, Name = "Carlos Rodriguez", OriginCountry = countries[7], Gender = Gender.Male },
                new Passenger { Id = 9, Name = "Sarah Johnson", OriginCountry = countries[0], Gender = Gender.Female },
                new Passenger { Id = 10, Name = "Michael Brown", OriginCountry = countries[1], Gender = Gender.Male }
            };
        }

        public static List<Flight> GetFlights(List<Country> countries, List<Passenger> passengers)
        {
            return new List<Flight>
            {
                new Flight 
                { 
                    Id = 1, 
                    Name = "New York to London", 
                    CountryFrom = countries[0], 
                    CountryTo = countries[2], 
                    Coast = 1200,
                    Passengers = new List<Passenger> { passengers[0], passengers[1] }
                },
                new Flight 
                { 
                    Id = 2, 
                    Name = "Paris to Tokyo", 
                    CountryFrom = countries[3], 
                    CountryTo = countries[5], 
                    Coast = 2500,
                    Passengers = new List<Passenger> { passengers[2], passengers[5] }
                },
                new Flight 
                { 
                    Id = 3, 
                    Name = "Berlin to Sydney", 
                    CountryFrom = countries[4], 
                    CountryTo = countries[6], 
                    Coast = 3000,
                    Passengers = new List<Passenger> { passengers[4] }
                },
                new Flight 
                { 
                    Id = 4, 
                    Name = "Toronto to Rio", 
                    CountryFrom = countries[1], 
                    CountryTo = countries[7], 
                    Coast = 800,
                    Passengers = new List<Passenger> { passengers[7], passengers[3] }
                },
                new Flight 
                { 
                    Id = 5, 
                    Name = "London to New York", 
                    CountryFrom = countries[2], 
                    CountryTo = countries[0], 
                    Coast = 1100,
                    Passengers = new List<Passenger> { passengers[6], passengers[8], passengers[9] }
                },
                new Flight 
                { 
                    Id = 6, 
                    Name = "Tokyo to Paris", 
                    CountryFrom = countries[5], 
                    CountryTo = countries[3], 
                    Coast = 2400,
                    Passengers = new List<Passenger> { passengers[5] }
                }
            };
        }
    }
} 