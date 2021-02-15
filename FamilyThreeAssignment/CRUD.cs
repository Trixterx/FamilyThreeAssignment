using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FamilyThreeAssignment
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
            var cnn = new SqlConnection(connString);
            cnn.Open();
            listOfPersons.Add(new Person(10, "Dennis", "Lindquist", "nej", "nej", 1, 1));
                var sql = "INSERT INTO People(firstname, lastname, mother, father) VALUES (@firstname, @lastname, @mother, @father)";
                var command = new SqlCommand(sql, cnn);
                command.Parameters.AddWithValue("@firstname", person.FirstName);
                command.Parameters.AddWithValue("@lastname", person.LastName);
                command.Parameters.AddWithValue("@mother", person.Mother);
                command.Parameters.AddWithValue("@father", person.Father);
                command.ExecuteNonQuery();
        }

        public void Read(Person person)
        {

        }

        public void Update(Person person)
        {

        }

        public void Delete(Person person)
        {
            var connString = string.Format(ConnectionString, DatabaseName);
            var cnn = new SqlConnection(connString);
            cnn.Open();
            var sql = "DELETE FROM People(firstname, lastname, mother, father) VALUES (@firstname, @lastname, @mother, @father)";
            var command = new SqlCommand(sql, cnn);
            command.Parameters.AddWithValue("@firstname", person.FirstName);
            command.Parameters.AddWithValue("@lastname", person.LastName);
            command.Parameters.AddWithValue("@mother", person.Mother);
            command.Parameters.AddWithValue("@father", person.Father);
            command.ExecuteNonQuery();
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
