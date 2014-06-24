using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectVersion001.Models;

namespace ProjectVersion001.Controllers
{
    public class CourseAssignController : Controller
    {
        private UnuversityDbContrex db = new UnuversityDbContrex();

        // GET: CourseAssign
        public ActionResult Index()
        {
            var courseAssigns = db.CourseAssigns.Include(c => c.Courses).Include(c => c.Departments).Include(c => c.Teachers);
            return View(courseAssigns.ToList());
        }

        // GET: CourseAssign/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseAssign = db.CourseAssigns.Find(id);
            if (courseAssign == null)
            {
                return HttpNotFound();
            }
            return View(courseAssign);
        }

        // GET: CourseAssign/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name");
            return View();
        }

        // POST: CourseAssign/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CourseAssignId,DepartmentId,TeacherId,CourseId")] CourseAssign courseAssign)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.CourseAssigns.Add(courseAssign);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", courseAssign.CourseId);
        //    ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", courseAssign.DepartmentId);
        //    ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", courseAssign.TeacherId);
        //    return View(courseAssign);
        //}

        public ActionResult Create(CourseAssign courseAssign)
        {
            var assignStatus = db.CourseAssigns.Any(u => u.CourseId == courseAssign.CourseId);
            if (assignStatus)
            {
                var teacherName =
                        db.CourseAssigns.Include(a => a.Teachers)
                            .Where(a => a.CourseId == courseAssign.CourseId)
                            .Select(a => a.Teachers.Name);

                var teacher = "";

                foreach (var name in teacherName)
                {
                    teacher = name;
                }

                TempData["error"] = "The Course is already Taken By " + teacher;
            }
            else
            {
                int teachersDepartment = 0, coursesDepartment = 0;
                var teacherDept = db.Teachers.Where(a => a.TeacherId == courseAssign.TeacherId).Select(a => a.DepartmentId);
                foreach (var i in teacherDept)
                {
                    teachersDepartment = i;
                }
                var courseDepartment = db.Courses.Where(a => a.CourseId == courseAssign.CourseId).Select(a => a.DepartmentId);
                foreach (var department in courseDepartment)
                {

                    coursesDepartment = department;
                }

                if (teachersDepartment == coursesDepartment)
                {
                    db.CourseAssigns.Add(courseAssign);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "This course is not valid for this department";
                }
            }


           


            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", courseAssign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", courseAssign.TeacherId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", courseAssign.CourseId);
            return View(courseAssign);
        }


        //public PartialViewResult TeacherFilteredSection(int? departmentId)
        //{
        //    if (departmentId != null)
        //    {
        //        ViewBag.Teachers = db.Teachers.Where(teacher => teacher.DepartmentId == departmentId);
        //        return PartialView("~/Views/Shared/_FilteredTeacherComboBox.cshtml");
        //    }
        //    else
        //    {
        //        return PartialView("~/Views/Shared/_FilteredTeacherComboBox.cshtml");
        //    }
        //}


        //public PartialViewResult FilteredTeachersCredit(int? teacherId)
        //{
        //    if (teacherId != null)
        //    {
        //        ViewBag.CreditToBeTaken =
        //           from a in db.CourseAssigns
        //           join c in db.Courses
        //               on a.CourseId equals c.CourseId
        //           where a.TeacherId == teacherId
        //           group c by a.TeacherId
        //               into g
        //               select new TotalSelectedCradit { Cradit = g.Sum(c => c.Credit) };




        //        ViewBag.AssigningCredit =
        //            db.Teachers.Where(teacher => teacher.TeacherId == teacherId).Select(teacher => teacher.TotalCreditToBeTaken);

        //        return PartialView("~/Views/Shared/_FilteredAssignCourse.cshtml");
        //    }
        //    else
        //    {
        //        return PartialView("~/Views/Shared/_FilteredAssignCourse.cshtml");
        //    }
        //}

   

    
   

        // GET: CourseAssign/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseAssign = db.CourseAssigns.Find(id);
            if (courseAssign == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", courseAssign.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", courseAssign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", courseAssign.TeacherId);
            return View(courseAssign);
        }

        // POST: CourseAssign/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseAssignId,DepartmentId,TeacherId,CourseId")] CourseAssign courseAssign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseAssign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", courseAssign.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", courseAssign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", courseAssign.TeacherId);
            return View(courseAssign);
        }

        // GET: CourseAssign/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseAssign = db.CourseAssigns.Find(id);
            if (courseAssign == null)
            {
                return HttpNotFound();
            }
            return View(courseAssign);
        }

        // POST: CourseAssign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseAssign courseAssign = db.CourseAssigns.Find(id);
            db.CourseAssigns.Remove(courseAssign);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
