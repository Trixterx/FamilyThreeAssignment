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

            Start(listOfPersons, crud, db);

            while (KeepGoing)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("1. Add Person");
                Console.WriteLine("2. List People");
                Console.WriteLine("3. Seach Person");
                Console.WriteLine("4. Update Person");
                Console.WriteLine("5. Delete Person");
                Console.WriteLine("0. Exit");
                Console.WriteLine("-------------------");
                var input = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-------------------");
                switch (input)
                {
                    case 1:
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
                    case 2:
                        crud.Read();
                        break;
                    case 3:
                        Console.WriteLine("Firstname of the person you search for: ");
                        firstNameInput = Console.ReadLine();
                        Console.WriteLine("Lastname of the person you search for: ");
                        lastNameInput = Console.ReadLine();
                        crud.Search(firstNameInput, lastNameInput);
                        break;
                    case 4:
                        crud.Read();
                        Console.WriteLine("Which person do you wanna update?");
                        int selectedPerson = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(listOfPersons[selectedPerson + 1]);


                        break;
                    case 5:
                        break;
                    case 0:
                        Console.WriteLine("Byebye");
                        KeepGoing = false;
                        break;
                }
            }
        }

        private static void Start(List<Person> listOfPersons, CRUD crud, Database db)
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
