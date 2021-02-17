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
            var sql = "INSERT INTO People (firstname, lastname, mother, father) VALUES (@firstname, @lastname, @mother, @father)";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@firstname", person.FirstName);
            cmd.Parameters.AddWithValue("@lastname", person.LastName);
            cmd.Parameters.AddWithValue("@mother", person.Mother);
            cmd.Parameters.AddWithValue("@father", person.Father);
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
        public void GetMother(Person person)
        {
            var connString = string.Format(ConnectionString, DatabaseName);
            var conn = new SqlConnection(connString);
            conn.Open();
            var sql = "SELECT * FROM People WHERE Id = mother";
            var cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Console.WriteLine(person.FullInfo);
            conn.Close();
        }
        public void GetFather(Person person)
        {

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
