namespace SystemTests.Database
{
    public interface IDatabaseManagementRepository
    {
        int DeleteDatabase(string databaseName);
        int RenameDatabase(string oldName, string newName);
        bool DatabaseExists(string databaseName);
    }
}
