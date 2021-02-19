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
            var listOfPersons = new List<Person>();
            var crud = new CRUD();
            var db = new Database();
            bool KeepGoing = true;
            string firstNameInput;
            string lastNameInput;
            var dt = new DataTable();

            Setup(listOfPersons, crud, db);

            while (KeepGoing)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("1. List All Persons");
                Console.WriteLine("2. Create Person");
                Console.WriteLine("3. Update Person");
                Console.WriteLine("4. Search Person");
                Console.WriteLine("5. Delete Person");
                Console.WriteLine("0. Exit");
                Console.WriteLine("-------------------");
                var input = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-------------------");
                switch (input)
                {
                    case 1:
                        crud.Read();
                        break;
                    case 2:
                        var newPerson = new Person();

                        Console.WriteLine("Enter Firstname: ");
                        newPerson.FirstName = Console.ReadLine();
                        Console.WriteLine("Enter Lastname: ");
                        newPerson.LastName = Console.ReadLine();
                        Console.WriteLine("Enter Birthdate: ");
                        newPerson.BirthDate = Console.ReadLine();
                        Console.WriteLine("If person IS dead enter the date of Death, else [Press Enter to continue..]");
                        newPerson.DeathDate = Console.ReadLine();

                        if (newPerson.DeathDate == null)
                        {
                            newPerson.DeathDate = "";
                        }

                        Console.WriteLine("Mothers firstname: ");
                        firstNameInput = Console.ReadLine();
                        Console.WriteLine("Mothers lastname: ");
                        lastNameInput = Console.ReadLine();
                        newPerson.MotherId = crud.GetParent(firstNameInput, lastNameInput);

                        Console.WriteLine("Fathers firstname: ");
                        firstNameInput = Console.ReadLine();
                        Console.WriteLine("Fathers lastname: ");
                        lastNameInput = Console.ReadLine();
                        newPerson.FatherId = crud.GetParent(firstNameInput, lastNameInput);

                        crud.Create(newPerson);
                        listOfPersons.Add(newPerson);
                        break;
                    case 3:
                        crud.Read();
                        Console.WriteLine("Which person do you wanna update?");
                        int selectedPerson = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(listOfPersons[selectedPerson + 1]);



                        break;
                    case 4:
                        Console.WriteLine("Firstname of the person you want to search for: ");
                        firstNameInput = Console.ReadLine();
                        Console.WriteLine("Lastname of the person you want to search for: ");
                        lastNameInput = Console.ReadLine();
                        crud.Search(firstNameInput, lastNameInput);
                        break;
                    case 5:
                        Console.WriteLine("Who do you wanna Delete?");
                        int inputId = Convert.ToInt32(Console.ReadLine());
                        crud.Delete(inputId);
                        listOfPersons.RemoveAt(inputId);
                        break;
                    case 6:
                        foreach (var person in listOfPersons)
                        {
                            Console.WriteLine(person.FirstName);
                        }
                        break;
                    case 0:
                        Console.WriteLine("Byebye");
                        KeepGoing = false;
                        break;
                }
            }
        }

        private static void Setup(List<Person> listOfPersons, CRUD crud, Database db)
        {
            db.CreateTable();
            CreateListOfPeople(listOfPersons);
            foreach (var person in listOfPersons)
            {
                crud.Create(person);
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
