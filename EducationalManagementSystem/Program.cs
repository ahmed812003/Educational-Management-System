using EducationalManagementSystem;
using System;
using System.Collections.Specialized;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.IO;

namespace EductionalManagementSystem 
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Doctor> doctors = new List<Doctor>();

            List<MainCourse> courses = new List<MainCourse>();
            
            List<Student> students = new List<Student>();

            // Read Doctors Data 
            string DoctorsFilePath = "F:\\Software Engineering\\mastering c#\\practice\\EducationalManagementSystem\\EducationalManagementSystem\\Doctors.txt";
            StreamReader Re = new StreamReader(DoctorsFilePath);
            string line = Re.ReadLine();
            while(line != null)
            {
                string[] array = line.Split('-');
                doctors.Add(new Doctor(array[0], array[1], Convert.ToInt32(array[2]), array[3] , array[4]));
                line = Re.ReadLine();
            }
            Re.Close();
            //end

            // Read Main Courses Data 
            string mainCoursesFilePath = "F:\\Software Engineering\\mastering c#\\practice\\EducationalManagementSystem\\EducationalManagementSystem\\MainCourses.txt";
            Re = new StreamReader(mainCoursesFilePath);
            line = Re.ReadLine();
            while (line != null)
            {
                string[] array = line.Split('-');
                MainCourse c = new MainCourse(array[0], array[1], array[2]);
                foreach(var doctor in doctors)
                {
                    if(doctor.FirstName == array[2])
                    {
                        doctor.AddCourse(c);
                    }
                }
                courses.Add(c);
                string StudentsFilePath = array[3];
                string AssignmentsFilePath = array[4];
                
                StreamReader Re2 = new StreamReader(StudentsFilePath);
                line = Re2.ReadLine();
                while(line != null)
                {
                    c.AddStudent(Convert.ToInt32(line));
                    line = Re2.ReadLine();
                }
                Re2.Close();
                Re2 = new StreamReader(AssignmentsFilePath);
                line = Re2.ReadLine();
                while (line != null)
                {
                    c.AddAssignment(line);
                    line = Re2.ReadLine();
                }
                Re2.Close();
                line = Re.ReadLine();
            }
            Re.Close();
            //end

            //Read Students Data
            string StudentsfilePath = "F:\\Software Engineering\\mastering c#\\practice\\EducationalManagementSystem\\EducationalManagementSystem\\Students.txt";
            Re = new StreamReader(StudentsfilePath);
            line = Re.ReadLine();
            while (line != null)
            {
                string[] array = line.Split("-");
                Student student = new Student(array[0], array[1], Convert.ToInt32(array[2]), array[3], array[4]);
                students.Add(student);
                
                // courses 
                string coursesPath = array[5];
                StreamReader Re2 = new StreamReader(coursesPath);
                line = Re2.ReadLine();
                while(line != null)
                {
                    string[] arr = line.Split("-");
                    Course c = new Course(arr[0], arr[1], arr[2]);
                    student.RegisterInCourse(c);
                    for(int idx=3; idx < arr.Length; idx++)
                    {
                        string assignment = arr[idx];
                        assignment = assignment.Substring(1, assignment.Length - 2);
                        string[] temp = assignment.Split('*');
                        c.AddAssignment(temp[0] , temp[1] , Convert.ToInt32(temp[2]));
                    }
                    line = Re2.ReadLine();
                }
                Re2.Close();
                //end
                line = Re.ReadLine();
            }
            Re.Close();
            //end


            //start point : 
            Painter gui = new Painter();
            gui.TitleOfProject();

            ChooseStudentOrDoctor:
            gui.ChooseStudentOrDoctor();
            int studentOrDoctor = Convert.ToInt32(Console.ReadLine());

            while (true)
            {
                //user is doctor
                if (studentOrDoctor == 1)
                {

                LoginOrSigninMenu:
                    gui.LoginOrSigninMenu();
                    int Choice = Convert.ToInt32(Console.ReadLine());

                    //login
                    if (Choice == 1)
                    {
                        //main objects
                        Doctor doctor;

                        //login info
                        gui.UserName();
                        string username = Console.ReadLine();
                        gui.Password();
                        string password = Console.ReadLine();

                        //find user
                        bool ValidLogin = false; int IndexOfdoctor = -1;
                        for (int index = 0; index < doctors.Count; index++)
                        {
                            if (doctors[index].UserName == username && doctors[index].Password == password)
                            {
                                IndexOfdoctor = index;
                                ValidLogin = true;
                            }
                        }

                        //wrong in username or password
                        if (!ValidLogin)
                        {
                            gui.InvalidLogin();
                            goto LoginOrSigninMenu;
                        }

                        // main doctor who run system now
                        doctor = doctors[IndexOfdoctor];
                        gui.ValidLogin(doctor.FirstName, doctor.LastName);

                    DoctorMainMenu:
                        gui.DoctorMainMenu();
                        int DoctorChoice = Convert.ToInt32(Console.ReadLine());

                        //list courses
                        if (DoctorChoice == 1)
                        {
                            gui.ListOfDoctorCourses(doctor, ref courses);
                        }
                        //create course
                        else if (DoctorChoice == 2)
                        {
                            gui.NameOfCourse();
                            string name = Console.ReadLine();
                            gui.CodeOfCourse();
                            string code = Console.ReadLine();
                            MainCourse NewCourse = doctor.CreateCourse(name, code, doctor.FirstName);
                            courses.Add(NewCourse);

                        }
                        //view course
                        else if (DoctorChoice == 3)
                        {
                            gui.ListOfDoctorCourses(doctor, ref courses);
                            int ChoosenCourse = Convert.ToInt32(Console.ReadLine());


                        ViewCourseMenu:
                            gui.ViewCourseMenu_2();
                            int DoctorChoice_2 = Convert.ToInt32(Console.ReadLine());
                            
                            //list assignment
                            if (DoctorChoice_2 == 1)
                            {
                                gui.ListOfAssignments(doctor.DoctorCourses[ChoosenCourse - 1].GeneralAssignments);
                            }
                            //create assignment
                            else if (DoctorChoice_2 == 2)
                            {
                                gui.GetNewAssignment();
                                string NewAssignment = Console.ReadLine();

                                //add to general course
                                doctor.CreateNewAssignment(NewAssignment, doctor.DoctorCourses[ChoosenCourse - 1]);

                                //add to sub courses in each student
                                foreach (var student in students)
                                {
                                    foreach (var course in student.Courses)
                                    {
                                        if (course.Id == doctor.DoctorCourses[ChoosenCourse - 1].Id)
                                        {
                                            doctor.CreateNewAssignment(NewAssignment, course);
                                        }
                                    }
                                }

                            }
                            //view assignment
                            else if (DoctorChoice_2 == 3)
                            {
                                gui.ListOfAssignments(doctor.DoctorCourses[ChoosenCourse - 1].GeneralAssignments);
                                gui.GetNumberOfAssignment(doctor.DoctorCourses[ChoosenCourse - 1].GeneralAssignments.Count);
                                int ChoosenAssignment = Convert.ToInt32(Console.ReadLine());

                            ViewAssignmentMenu:
                                gui.ViewAssignmentMenu();
                                int DoctorChoice_3 = Convert.ToInt32(Console.ReadLine());

                                //Student Solutions of this assignment
                                if (DoctorChoice_3 == 1)
                                {
                                    foreach (var student in students)
                                    {
                                        foreach (var course in student.Courses)
                                        {
                                            if (course.Id == doctor.DoctorCourses[ChoosenCourse - 1].Id)
                                            {
                                                gui.PrintSolutions(student.FirstName, student.Id, course.Assignments[ChoosenAssignment - 1].Second.First);
                                            }
                                        }
                                    }
                                }
                                //Set Grades
                                else if (DoctorChoice_3 == 2)
                                {
                                    gui.CorrectSolution();
                                    string solution = Console.ReadLine();
                                    foreach (var student in students)
                                    {
                                        foreach (var course in student.Courses)
                                        {
                                            if (course.Id == doctor.DoctorCourses[ChoosenCourse - 1].Id)
                                            {
                                                if (course.Assignments[ChoosenAssignment - 1].Second.First == solution)
                                                {
                                                    course.Assignments[ChoosenAssignment - 1].Second.Second = 25;
                                                }
                                            }
                                        }
                                    }
                                }
                                //back
                                else
                                {
                                    goto ViewCourseMenu;
                                }
                                goto ViewAssignmentMenu;

                            }
                            //back
                            else
                            {
                                goto DoctorMainMenu;
                            }

                            goto ViewCourseMenu;

                        }
                        //log out
                        else
                        {
                            goto LoginOrSigninMenu;
                        }
                        goto DoctorMainMenu;

                    }
                    //sign in
                    else if (Choice == 2)
                    {
                        gui.FirstName();
                        string fname = Console.ReadLine();
                        gui.LastName();
                        string lname = Console.ReadLine();
                        gui.UserName();
                        string username = Console.ReadLine();
                        gui.Password();
                        string password = Console.ReadLine();
                        doctors.Add(new Doctor(fname, lname, doctors.Count + 1, username, password));
                        goto LoginOrSigninMenu;

                    }
                    //back
                    else if (Choice == 3)
                    {
                        goto ChooseStudentOrDoctor;
                    }
                    //shutdown
                    else
                    {
                        gui.CloseSystem();
                        break;
                    }

                }
                //user is student
                else if (studentOrDoctor == 2)
                {

                LoginOrSigninMenu:
                    gui.LoginOrSigninMenu();
                    int Choice = Convert.ToInt32(Console.ReadLine());

                    //login
                    if (Choice == 1)
                    {
                        //main objects
                        Student student;

                        //login info
                        gui.UserName();
                        string username = Console.ReadLine();
                        gui.Password();
                        string password = Console.ReadLine();

                        //find user
                        bool ValidLogin = false; int IndexOfStudent = -1;
                        for (int index = 0; index < students.Count; index++)
                        {
                            if (students[index].UserName == username && students[index].Password == password)
                            {
                                IndexOfStudent = index;
                                ValidLogin = true;
                            }
                        }

                        //wrong in username or password
                        if (!ValidLogin)
                        {
                            gui.InvalidLogin();
                            goto LoginOrSigninMenu;
                        }

                        //main student who run system now
                        student = students[IndexOfStudent];
                        gui.ValidLogin(student.FirstName, student.LastName);

                    StudentMainMenu:
                        gui.StudentMainMenu();
                        int StudentChoice = Convert.ToInt32(Console.ReadLine());

                        //register in course
                        if (StudentChoice == 1)
                        {

                            List<Course> StudentCourses = student.Courses;
                            List<MainCourse> UnRegisteredCourses = new List<MainCourse>();
                            foreach (var mainCourse in courses)
                            {
                                bool IsFound = false;
                                foreach (var item in StudentCourses)
                                {
                                    if (mainCourse.Id == item.Id)
                                    {
                                        IsFound = true;
                                        break;
                                    }
                                }
                                if (IsFound == false)
                                {
                                    UnRegisteredCourses.Add(mainCourse);
                                }
                            }

                            gui.ListOfUnRegisteredCourses(ref UnRegisteredCourses);

                            //get id of wanted course
                            gui.GetCodeToregisterToCourse();
                            string CodeOfCourse = Console.ReadLine();
                            int IndexOfCourse = -1;
                            for (int idx = 0; idx < courses.Count; idx++)
                            {
                                if (courses[idx].Id == CodeOfCourse)
                                {
                                    IndexOfCourse = idx;
                                    break;
                                }
                            }

                            //register in course
                            student.RegisterInCourse(new Course(courses[IndexOfCourse]));
                            courses[IndexOfCourse].AddStudent(student.Id);

                        }
                        //list my course
                        else if (StudentChoice == 2)
                        {
                            gui.ListOfMyCoursesNewVersion(ref student);
                        }
                        //view course
                        else if (StudentChoice == 3)
                        {
                            //list of courses
                            gui.ListOfMyCoursesNewVersion(ref student);
                            int ChoosenCourse = Convert.ToInt32(Console.ReadLine()), IndexOfCourse = -1;
                            for (int idx = 0; idx < courses.Count; idx++)
                            {
                                if (courses[idx].Id == student.Courses[ChoosenCourse - 1].Id)
                                {
                                    IndexOfCourse = idx;
                                    break;
                                }
                            }


                            gui.ViewCourse(student.Courses[ChoosenCourse - 1]);
                            gui.ViewCourseMenu();
                            int StudentChoice_2 = Convert.ToInt32(Console.ReadLine());

                            //unregister
                            if (StudentChoice_2 == 1)
                            {
                                student.UnRegisterOfCourse(student.Courses[ChoosenCourse - 1], courses[IndexOfCourse]);
                            }
                            //submit solution
                            else if (StudentChoice_2 == 2)
                            {
                                gui.GetNumberOfAssignment(student.Courses[ChoosenCourse - 1].Assignments.Count);
                                int IndexOfAssignment = Convert.ToInt32(Console.ReadLine());
                                gui.GetSolutionOfAssignment();
                                string soltuion = Console.ReadLine();
                                student.SubmitAssignment(student.Courses[ChoosenCourse - 1], soltuion, IndexOfAssignment);
                            }

                        }
                        //grades report
                        else if (StudentChoice == 4)
                        {
                            gui.ReportOfGrades(ref student);
                        }
                        //log out
                        else if (StudentChoice == 5)
                        {
                            goto LoginOrSigninMenu;
                        }

                        goto StudentMainMenu;

                    }
                    //sign in
                    else if (Choice == 2)
                    {

                        gui.FirstName();
                        string fname = Console.ReadLine();
                        gui.LastName();
                        string lname = Console.ReadLine();
                        gui.UserName();
                        string username = Console.ReadLine();
                        gui.Password();
                        string password = Console.ReadLine();
                        students.Add(new Student(fname, lname, students.Count + 1, username, password));
                        goto LoginOrSigninMenu;

                    }
                    //back
                    else if (Choice == 3)
                    {
                        goto ChooseStudentOrDoctor;
                    }
                    //shutdown
                    else
                    {
                        gui.CloseSystem();
                        break;
                    }

                }
            }
            //end


            string GeneralPath = "F:\\Software Engineering\\mastering c#\\practice\\EducationalManagementSystem\\EducationalManagementSystem";

            //Write Students Informations
            string Path = GeneralPath;
            Path += "\\Students.txt";
            List<string> StudentsInfo = new List<string>();
            foreach (var student in students)
            {
                string info = student.StudentInfo();
                StudentsInfo.Add(info);

                string studentCoursesPath = student.StudentCoursesPath();
                List<string> CourseInfo = new List<string>();
                foreach(var course in student.Courses)
                {
                    info = course.CourseInfo();
                    CourseInfo.Add(info);
                }
                File.WriteAllLines(studentCoursesPath, CourseInfo.ToArray());
            }
            File.WriteAllLines(Path,StudentsInfo.ToArray());


            // write Doctors Informations
            Path = GeneralPath;
            Path += "\\Doctors.txt";
            List<string> DoctorsInfo = new List<string>();
            foreach(var doctor in doctors)
            {
                string info = doctor.DoctorInfo();
                DoctorsInfo.Add(info);
            }
            File.WriteAllLines(Path, DoctorsInfo.ToArray());


            //write MainCourses Informations 
            Path= GeneralPath;
            Path += "\\MainCourses.txt";
            List<string> MaibCourseInfo = new List<string>();
            foreach(var mainCourse in courses)
            {
                string info = mainCourse.MainCourseInfo();
                MaibCourseInfo.Add(info);

                info = mainCourse.AssignmentsPath();
                List<string> AssignmentsInfo = new List<string>();
                foreach (var item in mainCourse.GeneralAssignments)
                {
                    AssignmentsInfo.Add(item);
                }
                File.WriteAllLines(info, AssignmentsInfo.ToArray());

                info = mainCourse.StudentsPath();
                List<string> StudentInfo = new List<string>();
                foreach (var item in mainCourse.RegisteredStudents)
                {
                    StudentInfo.Add(Convert.ToString(item));
                }
                File.WriteAllLines(info, StudentInfo.ToArray());

            }
            File.WriteAllLines(Path, MaibCourseInfo.ToArray());

            Console.ReadKey();
        }
    }
}