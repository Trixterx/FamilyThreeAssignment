using System;
using System.Data.SqlClient;

namespace FamilyThreeAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            CRUD crud = new CRUD();
            Person person = new Person();
            crud.Create(person);
        }
    }
}
