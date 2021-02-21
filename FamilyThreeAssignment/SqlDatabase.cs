using FamilyThreeAssignment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FamilyTreeAssignment
{
    class SqlDatabase
    {
        public string DatabaseName { get; set; } = "Population";
        internal string ConnectionString { get; set; } = @"Data Source=.\SQLExpress;Integrated Security=true;database={0}";

        public void Create(Person person)
        {
            SqlConnection conn = sqlConn();
            var sql = "INSERT INTO People (firstname, lastname, birthdate, deathdate, motherId, fatherId) VALUES (@firstname, @lastname, @birthdate, @deathdate, @motherId, @fatherId)";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@firstname", person.FirstName);
            cmd.Parameters.AddWithValue("@lastname", person.LastName);
            cmd.Parameters.AddWithValue("@birthdate", person.BirthDate);
            cmd.Parameters.AddWithValue("@deathdate", person.DeathDate);
            cmd.Parameters.AddWithValue("@motherId", person.MotherId);
            cmd.Parameters.AddWithValue("@fatherId", person.FatherId);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Read()
        {
            SqlConnection conn = sqlConn();
            var sql = "SELECT Id, firstname, lastname, birthdate, deathdate FROM People";
            var cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetValue("Id")}. {reader.GetValue("firstname")} {reader.GetValue("lastname")} \t| {reader.GetValue("birthdate")}-{reader.GetValue("deathdate")}");
                }

            }
            reader.Close();
            conn.Close();
        }

        public void Update(string column, string input, int Id)
        {
            SqlConnection conn = sqlConn();
            var sql = $"UPDATE People SET {column} = @input WHERE Id = @Id";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@input", input);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Delete(int inputId)
        {
            SqlConnection conn = sqlConn();
            var sql = "DELETE FROM People WHERE Id = @Id";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", inputId);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Search(string firstNameInput, string lastNameInput)
        {
            SqlConnection conn = sqlConn();
            var sql = "SELECT * FROM People WHERE firstname LIKE @firstname AND lastname LIKE @lastname";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@firstname", firstNameInput);
            cmd.Parameters.AddWithValue("@lastname", lastNameInput);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetValue("Id")}. {reader.GetValue("firstname")} {reader.GetValue("lastname")} \t| {reader.GetValue("birthdate")}-{reader.GetValue("deathdate")}");
                }

            }
            reader.Close();
            conn.Close();
        }

        public void SearchByFirstNameLetter(string firstNameInput)
        {
            SqlConnection conn = sqlConn();
            var sql = "SELECT * FROM People WHERE firstname LIKE @firstNameInput";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@firstNameInput", firstNameInput);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetValue("Id")}. {reader.GetValue("firstname")} {reader.GetValue("lastname")} \t| {reader.GetValue("birthdate")}-{reader.GetValue("deathdate")}");
                }

            }
            reader.Close();
            conn.Close();
        }

        public void SearchByYear(string yearInput)
        {
            SqlConnection conn = sqlConn();
            var sql = "SELECT * FROM People WHERE birthdate = @yearInput";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@yearInput", yearInput);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetValue("Id")}. {reader.GetValue("firstname")} {reader.GetValue("lastname")} \t| {reader.GetValue("birthdate")}-{reader.GetValue("deathdate")}");
                }

            }
            reader.Close();
            conn.Close();
        }

        public bool DoesPersonExist(string firstName, string lastName)
        {
            return true;
        }
        public bool DoesPersonExist(int Id)
        {
            return true;
        }
        public void GetParent(int parentId)
        {
            SqlConnection conn = sqlConn();
            var sql = "SELECT Id, firstname, lastname, birthdate, deathdate FROM People WHERE Id = @parentId";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@parentId", parentId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetValue("Id")}. {reader.GetValue("firstname")} {reader.GetValue("lastname")} \t| {reader.GetValue("birthdate")}-{reader.GetValue("deathdate")}");
                }

            }
            reader.Close();
            conn.Close();
        }

        /// <summary>
        /// Gets parent Id
        /// </summary>
        /// <param name="firstNameInput"></param>
        /// <param name="lastNameInput"></param>
        /// <returns></returns>
        public int SetParent(string firstNameInput, string lastNameInput)
        {
            int Id = 0;
            SqlConnection conn = sqlConn();
            var sql = "SELECT Id FROM People WHERE firstname = @firstname AND lastname = @lastname";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@firstname", firstNameInput);
            cmd.Parameters.AddWithValue("@lastname", lastNameInput);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Id = Convert.ToInt32(reader.GetValue("Id"));
                }
            }
            reader.Close();
            conn.Close();
            return Id;
        }

        /// <summary>
        /// Database Connection
        /// </summary>
        /// <returns></returns>
        private SqlConnection sqlConn()
        {
            var connString = string.Format(ConnectionString, DatabaseName);
            var conn = new SqlConnection(connString);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// Creates a Data table if none exists.
        /// </summary>
        public void CreateTable()
        {
            try
            {
                SqlConnection conn = sqlConn();
                var sql = "CREATE TABLE People(Id int IDENTITY(1,1) PRIMARY KEY, firstname varchar(50), lastname varchar(50), birthdate varchar(50), deathdate varchar(50), motherId int, fatherId int);";
                var cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Data table was Created Successfully...");
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("exception occured while creating table:" + e.Message + "\t" + e.GetType());
            }
        }

        /// <summary>
        /// Check if Datatable exists.
        /// </summary>
        /// <param name="exists"></param>
        /// <returns></returns>
        public bool DoesTableExist(bool exists)
        {
            SqlConnection conn = sqlConn();
            var sql = "SELECT COUNT(*) FROM information_schema.tables WHERE table_name = 'People'";
            var cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int count = reader.GetInt32(0);
                if (count == 0)
                {
                    Console.WriteLine("No such data table exists and a new one will be created!");
                    exists = false;
                }
                else if (count == 1)
                {
                    Console.WriteLine("Such data table exists and will not be created!");
                    exists = true;
                }
            }
            reader.Close();
            conn.Close();
            return exists;
        }

        public void CreateDatabase()
        {
            SqlConnection conn = sqlConn();
            var sql = $"CREATE DATABASE {DatabaseName}";
            var cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Database was Created Successfully...");
            conn.Close();
        }
        /*
        public void CheckDatabase()
        {
            SqlConnection conn = sqlConn();
            var sql = $"SELECT db_id('{DatabaseName}')";
            if (!database.DatabaseName)
            {
                CreateDatabase();
            }
        }
        public static bool CheckDatabaseExists(string connectionString, string databaseName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand($"SELECT db_id('{databaseName}')", connection))
                {
                    connection.Open();
                    return (command.ExecuteScalar() != DBNull.Value);
                }
            }
        }
        */
    }
}
