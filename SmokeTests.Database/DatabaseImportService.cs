using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Dac;
using System;

namespace SystemTests.Database
{
    public class DatabaseImportService : IDatabaseImportService
    {
        public DatabaseImportService(IConfiguration config, ILogger<DatabaseImportService> logger, IDatabaseManagementService databaseManagementService, IFileManagementService fileManagementService)
        {
            _config = config;
            _logger = logger;
            _databaseManagementService = databaseManagementService;
            _fileManagementService = fileManagementService;
        }

        public void ImportDatabase(string databaseName)
        {
            _logger.LogInformation($"Importing target database with name {databaseName}");

            using (var bacpac = BacPackage.Load(_fileManagementService.GetFilePath(_config["TargetDbFileName"])))
            {
                var dacpacService = new DacServices(_config["TargetDbConnectionString"]);
                dacpacService.ImportBacpac(bacpac, databaseName);
            }

            if (!_databaseManagementService.DatabaseExists(databaseName))
            {
                const string errorMessage = "Database doesn't exist after import.";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }

            _logger.LogInformation("Successfully imported database");
        }

        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly IDatabaseManagementService _databaseManagementService;
        private readonly IFileManagementService _fileManagementService;
    }
}
