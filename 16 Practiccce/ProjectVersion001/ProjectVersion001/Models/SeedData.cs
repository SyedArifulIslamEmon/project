using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectVersion001.Models
{
    public class SeedData : DropCreateDatabaseIfModelChanges<UnuversityDbContrex>
    {
        protected override void Seed(UnuversityDbContrex context)
        {

            //Semisters
            context.Semisters.Add(new Semister() {Semistername = "Fall 2013"});
            context.Semisters.Add(new Semister() { Semistername = "summer 2013" });
            context.Semisters.Add(new Semister() { Semistername = "Spring 2013" });
            context.Semisters.Add(new Semister() { Semistername = "Fall 2014" });
            context.Semisters.Add(new Semister() { Semistername = "Summer 2014" });
            context.Semisters.Add(new Semister() { Semistername = "spring 2014" });
            context.Semisters.Add(new Semister() { Semistername = "Fall 2015" });
            context.Semisters.Add(new Semister() { Semistername = "Summer 2015" });



            //Designation
            context.Designations.Add(new Designation(){DesignationName = "Professor"});
            context.Designations.Add(new Designation() { DesignationName = "asst. Professor" });
            context.Designations.Add(new Designation() { DesignationName = "Lecturer" });
            context.Designations.Add(new Designation() { DesignationName = "Asst Lecturer" });

            //Department
            context.Departments.Add(new Department(){Code = "CSE",Name = "Computer Science"});


            //

            

            context.SaveChanges();


        }
    }
}