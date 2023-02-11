using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem
{
    public class MainCourse : Course
    {

        private List<int> registeredStudents = new List<int>();

        private List<string> generalAssignments = new List<string>();

        public MainCourse()
        {

        }

        public MainCourse(string name , string id , string doctor)
        {
            this.Id = id;
            this.Name=name;
            this.Doctor= doctor;
        }

        public List<string> GeneralAssignments
        {
            get
            {
                return this.generalAssignments;
            }
        }

        public override void AddAssignment(string assignment)
        {
            this.generalAssignments.Add(assignment);
        }

        public void AddStudent(int id)
        {
            this.registeredStudents.Add(id);
        }

        public void RemoveStudent(int id)
        {
            this.registeredStudents.Remove(id);
        }
        
        public string registeredStudentsReport()
        {
            string report = "";
            foreach (var id in this.registeredStudents)
            {
                report += Convert.ToString(id);
                report += " ";
            }
            return report;
        }

        public string GetGeneralAssignments()
        {
            string report = "";
            foreach(var assignment in this.generalAssignments)
            {
                report += assignment;
                report += " ";
            }
            return report;
        }

        public override string ToString()
        {
            return $"Course name : {this.Name} - Code : {this.Id} - Doctor : {this.Doctor} \n" +
                   $"Registered students ID : {this.registeredStudentsReport()}"; 
        }

    }
}
