using Rayhan_EmployManagment.Models.ViewModels;
using Rayhan_EmployManagment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Rayhan_EmployManagment.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        private appDBcontex1 db = new appDBcontex1();
        public ActionResult Index(string sortOrder, string searchString, int? page, string currentFilter)
        {



            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.dateSortParam = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var students = db.Employees.OrderByDescending(e => e.EmployeeId).Include(x => x.QualificationEntries.Select(q => q.Qualification));

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.EmployeeName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.EmployeeName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.JoinDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.JoinDate);
                    break;
                default:
                    students = students.OrderBy(s => s.EmployeeName);
                    break;
            }
            int pageSize = 1;
            int pageNumber = page ?? 1;
            return View(students.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        public ActionResult AddNewQualification(int? id)
        {
            ViewBag.qualifications = new SelectList(db.Qualifications.ToList(), "QualificationId", "QualifcationName", (id != null) ? id.ToString() : "");
            return PartialView("_AddNewQualification");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel obJ, int[] qualificationId)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee
                {
                    EmployeeName = obJ.EmployeeName,
                    JoinDate = obJ.JoinDate,
                    Salary = obJ.Salary,
                    IsActive = obJ.IsActive,
                    Email = obJ.Email,


                };
                HttpPostedFileBase file = obJ.PicturePath;
                string filepath = Path.Combine("/Images", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                file.SaveAs(Server.MapPath(filepath));
                employee.Picture = filepath;
                foreach (var item in qualificationId)
                {
                    QualificationEntry qe = new QualificationEntry()
                    {
                        Employee = employee,
                        EmployeeId = employee.EmployeeId,
                        QualificationId = item


                    };
                    db.QualificationEntries.Add(qe);

                }
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View();

        }
        [HttpGet]

        public ActionResult Edit(int id)
        {
            Employee employee = db.Employees.First(x => x.EmployeeId == id);
            var qualifications = db.QualificationEntries.Where(x => x.EmployeeId == id).ToList();
            EmployeeViewModel obJ = new EmployeeViewModel()
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Salary = employee.Salary,
                JoinDate = employee.JoinDate,
                Picture = employee.Picture,
                IsActive = employee.IsActive,
                Email = employee.Email,

            };
            if (qualifications.Count > 0)
            {
                foreach (var item in qualifications)
                {
                    obJ.QualificationList.Add(item.QualificationId);


                }


            }
            return View(obJ);


        }
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel vObj, int[] qualificationId)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    EmployeeName = vObj.EmployeeName,
                    JoinDate = vObj.JoinDate,
                    Salary = vObj.Salary,
                    IsActive = vObj.IsActive,
                    Email = vObj.Email,
                    EmployeeId = vObj.EmployeeId
                };
                HttpPostedFileBase file = vObj.PicturePath;
                if (file != null)
                {
                    string filepath = Path.Combine("/Images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filepath));
                    employee.Picture = filepath;
                }
                else
                {
                    employee.Picture = vObj.Picture;
                }
                var existingQualification = db.QualificationEntries.Where(x => x.EmployeeId == employee.EmployeeId).ToList();
                foreach (var item in existingQualification)
                {
                    db.QualificationEntries.Remove(item);
                }
                foreach (var item in qualificationId)
                {
                    QualificationEntry qe = new QualificationEntry()
                    {
                        EmployeeId = employee.EmployeeId,
                        QualificationId = item
                    };
                    db.QualificationEntries.Add(qe);
                }
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Delete(int? id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee != null)
            {
                var existingQualifications = db.QualificationEntries.Where(x => x.EmployeeId == id).ToList();
                if (existingQualifications.Any())
                {
                    foreach (var item in existingQualifications)
                    {
                        db.QualificationEntries.Remove(item);
                    }
                }
                db.Entry(employee).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}