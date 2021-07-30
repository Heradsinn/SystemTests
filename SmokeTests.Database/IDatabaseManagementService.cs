namespace SystemTests.Database
{
    public interface IDatabaseManagementService
    {
        void DeleteDatabase(string databaseName);
        void RenameDatabase(string oldName, string newName);
        bool DatabaseExists(string databaseName);
    }
}
