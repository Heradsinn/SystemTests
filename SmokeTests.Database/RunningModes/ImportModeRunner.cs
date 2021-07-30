using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace SystemTests.Database.RunningModes
{
    public class ImportModeRunner : IImportModeRunner
    {

        public ImportModeRunner(IConfiguration config, ILogger<DatabaseRegenerationApp> logger, IDatabaseManagementService databaseManagementService, IDatabaseImportService databaseImportService)
        {
            _logger = logger;
            _databaseManagementService = databaseManagementService;
            _databaseImportService = databaseImportService;
            _targetDbName = config["TargetDbName"];
        }

        public void Run()
        {
            _logger.LogInformation("Running fresh DB import...");
            var temporaryDbName = _targetDbName + "Importing"; // Temporary name to limit access while importing

            // Check if target DB already exists
            if (_databaseManagementService.DatabaseExists(_targetDbName))
            {
                _logger.LogWarning("Target database already exists. Cancelling request to import fresh database...");
                return;
            }

            // Kill placeholder DB, if exists
            if (_databaseManagementService.DatabaseExists(temporaryDbName))
            {
                _logger.LogDebug("Placeholder DB should not exist. Needs investigating.");

                try
                {

                    _logger.LogInformation($"Killing placeholder database used for importing...");
                    _databaseManagementService.DeleteDatabase(temporaryDbName);
                }
                catch (Exception)
                {
                    _logger.LogError("Failed to kill placeholder DB");
                    throw;
                }
            }

            // Import DB
            _logger.LogInformation("Importing database bacpac file...");

            try
            {
                _databaseImportService.ImportDatabase(temporaryDbName);
                _databaseManagementService.RenameDatabase(temporaryDbName, _targetDbName);
            }
            catch (Exception)
            {
                _logger.LogError("Failed to import new DB");
                throw;
            }

            _logger.LogInformation("Successfully imported fresh DB");
        }

        private readonly ILogger<DatabaseRegenerationApp> _logger;
        private readonly IDatabaseManagementService _databaseManagementService;
        private readonly IDatabaseImportService _databaseImportService;
        private readonly string _targetDbName;
    }
}
