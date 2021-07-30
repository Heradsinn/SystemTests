using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using SystemTests.Database.RunningModes;

namespace SystemTests.Database
{
    public class DatabaseRegenerationApp : IDatabaseRegenerationApp
    {
        public DatabaseRegenerationApp(ILogger<DatabaseRegenerationApp> logger, IImportModeRunner importModeRunner, IBackupModeRunner backupModeRunner, IDeleteModeRunner deleteModeRunner, IDeleteBackupModeRunner deleteBackupModeRunner, IBackupSwapModeRunner backupSwapModeRunner)
        {
            _logger = logger;
            _importModeRunner = importModeRunner;
            _backupModeRunner = backupModeRunner;
            _deleteModeRunner = deleteModeRunner;
            _deleteBackupModeRunner = deleteBackupModeRunner;
            _backupSwapModeRunner = backupSwapModeRunner;
        }

        public void Run()
        {
            var input = "";

            while (!input.ToLower().Trim().Equals("exit"))
            {
                Console.WriteLine("The application can be run in the following modes:");
                Console.WriteLine("1 - Import fresh database. There must be no existing 'current' database. If one exists, please either delete it or back it up first.");
                Console.WriteLine("2 - Backup current database, bringing it out of scope. Only works if there is a database to backup, and no existing backup.");
                Console.WriteLine("3 - Delete current database. There must be a current database in scope.");
                Console.WriteLine("4 - Delete database backup. There must be an existing backup.");
                Console.WriteLine("5 - Swap current database and backup. Creates a backup of current database, if it exists, bringing it out of scope, and brings the backup database into scope, if it exists, making it the current. ");
                Console.WriteLine("Please enter a number corresponding to the mode you want to run. To exit, please type 'Exit'.");

                input = Console.ReadLine().ToLower().Trim();

                ProcessInput(input);
            }

            Console.WriteLine("Application has terminated.");

        }

        private void ProcessInput(string input)
        {
            switch (input)
            {
                case "exit":
                    break;

                case "1":
                    _importModeRunner.Run();
                    break;

                case "2":
                    _backupModeRunner.Run();
                    break;

                case "3":
                    _deleteModeRunner.Run();
                    break;

                case "4":
                    _deleteBackupModeRunner.Run();
                    break;

                case "5":
                    _backupSwapModeRunner.Run();
                    break;
                
                default:
                    Console.WriteLine("Couldn't parse input. Please make sure you enter a number corresponding to one of the modes, or enter 'Exit' to exit the application.");
                    Console.WriteLine();
                    break;
            }
        }

        private readonly ILogger<DatabaseRegenerationApp> _logger;
        private readonly IImportModeRunner _importModeRunner;
        private readonly IBackupModeRunner _backupModeRunner;
        private readonly IDeleteModeRunner _deleteModeRunner;
        private readonly IDeleteBackupModeRunner _deleteBackupModeRunner;
        private readonly IBackupSwapModeRunner _backupSwapModeRunner;
    }
}
