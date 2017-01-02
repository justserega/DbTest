namespace DbTest
{
    public interface IConnection
    {
        void Execute(string query);
        void CreateDatabase();
    }
}
