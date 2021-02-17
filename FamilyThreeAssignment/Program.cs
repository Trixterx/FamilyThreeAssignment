using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FamilyTreeAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        private static void Menu()
        {
            List<Person> listOfPersons = new List<Person>();
            var crud = new CRUD();
            var sqlDB = new SQLDatabase();
            bool KeepGoing = true;

            while (KeepGoing)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("1. Create DB/Tables");
                Console.WriteLine("2. Read DB");
                Console.WriteLine("3. Update DB");
                Console.WriteLine("0. Exit");
                Console.WriteLine("-------------------");
                var input = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-------------------");
                switch (input)
                {
                    case 1:
                        sqlDB.CreateTable();
                        CreateListOfPeople(listOfPersons);
                        foreach (var person in listOfPersons)
                        {
                            crud.Create(person);
                        }
                        break;
                    case 2:
                        crud.Read();
                        break;
                    case 3:
                        break;
                    case 0:
                        Console.WriteLine("Byebye");
                        KeepGoing = false;
                        break;
                }
            }
        }

        private static void CreateListOfPeople(List<Person> listOfPersons)
        {
            listOfPersons.Add(new Person(1, "Dennis", "Lindquist", "1988", "Alive", 4, 5));
            listOfPersons.Add(new Person(2, "Marielle", "Lunnan", "1989", "Alive", 0, 0));
            listOfPersons.Add(new Person(3, "Saga", "Lindquist", "2020", "Alive", 2, 1));
            listOfPersons.Add(new Person(4, "Katarina", "Lindquist", "1964", "Alive", 7, 6));
            listOfPersons.Add(new Person(5, "Mikael", "Lindh", "1965", "Alive", 9, 8));
            listOfPersons.Add(new Person(6, "Elis", "Lindquist", "1920", "Dead", 0, 0));
            listOfPersons.Add(new Person(7, "Gun", "Lindquist", "1941", "Alive", 0, 0));
            listOfPersons.Add(new Person(8, "Erik", "Lindh", "1920", "Alive", 0, 0));
            listOfPersons.Add(new Person(9, "Barbro", "Lindh", "1931", "Alive", 0, 0));

        }
    }
}
