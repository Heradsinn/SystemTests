using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace SystemTests.Database.RunningModes
{
    public class BackupSwapModeRunner : IBackupSwapModeRunner
    {
        public BackupSwapModeRunner(IConfiguration config, ILogger<DatabaseRegenerationApp> logger, IDatabaseManagementService databaseManagementService)
        {
            _logger = logger;
            _databaseManagementService = databaseManagementService;
            _targetDbName = config["TargetDbName"];
            _backupDbName = _targetDbName + "Backup";
        }

        public void Run()
        {
            _logger.LogInformation("Running Backup-Swap mode...");

            var tempDbName = _targetDbName + "Temp";

            // Killing temporary placeholder DB, if it exists
            if (_databaseManagementService.DatabaseExists(tempDbName))
            {
                _logger.LogDebug("Temp DB should not exist. Needs investigating.");
                try
                {
                    _logger.LogInformation($"Dropping placeholder DB {tempDbName}...");
                    _databaseManagementService.DeleteDatabase(tempDbName);
                }
                catch (Exception)
                {
                    _logger.LogError("Failed to drop placeholder DB");
                    throw;
                }
            }

            // Renaming backup temporarily, if it exists
            if (_databaseManagementService.DatabaseExists(_backupDbName))
            {
                try
                {
                    _logger.LogInformation($"Giving backup DB {_backupDbName} temporary name {tempDbName}...");
                    _databaseManagementService.RenameDatabase(_backupDbName, tempDbName);
                }
                catch (Exception)
                {
                    _logger.LogError("Failed to rename backup DB temporarily");
                    throw;
                }

                _logger.LogInformation($"Successfully renamed backup temporarily to {tempDbName}");
            }

            // Backing up current database, if exists
            if (_databaseManagementService.DatabaseExists(_targetDbName))
            {
                try
                {
                    _logger.LogInformation($"Backing up current DB {_targetDbName} as {_backupDbName}...");
                    _databaseManagementService.RenameDatabase(_targetDbName, _backupDbName);
                }
                catch (Exception)
                {
                    _logger.LogError("Failed to backup current database");
                    throw;
                }

                _logger.LogInformation("Successfully backed up current database");
            }

            // Bringing old backup (temporarily renamed) into scope as current DB
            if (_databaseManagementService.DatabaseExists(tempDbName))
            {
                try
                {
                    _logger.LogInformation($"Bringing old backup {tempDbName} into scope as current DB {_targetDbName}...");
                    _databaseManagementService.RenameDatabase(tempDbName, _targetDbName);
                }
                catch (Exception)
                {
                    _logger.LogError("Failed to bring old backup into scope");
                    throw;
                }

                _logger.LogInformation("Successfully brought old backup into scope");
            }

            _logger.LogInformation("Successfully swapped current and backup databases");
        }

        private readonly ILogger<DatabaseRegenerationApp> _logger;
        private readonly IDatabaseManagementService _databaseManagementService;
        private readonly string _targetDbName;
        private readonly string _backupDbName;
    }
}
