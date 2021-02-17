using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FamilyTreeAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> listOfPersons = new List<Person>();
            var crud = new CRUD();
            var sqlDB = new SQLDatabase();


            listOfPersons.Add(new Person(1 ,"Dennis", "Lindquist", "1988", "Alive", 0, 1));
            listOfPersons.Add(new Person(2 ,"Marielle", "Lunnan", "1989", "Alive", 1, 0));
            listOfPersons.Add(new Person(3 ,"Saga", "Lindquist", "2020", "Alive", 0, 0));
            listOfPersons.Add(new Person(4 ,"Katarina", "Lindquist", "1964", "Alive", 1, 0));
            listOfPersons.Add(new Person(5 ,"Mikael", "Lindh", "1965", "Alive", 0, 1));
            sqlDB.CreateTable();

            foreach (var person in listOfPersons)
            {
                crud.Create(person);
            }

            foreach (var person in listOfPersons)
            {
                crud.Read(person);
                
            }


        }
    }
}
