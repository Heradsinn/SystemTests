using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SystemTests.Database
{
    public class DatabaseManagementRepository : IDatabaseManagementRepository
    {
        public DatabaseManagementRepository(IConfiguration config)
        {
            _connectionString = config["TargetDbConnectionString"];
        }

        public bool DatabaseExists(string databaseName)
        {
            var sqlCommandText = $@"
                        SELECT COUNT(*) 
                        FROM sys.databases
                        WHERE name='{databaseName}'";
            var dbCount = 0;

            var resultObj = ExecuteQuerySqlCommand(sqlCommandText);
            if (resultObj != null)
            {
                int.TryParse(resultObj.ToString(), out dbCount);
            }

            return dbCount > 0;
        }

        public int DeleteDatabase(string databaseName)
        {
            var sqlCommandText = $@"
                ALTER DATABASE {databaseName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                ALTER DATABASE {databaseName} SET ONLINE;
                USE [master];
                DROP DATABASE {databaseName}";
            return ExecuteNonQuerySqlCommand(sqlCommandText);
        }


        public int RenameDatabase(string oldName, string newName)
        {
            /**
             * CREDIT
             * The below SQL method was taken from Stack Overflow.
             * It has been modified (and debugged) to fit our purposes, specifically
             * it has been adapted to only change the name of one database (and its files).
             * The order of the blocks of code has been changed, 
             * and variables have been renamed. Finally, it has been changed from using
             * the "attach-detach" method to simply changing the filenames with an 
             * ALTER command.
             * 
             * Link to answer: https://stackoverflow.com/a/17337603/
             * User: https://stackoverflow.com/users/1358357/sverrehundeide
             * 
             * Original thread: https://stackoverflow.com/questions/4758551/how-to-rename-the-physical-database-files
             * Question asked by: https://stackoverflow.com/users/51649/abs
             * 
             * Licensed under: https://creativecommons.org/licenses/by-sa/3.0/
             */

            var sqlCommandText = $@"

                declare @OriginalFilePath nvarchar(300) = (select physical_name FROM sys.master_files where name = '{oldName}'); 
                PRINT 'Original physical file name: ' + @OriginalFilePath;
                declare @NewFilePath nvarchar(300) = replace(@OriginalFilePath, '{oldName}', '{newName}');
            
                declare @OriginalFileName nvarchar(300) = '{oldName}_Primary.mdf';
                declare @NewFileName nvarchar(300) = '{newName}_Primary.mdf';

                declare @OriginalLogFilePath nvarchar(300) = (select physical_name FROM sys.master_files where name = '{oldName}_log'); 
                PRINT 'Original physical log file name: ' + @OriginalLogFilePath;
                declare @NewLogFilePath nvarchar(300) = replace(@OriginalLogFilePath, '{oldName}', '{newName}');

                declare @OriginalLogFileName nvarchar(300) = '{oldName}_Primary.ldf';
                declare @NewLogFileName varchar(300) = '{newName}_Primary.ldf';

                

                IF (EXISTS (select * from sys.databases where name = '{oldName}'))
                BEGIN
                    USE master;
                    ALTER DATABASE {oldName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

                    -- Set new database name
                    ALTER DATABASE {oldName} MODIFY NAME = {newName}

                    -- Update logical names
                    EXEC('ALTER DATABASE {newName} MODIFY FILE (NAME=N''{oldName}'', NEWNAME=N''{newName}'')')
                    EXEC('ALTER DATABASE {newName} MODIFY FILE (NAME=N''{newName}'', FILENAME=N''' + @NewFilePath + ''')')
                    EXEC('ALTER DATABASE {newName} MODIFY FILE (NAME=N''{oldName}_log'', NEWNAME=N''{newName}_log'')')
                    EXEC('ALTER DATABASE {newName} MODIFY FILE (NAME=N''{newName}_log'', FILENAME=N''' + @NewLogFilePath + ''')')

                    ALTER DATABASE {newName} SET OFFLINE;

                    
                        

                    -- Enable xp_cmdshell:

                    EXEC sp_configure 'show advanced options', 1;
                    RECONFIGURE WITH OVERRIDE;

                    EXEC sp_configure 'xp_cmdshell', 1;
                    RECONFIGURE WITH OVERRIDE;



                    -- Rename physical files

                    declare @Command nvarchar(500);

                    SET @Command = 'RENAME ""' + @OriginalFilePath + '"",' + @NewFileName + '';
                    EXEC xp_cmdshell @Command, NO_OUTPUT;

                    SET @Command = 'RENAME ""' + @OriginalLogFilePath + '"", ' + @NewLogFileName + '';
                    EXEC xp_cmdshell @Command, NO_OUTPUT;



                    --Disable xp_cmdshell for security reasons:
                    EXEC sp_configure 'xp_cmdshell', 0
                    RECONFIGURE WITH OVERRIDE;
    
                    EXEC sp_configure 'show advanced options', 0
                    RECONFIGURE WITH OVERRIDE;

                    ALTER DATABASE {newName} SET ONLINE
                    ALTER DATABASE {newName} SET MULTI_USER

                    


                END;

                
            ";
            
            return ExecuteNonQuerySqlCommand(sqlCommandText);
        }

        private int ExecuteNonQuerySqlCommand(string sqlCommandText)
        {
            using (var conn = GetOpenConnection())
            {
                var sqlCommand = new SqlCommand(sqlCommandText, conn);
                return sqlCommand.ExecuteNonQuery();
            }
        }

        private object ExecuteQuerySqlCommand(string sqlCommandText)
        {
            using (var conn = GetOpenConnection())
            {
                var sqlCommand = new SqlCommand(sqlCommandText, conn);
                return sqlCommand.ExecuteScalar();
            }
        }

        private SqlConnection GetOpenConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();

            return conn;
        }

        private readonly string _connectionString;
    }
}
