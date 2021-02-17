using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FamilyTreeAssignment
{
    class SQLDatabase
    {
        public string DatabaseName { get; set; } = "Population";
        internal string ConnectionString { get; set; } = @"Data Source=.\SQLExpress;Integrated Security=true;database={0}";
        
        public void CreateTable()
        {
            try
            {
                var connString = string.Format(ConnectionString, DatabaseName);
                var conn = new SqlConnection(connString);
                conn.Open();
                var sql = "DROP TABLE People CREATE TABLE People(Id int IDENTITY(1,1) PRIMARY KEY, firstname varchar(50), lastname varchar(50), mother int, father int);";
                var cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table Created Successfully...");
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("exception occured while creating table:" + e.Message + "\t" + e.GetType());
            }
            Console.ReadKey();


            /*
            void CreateDatabase()
            {
            var db = new SQLDatabase();
            var sql = "CREATE DATABASE " + DatabaseName;
            db.ExecuteSQL(sql);
            }
            void CheckDatabase()
            {
                var db = new SQLDatabase();
                var myDB = "FamilyTree";
                if (!db.DoesDatabaseExist(myDB))
                {
                    CreateDatabase();
                }

            }
            */
        }
    }
}
