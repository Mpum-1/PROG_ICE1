using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class PartTime : Student
    {
        public PartTime(string name, string course, string department, int academicPerformance)
            : base(name, course, department, academicPerformance) { }
    }
}
