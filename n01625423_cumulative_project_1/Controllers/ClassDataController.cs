using MySql.Data.MySqlClient;
using n01625423_cumulative_project_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Web.Http;

namespace n01625423_cumulative_project_1.Controllers
{
    ///  Controller for access class table data from school database
    ///  A WebAPI Controller which allows you to access information about classes
    public class ClassDataController : ApiController
    {
            /// 1. Create context to access database.
            private SchoolDbContext ClassData = new SchoolDbContext();

            /// <summary>
            ///  Access Data From the class dataset.
            /// </summary>
            /// <returns>
            /// 
            /// </returns>

            [HttpGet]
            public IEnumerable<Course> ListClasses()
            {
                /// 2. Make a connection variable
                MySqlConnection Conn = ClassData.DatabaseAccess();

                /// 3. Open the connection between web server and database
                Conn.Open();

                /// 4. Establish connection through command
                MySqlCommand cmd = Conn.CreateCommand();

                /// 5. SQL Query For Get All Class Details from Classes Table [DB school]
                cmd.CommandText = "Select * From classes";

                /// 6. Get Data From SQL Query as a Result Set
                MySqlDataReader ResultSet = cmd.ExecuteReader();

                /// 7. As Multiple Data -> Create List  For  Classes
                List<Course> Classes = new List<Course> { };

                /// 8. Loop for get data individually
                while (ResultSet.Read())
                {
                    /// 9. Accss Data From The Datatable through column
                    int ClassID = (int)ResultSet["classid"];
                    string ClassCode = ResultSet["classcode"].ToString();
                    int TeacherID = (int)(long)ResultSet["teacherid"]; // Need to long because the data in database in bigint so if we use int only then it only gives data as a 32-bit so here we need to convert in long.
                    DateTime StartDate = (DateTime)ResultSet["startdate"];
                    DateTime EndDate = (DateTime)ResultSet["finishdate"];
                    string ClassName = ResultSet["classname"].ToString();

                    Course NewTemp = new Course();
                    NewTemp.ClassID = ClassID;
                    NewTemp.ClassCode = ClassCode;
                    NewTemp.TeacherId = TeacherID;
                    NewTemp.StartDate = StartDate;
                    NewTemp.FinishDate = EndDate;
                    NewTemp.ClassName = ClassName;

                    /// 10. Add ClassData to the List variable
                    Classes.Add(NewTemp);
                }

                /// 11. Close the DB Connection
                Conn.Close();

                // return the final classes data
                return Classes;
            }

            /// <summary>
            /// Finds an class in the system given an ID
            /// </summary>
            /// <param name="id">The class primary key</param>
            /// <returns>An class object</returns>
            [HttpGet]
            public Course FindClass(int id)
            {
                Course ClassTemp = new Course();

                // Create variable for database connection
                MySqlConnection Conn = ClassData.DatabaseAccess();

                // Connection Open
                Conn.Open();

                // Create Command for establish the connection between database and web server
                MySqlCommand cmd = Conn.CreateCommand();

                // Write a SQL Query
                cmd.CommandText = "Select * from Classes where classid = " + id;

                // Get data from the classes datatable through query variable
                MySqlDataReader ResultSet = cmd.ExecuteReader();

                while (ResultSet.Read())
                {
                    // Accss Data From The Datatable through column
                    int ClassID = (int)ResultSet["classid"];
                    string ClassCode = ResultSet["classcode"].ToString();
                    int TeacherID = (int)(long)ResultSet["teacherid"]; // Need to long because the data in database in bigint so if we use int only then it only gives data as a 32-bit so here we need to convert in long.
                    DateTime StartDate = (DateTime)ResultSet["startdate"];
                    DateTime EndDate = (DateTime)ResultSet["finishdate"];
                    string ClassName = ResultSet["classname"].ToString();

                    ClassTemp.ClassID = ClassID;
                    ClassTemp.ClassCode = ClassCode;
                    ClassTemp.TeacherId = TeacherID;
                    ClassTemp.StartDate = StartDate;
                    ClassTemp.FinishDate = EndDate;
                    ClassTemp.ClassName = ClassName;
                }

                /// Close the DB Connection
                Conn.Close();

            // Again Connection Open For Access Teacher Data
            Conn.Open();

            // Create Command for establish the connection between database and web server
            MySqlCommand cmd1 = Conn.CreateCommand();

            // Write a SQL Query
            cmd1.CommandText = "Select teacherfname,teacherlname from teachers where teacherid = " + id;

            // Get data from the classes datatable through query variable
            MySqlDataReader ResultSet1 = cmd1.ExecuteReader();

            while (ResultSet1.Read())
            {
                // Accss Data From The Datatable through 
                string TeacherFirstName = ResultSet1["teacherfname"].ToString();
                string TeacherLastName = ResultSet1["teacherlname"].ToString();

                ClassTemp.TeacherName = TeacherFirstName + " " + TeacherLastName;
            }

            /// Close the DB Connection
            Conn.Close();

            // return individual class data
            return ClassTemp;
            }
        }
    
}
