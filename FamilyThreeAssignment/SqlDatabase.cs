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
        public int MaxRows { get; set; } = 10;
        public string OrderBy { get; set; } = "lastName";
        internal string ConnectionString { get; set; } = @"Data Source=.\SQLExpress;Integrated Security=true;database={0}";
        public List<Person> listOfPersons = new List<Person>();
        //public List<Person> List(string filter = "firstName LIKE @input", string paramValue){}

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
                    Console.WriteLine($"{reader.GetValue("Id")}. {reader.GetValue("firstname")} {reader.GetValue("lastname")} Born: {reader.GetValue("birthdate")} Dead: {reader.GetValue("deathdate")}");
                }

            }
            reader.Close();
            conn.Close();
        }
    

        public bool DoesPersonExist(string name)
        {
            return true;
        }
        public bool DoesPersonExist(int Id)
        {
            return true;
        }
        public void GetMother()
        {
        }
        public void GetFather()
        {
        }

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

        private SqlConnection sqlConn()
        {
            var connString = string.Format(ConnectionString, DatabaseName);
            var conn = new SqlConnection(connString);
            conn.Open();
            return conn;
        }

        public void CreateTable()
        {
            try
            {
                var connString = string.Format(ConnectionString, DatabaseName);
                var conn = new SqlConnection(connString);
                conn.Open();
                var sql = "DROP TABLE People CREATE TABLE People(Id int IDENTITY(1,1) PRIMARY KEY, firstname varchar(50), lastname varchar(50), birthdate varchar(50), deathdate varchar(50), motherId int, fatherId int);";
                var cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Old Table was Dropped and a New Table was Created Successfully...");
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("exception occured while creating table:" + e.Message + "\t" + e.GetType());
            }
        }

        /*  public void CreateDatabase()
          {
              var database = new SQLDatabase();
              var sql = "Create Database " + DatabaseName;
              database.ExecuteSQL(sql);
          }
          public void CheckDatabase()
          {
              var database = new SQLDatabase();
              var myDatabaseName = "FamilyTree";
              if (!database.DoesDatabaseExist(myDatabaseName))
              {
                  CreateDatabase();
              }
          }
        */
    }
}
