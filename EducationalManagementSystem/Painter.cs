using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem
{
    public class Painter
    {
        public void TitleOfProject()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t\tEductional management system");
            Console.WriteLine("\t\t\t\t\t----------------------------");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ChooseStudentOrDoctor()
        {
            Console.WriteLine("Please make choice : \n1 - Doctor \n2 - Student");
        }

        public void LoginOrSigninMenu()
        {
            Console.WriteLine("1 - Login \n2 - Sign in \n3 - Back \n4 - Shutdown System");
        }

        public void FirstName()
        {
            Console.WriteLine("Please enter first name :");
        }

        public void LastName()
        {
            Console.WriteLine("Please enter Last name :");
        }

        public void UserName()
        {
            Console.WriteLine("Please enter username :");
        }

        public void Password()
        {
            Console.WriteLine("Please enter password :");
        }
    
        public void InvalidLogin()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Sorry invalid login try again please");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    
        public void ValidLogin(string fname , string lname)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Welcome {fname} {lname}. You logged in");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    
        public void StudentMainMenu()
        {
            Console.WriteLine("1 - Register in course \n2 - List my courses \n3 - View course \n4 - Grades Report \n5 - Log out");
        }

        public void ListOfUnRegisteredCourses(ref List<MainCourse>UnRegisteredCourses)
        {
            int counter = 1;
            foreach (var course in UnRegisteredCourses)
            {
                Console.Write($"{counter++}) ");
                Console.Write($"Course name : {course.Name} - Code : {course.Id} - Doctor : {course.Doctor} \n");
            }
        }

        public void GetCodeToregisterToCourse()
        {
            Console.WriteLine("Please enter code of course You want to register in : ");
        }

        public void ListOfMyCourses(ref Student student)
        {
            int counter = 1;
            foreach(var course in student.Courses)
            {
                Console.Write($"{counter++}) ");
                Console.WriteLine(course);
            }
        }
    
        public void ReportOfGrades(ref Student student)
        {
            foreach(var course in student.Courses)
            {
                string CourseReport = course.GetGradeReport();
                Console.WriteLine(CourseReport);
            }
        }
    
        public void ViewCourseMenu()
        {
            Console.WriteLine("1 - UnResiter from course \n2 - Submit solution \n3 - Back");
        }

        public void ViewCourseMenu_2()
        {
            Console.WriteLine("1 - List Assignments \n2 - Create Assignment \n3 - View Assignment \n4 - Back");
        }

        public void ListOfMyCoursesNewVersion (ref Student student)
        {
            int counter = 1;
            foreach (var course in student.Courses)
            {
                Console.WriteLine($"{counter++}) Course Name : {course.Name} - Course Id : {course.Id}");                
            }
        }
         
        public void ViewCourse(Course course)
        {
            Console.WriteLine(course);
        }

        public void GetNumberOfAssignment(int numberOfAssignments)
        {
            Console.WriteLine($"Which ith [1 - {numberOfAssignments}] assignment to submit ?");
        }

        public void GetSolutionOfAssignment()
        {
            Console.WriteLine("Please enter solution :");
        }
    
        public void DoctorMainMenu()
        {
            Console.WriteLine("1 - List courses \n2 - Create course \n3 - View course \n4 - Log out");
        }

        public void ListOfDoctorCourses(Doctor doctor , ref List<MainCourse> mainCourses)
        {
            int counter = 1;
            foreach (var course in doctor.DoctorCourses)
            {
                Console.Write($"{counter++})");
                Console.WriteLine(course);
            }
        }

        public void NameOfCourse()
        {
            Console.WriteLine("Please enter course name :");
        }

        public void CodeOfCourse()
        {
            Console.WriteLine("Please enter course code : ");
        }

        public void ListOfAssignments(List<string> Assignments)
        {
            Console.WriteLine("Your Course Assignments : ");
            int counter = 1;
            foreach (var assignment in Assignments)
            {
                Console.Write($"{counter++}) {assignment} \n");
            }
        }

        public void GetNewAssignment()
        {
            Console.WriteLine("Please enter new assignment : ");
        }

        public void ViewAssignmentMenu()
        {
            Console.WriteLine("1 - Student Solutions of this assignment \n2 - Set Grades \n3 - back ");
        }

        public void PrintSolutions (string fname , int id , string solution)
        {
            Console.WriteLine($"Student Name : {fname} - Student Id : {id} - Student Solution : {solution} ");
        }
    
        public void CorrectSolution()
        {
            Console.WriteLine("Please enter correct solution :");
        }

        public void CloseSystem()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Thank you for using our system");
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
