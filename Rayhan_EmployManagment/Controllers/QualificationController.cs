using Rayhan_EmployManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rayhan_EmployManagment.Controllers
{
    public class QualificationController : Controller
    {
        private appDBcontex1 db = new appDBcontex1();


        public ActionResult Index()
        {
            return View(db.Qualifications.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QualificationId,QualifcationName")] Qualification qualification)
        {
            if (ModelState.IsValid)
            {
                db.Qualifications.Add(qualification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(qualification);
        }

    }
}