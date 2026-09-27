using Microsoft.Data.Sqlite;
using System;

namespace WeAreCars.Services
{
    public static class StaffUserService
    {
        public static bool CreateStaffUser(
            string username,
            string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            if (string.IsNullOrWhiteSpace(password))
                return false;

            string passwordHash =
                PasswordService.HashPassword(password);

            using SqliteConnection connection =
                new SqliteConnection(
                    DatabaseService.ConnectionString);

            connection.Open();

            using SqliteCommand command =
                connection.CreateCommand();

            command.CommandText = @"
                INSERT INTO StaffUsers
                (
                    Username,
                    PasswordHash
                )
                VALUES
                (
                    $username,
                    $passwordHash
                );
            ";

            command.Parameters.AddWithValue(
                "$username",
                username.Trim());

            command.Parameters.AddWithValue(
                "$passwordHash",
                passwordHash);

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }


        public static bool VerifyStaffUser(
            string username,
            string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            if (string.IsNullOrWhiteSpace(password))
                return false;

            using SqliteConnection connection =
                new SqliteConnection(
                    DatabaseService.ConnectionString);

            connection.Open();

            using SqliteCommand command =
                connection.CreateCommand();

            command.CommandText = @"
                SELECT PasswordHash
                FROM StaffUsers
                WHERE Username = $username
                COLLATE NOCASE
                LIMIT 1;
            ";

            command.Parameters.AddWithValue(
                "$username",
                username.Trim());

            object? result =
                command.ExecuteScalar();

            if (result == null ||
                result == DBNull.Value)
            {
                return false;
            }

            string storedHash =
                result.ToString() ?? string.Empty;

            return PasswordService.VerifyPassword(
                password,
                storedHash);
        }
    }
}