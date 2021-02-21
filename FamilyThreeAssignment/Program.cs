using FamilyThreeAssignment;
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
            var family = new FamilyTree();
            family.Start();
        }
    }
}
