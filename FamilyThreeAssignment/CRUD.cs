using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FamilyTreeAssignment
{
    class CRUD
    {
        public string DatabaseName { get; set; } = "Population";
        public int MaxRows { get; set; } = 10;
        public string OrderBy { get; set; } = "lastName";
        internal string ConnectionString { get; set; } = @"Data Source=.\SQLExpress;Integrated Security=true;database={0}";
        public List<Person> listOfPersons = new List<Person>();
        //public List<Person> List(string filter = "firstName LIKE @input", string paramValue){}

        public void Create(Person person)
        {
            var connString = string.Format(ConnectionString, DatabaseName);
            var conn = new SqlConnection(connString);
            conn.Open();
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
            var connString = string.Format(ConnectionString, DatabaseName);
            var conn = new SqlConnection(connString);
            conn.Open();
            var sql = "SELECT Id, firstname, lastname FROM People";
            var cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetValue("Id")}. {reader.GetValue("firstname")} {reader.GetValue("lastname")}");
                }

            }
            reader.Close();
            conn.Close();
        }

        public void Update(Person person)
        {

        }

        public void Delete(Person person)
        {
            var connString = string.Format(ConnectionString, DatabaseName);
            var conn = new SqlConnection(connString);
            conn.Open();
            var sql = "DELETE FROM People(firstname, lastname, mother, father) VALUES (@firstname, @lastname, @mother, @father)";
            var cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
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
        public int GetMother(string motherFirstNameInput, string motherLastNameInput)
        {
            int Id = 0;
            var connString = string.Format(ConnectionString, DatabaseName);
            var conn = new SqlConnection(connString);
            conn.Open();
            var sql = $"SELECT Id FROM People WHERE firstname = '{motherFirstNameInput}' AND lastname = '{motherLastNameInput}'";
            var cmd = new SqlCommand(sql, conn);
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
        public int GetFather(string fatherFirstNameInput, string fatherLastNameInput)
        {
            int Id = 0;
            var connString = string.Format(ConnectionString, DatabaseName);
            var conn = new SqlConnection(connString);
            conn.Open();
            var sql = $"SELECT Id FROM People WHERE firstname = '{fatherFirstNameInput}' AND lastname = '{fatherLastNameInput}'";
            var cmd = new SqlCommand(sql, conn);
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

        public int AddParent(string parentFirstNameInput, string parentLastNameInput)
        {
            int parentId = 0;
            var connString = string.Format(ConnectionString, DatabaseName);
            var conn = new SqlConnection(connString);
            conn.Open();
            var sql = $"SELECT Id FROM People WHERE firstname = '{parentFirstNameInput}' AND lastname = '{parentLastNameInput}'";
            var cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    parentId = Convert.ToInt32(reader.GetValue("Id"));
                }
            }
            reader.Close();
            conn.Close();
            return parentId;
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
