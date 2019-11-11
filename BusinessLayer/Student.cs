using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Student.BusinessLayer
{
    //Represents a student 
    public class Student
    {
        //Primary key
        public int Id { get; set; }

        //Student name
        public string Name { get; set; }

        //Studne contact number
        public string ContactNumber { get; set; }

        //Student registration Id
        public string RegistrationId {
            get {
                return "STU0000" + this.Id;
            }
        }
    }
}
