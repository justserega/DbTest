namespace StockApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PwdHash { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
    }
}
