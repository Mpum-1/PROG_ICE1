using StudentManagementSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Datastructures
{
    public class StudentList<T> : IEnumerable<T> where T : Student
    {
        private List<T> _students;

        public StudentList()
        {
            _students = new List<T>();
        }

        public void Add(T student)
        {
            _students.Add(student);
        }

        public void Remove(T student)
        {
            _students.Remove(student);
        }
        public IEnumerator<T> GetEnumerator()
        {
            return _students.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public T this[int index]
        {
            get { return _students[index]; }
        }

        public int Count
        {
            get { return _students.Count; }
        }

        public List<T> GetTopPerformingStudents(int count)
        {
            return _students.OrderByDescending(s => s.AcademicPerformance).Take(count).ToList();
        }

        public static StudentList<T> operator +(StudentList<T> list1, StudentList<T> list2)
        {
            var mergedList = new StudentList<T>();
            mergedList._students.AddRange(list1._students);
            mergedList._students.AddRange(list2._students);
            return mergedList;
        }
        public void MergeDepartments(string fromDepartment, string toDepartment)
        {
            foreach (var student in _students)
            {
                if (student.Department.Equals(fromDepartment, StringComparison.OrdinalIgnoreCase))
                {
                    student.Department = toDepartment;
                }
            }
        }
    
        public List <T> FindName(string name)
        {
            return _students.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List <T> FilterByDepartment(string department)
        {
            return _students.Where(s => s.Department.Equals(department, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List<T> SortByPerformance(bool ascending = true)
        {
            return ascending
                ? _students.OrderBy(s => s.AcademicPerformance).ToList()
                : _students.OrderByDescending(s => s.AcademicPerformance).ToList();
        }
        public List<T> FilterByCourse(string course)
        {
            return _students.Where(s => s.Course.Equals(course, StringComparison.OrdinalIgnoreCase)).ToList();
        }

    }
}