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
                Console.WriteLine("4. View Top Performing Students");
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
                        ViewTopPerformingStudents();
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
                // Assuming different student types: Undergraduate, Postgraduate, PartTime
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
            // Create a new list to merge with the current one
            StudentList<Student> secondList = new StudentList<Student>();

            Console.WriteLine("Creating a new list to merge with the current one.");
            Console.Write("Enter the number of students to add to the second list: ");
            if (int.TryParse(Console.ReadLine(), out int numberOfStudents) && numberOfStudents > 0)
            {
                for (int i = 0; i < numberOfStudents; i++)
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
                                continue;
                        }

                        secondList.Add(student);
                    }
                    else
                    {
                        Console.WriteLine("Invalid performance value.");
                    }
                }

                studentList += secondList;  // Merge the two lists
                Console.WriteLine("Lists merged successfully.");
            }
            else
            {
                Console.WriteLine("Invalid number of students.");
            }
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
    }
}
