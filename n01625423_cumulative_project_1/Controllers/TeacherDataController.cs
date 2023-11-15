using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using n01625423_cumulative_project_1.Models;
using MySql.Data.MySqlClient;

namespace n01625423_cumulative_project_1.Controllers
{

    ///  Controller for access teacher table data from school database
    ///  A WebAPI Controller which allows you to access information about teachers
    public class TeacherDataController : ApiController
    {
        /// 1. Create context to access database.
        private SchoolDbContext TeacherData = new SchoolDbContext();

        /// <summary>
        ///  Access Data Through One String As Per 1st November Class.
        /// </summary>
        /// <returns>
        ///     <string>Name: Alexander Bennett Employeement ID: T378Salary: 55.30</string>
        ///<string>Name: Caitlin Cummings Employeement ID: T381Salary: 62.77</string>
        ///<string>Name: Linda Chan Employeement ID: T382Salary: 60.22</string>
        ///<string>Name: Lauren Smith Employeement ID: T385Salary: 74.20</string>
        ///<string>Name: Jessica Morris Employeement ID: T389Salary: 48.62</string>
        ///<string>Name: Thomas Hawkins Employeement ID: T393Salary: 54.45</string>
        ///<string>Name: Shannon Barton Employeement ID: T397Salary: 64.70</string>
        ///<string>Name: Dana Ford Employeement ID: T401Salary: 71.15</string>
        ///<string>Name: Cody Holland Employeement ID: T403Salary: 43.20</string>
        /// <string>Name: John Taram Employeement ID: T505Salary: 79.63</string>
        /// </returns>

        /*
            [HttpGet]
            public IEnumerable<string> ListTeachers()
            {
                /// 2. Create variable for connection 
                MySqlConnection Conn = TeacherData.DatabaseAccess();

                /// 3. Now establish connection between web application and database
                Conn.Open();

                /// 4. create variable for query -> to fetcg data from the teachers table
                MySqlCommand cmd = Conn.CreateCommand();

                /// 5. Sql query
                cmd.CommandText = "Select * From Teachers";

                /// 6. get results from the query
                MySqlDataReader ResultSet = cmd.ExecuteReader();

                /// 7. Create an empty list of Teacher 
                List<String> TeacherInfo = new List<string> { };

                /// 8. Loop Through Each Row the Result Set
                while (ResultSet.Read())
                {
                    /// Access Column information by the DB column name as an index
                    string TeacherDetails = "Name: " + ResultSet["teacherfname"] + " " + ResultSet["teacherlname"] + " & Employeement ID: " + ResultSet["employeenumber"] + " & Salary: " + ResultSet["salary"];

                    /// Add the Teacher Data to the List
                    TeacherInfo.Add(TeacherDetails);
                }

                /// 9. close connection
                Conn.Close();

                /// 10. return final output as teacher tabel details
                return TeacherInfo;
            }
        */

        [HttpGet]
        public IEnumerable<Teacher> ListTeachers()
        {
            /// 2. Make a connection variable
            MySqlConnection Conn = TeacherData.DatabaseAccess();

            /// 3. Open the connection between web server and database
            Conn.Open();

            /// 4. Establish connection through command
            MySqlCommand cmd = Conn.CreateCommand();

            /// 5. SQL Query For Get All Teacher Details from Teacher Table [DB school]
            cmd.CommandText = "Select * From Teachers";

            /// 6. Get Data From SQL Query as a Result Set
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            /// 7. As Multiple Data -> Create List  For  Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            /// 8. Loop for get data individually
            while (ResultSet.Read())
            {
                /// 9. Accss Data From The Datatable through column
                int TeacherID = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();
                string Salary = ResultSet["salary"].ToString();

                Teacher NewTemp = new Teacher();
                NewTemp.TeacherID = TeacherID;
                NewTemp.TeacherFName = TeacherFname;
                NewTemp.TeacherLName = TeacherLname;
                NewTemp.EmployeeNumber = EmployeeNumber;
                NewTemp.HireDate = HireDate;
                NewTemp.Salary = Salary;

                /// 10. Add TeacherData to the List variable
                Teachers.Add(NewTemp);
            }

            /// 11. Close the DB Connection
            Conn.Close();

            // return the final teacher data
            return Teachers;
        }

        /// <summary>
        /// Finds an teacher in the system given an ID
        /// </summary>
        /// <param name="id">The teacher primary key</param>
        /// <returns>An teacher object</returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher TeacherTemp = new Teacher();

            // Create variable for database connection
            MySqlConnection Conn = TeacherData.DatabaseAccess();

            // Connection Open
            Conn.Open();

            // Create Command for establish the connection between database and web server
            MySqlCommand cmd = Conn.CreateCommand();

            // Write a SQL Query
            cmd.CommandText = "Select * from Teachers where teacherid = " + id;

            // Get data from the teacher datatable through query variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                // Accss Data From The Datatable through column
                int TeacherID = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();
                string Salary = ResultSet["salary"].ToString();

                TeacherTemp.TeacherID = TeacherID;
                TeacherTemp.TeacherFName = TeacherFname;
                TeacherTemp.TeacherLName = TeacherLname;
                TeacherTemp.EmployeeNumber = EmployeeNumber;
                TeacherTemp.HireDate = HireDate;
                TeacherTemp.Salary = Salary;
            }

            /// Close the DB Connection
            Conn.Close();

            // Again Connection Open For Access Class Data
            Conn.Open();

            // Create Command for establish the connection between database and web server
            MySqlCommand cmd1 = Conn.CreateCommand();

            // Write a SQL Query
            cmd1.CommandText = "Select * from classes where teacherid = " + id;

            // Get data from the classes datatable through query variable
            MySqlDataReader ResultSet1 = cmd1.ExecuteReader();

            /// 7. As Multiple Data -> Create List For Courses
            List<Course> TeacherCourse = new List<Course> { };

            while (ResultSet1.Read())
            {
                // Accss Data From The Datatable through 
                string CourseCodes = ResultSet1["classcode"].ToString();
                string CourseName = ResultSet1["classname"].ToString();

                Course NewCourse = new Course();
                NewCourse.ClassCode = CourseCodes;
                NewCourse.ClassName = CourseName;

                /// Add CourseData to the List variable
                TeacherCourse.Add(NewCourse);
            }
            TeacherTemp.Courses = TeacherCourse;

            /// Close the DB Connection
            Conn.Close();

            // return individual teacher data
            return TeacherTemp;
        }
    }
}