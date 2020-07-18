using ELearningManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Web.Security;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using DHTMLX.Common;

namespace ELearningManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        ELearning db;
        public StudentController()
        {
            db = new ELearning();
        }
        // GET: Student
        [HttpGet]
        public ActionResult StudentCourses(int id)
        {
            var courses = (from c in db.Courses
                           join sc in db.Student_Courses on c.Course_Code equals sc.Course_Code
                           where sc.Student_Code == id
                           select c).ToList();
         

            return View(courses);
        }
        [HttpGet]
        public ActionResult CourseLectures(string id)
        {
            var lectures = db.Lectures.Where(ww => ww.Course_Code == id).ToList();
            //List<MCQ> li = db.MCQs.ToList();
            //Queue<MCQ> queue = new Queue<MCQ>();
            //foreach (MCQ b in li)
            //{
            //    queue.Enqueue(b);
            //}
            //TempData["questions"] = queue;
            //TempData["score"] = 0;
            //TempData["wrong"] = 0;
            //TempData.Keep();
            return View(lectures);
        }

        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            var student = db.Students.Find(id);

            Session.Add("gender", student.Gender);
            Session.Add("admin_id", student.Admin_Id);
            Session.Add("attendance", student.Attendence);
            Session.Add("visible", student.Visible);
            Session.Add("type", student.Type);
            Session.Add("bd", student.BirthDate);


            ViewBag.DeptList = new SelectList(db.Departments.ToList(), "Dept_Id", "DeptName", student.Dept_Id);
            return View(student);
        }

        [HttpPost]
        public ActionResult EditProf(Student std, HttpPostedFileBase Poster)
        {
            var ad = db.Students.Any(ww => ww.UserName == std.UserName && ww.Student_Code != std.Student_Code);

            if (ad)
            {
                ViewBag.error = "This UserName Is Already Exist!!";
                ViewBag.DeptList = new SelectList(db.Departments.ToList(),"Dept_Id","DeptName",std.Dept_Id);
                return View("Editprofile",std);

            }


            if (ModelState.IsValid)
            {
                std.Gender = Session["gender"].ToString();
                std.Admin_Id = int.Parse(Session["admin_id"].ToString());
                std.Attendence = int.Parse(Session["attendance"].ToString());
                std.Visible = bool.Parse(Session["visible"].ToString());
                std.BirthDate =DateTime.Parse( Session["bd"].ToString());
                std.Type = Session["type"].ToString();

                if (Poster != null && Poster.ContentLength > 0)
                {
                    Poster.SaveAs(Server.MapPath($"~/Images/{Poster.FileName}"));
                    std.Photo = Poster.FileName;
                }
                if (std.Photo == null)
                {
                    std.Photo = "user2.png";
                }

                db.Entry(std).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction($"StudentCourses/{std.Student_Code}");
            }
            ViewBag.DeptList = new SelectList(db.Departments.ToList(), "Dept_Id", "DeptName", std.Dept_Id);
            return View("Editprofile", std);
        }

        //[HttpGet]
        //public ActionResult Result(int id,Result r)
        //{
        //    var std = db.Students.Find(id);
        //    ViewBag.id = std.Student_Code;
        //    ViewBag.pn = std.Personal_Name;
        //    ViewBag.dep = std.Department.DeptName;
        //    //var stds = db.Students.Any(ww => ww.Student_Code == id);

        //    r.Student_code = ViewBag.id;
        //    r.Exercises = Convert.ToInt32(TempData["result"]);
        //    r.Course_code = Session["course_id"].ToString();
        //    var title = Session["title_id"].ToString();
        //    var c = db.Courses.Find(r.Course_code);
        //    var q = db.Exercises.FirstOrDefault(ww => ww.Course_Code == r.Course_code && ww.Title == title);
        //    ViewBag.c_name = c.Course_Name;
        //    r.QType = q.Type;
        //    db.Results.Add(r);
        //    db.SaveChanges();
        //    var res = db.Results.Where(ww => ww.Student_code == id).ToList();

        //    return View(res);
        //}
       
        public ActionResult GetAllExercises()
        {
            var exercise = db.Exercises.Where(ww => ww.Is_Active == true).ToList(); 
            return View(exercise);
        }

        public ActionResult questions(string Title,string c_id)
        {
            var exe = db.Exercises.FirstOrDefault(ww => ww.Course_Code == c_id && ww.Title == Title);
            Session.Add("course_id", c_id);
            Session.Add("title_id", Title);

            if (exe.Type == "MCQ")
            {
                return RedirectToAction("Exe",new { t = Title , c = c_id });
            }
            else
            {
                

                return RedirectToAction("exeTOF",new { t = Title , c = c_id });

            }
        }

        public ActionResult Exe(string t , string c)
        {
            
            
            var quests = db.MCQs.Where(ww => ww.Title_Id == t && ww.Cource_Code == c).ToList();
            Queue<MCQ> queue = new Queue<MCQ>();
            foreach (MCQ item in quests)
            {
                queue.Enqueue(item);
            }
            TempData["questions"] = queue;
            TempData["score"] = 0;
            TempData["wrong"] = 0;
            TempData["tt"] = t;
            TempData["cc"] = c;


            TempData.Keep();
            ViewBag.title = t;
            return RedirectToAction("questsMCQ");

        }

        public ActionResult questsMCQ()
        {
            MCQ q = null;
            if (TempData["questions"] != null)
            {
                Queue<MCQ> qlist = (Queue<MCQ>)TempData["questions"];
                if (qlist.Count > 0)
                {
                    q = qlist.Peek();
                    qlist.Dequeue();
                    TempData["questions"] = qlist;
                    TempData.Keep();

                }
                else
                {
                    return RedirectToAction("Score");
                }
            }
            
            return PartialView(q);
        }
        [HttpPost]
        public ActionResult QuestionMCQ(string chosen,string correct)
        {
            var result = 0;
            string correctAns = null;
            if (chosen != null)
            {
                correctAns = chosen;
            }
            
            if (correctAns == correct)
            {
                TempData["score"] = Convert.ToInt32(TempData["score"]) + 1;
                result += Convert.ToInt32(TempData["score"]) * 5;
                TempData["result"] = result;
            }
            else
            {
                TempData["wrong"] = Convert.ToInt32(TempData["wrong"]) + 1;

            }
            TempData.Keep();
            return RedirectToAction("questsMCQ");
        }

        [HttpPost]
        public ActionResult QuestionTOF(string chosen, string correct)
        {
            int result = 0;
            int count = 0;
            string correctAns = null;
            if (chosen != null)
            {
                correctAns = chosen;
            }

            if (correctAns == correct)
            {
                TempData["score"] = Convert.ToInt32(TempData["score"]) + 1;
                result += Convert.ToInt32(TempData["score"]) * 5;
                TempData["result"] = result;
                
            }
            else
            {
                TempData["wrong"] = Convert.ToInt32(TempData["wrong"]) + 1;

            }
            count++;
            TempData["count"] = count*5;
            TempData.Keep();
            return RedirectToAction("questsTOF");
        }

        public ActionResult exeTOF(string t , string c)
        {
            var questsTof = db.True_False.Where(ww => ww.Title_Id == t && ww.Course_Id == c).ToList();
            Queue<True_False> queue = new Queue<True_False>();
            foreach (True_False item in questsTof)
            {
                queue.Enqueue(item);
            }
            TempData["questions"] = queue;
            TempData["score"] = 0;
            TempData["wrong"] = 0;
            TempData["tt"] = t;
            TempData["cc"] = c;
            TempData["result"] = 0;
            TempData["count"] = 0;

            TempData.Keep();
            return RedirectToAction("questsTOF");

        }
        public ActionResult questsTOF()
        {
            True_False q = null;
            if (TempData["questions"] != null)
            {
                Queue<True_False> qlist = (Queue<True_False>)TempData["questions"];
                if (qlist.Count > 0)
                {
                    q = qlist.Peek();
                    qlist.Dequeue();
                    TempData["questions"] = qlist;
                    TempData.Keep();

                }
                else
                {
                    return RedirectToAction("Score");
                }
            }

            return PartialView(q);
        }

        public ActionResult score()
        {
            return PartialView();
        }

        public ActionResult showvideo(string c , string l)
        {
            var video = db.Lectures.FirstOrDefault(ww => ww.Lecture1 == l && ww.Course_Code == c);
            return PartialView(video);
        }

        public FileResult DownloadPDF(string l , string c)
        {
            var pdf = db.Lectures.FirstOrDefault(ww => ww.Lecture1 == l && ww.Course_Code == c);
            string filepath = Server.MapPath($"~/pdfs/{pdf.PDF}");
            byte[] pdfByte = GetBytesFromFile(filepath);
            return File(pdfByte, "application/pdf", pdf.PDF);
        }
        public byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)
            FileStream fs = null;
            try
            {
                fs = System.IO.File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        public ActionResult Logout()
        {

            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            FormsAuthentication.SignOut();
            return RedirectToAction("../home/home");
        }

        public ActionResult Chat(int id)
        {
            var std = db.Students.Find(id);
            Session.Add("phostd", std.Photo);
            return View(std);
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
                .Select(e => new { e.id,e.text, e.start_date, e.end_date})
                )
                );
        }

        //public ContentResult Save(int? id, FormCollection actionValues)
        //{
        //    var action = new DataAction(actionValues);
        //    var changedEvent = DHXEventsHelper.Bind<Event>(actionValues);
        //    //var entities = new ELearning();
        //    try
        //    {
        //        switch (action.Type)
        //        {
        //            case DataActionTypes.Insert:
        //                db.Course_Schedule.Add(changedEvent);
        //                break;
        //            case DataActionTypes.Delete:
        //                changedEvent = db.Course_Schedule.FirstOrDefault(ev => ev.Id == action.SourceId);
        //                db.Course_Schedule.Remove(changedEvent);
        //                break;
        //            default:// "update"
        //                var target = db.Course_Schedule.Single(e => e.id == changedEvent.id);
        //                DHXEventsHelper.Update(target, changedEvent, new List<string> { "id" });
        //                break;
        //        }
        //        db.SaveChanges();
        //        action.TargetId = changedEvent.id;
        //    }
        //    catch (Exception a)
        //    {
        //        action.Type = DataActionTypes.Error;
        //    }

        //    return (new AjaxSaveResponse(action));
        //}


    }
}