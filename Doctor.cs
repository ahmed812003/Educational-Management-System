using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem
{
    public class Doctor : Human
    {
        private List<MainCourse> doctorCourses = new List<MainCourse>();

        public Doctor()
        {

        }

        public Doctor(string fname, string lname, int id, string username, string password) : base(fname, lname, id, username, password)
        {

        }

        public List<MainCourse> DoctorCourses
        {
            get
            {
                return this.doctorCourses;
            }
        }

        public MainCourse CreateCourse(string name , string id , string doctor)
        {
            MainCourse mainCourse = new MainCourse(name , id , this.FirstName);
            doctorCourses.Add(mainCourse);
            return mainCourse;
        }

        public void CreateNewAssignment (string assignment , MainCourse mainCourse)
        {
            mainCourse.AddAssignment(assignment);
        }

        public void CreateNewAssignment(string assignment, Course course)
        {
            course.AddAssignment(assignment);
        }

        public string GetReportOfAssignments (MainCourse mainCourse)
        {
            string report = mainCourse.GetGeneralAssignments();
            return report;
        }
        


    }
}
