using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectVersion001.Models
{
    public class Teacher
    {
        public Teacher()
        {
            this.TeachersCourses = new Collection<Course>();
        }
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Fild mist be given")]
        public String Name { get; set; }
        public String Address { get; set; }
        [EmailAddress]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Must be a valid Email Address")]
        public String Email { get; set; }
        public String Contact { get; set; }
        public int DesignationId { get; set; }
        public float CreditToBeTaken { get; set; }

       // public float TotalCradit { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department TeacherDepartment { get; set; }

        public virtual Designation TeachersDesignations  { get; set; }

        public virtual ICollection<Course> TeachersCourses { get; set; }




    }
}