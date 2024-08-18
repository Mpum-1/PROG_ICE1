using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public abstract class Student
    {
        public string Name { get; set; }
        public string Course { get; set; }
        public string Department { get; set; }
        public int AcademicPerformance { get; set; }

        public Student(string name, string course, string department, int academicPerformance)
        {
            Name = name;
            Course = course;
            Department = department;
            AcademicPerformance = academicPerformance;
        }
    }
}