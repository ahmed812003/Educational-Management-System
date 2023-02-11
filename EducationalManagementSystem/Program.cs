using EducationalManagementSystem;
using System;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;

namespace EductionalManagementSystem 
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Doctor> doctors = new List<Doctor>();
            doctors.Add(new Doctor("Ali", "Ahmed", 1, "ali", "11"));//0
            doctors.Add(new Doctor("Mostafa", "Ahmed", 2, "mostafa", "22"));//1
            doctors.Add(new Doctor("Hani", "Ahmed", 3, "hani", "33"));//2
            doctors.Add(new Doctor("Mohamed", "Ahmed", 4, "mohamed", "44"));//3
            doctors.Add(new Doctor("Ashraf", "Ahmed", 5, "ashraf", "55"));//4
            doctors.Add(new Doctor("Samy", "Ahmed", 6, "samy", "66"));//5
            doctors.Add(new Doctor("Morad", "Ahmed", 7, "morad", "77"));//6
            doctors.Add(new Doctor("Sayed", "Ahmed", 8, "sayed", "88"));//7
            doctors.Add(new Doctor("Hussien", "Ahmed", 9, "hussien", "99"));//8

            List<MainCourse> courses = new List<MainCourse>();
            courses.Add(doctors[5].CreateCourse("Prog1", "CS111", "Samy"));//0
            courses.Add(doctors[6].CreateCourse("Prog2", "CS112",  "Morad"));//1
            courses.Add(doctors[4].CreateCourse("Math1", "CS123", "Ashraf"));//2
            courses.Add(doctors[2].CreateCourse("Math2", "CS333", "Hani"));//3
            courses.Add(doctors[7].CreateCourse("Prog3", "CS136", "Sayed"));//4
            courses.Add(doctors[8].CreateCourse("Stat1", "CS240", "hussien"));//5
            courses.Add(doctors[6].CreateCourse("Stat2", "CS350", "Morad"));//6


            doctors[5].CreateNewAssignment("assignment 1", courses[0]);
            doctors[5].CreateNewAssignment("assignment 2", courses[0]);
            doctors[5].CreateNewAssignment("assignment 3", courses[0]);
            
            doctors[6].CreateNewAssignment("assignment 1", courses[1]);
            doctors[6].CreateNewAssignment("assignment 2", courses[1]);
            doctors[6].CreateNewAssignment("assignment 3", courses[1]);

            doctors[4].CreateNewAssignment("assignment 1", courses[2]);
            doctors[4].CreateNewAssignment("assignment 2", courses[2]);
            doctors[4].CreateNewAssignment("assignment 3", courses[2]);

            doctors[2].CreateNewAssignment("assignment 1", courses[3]);
            doctors[2].CreateNewAssignment("assignment 2", courses[3]);
            doctors[2].CreateNewAssignment("assignment 3", courses[3]);

            doctors[7].CreateNewAssignment("assignment 1", courses[4]);
            doctors[7].CreateNewAssignment("assignment 2", courses[4]);
            doctors[7].CreateNewAssignment("assignment 3", courses[4]);

            doctors[8].CreateNewAssignment("assignment 1", courses[5]);
            doctors[8].CreateNewAssignment("assignment 2", courses[5]);
            doctors[8].CreateNewAssignment("assignment 3", courses[5]);

            doctors[6].CreateNewAssignment("assignment 1", courses[6]);
            doctors[6].CreateNewAssignment("assignment 2", courses[6]);
            doctors[6].CreateNewAssignment("assignment 3", courses[6]);

            List<Student> students = new List<Student>();
            students.Add(new Student("Ahmed", "Yaser", 1, "ahmed2003", "11"));
            students.Add(new Student("Ahmed", "Awad", 2, "ahmed2001", "22"));
            students.Add(new Student("Ahmed", "Basser", 3, "basser2001", "33"));
            students.Add(new Student("islam", "mohsen", 4, "islam2001", "44"));

            Course c1 = new Course(courses[0]);
            Course c2 = new Course(courses[1]);
            Course c3 = new Course(courses[2]);
            Course c4 = new Course(courses[3]);
            Course c5 = new Course(courses[4]);
            Course c6 = new Course(courses[5]);
            Course c7 = new Course(courses[6]);
            Course c8 = new Course(courses[0]);
                        
            students[0].RegisterInCourse(c1);
            courses[0].AddStudent(1);
            students[0].RegisterInCourse(c2);
            courses[1].AddStudent(1);
            
            students[1].RegisterInCourse(c3);
            courses[2].AddStudent(2);
            students[1].RegisterInCourse(c4);
            courses[3].AddStudent(2);

            students[2].RegisterInCourse(c5);
            courses[4].AddStudent(3);
            students[2].RegisterInCourse(c6);
            courses[5].AddStudent(3);

            students[3].RegisterInCourse(c7);
            courses[6].AddStudent(4);
           
            //start point : 

            Painter gui = new Painter();
            gui.TitleOfProject();

            ChooseStudentOrDoctor:
            gui.ChooseStudentOrDoctor();
            int studentOrDoctor = Convert.ToInt32(Console.ReadLine());

            while (true)
            {
                if (studentOrDoctor == 1) // user is doctor
                {

                    LoginOrSigninMenu:
                    gui.LoginOrSigninMenu();
                    int Choice = Convert.ToInt32(Console.ReadLine());

                    // login
                    if (Choice == 1)
                    {
                        // main objects 
                        Doctor doctor;

                        // login info
                        gui.UserName();
                        string username = Console.ReadLine();
                        gui.Password();
                        string password = Console.ReadLine();

                        // find user 
                        bool ValidLogin = false; int IndexOfdoctor = -1;
                        for (int index = 0; index < doctors.Count; index++)
                        {
                            if (doctors[index].UserName == username && doctors[index].Password == password)
                            {
                                IndexOfdoctor = index;
                                ValidLogin = true;
                            }
                        }

                        // wrong in username or password 
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

                        // list courses
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
                        // view course
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

                                // add to general course
                                doctor.CreateNewAssignment(NewAssignment, doctor.DoctorCourses[ChoosenCourse - 1]);

                                // add to sub courses in each student
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

                                // Student Solutions of this assignment
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
                                // Set Grades
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
                            // back
                            else
                            {
                                goto DoctorMainMenu;
                            }

                            goto ViewCourseMenu;

                        }
                        // log out
                        else
                        {
                            goto LoginOrSigninMenu;
                        }
                        goto DoctorMainMenu;

                    }
                    // sign in
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
                        doctors.Add(new Doctor(fname, lname, students.Count + 1, username, password));
                        goto LoginOrSigninMenu;

                    }
                    // back
                    else if (Choice == 3)
                    {
                        goto ChooseStudentOrDoctor;
                    }
                    // shutdown
                    else {
                        gui.CloseSystem();
                        break;
                    }

                }
                // user is student
                else if (studentOrDoctor == 2) 
                {

                    LoginOrSigninMenu:
                    gui.LoginOrSigninMenu();
                    int Choice = Convert.ToInt32(Console.ReadLine());

                    // login
                    if (Choice == 1)
                    {
                        // main objects 
                        Student student;
                        
                        // login info
                        gui.UserName();
                        string username = Console.ReadLine();
                        gui.Password();
                        string password = Console.ReadLine();

                        // find user 
                        bool ValidLogin = false; int IndexOfStudent = -1;
                        for (int index = 0; index < students.Count; index++)
                        {
                            if (students[index].UserName == username && students[index].Password == password)
                            {
                                IndexOfStudent = index;
                                ValidLogin = true;
                            }
                        }
                        
                        // wrong in username or password 
                        if (!ValidLogin)
                        {
                            gui.InvalidLogin();
                            goto LoginOrSigninMenu;
                        }

                        // main student who run system now 
                        student = students[IndexOfStudent];
                        gui.ValidLogin(student.FirstName, student.LastName);

                        StudentMainMenu:
                        gui.StudentMainMenu();
                        int StudentChoice = Convert.ToInt32(Console.ReadLine());
                        
                        // register in course
                        if (StudentChoice == 1) 
                        {
                            
                            List<Course> StudentCourses = student.Courses;
                            List<MainCourse> UnRegisteredCourses = new List<MainCourse>();
                            foreach(var mainCourse in courses)
                            {
                                bool IsFound = false;
                                foreach(var item in StudentCourses)
                                {
                                    if(mainCourse.Id == item.Id)
                                    {
                                        IsFound = true;
                                        break;
                                    }
                                }
                                if(IsFound == false)
                                {
                                    UnRegisteredCourses.Add(mainCourse);
                                }
                            }

                            gui.ListOfUnRegisteredCourses(ref UnRegisteredCourses);
                            
                            // get id of wanted course 
                            gui.GetCodeToregisterToCourse();
                            string CodeOfCourse = Console.ReadLine();
                            int IndexOfCourse = -1;
                            for(int idx=0; idx<courses.Count; idx++)
                            {
                                if (courses[idx].Id == CodeOfCourse)
                                {
                                    IndexOfCourse = idx;
                                    break;
                                }
                            }

                            // register in course 
                            student.RegisterInCourse(new Course(courses[IndexOfCourse]));
                            courses[IndexOfCourse].AddStudent(student.Id);

                        }
                        // list my course
                        else if (StudentChoice == 2)  
                        {
                            gui.ListOfMyCoursesNewVersion(ref student);
                        }
                        // view course
                        else if (StudentChoice == 3) 
                        {
                            // list of courses
                            gui.ListOfMyCoursesNewVersion(ref student);
                            int ChoosenCourse = Convert.ToInt32(Console.ReadLine()) , IndexOfCourse =-1;
                            for(int idx=0; idx<courses.Count; idx++)
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

                            // unregister
                            if (StudentChoice_2 == 1)  
                            {
                                student.UnRegisterOfCourse(student.Courses[ChoosenCourse - 1], courses[IndexOfCourse]); 
                            }
                            // submit solution
                            else if (StudentChoice_2 == 2) 
                            {
                                gui.GetNumberOfAssignment(student.Courses[ChoosenCourse - 1].Assignments.Count);
                                int IndexOfAssignment = Convert.ToInt32(Console.ReadLine());
                                gui.GetSolutionOfAssignment();
                                string soltuion = Console.ReadLine();
                                student.SubmitAssignment(student.Courses[ChoosenCourse - 1], soltuion, IndexOfAssignment);
                            }
                            
                        }
                        // grades report
                        else if (StudentChoice == 4) 
                        {
                            gui.ReportOfGrades(ref student);
                        }
                        // log out
                        else if (StudentChoice == 5) 
                        {
                            goto LoginOrSigninMenu;
                        }

                        goto StudentMainMenu;

                    }
                    // sign in
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
                    // back
                    else if (Choice == 3)
                    {
                        goto ChooseStudentOrDoctor;
                    }
                    // shutdown
                    else
                    {
                        gui.CloseSystem();
                        break;
                    }
                
                }
            }
            Console.ReadKey();
        }
    }
}