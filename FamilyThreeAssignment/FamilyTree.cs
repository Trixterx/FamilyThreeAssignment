using FamilyTreeAssignment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FamilyThreeAssignment
{
    class FamilyTree
    {
        public void MainMenu()
        {
            var listOfPersons = new List<Person>();
            var database = new SqlDatabase();
            bool mainMenuKeepGoing = true;

            /// <summary>
            /// Setup runs first to create database, table and a list of persons.
            /// </summary>
            Setup(listOfPersons, database);

            /// <summary>
            /// The Main Menu.
            /// </summary>
            do
            {                
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("|  Main Menu                                               |");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("1. List All Persons");
                Console.WriteLine("2. Create Person");
                Console.WriteLine("3. Update Person");
                Console.WriteLine("4. Search Person");
                Console.WriteLine("5. Delete Person");
                Console.WriteLine("6. Select Person | Get Details About Selected Person");
                Console.WriteLine("0. Exit");
                Console.WriteLine("------------------------------------------------------------");
                var mainMenuInput = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("------------------------------------------------------------");
                switch (mainMenuInput)
                {
                    case 1:
                        database.Read();
                        break;
                    case 2:
                        CreatePerson(listOfPersons, database);
                        break;
                    case 3:
                        UpdatePerson(listOfPersons, database);
                        break;
                    case 4:
                        SearchPerson(database);
                        break;
                    case 5:                    
                        DeletePerson(listOfPersons, database);
                        break;
                    case 6:
                        SelectPerson(listOfPersons, database);
                        break;
                    case 0:
                        Console.WriteLine("Byebye");
                        mainMenuKeepGoing = false;
                        break;
                    default:
                        Console.WriteLine("Wrong input, try again!");
                        break;
                }
            } while (mainMenuKeepGoing);
        }

        /// <summary>
        /// Select Person Menu. Select a person to then get mother, father or children printed.
        /// </summary>
        /// <param name="listOfPersons"></param>
        /// <param name="database"></param>
        private static void SelectPerson(List<Person> listOfPersons, SqlDatabase database)
        {
            Console.WriteLine("Which person do you wanna select?");
            Console.WriteLine("------------------------------------------------------------");
            int selectPersonInput = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.WriteLine($"{listOfPersons[selectPersonInput].Id}. {listOfPersons[selectPersonInput].FirstName} {listOfPersons[selectPersonInput].LastName}");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("|  Select Person Menu                                      |");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("1. Get Mother");
            Console.WriteLine("2. Get Father");
            Console.WriteLine("3. Get Children");
            Console.WriteLine("0. Main Menu");
            Console.WriteLine("------------------------------------------------------------");
            int getParent = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("------------------------------------------------------------");
            switch (getParent)
            {
                case 1:
                    database.GetParent(listOfPersons[selectPersonInput].MotherId);
                    break;
                case 2:
                    database.GetParent(listOfPersons[selectPersonInput].FatherId);
                    break;
                case 3:
                    database.GetParent(listOfPersons[selectPersonInput].ChildId);
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("Wrong input, try again!");
                    break;
            }
        }

        /// <summary>
        /// Delete Person.
        /// </summary>
        /// <param name="listOfPersons"></param>
        /// <param name="database"></param>
        private static void DeletePerson(List<Person> listOfPersons, SqlDatabase database)
        {
            database.Read();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Who do you wanna Delete?");
            Console.WriteLine("------------------------------------------------------------");
            int inputId = Convert.ToInt32(Console.ReadLine());
            database.Delete(inputId);
            listOfPersons.RemoveAt(inputId - 1);
        }

        /// <summary>
        /// Search Person Manu.
        /// </summary>
        /// <param name="database"></param>
        private static void SearchPerson(SqlDatabase database)
        {
            string firstNameInput, lastNameInput, yearInput;
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("|  Search Person Menu                                      |");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("1. Search by First and Lastname");
            Console.WriteLine("2. Search by Starting Letter of Firstname");
            Console.WriteLine("3. Search by Birth Year");
            Console.WriteLine("------------------------------------------------------------");
            int searchPersonInput = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("------------------------------------------------------------");
            switch (searchPersonInput)
            {
                case 1:
                    Console.WriteLine("Firstname of the person you want to search for: ");
                    firstNameInput = Console.ReadLine();
                    Console.WriteLine("Lastname of the person you want to search for: ");
                    lastNameInput = Console.ReadLine();
                    database.Search(firstNameInput, lastNameInput);
                    break;
                case 2:
                    Console.WriteLine("Firstname of the person you want to search for: ");
                    firstNameInput = Console.ReadLine();
                    database.SeachFirstnameByLetter($"%{firstNameInput}%");
                    break;
                case 3:
                    Console.WriteLine("What year do you want to search for: ");
                    yearInput = Console.ReadLine();
                    database.SearchByYear(yearInput);
                    break;
                default:
                    Console.WriteLine("Wrong input, try again!");
                    break;
            }
        }

        /// <summary>
        /// Update Person Menu.
        /// </summary>
        /// <param name="listOfPersons"></param>
        /// <param name="database"></param>
        private static void UpdatePerson(List<Person> listOfPersons, SqlDatabase database)
        {

            string firstNameInput, lastNameInput, birthDateInput, deathDateInput;

            database.Read();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Which person do you wanna update?");
            Console.WriteLine("------------------------------------------------------------");
            int selectedPerson = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine($"{listOfPersons[selectedPerson].Id}. {listOfPersons[selectedPerson].FirstName} {listOfPersons[selectedPerson].LastName}");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("|  Update Person Menu                                      |");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("1. Firstname");
            Console.WriteLine("2. Lastname");
            Console.WriteLine("3. Birthdate");
            Console.WriteLine("4. Deathdate");
            Console.WriteLine("5. Mother");
            Console.WriteLine("6. Father");
            Console.WriteLine("0. Main Menu");
            Console.WriteLine("------------------------------------------------------------");
            int updatePersonInput = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("------------------------------------------------------------");
            switch (updatePersonInput)
            {
                case 1:
                    Console.WriteLine("Enter new Firstname: ");
                    firstNameInput = Console.ReadLine();
                    listOfPersons[selectedPerson].FirstName = firstNameInput;
                    database.Update("firstname", listOfPersons[selectedPerson].FirstName, listOfPersons[selectedPerson].Id);
                    break;
                case 2:
                    Console.WriteLine("Enter new Lastname: ");
                    lastNameInput = Console.ReadLine();
                    listOfPersons[selectedPerson].LastName = lastNameInput;
                    database.Update("lastname", listOfPersons[selectedPerson].LastName, listOfPersons[selectedPerson].Id);
                    break;
                case 3:
                    Console.WriteLine("Enter new Birthdate: ");
                    birthDateInput = Console.ReadLine();
                    listOfPersons[selectedPerson].BirthDate = birthDateInput;
                    database.Update("birthdate", listOfPersons[selectedPerson].BirthDate, listOfPersons[selectedPerson].Id);
                    break;
                case 4:
                    Console.WriteLine("Enter new Deathdate: ");
                    deathDateInput = Console.ReadLine();
                    listOfPersons[selectedPerson].DeathDate = deathDateInput;
                    database.Update("deathdate", listOfPersons[selectedPerson].DeathDate, listOfPersons[selectedPerson].Id);    
                    break;
                case 5:
                    Console.WriteLine("Mothers Firstname: ");
                    firstNameInput = Console.ReadLine();
                    Console.WriteLine("Mothers Lastname: ");
                    lastNameInput = Console.ReadLine();
                    listOfPersons[selectedPerson].MotherId = database.SetParent(firstNameInput, lastNameInput);
                    database.Update("motherId", listOfPersons[selectedPerson].MotherId.ToString(), listOfPersons[selectedPerson].Id);          
                    break;
                case 6:
                    Console.WriteLine("Fathers Firstname: ");
                    firstNameInput = Console.ReadLine();
                    Console.WriteLine("Fathers Lastname: ");
                    lastNameInput = Console.ReadLine();
                    listOfPersons[selectedPerson].FatherId = database.SetParent(firstNameInput, lastNameInput);
                    database.Update("motherId", listOfPersons[selectedPerson].FatherId.ToString(), listOfPersons[selectedPerson].Id);   
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("Wrong input, try again!");
                    break;
            }
        }

        private static void CreatePerson(List<Person> listOfPersons, SqlDatabase database)
        {
            var newPerson = new Person();
            string firstNameInput, lastNameInput;

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
            CreateListOfPeople(listOfPersons);
            database.CreateDatabase(database.DatabaseName);
            

            if (!database.DoesTableExist())
            {
                database.CreateTable();

                foreach (var person in listOfPersons)
                {
                    database.Create(person);
                }
            }
        }

        private static void CreateListOfPeople(List<Person> listOfPersons)
        {
            listOfPersons.Add(new Person(1, "Dennis", "Lindquist", "1988", "", 4, 5, 3));
            listOfPersons.Add(new Person(2, "Marielle", "Lunnan", "1989", "", 0, 0, 3));
            listOfPersons.Add(new Person(3, "Saga", "Lindquist", "2020", "", 2, 1, 0));
            listOfPersons.Add(new Person(4, "Katarina", "Lindquist", "1964", "", 7, 6, 1));
            listOfPersons.Add(new Person(5, "Mikael", "Lindh", "1965", "", 9, 8, 1));
            listOfPersons.Add(new Person(6, "Elis", "Lindquist", "1920", "2008", 0, 0, 4));
            listOfPersons.Add(new Person(7, "Gun", "Lindquist", "1941", "", 0, 0, 4));
            listOfPersons.Add(new Person(8, "Erik", "Lindh", "1920", "2000", 0, 0, 5));
            listOfPersons.Add(new Person(9, "Barbro", "Lindh", "1931", "", 0, 0, 5));

        }
    }
}
