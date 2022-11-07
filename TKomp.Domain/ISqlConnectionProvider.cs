namespace TKomp.Domain
{
    public interface ISqlConnectionProvider
    {
        bool Test(string username, string password);
        void SetCredentials(string username, string password);
        string GetConnectionString();
    }
}
