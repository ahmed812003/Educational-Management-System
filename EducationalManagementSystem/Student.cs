using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem
{
    public class Student : Human
    {

        private List<Course> courses = new List<Course>();

        public Student()
        {
                
        }

        public Student(string fname , string lname , int id , string username , string password) : base(fname , lname , id , username , password)
        {

        }

        public List<Course> Courses
        { 
            get 
            { 
                return this.courses;
            } 
        }
        
        public void RegisterInCourse (Course course)
        {
            courses.Add(course);
        }

        public void UnRegisterOfCourse (Course course , MainCourse mainCourse)
        {
            for(int i=0; i<courses.Count; i++)
            {
                if(course.Id == courses[i].Id)
                {
                    courses.Remove(courses[i]);
                }
            }
            mainCourse.RemoveStudent(this.Id);
        }

        public void SubmitAssignment(Course course, string solution, int indexOfAssignment)
        {
            course.SumbitSolution(solution, indexOfAssignment-1);
        }

        public string GetCoursesList()
        {
            string CoursesList = "";
            foreach(var course in this.courses)
            {
                CoursesList += course.Id;
                CoursesList += " ";
            }
            return CoursesList;
        }

        public string GradeReport()
        {
            string report = " ";
            foreach(var course in this.courses)
            {
                report += course.GetGradeReport();
                report += '\n';
            }
            return report;
        }

        public string StudentCoursesPath()
        {
            string path = $"F:\\Software Engineering\\mastering c#\\practice\\EducationalManagementSystem\\EducationalManagementSystem\\students courses files\\{this.Id}.txt";
            return path;
        }
        
        public string StudentInfo()
        {
            string info = $"{this.FirstName}-{this.LastName}-{this.Id}-{this.UserName}-{this.Password}-{this.StudentCoursesPath()}";
            return info;
        }

        public override string ToString()
        {
            return $"Student Name : {this.FirstName} {this.LastName}" +
                   $"Student Id : {this.Id}" +
                   $"Courses List : {this.GetCoursesList()}";
        }

    }
}
