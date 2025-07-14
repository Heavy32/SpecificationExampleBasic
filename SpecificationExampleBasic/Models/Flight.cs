using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecificationExampleBasic.Models
{
    public class Flight : CommonEntity
    {
        public Country CountryFrom { get; set; }
        public Country CountryTo { get; set; }
        public IEnumerable<Passenger> Passengers { get; set; }
        public decimal Coast { get; set; }
    }
}
