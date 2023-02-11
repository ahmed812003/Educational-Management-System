using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem
{
    public class Human
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public Human()
        {

        }

        public Human(string fname, string lname, int id, string username, string password)
        {
            this.FirstName = fname;
            this.LastName = lname;
            this.Id = id;
            this.UserName = username;
            this.Password = password;
        }
    
    }
}
