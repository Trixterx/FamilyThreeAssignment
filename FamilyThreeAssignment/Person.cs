﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FamilyThreeAssignment
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
    }
}
