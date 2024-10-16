using System.ComponentModel.DataAnnotations.Schema;

namespace StockApp.Models
{
    public class TestEntity
    {
        public Guid Id { get; set; }
        public decimal Decimal { get; set; }
        public string Empty { get; set; } = string.Empty;
        public double Double { get; set; }

        [Column(TypeName = "jsonb")]
        public string[] Tags { get; set; } = [];
        public TestEntityType EntityType { get; set; }
        public DateTime Time { get; set; }
        public DateOnly Date { get; set; }

        public TimeOnly TimeOnly { get; set; }
    }

    public enum TestEntityType
    {
        One, Two, Three
    }
}
