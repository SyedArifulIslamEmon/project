using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectVersion001.Models
{
    public class CourseAssign
    {
        public int CourseAssignId { get; set; }

        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Required Field Must Be Filled")]
        public virtual Department Departments  { get; set; }

        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Required Field Must Be Filled")]
        public virtual Teacher Teachers  { get; set; }
        [Required(ErrorMessage = "Required Field Must Be Filled")]
        public int CourseId { get; set; }
        public virtual  Course Courses  { get; set; }





    }
}