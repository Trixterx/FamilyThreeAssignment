using FamilyThreeAssignment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace FamilyTreeAssignment
{
    class SqlDatabase
    {
        public string DatabaseName { get; set; } = "FamilyTree";
        internal string ConnectionString { get; set; } = @"Data Source=.\SQLExpress;Integrated Security=true;database={0}";

        /// <summary>
        /// Create Person's in the database.
        /// </summary>
        /// <param name="person"></param>
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

        /// <summary>
        /// Read from Database.
        /// </summary>
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

        /// <summary>
        /// Update person in database by Id.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="input"></param>
        /// <param name="Id"></param>
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

        /// <summary>
        /// Delete person from database by Id.
        /// </summary>
        /// <param name="inputId"></param>
        public void Delete(int inputId)
        {
            SqlConnection conn = sqlConn();
            var sql = "DELETE FROM People WHERE Id = @Id";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", inputId);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// Seach by first- and lastname.
        /// </summary>
        /// <param name="firstNameInput"></param>
        /// <param name="lastNameInput"></param>
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

        /// <summary>
        /// Seach with letters on firstname.
        /// </summary>
        /// <param name="firstNameInput"></param>
        public void SeachFirstnameByLetter(string firstNameInput)
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

        /// <summary>
        /// Seach by year by inputted year.
        /// </summary>
        /// <param name="yearInput"></param>
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

        /// <summary>
        /// Gets the Id from the parent.
        /// </summary>
        /// <param name="parentId"></param>
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
        /// Gets parent Id.
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
        /// Creates a Data table if none exists.
        /// </summary>
        public void CreateTable()
        {
            try
            {
                var sql = "CREATE TABLE People(Id int IDENTITY(1,1) PRIMARY KEY, firstname varchar(50), lastname varchar(50), birthdate varchar(50), deathdate varchar(50), motherId int, fatherId int);";
                ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Check if Datatable exists.
        /// </summary>
        /// <param name="exists"></param>
        /// <returns></returns>
        public bool DoesTableExist()
        {
            bool exists = false;
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

        /// <summary>
        /// Sets name to "Master" so it can connect then Create the Database with DatabaseName.
        /// </summary>
        /// <param name="databaseName"></param>
        public void CreateDatabase(string databaseName)
        {
            var sql = $"CREATE DATABASE {databaseName}";
            DatabaseName = "Master";
            try
            {
                ExecuteSQL(sql);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            DatabaseName = databaseName;
        }

        /// <summary>
        /// Open SQL Connection.
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
        /// SQL Execute.
        /// </summary>
        /// <param name="sql"></param>
        public void ExecuteSQL(string sql)
        {
            var conn = new SqlConnection(string.Format(ConnectionString, DatabaseName));
            conn.Open();
            var cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
