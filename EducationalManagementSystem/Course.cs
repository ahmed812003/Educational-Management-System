using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem
{
    public class Course
    {

        private List<Pair<string , Pair<string , int>>> assignments = new List<Pair<string, Pair<string, int>>>();

        public string Name { get; set; }

        public string Id { get; set; }

        public string Doctor { get; set; }

        public List<Pair<string, Pair<string, int>>> Assignments
        {
            get
            {
                return this.assignments;
            }
        }

        public Course()
        {

        }
        public Course(string name, string id, string doctor)
        {
            this.Id = id;
            this.Name = name;
            this.Doctor = doctor;
        }

        public Course(MainCourse mainCourse)
        {
            this.Doctor = mainCourse.Doctor;
            this.Id = mainCourse.Id;
            this.Name= mainCourse.Name;
            foreach (var assignment in mainCourse.GeneralAssignments)
            {
                this.AddAssignment(assignment);
            }
        }

        public virtual void AddAssignment(string assignment)
        {
            Pair<string, Pair<string, int>> newAssignment = new Pair<string, Pair<string, int>>();
            newAssignment.Second = new Pair<string, int>();
            newAssignment.First = assignment;
            newAssignment.Second.First = "";
            newAssignment.Second.Second = 0;
            assignments.Add(newAssignment);
        }

        public virtual void AddAssignment(string assignment , string solution , int grade)
        {
            Pair<string, Pair<string, int>> newAssignment = new Pair<string, Pair<string, int>>();
            newAssignment.Second = new Pair<string, int>();
            newAssignment.First = assignment;
            newAssignment.Second.First = solution;
            newAssignment.Second.Second = grade;
            assignments.Add(newAssignment);
        }

        public void SumbitSolution(string solution, int indexOfAssignment)
        {
            this.assignments[indexOfAssignment].Second.First = solution;
        }

        public string GetGradeReport()
        {
            int TotalGrades = 0, Solved = 0;
            foreach(var assignment in this.assignments)
            {
                if(assignment.Second.Second != 0)
                {
                    Solved++;
                }
            }
            TotalGrades = Solved * 25;
            string report = $"Course code : {this.Id} - Total submitted {Solved} assignments - Grade {TotalGrades}/{this.assignments.Count * 25}";
            return report;
        }

        public string GetAssignmentsReport()
        {
            string report = "";
            foreach(var assignment in this.assignments)
            {
                report += assignment.First;
                if(assignment.Second.First == "")
                {
                    report += " Not submitted - ";
                }
                else
                {
                    report += " submitted - ";
                }
                if(assignment.Second.Second == 0)
                {
                    report += "NA / 25 \n";
                }
                else
                {
                    report += $"{Convert.ToString(assignment.Second.Second)} / 25 \n";
                }
            }
            return report;
        }

        public string CourseInfo()
        {
            string info = $"{this.Name}-{this.Id}-{this.Doctor}";
            foreach (var assignment in assignments)
            {
                info += "-";
                info += $"({assignment.First}*{assignment.Second.First}*{assignment.Second.Second})";
            }
            return info;
        }

        public override string ToString()
        {
            return $"Course Name : {this.Name} - Course code : {this.Id} \n" +
                   $"Course has {this.assignments.Count} assignment : \n" +
                   $"{this.GetAssignmentsReport()}";
        }

    }
}

