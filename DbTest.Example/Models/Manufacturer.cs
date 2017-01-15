namespace DbTest.Example.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public bool IsDeleted { get; set; }

        // Related models
        public Country Country { get; set; }
    }
}
