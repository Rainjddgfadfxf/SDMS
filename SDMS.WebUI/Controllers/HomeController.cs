using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDMS.Domain.Abstract;
using SDMS.Domain.Concrete;
namespace SDMS.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IAdminsRepository adminsRepository;
        public HomeController(IAdminsRepository repository)
        {
            this.adminsRepository = repository;
        }
        public ActionResult Index()
        {
            IEnumerable<Admin> admins = adminsRepository.Admins;
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
    }
}