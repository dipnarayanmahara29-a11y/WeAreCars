using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace WeAreCars.Services
{
   public static class DatabaseService
    {
        private static readonly string DataFolder = Path.Combine(
            AppContext.BaseDirectory,
            "Data"

            );

        private static readonly string DatabasePath = Path.Combine(
            DataFolder,
            "WeAreCars.db"
            );

        public static string ConnectionString => $"Data Source={DatabasePath}";

        public static void InitializeDatabase()
        {
            // Make sure the data folder exists.

            Directory.CreateDirectory(DataFolder);

            // Create/Open the SQLite DAtabase
            using SqliteConnection connection
                 = new SqliteConnection(ConnectionString);

            connection.Open();

            // Create the StaffUser table if it does not exits
            using SqliteCommand command = connection.CreateCommand();

            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS StaffUsers
                (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,

                    Username TEXT NOT NULL UNIQUE,

                    PasswordHash TEXT NOT NULL
                );
            ";
            command.ExecuteNonQuery();
        }
    }
}
