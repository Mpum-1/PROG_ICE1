using StudentManagementSystem.Datastructures;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem
{
    public class Menu
    {
        private StudentList<Student> studentList;

        public Menu()
        {
            studentList = new StudentList<Student>();
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---- IIEMSA Student Management System ----");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Merge Two Lists");
                Console.WriteLine("3. Compare Performance of Two Students");
                Console.WriteLine("4. Students");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        MergeLists();
                        break;
                    case "3":
                        CompareStudents();
                        break;
                    case "4":
                        ViewStudents();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void AddStudent()
        {
            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Course: ");
            string course = Console.ReadLine();

            Console.Write("Enter Department: ");
            string department = Console.ReadLine();

            Console.Write("Enter Academic Performance (0-100): ");
            if (int.TryParse(Console.ReadLine(), out int performance) && performance >= 0 && performance <= 100)
            {
                Console.Write("Enter Student Type (1- Undergraduate, 2- Postgraduate, 3- PartTime): ");
                string studentTypeInput = Console.ReadLine();
                Student student = null;

                switch (studentTypeInput)
                {
                    case "1":
                        student = new Undergraduate(name, course, department, performance);
                        break;
                    case "2":
                        student = new Postgraduate(name, course, department, performance);
                        break;
                    case "3":
                        student = new PartTime(name, course, department, performance);
                        break;
                    default:
                        Console.WriteLine("Invalid student type.");
                        return;
                }

                studentList.Add(student);
                Console.WriteLine("Student added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid performance value.");
            }
            Console.ReadKey();
        }

        private void MergeLists()
        {
            Console.Write("Enter the path to the second list file: ");
            string filePath = Console.ReadLine();

            // For simplicity, we assume the second list is loaded from a file or predefined.
            // You might need additional code to load this list from a file or another source.
            StudentList<Student> secondList = new StudentList<Student>();
            // Add your method to load students from the file or other source
            // Example: secondList = LoadStudentsFromFile(filePath);

            studentList += secondList;  // Merges the two lists
            Console.WriteLine("Lists merged successfully.");
            Console.ReadKey();
        }

        private void CompareStudents()
        {
            if (studentList.Count < 2)
            {
                Console.WriteLine("Not enough students to compare.");
            }
            else
            {
                Console.Write("Enter index of first student to compare: ");
                if (int.TryParse(Console.ReadLine(), out int index1) && index1 >= 0 && index1 < studentList.Count)
                {
                    Console.Write("Enter index of second student to compare: ");
                    if (int.TryParse(Console.ReadLine(), out int index2) && index2 >= 0 && index2 < studentList.Count)
                    {
                        Student student1 = studentList[index1];
                        Student student2 = studentList[index2];

                        if (student1.AcademicPerformance > student2.AcademicPerformance)
                        {
                            Console.WriteLine($"{student1.Name} has a better academic performance than {student2.Name}");
                        }
                        else if (student1.AcademicPerformance < student2.AcademicPerformance)
                        {
                            Console.WriteLine($"{student2.Name} has a better academic performance than {student1.Name}");
                        }
                        else
                        {
                            Console.WriteLine($"{student1.Name} and {student2.Name} have the same academic performance.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid index for the second student.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid index for the first student.");
                }
            }
            Console.ReadKey();
        }

        private void ViewTopPerformingStudents()
        {
            Console.Write("Enter the number of top-performing students to display: ");
            if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
            {
                var topStudents = studentList.GetTopPerformingStudents(count);

                Console.WriteLine("Top Performing Students:");
                foreach (var student in topStudents)
                {
                    Console.WriteLine($"{student.Name} - {student.Course} - {student.AcademicPerformance}");
                }
            }
            else
            {
                Console.WriteLine("Invalid number.");
            }
            Console.ReadKey();
        }

        private void ViewStudents()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---- View Students ----");
                Console.WriteLine("1. View All Students");
                Console.WriteLine("2. Search Students by Name");
                Console.WriteLine("3. Filter Students by Department");
                Console.WriteLine("4. Filter Students by Course");
                Console.WriteLine("5. Sort Students by Performance");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Choose an option: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ViewAllStudents();
                        break;
                    case "2":
                        SearchStudents();
                        break;
                    case "3":
                        FilterStudents();
                        break;
                    case "4":
                        FilterStudentsCourse();
                        break;
                    case "5":
                        SortStudents();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void ViewAllStudents()
        {
            Console.Clear();
            Console.WriteLine("---- All Students ----");

            if (studentList.Count == 0)
            {
                Console.WriteLine("No students available.");
            }
            else
            {
                for (int i = 0; i < studentList.Count; i++)
                {
                    Student student = studentList[i];
                    Console.WriteLine($"{i + 1}. {student.Name} - {student.Course} - {student.Department} - {student.AcademicPerformance}");
                }
            }
            Console.ReadKey();
        }

        private void SearchStudents()
        {
            Console.Write("Enter name to search: ");
            string name = Console.ReadLine();

            var results = studentList.FindName(name);
            if (results.Count == 0)
            {
                Console.WriteLine("No students found.");
            }
            else
            {
                Console.WriteLine("---- Search Results ----");
                foreach (var student in results)
                {
                    Console.WriteLine($"{student.Name} - {student.Course} - {student.Department} - {student.AcademicPerformance}");
                }
            }
            Console.ReadKey();
        }

        private void FilterStudents()
        {
            Console.Write("Enter department to filter by: ");
            string department = Console.ReadLine();

            var results = studentList.FilterByDepartment(department);
            if (results.Count == 0)
            {
                Console.WriteLine("No students found in the specified department.");
            }
            else
            {
                Console.WriteLine("---- Filter Results ----");
                foreach (var student in results)
                {
                    Console.WriteLine($"{student.Name} - {student.Course} - {student.Department} - {student.AcademicPerformance}");
                }
            }
            Console.ReadKey();
        }
        private void FilterStudentsCourse()
        {
            Console.Write("Enter course to filter by: ");
            string course = Console.ReadLine();

            var results = studentList.FilterByCourse(course);
            if (results.Count == 0)
            {
                Console.WriteLine("No students found in the specified department.");
            }
            else
            {
                Console.WriteLine("---- Filter Results ----");
                foreach (var student in results)
                {
                    Console.WriteLine($"{student.Name} - {student.Course} - {student.Department} - {student.AcademicPerformance}");
                }
            }
            Console.ReadKey();
        }

        private void SortStudents()
        {
            Console.WriteLine("Sort by academic performance:");
            Console.WriteLine("1. Ascending");
            Console.WriteLine("2. Descending");
            Console.Write("Choose an option: ");

            string input = Console.ReadLine();
            bool ascending = input == "1";

            var sortedStudents = studentList.SortByPerformance(ascending);

            Console.WriteLine("---- Sorted Students ----");
            foreach (var student in sortedStudents)
            {
                Console.WriteLine($"{student.Name} - {student.Course} - {student.Department} - {student.AcademicPerformance}");
            }
            Console.ReadKey();
        }
  
        
    }
}