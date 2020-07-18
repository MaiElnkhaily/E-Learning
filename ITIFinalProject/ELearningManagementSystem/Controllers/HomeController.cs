using ELearningManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearningManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        ELearning db;

        public HomeController()
        {
            db = new ELearning();
        }
        // GET: Home
        public ActionResult Home()
        {
            var news = db.News.ToList();
            var courses = db.Courses.Count<Cours>();
            var stds = db.Students.Count<Student>();
            var profs = db.Professors.Count<Professor>();
            TempData["count"] = courses;
            TempData["stds"] = stds;
            TempData["profs"] = profs;
            return View(news);
        }


        public ActionResult Login(string USerName,string Password,string Type)
        {
            if (Type == "Professor")
            {
                var prof = db.Professors.FirstOrDefault(ww => ww.UserName == USerName && ww.Password == Password);
                if (prof != null)
                {

                    Session.Add("uname", prof.PersonalName);
                    Session.Add("img", prof.Photo);
                    Session.Add("id", prof.Prof_Id);
                    Session.Add("Type", prof.Type);
                    //ViewBag.uname = prof.PersonalName;
                    //ViewBag.img = prof.Photo;
                    //ViewBag.id = prof.Prof_Id;
                    //ViewBag.Type = prof.Type;
                    return RedirectToAction("../professor/professorcourses", new { id = prof.Prof_Id });
                }
                else
                {
                    return RedirectToAction("Home");
                }
            }
            else if(Type == "Student")
            {
                var std = db.Students.FirstOrDefault(ww => ww.UserName == USerName && ww.Password == Password && ww.Type == Type);
                if (std != null)
                {
                    //ViewBag.uname = std.Personal_Name;
                    //ViewBag.img = std.Photo;
                    //ViewBag.id = std.Student_Code;
                    //ViewBag.Type = std.Type;
                    Session.Add("uname", std.Personal_Name);
                    Session.Add("img", std.Photo);
                    Session.Add("id", std.Student_Code);
                    Session.Add("Type", std.Type);
                    return RedirectToAction("../student/StudentCourses", new { id = std.Student_Code});
                }
                else
                {
                    
                    return RedirectToAction("home");
                }
            }
            else
            {
                var admin = db.Admins.FirstOrDefault(ww => ww.UserName == USerName && ww.Password == Password && ww.Type == Type);

                if (admin != null)
                {
                    Session.Add("uname", admin.PersonalName);
                    Session.Add("img", admin.Photo);
                    Session.Add("id", admin.Admin_Id);
                    Session.Add("Type", admin.Type);
                    return RedirectToAction("../admin/adminhome");
                }
                else
                {
                    return RedirectToAction("Home");
                }
            }
        }

    }
}