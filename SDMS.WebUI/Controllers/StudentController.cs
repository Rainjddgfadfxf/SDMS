using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDMS.WebUI.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult RoomService()//寝室报修
        {
            return View();
        }
        public ActionResult RenInfo()//租赁查询
        {
            return View();
        }
        public ActionResult Utilities()//水电费
        {
            return View();
        }
    }
  
}