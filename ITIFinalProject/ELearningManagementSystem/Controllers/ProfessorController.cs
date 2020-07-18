using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using ELearningManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace ELearningManagementSystem.Controllers
{
    public class ProfessorController : Controller
    {
        ELearning db;
        public ProfessorController()
        {
            db = new ELearning();
        }
        // GET: Professor
        public ActionResult ProfessorCourses(int id)
        {
            var courses = db.Courses.Where(ww => ww.Prof_Id == id).ToList();
            return View(courses);
        }
        public ActionResult Delete_course(string Course_Code , string title)
        {
            var del_course = db.Exercises.FirstOrDefault(ww => ww.Course_Code == Course_Code && ww.Title == title);
            del_course.Is_Active = false;
            db.SaveChanges();
            return RedirectToAction("GetExercises");
        }

        public ActionResult CourseLectures(string id)
        {
            Session.Add("course_id", id);
            var lectures = db.Lectures.Where(ww => ww.Course_Code == id).ToList();
            return View(lectures);
        }

        [HttpGet]
        public ActionResult GetExercises()
        {
            var exercises = db.Exercises.Where(ww => ww.Is_Active == true).ToList();
            return View(exercises);
        }

        public ActionResult add_exersise()
        {
            ViewBag.courses = new SelectList(db.Courses.ToList(), "course_code", "course_name", 1);
            return View();
        }

        [HttpPost]
        public ActionResult add_exersise(Exercise e)
        {
            if (e.Type == "MCQ")
            {
                var cname = db.Courses.Find(e.Course_Code);
                Session.Add("type", e.Type);
                Session.Add("cname", cname.Course_Name);
                e.Is_Active = true;
                var quest = db.Exercises.Add(e);
                db.SaveChanges();
                return RedirectToAction("addQuestionsMCQ", new { id = e.Course_Code, title = e.Title });
            }
            else
            {
                var cname = db.Courses.Find(e.Course_Code);
                Session.Add("type", e.Type);
                Session.Add("cname", cname.Course_Name);
                e.Is_Active = true;
                var quest = db.Exercises.Add(e);
                db.SaveChanges();
                return RedirectToAction("addQuestionsTOF", new { id = e.Course_Code, title = e.Title });
            }
        }

        [HttpGet]
        public ActionResult addQuestionsMCQ(string id, string title)
        {
            Session.Add("c_Id", id);
            Session.Add("title", title);
            return View();
        }

        [HttpPost]
        public ActionResult addQuestionsMCQ(MCQ mCQ)
        {
            var mcq = db.MCQs.Add(mCQ);
            mcq.Cource_Code = Session["c_id"].ToString();
            mcq.Title_Id = Session["title"].ToString();
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult addQuestionsTOF(string id, string title)
        {
            Session.Add("c_Id", id);
            Session.Add("title", title);
            return View();

        }

        [HttpPost]
        public ActionResult addQuestionsTOF(True_False Tof)
        {
            var tof = db.True_False.Add(Tof);
            tof.Course_Id = Session["c_id"].ToString();
            tof.Title_Id = Session["title"].ToString();
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }
        [HttpGet]
        public ActionResult SaveCahnges()
        {
            return RedirectToAction("GetExercises");
        }

        [HttpGet]
        public ActionResult addLecture()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addLecture(Lecture l)
        {
            if (l.File != null)
            {
            String FileExt = Path.GetExtension(l.File.FileName).ToUpper();
                if (FileExt == ".PDF")
                {
                    if (l.File.ContentLength <= 0) return null;

                    Byte[] data = new byte[l.File.ContentLength];
                    l.File.InputStream.Read(data, 0, l.File.ContentLength);
                    l.PDF = l.File.FileName;
                    var path = Path.Combine(Server.MapPath("~/pdfs"), l.PDF);
                    l.File.SaveAs(path);
                    l.FileContent = data;

                }
            }
            if (l.Vid !=null)
            {
            String FileExtvid = Path.GetExtension(l.Vid.FileName).ToUpper();

            if (FileExtvid == ".MP4")
            {
                if (l.Vid.ContentLength <= 0) return null;
                var fileName = Path.GetFileName(l.Vid.FileName);
                if (fileName == null) return null;
                var path = Path.Combine(Server.MapPath("~/Videos"), fileName);
                l.Vid.SaveAs(path);
                l.Video = l.Vid.FileName;
            }
            }
            if (l.Aud != null)
            {
            String FileExtaud = Path.GetExtension(l.Aud.FileName).ToUpper();

            if (FileExtaud == ".MP3")
            {
                if (l.Aud.FileName == null) return null;
                var fileName = Path.GetFileName(l.Aud.FileName);
                if (fileName == null) return null;
                var path = Path.Combine(Server.MapPath("~/Audios"), fileName);
                l.Aud.SaveAs(path);
                l.Audio = l.Aud.FileName;
            }
            }
                l.Course_Code = Session["course_id"].ToString();
            if (l.PDF == null)
            {
                l.PDF = "";
            }
             if (l.Video == null)
            {
                l.Video = "";
            }
            if (l.Audio == null)
            {
                l.Audio = "";
            }
            db.Lectures.Add(l);
                var x = db.SaveChanges();
                if (x == 1)
                {
                    return RedirectToAction("/CourseLectures/", new { id = l.Course_Code });
                }

            return View("addLecture");



        }
        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            var prof = db.Professors.Find(id);
            Session.Add("t", prof.Type);
            Session.Add("a_id", prof.Admin_Id);
            Session.Add("hd", prof.HireDate);
            Session.Add("v", prof.Visible);
            //Session.Add("p", prof.Photo);
            ViewBag.DeptList = new SelectList(db.Departments.ToList(), "Dept_Id", "DeptName", prof.Dept_Id);
            return View(prof);
        }

        public ActionResult EditLecture(string c_id,string l_id)
        {
            var lec = db.Lectures.FirstOrDefault(ww => ww.Course_Code == c_id && ww.Lecture1 == l_id);
            return View(lec);

        }
        public ActionResult editLec(Lecture l)
        {
            if (l.File != null)
            {
                String FileExt = Path.GetExtension(l.File.FileName).ToUpper();
                if (FileExt == ".PDF")
                {
                    if (l.File.ContentLength <= 0) return null;

                    Byte[] data = new byte[l.File.ContentLength];
                    l.File.InputStream.Read(data, 0, l.File.ContentLength);
                    l.PDF = l.File.FileName;
                    var path = Path.Combine(Server.MapPath("~/pdfs"), l.PDF);
                    l.File.SaveAs(path);
                    l.FileContent = data;

                }
            }
            if (l.Vid != null)
            {
                String FileExtvid = Path.GetExtension(l.Vid.FileName).ToUpper();

                if (FileExtvid == ".MP4")
                {
                    if (l.Vid.ContentLength <= 0) return null;
                    var fileName = Path.GetFileName(l.Vid.FileName);
                    if (fileName == null) return null;
                    var path = Path.Combine(Server.MapPath("~/Videos"), fileName);
                    l.Vid.SaveAs(path);
                    l.Video = l.Vid.FileName;
                }
            }
            if (l.Aud != null)
            {
                String FileExtaud = Path.GetExtension(l.Aud.FileName).ToUpper();

                if (FileExtaud == ".MP3")
                {
                    if (l.Aud.FileName == null) return null;
                    var fileName = Path.GetFileName(l.Aud.FileName);
                    if (fileName == null) return null;
                    var path = Path.Combine(Server.MapPath("~/Audios"), fileName);
                    l.Aud.SaveAs(path);
                    l.Audio = l.Aud.FileName;
                }
            }
            l.Course_Code = Session["course_id"].ToString();
            db.Entry(l).State = EntityState.Modified;
            var x = db.SaveChanges();
            if (x == 1)
            {
                return RedirectToAction("/CourseLectures/", new { id = l.Course_Code });
            }

            return View("EditLecture", l);

        }

        public ActionResult deleteLecture(string c_id,string l_id)
        {
            var lec = db.Lectures.FirstOrDefault(ww => ww.Course_Code == c_id && ww.Lecture1 == l_id);
            db.Lectures.Remove(lec);
            db.SaveChanges();
            return RedirectToAction("CourseLectures", new { id = lec.Course_Code });
        }


        [HttpPost]
        public ActionResult EditProfile(Professor p, HttpPostedFileBase Photo)
        //public ActionResult EditProfile([Bind(Include = "PersonalName,Dept_Id,UserName,Dept_Id,Password")]Professor p, HttpPostedFileBase p_pic)

        {
            var ad = db.Professors.Any(ww => ww.UserName == p.UserName && ww.Prof_Id != p.Prof_Id);

            if (ad)
            {
                ViewBag.error = "This UserName Is Already Exist!!";
                ViewBag.DeptList = new SelectList(db.Departments.ToList(), "Dept_Id", "DeptName", p.Dept_Id);
                return View(p);

            }

            if (ModelState.IsValid)
            {

                if (Photo != null)
                {
                Photo.SaveAs(Server.MapPath($"~/Images/{Photo.FileName}"));
                p.Photo = Photo.FileName;

                }
                p.Type = Session["t"].ToString();
                p.HireDate = DateTime.Parse(Session["hd"].ToString());
                p.Admin_Id = int.Parse(Session["a_id"].ToString());
                p.Visible = bool.Parse(Session["v"].ToString());

                if (p.Photo == null)
                {
                    p.Photo = "user2.png";
                }
                db.Entry(p).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("ProfessorCourses",new { id = p.Prof_Id });
            }
            ViewBag.DeptList = new SelectList(db.Departments.ToList(), "Dept_Id", "DeptName", p.Dept_Id);
            return View(p);

            //if (ModelState.IsValid)
            //{
            //    var currentPerson = db.Professors.SingleOrDefault(o => o.Prof_Id == p.Prof_Id);
            //    if (currentPerson == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    //Poster.SaveAs(Server.MapPath($"~/Images/{Poster.FileName}"));
            //    //p.Photo = Poster.FileName;
            //    //db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            //    currentPerson.PersonalName = p.PersonalName;
            //    currentPerson.Dept_Id = p.Dept_Id;
            //    currentPerson.UserName = p.UserName;
            //    currentPerson.E_Mail = p.E_Mail;
            //    currentPerson.Password = p.Password;
            //    db.Entry(currentPerson).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction($"ProfessorCourses/{p.Prof_Id}");
            //}
            //ViewBag.DeptList = new SelectList(db.Departments.ToList(), "Dept_Id", "DeptName", p.Prof_Id);
            //return View(p);


        }

        public ActionResult Chat(int id)
        {
            var prof = db.Professors.Find(id);
            Session.Add("pho", prof.Photo);
            return View(prof);
        }

        public ActionResult Schedule()
        {
            var sched = new DHXScheduler(this);
            sched.Skin = DHXScheduler.Skins.Material;
            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            //sched.Config.isReadonly = true;                           ///disable click on event  make it read only
            sched.InitialDate = new DateTime();
            sched.Config.readonly_form = true;                           ///enable click on event & open it  but can't editing it
            //sched.Config.displayed_event_color = "#green";
            //sched.Config.displayed_event_text_color = "red";
            return View(sched);
        }

        public ContentResult Data()
        {
            return (new SchedulerAjaxData(
                db.Events
                .Select(e => new { e.id, e.text, e.start_date, e.end_date })
                )
                );
        }

        public ActionResult Logout()
        {

            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("../home/home");
        }



    }
}