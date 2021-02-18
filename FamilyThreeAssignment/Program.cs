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

            sqlDB.CreateTable();
            CreateListOfPeople(listOfPersons);
            foreach (var person in listOfPersons)
            {
            crud.Create(person);
            }

            while (KeepGoing)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("1. Add Person");
                Console.WriteLine("2. List People");
                Console.WriteLine("3. Update Person");
                Console.WriteLine("0. Exit");
                Console.WriteLine("-------------------");
                var input = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-------------------");
                switch (input)
                {
                    case 1:
                        var addNewPerson = new Person();
                        string parentFirstNameInput;
                        string parentLastNameInput;

                        Console.WriteLine("Enter Firstname: ");
                        addNewPerson.FirstName = Console.ReadLine();
                        Console.WriteLine("Enter Lastname: ");
                        addNewPerson.LastName = Console.ReadLine();
                        Console.WriteLine("Enter Birthdate: ");
                        addNewPerson.BirthDate = Console.ReadLine();
                        Console.WriteLine("If person IS dead enter the Deathdate, else [Press Enter to continue..]");
                        addNewPerson.DeathDate = Console.ReadLine();

                        if (addNewPerson.DeathDate == null)
                        {
                            addNewPerson.DeathDate = "";
                        }

                        Console.WriteLine("Mothers firstname: ");
                        parentFirstNameInput = Console.ReadLine();
                        Console.WriteLine("Mothers lastname: ");
                        parentLastNameInput = Console.ReadLine();
                        addNewPerson.MotherId = crud.AddParent(parentFirstNameInput, parentLastNameInput);



                        Console.WriteLine("Fathers firstname: ");
                        parentFirstNameInput = Console.ReadLine();
                        Console.WriteLine("Fathers lastname: ");
                        parentLastNameInput = Console.ReadLine();
                        addNewPerson.FatherId = crud.AddParent(parentFirstNameInput, parentLastNameInput);

                        crud.Create(addNewPerson);
                        break;
                    case 2:
                        crud.Read();
                        break;
                    case 3:
                        crud.Read();
                        Console.WriteLine("Which person do you wanna update?");
                        var selectedPerson = listOfPersons[+1];

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
            listOfPersons.Add(new Person(1, "Dennis", "Lindquist", "1988", "", 4, 5));
            listOfPersons.Add(new Person(2, "Marielle", "Lunnan", "1989", "", 0, 0));
            listOfPersons.Add(new Person(3, "Saga", "Lindquist", "2020", "", 2, 1));
            listOfPersons.Add(new Person(4, "Katarina", "Lindquist", "1964", "", 7, 6));
            listOfPersons.Add(new Person(5, "Mikael", "Lindh", "1965", "", 9, 8));
            listOfPersons.Add(new Person(6, "Elis", "Lindquist", "1920", "2010", 0, 0));
            listOfPersons.Add(new Person(7, "Gun", "Lindquist", "1941", "", 0, 0));
            listOfPersons.Add(new Person(8, "Erik", "Lindh", "1920", "", 0, 0));
            listOfPersons.Add(new Person(9, "Barbro", "Lindh", "1931", "", 0, 0));

        }
    }
}
