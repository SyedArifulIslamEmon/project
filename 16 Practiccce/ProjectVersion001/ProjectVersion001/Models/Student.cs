
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectVersion001.Models
{
    public class Student
    {

        public int StudentId { get; set; }
        [Display(Name = "Student Name")]
        public String Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Required!")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
        [Display(Name = "Contact Number")]
        public String Contact { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Address Must Be Given ")]
        public String Address { get; set; }
        public String RegistrationNumber { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department StudentDepartment { get; set; }




    }
}