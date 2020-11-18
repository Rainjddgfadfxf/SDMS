using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDMS.WebUI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult StudentManage()
        {
            return View();
        }
        public ActionResult DormitoryManage()
        {
            return View();
        }
        public ActionResult StaffManage()
        {
            return View();
        }
    }
}