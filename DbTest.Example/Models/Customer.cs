using System;

namespace DbTest.Example.Models
{
    public enum CustomerType
    {
        Default, Best
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BornDay { get; set; }
        public CustomerType Type { get; set; }
    }
}
