using FamilyThreeAssignment;
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
        public int MotherId { get; set; }
        public int FatherId { get; set; }

        public Person(int id, string firstName, string lastName, string birthDate, string deathDate, int motherId, int fatherId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            DeathDate = deathDate;
            MotherId = motherId;
            FatherId = fatherId;
        }

        public Person()
        {

        }
    }
}