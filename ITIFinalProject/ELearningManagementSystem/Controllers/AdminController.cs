using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using ELearningManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ELearningManagementSystem.Controllers
{

    //[NoBrowserCache]
    public class AdminController : Controller
    {
        ELearning db;
        public AdminController()
        {
            db = new ELearning();
        }
        public ActionResult adminHome()
        {
            return View();
        }

        public ActionResult getAllCourses()
        {

            var courses = (from c in db.Courses
                           join p in db.Professors on c.Prof_Id equals p.Prof_Id
                           join d in db.Departments on c.Dept_Id equals d.Dept_Id
                           where c.Visible == true
                           select c).ToList();
            return View(courses);
        }
        public ActionResult DeleteCourse(string id)
        {
            var course = db.Courses.Find(id);
            course.Visible = false;
            db.SaveChanges();
            return RedirectToAction("getAllCourses");
        }

        [HttpGet]
        public ActionResult EditCourse(string id)
        {



            var course = db.Courses.Find(id);
            ViewBag.depts = new SelectList(db.Departments.ToList(), "dept_id", "DeptName", course.Department.DeptName);
            ViewBag.profs = new SelectList(db.Professors.ToList(), "prof_id", "PersonalName", course.Professor.PersonalName);
            return View(course);
        }

        [HttpPost]
        public ActionResult Editcour(Cours c)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("getAllCourses");
            }
            else
            {
                ViewBag.depts = new SelectList(db.Departments.ToList(), "dept_id", "DeptName", c.Department.DeptName);
                ViewBag.profs = new SelectList(db.Professors.ToList(), "prof_id", "PersonalName", c.Professor.PersonalName);
                return View(c);
            }
        }

        [HttpGet]
        public ActionResult addCourse()
        {
            ViewBag.depts = new SelectList(db.Departments.ToList(), "dept_id", "DeptName", 1);
            ViewBag.profs = new SelectList(db.Professors.ToList(), "Prof_Id", "PersonalName", 1);
            return View();
        }

        [HttpPost]

        public ActionResult addCour(Cours c)
        {
            if (ModelState.IsValid)
            {
                
            var courses = db.Courses.Any(ww => ww.Course_Code == c.Course_Code);
            if (courses)
            {
                ViewBag.error = "This Id Already Exist!!";
                    ViewBag.depts = new SelectList(db.Departments.ToList(), "dept_id", "DeptName", 1);
                    ViewBag.profs = new SelectList(db.Professors.ToList(), "Prof_Id", "PersonalName", 1);
                    return View("addCourse");
            }
                c.Visible = true;
                db.Courses.Add(c);
                db.SaveChanges();
                return RedirectToAction("getAllCourses");
            }

            ViewBag.depts = new SelectList(db.Departments.ToList(), "dept_id", "DeptName", 1);
            ViewBag.profs = new SelectList(db.Professors.ToList(), "Prof_Id", "PersonalName", 1);
            return View("addCourse");

        }

        public ActionResult getAllProfessors()
        {
            var profs = (from p in db.Professors
                         join d in db.Departments on p.Dept_Id equals d.Dept_Id
                         where p.Visible == true
                         select p).ToList();
            return View(profs);
        }
        [HttpGet]
        public ActionResult AddProfessor()
        {
            ViewBag.depts = new SelectList(db.Departments.ToList(), "dept_id", "DeptName", 1);
            ViewBag.courses = new SelectList(db.Courses.ToList(), "course_code", "course_name", 1);
            return View();
        }

        [HttpPost]
        public ActionResult addprof(Professor p)
        {

            var exist = db.Professors.Any(ww => ww.UserName == p.UserName);

            if (exist)
            {
                ViewBag.error = "This UserName Is Already Exist!!";
                ViewBag.depts = new SelectList(db.Departments.ToList(), "Dept_Id", "DeptName", 1);
                ViewBag.courses = new SelectList(db.Courses.ToList(), "Course_Code", "Course_Name", 1);
                return View("AddProfessor");
            }
            p.Type = "Professor";
            p.HireDate = DateTime.Now;
            p.Admin_Id = 1;
            p.Photo = "user2.png";
            p.Visible = true;
            if (ModelState.IsValid)
            {

                db.Professors.Add(p);
                db.SaveChanges();
                return RedirectToAction("GetAllProfessors");
            }

            ViewBag.depts = new SelectList(db.Departments.ToList(), "dept_id", "DeptName", 1);
            ViewBag.courses = new SelectList(db.Courses.ToList(), "Course_Code", "Course_Name", 1);

            return View("AddProfessor");
        }

        [HttpGet]
        public ActionResult Deleteprofessor(int id)
        {
            var prof = db.Professors.Find(id);
            prof.Visible = false;
            prof.CPassword = prof.Password;
            db.SaveChanges();
            return RedirectToAction("GetAllProfessors");
        }

        [HttpGet]
        public ActionResult EditProfessor(int id)
        {
            var prof = db.Professors.Find(id);
            ViewBag.depts = new SelectList(db.Departments.ToList(), "Dept_id", "DeptName", prof.Dept_Id);
            return View(prof);
        }

        [HttpPost]
        public ActionResult EditProf(Professor p)
        {
            var check = db.Professors.Any(ww => ww.UserName == p.UserName && ww.Prof_Id != p.Prof_Id);
            if (check == true)
            {
                ViewBag.error = "This UserName Is Already Exist!!";
                ViewBag.depts = new SelectList(db.Departments.ToList(), "Dept_Id", "DeptName", p.Dept_Id);
                return View("EditProfessor",p);
            }
            if (ModelState.IsValid)
            {
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetAllProfessors");
            }
            ViewBag.depts = new SelectList(db.Departments.ToList(), "Dept_id", "DeptName", p.Dept_Id);
            return View();

        }

        public ActionResult Students()
        {
            var student = (from s in db.Students
                           where s.Visible == true
                           select s
                           ).ToList();
            return View(student);
        }

        public ActionResult DelStudent(int id)
        {
            var student = db.Students.FirstOrDefault(p => p.Student_Code == id);
            student.Visible = false;
            student.CPassword = student.Password;
            db.SaveChanges();
            return RedirectToAction("Students");
        }

        public ActionResult Add_Student()
        {
            ViewBag.DeptList = new SelectList(db.Departments.ToList(), "Dept_Id", "DeptName", 1);
            ViewBag.courses = new SelectList(db.Courses.ToList(), "Course_Code", "Course_Name", 1);
            return View();
        }

        [HttpPost]
        public ActionResult Add_Student(Student st, Student_Courses sc)
        {
            if (st.Photo == null)
            {
                st.Photo = "user2.png";
            }
            var exist = db.Students.Any(ww => ww.UserName == st.UserName);

            if (exist)
            {
                ViewBag.error = "This UserName Is Already Exist!!";
                ViewBag.DeptList = new SelectList(db.Departments.ToList(), "Dept_Id", "DeptName", 1);
                ViewBag.courses = new SelectList(db.Courses.ToList(), "Course_Code", "Course_Name", 1);
                return View();
            }

            st.Admin_Id = 1;
            st.Type = "Student";
            st.Visible = true;
            st.Attendence = 1;
            sc.Course_Code = st.Course;
            sc.Student_Code = st.Student_Code;
            db.Student_Courses.Add(sc);
            db.Students.Add(st);
            db.SaveChanges();

            return RedirectToAction("Students");
        }

        public ActionResult EditStudent(int id)
        {
            ViewBag.DeptList = new SelectList(db.Departments.ToList(), "Dept_Id", "DeptName", 1);
            return View(db.Students.Find(id));
        }

        [HttpPost]
        public ActionResult EditStudent(Student st)
        {
            var check = db.Students.Any(ww => ww.UserName == st.UserName && ww.Student_Code != st.Student_Code);
            if (check == true)
            {
                ViewBag.error = "This UserName Is Already Exist!!";
                ViewBag.DeptList = new SelectList(db.Departments.ToList(), "Dept_Id", "DeptName", st.Dept_Id);
                return View(st);
            }
            if (ModelState.IsValid)
            {
                db.Entry(st).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Students");
            }

            ViewBag.DeptList = new SelectList(db.Departments.ToList(), "Dept_Id", "DeptName", st.Dept_Id);
            return View(st);
        }

        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            var admin = db.Admins.Find(id);
            return View(admin);
        }

        [HttpPost]
        public ActionResult EditProfileAd(Admin admin, HttpPostedFileBase Poster)
        {
            var ad = db.Admins.Any(ww => ww.UserName == admin.UserName && ww.Admin_Id != admin.Admin_Id);

            if (ad)
            {
                ViewBag.error = "This UserName Is Already Exist!!";

                return View(admin);

            }

            if (ModelState.IsValid)
            {


                if (Poster != null && Poster.ContentLength > 0)
                {
                    Poster.SaveAs(Server.MapPath($"~/Images/{Poster.FileName}"));
                    admin.Photo = Poster.FileName;
                }

                if (admin.Photo == null)
                {
                    admin.Photo = "user2.png";
                }

                db.Entry(admin).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("AdminHome");
            }
            return View(admin);

        }
        public ActionResult Logout()
        {

            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            FormsAuthentication.SignOut();
            return RedirectToAction("../home/home");
        }
        public ActionResult Schedule()
        {
            var sched = new DHXScheduler(this);
            sched.Skin = DHXScheduler.Skins.Material;
            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            //sched.Config.isReadonly = true;                           ///disable click on event  make it read only
            sched.InitialDate = new DateTime();
            //sched.Config.readonly_form = true;                           ///enable click on event & open it  but can't editing it
            //sched.Config.displayed_event_color = "#green";
            //sched.Config.displayed_event_text_color = "red";


            return View(sched);
        }

        public ContentResult Data()
        {
            return (new SchedulerAjaxData(
                new ELearning().Events
                .Select(e => new { e.id, e.text, e.start_date, e.end_date })
                )
                );
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var changedEvent = DHXEventsHelper.Bind<Event>(actionValues);
            var entities = new ELearning();
            //try
            //{
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        entities.Events.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        changedEvent = entities.Events.FirstOrDefault(ev => ev.id == action.SourceId);
                        entities.Events.Remove(changedEvent);
                        break;
                    default:// "update"
                        var target = entities.Events.Single(e => e.id == changedEvent.id);
                        DHXEventsHelper.Update(target, changedEvent, new List<string> { "id" });
                        break;
                }
                entities.SaveChanges();
                action.TargetId = changedEvent.id;
            //}
            //catch (Exception a)
            //{
            //    action.Type = DataActionTypes.Error;
            //}

            return (new AjaxSaveResponse(action));
        }

        


    }
    //public class NoBrowserCache : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        // Haha ... tried everything I found on the web here: 
    //        // Make sure the requested page is not put in the browser cache. 
    //        filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //        filterContext.HttpContext.Response.Cache.SetNoStore();
    //        filterContext.HttpContext.Response.Cache.AppendCacheExtension("no-cache");
    //        filterContext.HttpContext.Response.Cache.SetExpires(DateTime.Now);
    //        filterContext.HttpContext.Response.Expires = 0;
    //    }
    //}



}