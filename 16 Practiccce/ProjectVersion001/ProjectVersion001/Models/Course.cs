using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectVersion001.Models
{
    public class Course
    {
        public Course()
        {
            this.CourseTeacher=new Collection<Teacher>();
        }
        public int CourseId { get; set; }
    
        [Required(ErrorMessage = "There must be a Course Code\nSample: CSE-***")]
        public String Code { get; set; }
        [Required(ErrorMessage = "There must be a Course Name")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Required Field Must Be Filled")]
        public float Credit { get; set; }
        public String Description{ get; set; }
        public int SemisterId { get; set; }

        public virtual Semister CourseSemister { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department CourseDepartment{ get; set; }

        public virtual ICollection<Teacher> CourseTeacher{ get; set; }




        


    }
}