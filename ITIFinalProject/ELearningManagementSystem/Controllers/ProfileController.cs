using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearningManagementSystem.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult EditProfile(int id, string Type)
        {


            if (Type == "Professor")
            {
                return RedirectToAction("../professor/EditProfile", new { id = id });
            }
            else if (Type == "Student")
            {
                return RedirectToAction("../Student/EditProfile", new { id = id });

            }
            else
            {
                return RedirectToAction("../admin/EditProfile", new { id = id });

            }


        }
    }
}