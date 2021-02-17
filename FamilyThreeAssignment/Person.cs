using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FamilyTreeAssignment
{
    class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string DeathDate { get; set; }
        public int Mother { get; set; }
        public int Father { get; set; }

        public Person(int id, string firstName, string lastName, string birthDate, string deathDate, int mother, int father)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            DeathDate = deathDate;
            Mother = mother;
            Father = father;
        }

        public Person()
        {

        }

        public string FullInfo
        {
            get
            {
                return $"{Id}. {FirstName} {LastName}";
            }
        }

    }
}