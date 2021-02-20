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
            var database = new SqlDatabase();
            bool KeepGoing = true;
            string firstNameInput;
            string lastNameInput;
            string birthDateInput;
            string deathDateInput;

            Setup(listOfPersons, database);

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
                        database.Read();
                        break;
                    case 2:
                        CreatePerson(listOfPersons, database, out firstNameInput, out lastNameInput);
                        break;
                    case 3:
                        bool updatePersonKeepGoing = true;
                        database.Read();
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("Which person do you wanna update?");
                        int selectedPerson = Convert.ToInt32(Console.ReadLine()) - 1;
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine($"{listOfPersons[selectedPerson].Id}. {listOfPersons[selectedPerson].FirstName} {listOfPersons[selectedPerson].LastName}");
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("What part do you wanna update?");
                        Console.WriteLine("1. Firstname");
                        Console.WriteLine("2. Lastname");
                        Console.WriteLine("3. Birthdate");
                        Console.WriteLine("4. Deathdate");
                        Console.WriteLine("5. Mother");
                        Console.WriteLine("6. Father");
                        Console.WriteLine("--------------------------------");
                        int updatePersonInput = Convert.ToInt32(Console.ReadLine());
                        do
                        {
                            switch (updatePersonInput)
                            {
                                case 1:
                                    Console.WriteLine("Enter new Firstname: ");
                                    firstNameInput = Console.ReadLine();
                                    listOfPersons[selectedPerson].FirstName = firstNameInput;
                                    database.Update("firstname", listOfPersons[selectedPerson].FirstName, listOfPersons[selectedPerson].Id);
                                    updatePersonKeepGoing = false;
                                    break;
                                case 2:
                                    Console.WriteLine("Enter new Lastname: ");
                                    lastNameInput = Console.ReadLine();
                                    listOfPersons[selectedPerson].LastName = lastNameInput;
                                    database.Update("lastname", listOfPersons[selectedPerson].LastName, listOfPersons[selectedPerson].Id);
                                    updatePersonKeepGoing = false;
                                    break;
                                case 3:
                                    Console.WriteLine("Enter new Birthdate: ");
                                    birthDateInput = Console.ReadLine();
                                    listOfPersons[selectedPerson].BirthDate = birthDateInput;
                                    database.Update("birthdate", listOfPersons[selectedPerson].BirthDate, listOfPersons[selectedPerson].Id);
                                    updatePersonKeepGoing = false;
                                    break;
                                case 4:
                                    Console.WriteLine("Enter new Deathdate: ");
                                    deathDateInput = Console.ReadLine();
                                    listOfPersons[selectedPerson].DeathDate = deathDateInput;
                                    database.Update("deathdate", listOfPersons[selectedPerson].DeathDate, listOfPersons[selectedPerson].Id);
                                    updatePersonKeepGoing = false;
                                    break;
                                case 5:
                                    Console.WriteLine("Mothers Firstname: ");
                                    firstNameInput = Console.ReadLine();
                                    Console.WriteLine("Mothers Lastname: ");
                                    lastNameInput = Console.ReadLine();
                                    listOfPersons[selectedPerson].MotherId = database.SetParent(firstNameInput, lastNameInput);
                                    database.Update("motherId", listOfPersons[selectedPerson].MotherId.ToString(), listOfPersons[selectedPerson].Id);
                                    updatePersonKeepGoing = false;
                                    break;
                                case 6:
                                    Console.WriteLine("Fathers Firstname: ");
                                    firstNameInput = Console.ReadLine();
                                    Console.WriteLine("Fathers Lastname: ");
                                    lastNameInput = Console.ReadLine();
                                    listOfPersons[selectedPerson].FatherId = database.SetParent(firstNameInput, lastNameInput);
                                    database.Update("motherId", listOfPersons[selectedPerson].FatherId.ToString(), listOfPersons[selectedPerson].Id);
                                    updatePersonKeepGoing = false;
                                    break;
                            }
                        } while (updatePersonKeepGoing);
                        break;
                    case 4:
                        Console.WriteLine("Firstname of the person you want to search for: ");
                        firstNameInput = Console.ReadLine();
                        Console.WriteLine("Lastname of the person you want to search for: ");
                        lastNameInput = Console.ReadLine();
                        database.Search(firstNameInput, lastNameInput);
                        break;
                    case 5:
                        Console.WriteLine("Who do you wanna Delete?");
                        int inputId = Convert.ToInt32(Console.ReadLine());
                        database.Delete(inputId);
                        listOfPersons.RemoveAt(inputId - 1);
                        break;
                    case 6:
                        // For testing!
                        foreach (var person in listOfPersons)
                        {
                            Console.WriteLine($"{person.Id}. {person.FirstName} {person.LastName} Mother: {person.MotherId} Father: {person.FatherId}");
                        }
                        break;
                    case 0:
                        Console.WriteLine("Byebye");
                        KeepGoing = false;
                        break;
                }
            }
        }

        private static void CreatePerson(List<Person> listOfPersons, SqlDatabase database, out string firstNameInput, out string lastNameInput)
        {
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

            Console.WriteLine("Mothers Firstname: ");
            firstNameInput = Console.ReadLine();
            Console.WriteLine("Mothers Lastname: ");
            lastNameInput = Console.ReadLine();
            newPerson.MotherId = database.SetParent(firstNameInput, lastNameInput);

            Console.WriteLine("Fathers Firstname: ");
            firstNameInput = Console.ReadLine();
            Console.WriteLine("Fathers Lastname: ");
            lastNameInput = Console.ReadLine();
            newPerson.FatherId = database.SetParent(firstNameInput, lastNameInput);

            database.Create(newPerson);
            listOfPersons.Add(newPerson);
        }

        private static void Setup(List<Person> listOfPersons, SqlDatabase database)
        {
            database.CreateTable();
            CreateListOfPeople(listOfPersons);
            foreach (var person in listOfPersons)
            {
                database.Create(person);
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
