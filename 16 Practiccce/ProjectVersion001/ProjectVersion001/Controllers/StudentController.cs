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
    public class StudentController : Controller
    {
        private UnuversityDbContrex db = new UnuversityDbContrex();

        // GET: Student
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentDepartment);
            return View(students.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                student.RegistrationNumber = GenerateStudentRegistrationNumber(student);
                ViewBag.registrationId = student.RegistrationNumber;
                db.Students.Add(student);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", student.DepartmentId);
                return View();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", student.DepartmentId);
            return View(student);
        }

        private string GenerateStudentRegistrationNumber(Student student)
        {
            string registrationId = GetDepartmentCode(student.DepartmentId) + "-" + student.Date.Year + "-";
            var result = db.Students.Where(s => (s.DepartmentId == student.DepartmentId && s.Date.Year == student.Date.Year)).ToList();

            if (!result.Any())
            {
                registrationId += "001";
            }
            else
            {
                string studentRegistrationId = (result.Last().RegistrationNumber);
                studentRegistrationId = (Convert.ToInt32(studentRegistrationId.Substring(studentRegistrationId.Length - 3)) + 1).ToString();

                int len = 3 - studentRegistrationId.Length;
                for (var i = 0; i < len; i++)
                {
                    studentRegistrationId = "0" + studentRegistrationId;
                }
                registrationId += studentRegistrationId;
            }
            return registrationId;
        }

        private string GetDepartmentCode(int departmentId)
        {
            var result =
                db.Departments.Where(d => d.DepartmentId == departmentId)
                    .Select(departments => new { departments.Code });

            return Enumerable.FirstOrDefault(result.Select(code => code.Code));
        }





        //public ActionResult Show([Bind(Include = "StudentId,Name,Email,Contact,Date,Address,RegistrationNumber,DepartmentId")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Students.Add(student);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", student.DepartmentId);
        //    return View(student);
        //}

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", student.DepartmentId);
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,Name,Email,Contact,Date,Address,RegistrationNumber,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", student.DepartmentId);
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
