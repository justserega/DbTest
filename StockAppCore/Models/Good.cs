namespace StockAppCore.Models
{
    public class Good
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public int UnitId { get; set; }
        public bool IsDeleted { get; set; }

        // Related models
        public Manufacturer Manufacturer { get; set; }
        public Unit Unit { get; set; }
    }
}
