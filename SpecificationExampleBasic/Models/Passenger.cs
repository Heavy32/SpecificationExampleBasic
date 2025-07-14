namespace SpecificationExampleBasic.Models
{
    public class Passenger : CommonEntity
    {
        public Country OriginCountry { get; set; }
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Female = 0,
        Male = 1
    }
}
