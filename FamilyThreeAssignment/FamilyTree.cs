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
        public void Start()
        {
            var listOfPersons = new List<Person>();
            var database = new SqlDatabase();
            Console.WriteLine("Hej");
            Setup(listOfPersons, database);
            MainMenu(listOfPersons, database);
        }
            /// <summary>
            /// The Main Menu.
            /// </summary>
            private static void MainMenu(List<Person> listOfPersons, SqlDatabase database)
            {
            bool mainMenuKeepGoing = true;

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
            bool selectKeepGoing = true;
            database.Read();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Which person do you wanna select?");
            Console.WriteLine("------------------------------------------------------------");
            int selectInput = Convert.ToInt32(Console.ReadLine());
            Person selectedPerson = listOfPersons[selectInput - 1];
            Console.WriteLine($"You Selected: {selectedPerson.Id}. {selectedPerson.FirstName} {selectedPerson.LastName}");
            do
            {
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
                        database.GetParent(selectedPerson.MotherId);
                        break;
                    case 2:
                        database.GetParent(selectedPerson.FatherId);
                        break;
                    case 3:
                        database.GetParent(selectedPerson.ChildId);
                        break;
                    case 0:
                        selectKeepGoing = false;
                        break;
                    default:
                        Console.WriteLine("Wrong input, try again!");
                        break;
                }
            } while (selectKeepGoing);
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
            Person selectedDelete = listOfPersons[inputId - 1];
            Console.WriteLine($"You Deleted: {selectedDelete.Id}. {selectedDelete.FirstName} {selectedDelete.LastName}");
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
            int updateInput = Convert.ToInt32(Console.ReadLine());
            Person selectedPerson = listOfPersons[updateInput - 1];
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine($"{selectedPerson.Id}. {selectedPerson.FirstName} {selectedPerson.LastName}");
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
                    selectedPerson.FirstName = firstNameInput;
                    database.Update("firstname", selectedPerson.FirstName, selectedPerson.Id);
                    break;
                case 2:
                    Console.WriteLine("Enter new Lastname: ");
                    lastNameInput = Console.ReadLine();
                    selectedPerson.LastName = lastNameInput;
                    database.Update("lastname", selectedPerson.LastName, selectedPerson.Id);
                    break;
                case 3:
                    Console.WriteLine("Enter new Birthdate: ");
                    birthDateInput = Console.ReadLine();
                    selectedPerson.BirthDate = birthDateInput;
                    database.Update("birthdate", selectedPerson.BirthDate, selectedPerson.Id);
                    break;
                case 4:
                    Console.WriteLine("Enter new Deathdate: ");
                    deathDateInput = Console.ReadLine();
                    selectedPerson.DeathDate = deathDateInput;
                    database.Update("deathdate", selectedPerson.DeathDate, selectedPerson.Id);    
                    break;
                case 5:
                    Console.WriteLine("Mothers Firstname: ");
                    firstNameInput = Console.ReadLine();
                    Console.WriteLine("Mothers Lastname: ");
                    lastNameInput = Console.ReadLine();
                    selectedPerson.MotherId = database.SetParent(firstNameInput, lastNameInput);
                    database.Update("motherId", selectedPerson.MotherId.ToString(), selectedPerson.Id);          
                    break;
                case 6:
                    Console.WriteLine("Fathers Firstname: ");
                    firstNameInput = Console.ReadLine();
                    Console.WriteLine("Fathers Lastname: ");
                    lastNameInput = Console.ReadLine();
                    selectedPerson.FatherId = database.SetParent(firstNameInput, lastNameInput);
                    database.Update("motherId", selectedPerson.FatherId.ToString(), selectedPerson.Id);   
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
