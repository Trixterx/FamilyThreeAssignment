using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyThreeAssignment
{
    class CRUD
    {
        public string DatabaseName { get; set; } = "FamilyThree";
        public int MaxRows { get; set; } = 10;
        public string OrderBy { get; set; } = "lastName";
        public void Create(Person person)
        {

        }
        public void Delete(Person person)
        {

        }
        public bool DoesPErsonExist(string name)
        {
            return true;
        }
        public bool DoesPersonExist(int Id)
        {
            return true;
        }
        public void GetMother(Person person)
        {

        }
        public void GetFather(Person person)
        {

        }
       // public List<Person> List(string filter = "firstName LIKE @input", string paramValue){}

      //  public Person Read(string name){}

        public void Update(Person person)
        {

        }
    }
}
