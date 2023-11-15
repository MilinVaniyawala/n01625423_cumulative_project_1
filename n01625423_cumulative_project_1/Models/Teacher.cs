using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace n01625423_cumulative_project_1.Models
{
    public class Teacher
    {
        /// Fields Which Describes Teacher Data 
        
        public int TeacherID;
        public string TeacherFName;
        public string TeacherLName;
        public string EmployeeNumber;
        public string HireDate;
        public string Salary;
        public List<Course> Courses;
    }
}