using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDMS.Domain.Abstract;
using SDMS.Domain.Concrete;
using SDMS.Domain.Concrete.Model;
using SDMS.WebUI.Models;
using SSLS.WebUI.Models;

namespace SDMS.WebUI.Controllers
{
    public class StudentController : Controller
    {
        private IAdminsRepository adminsRepository;
        private IStudentRepository studentRepository;
        private ICommonRepository commonRepository;
        public StudentController(IAdminsRepository repository, IStudentRepository studentRepository, ICommonRepository commonRepository)
        {
            this.adminsRepository = repository;
            this.studentRepository = studentRepository;
            this.commonRepository = commonRepository;
        }
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
        //验证码
        public ContentResult CheckCode()
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "image/JPEG";//定义图片类型
            Random r = new Random();
            string code = r.Next(1000, 9999).ToString();//取随机数
            Session["check"] = code;
            Bitmap map = new Bitmap(60, 30);//定义大小
            Graphics g = Graphics.FromImage(map);//画图
            g.FillRectangle(Brushes.White, 1, 1, 58, 28);//定义矩形
            g.DrawString(code, new Font("微软雅黑", 16), Brushes.Black, new PointF(1, 1));//向矩形中绘入文字以及定义字体和大小
            for (int i = 0; i < 300; i++)
            {
                map.SetPixel(r.Next(1, 58), r.Next(1, 28), Color.Gray);
            }
            map.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);//保存到流中
            return cr;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl, string code)//登录，跳转自定
        {
            if (ModelState.IsValid)
            {
                if (Session["check"].ToString() == code)
                {
                    Student studentEntry = adminsRepository.Students.FirstOrDefault(c => c.Code == model.Id
                        && c.Password == model.Password);
                    if (studentEntry != null)
                    {
                        HttpContext.Session["Student"] = studentEntry;
                        TempData["success"] = "欢迎你," + studentEntry.Name;
                        return Redirect(returnUrl ?? Url.Action("Index", "Student"));
                    }
                    else
                    {
                        ModelState.AddModelError("pwderror", "用户名或密码不正确！");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "验证码不正确");
                    return View();
                }
            }
            return View();

        }
        public RedirectToRouteResult LoginOut()//退出
        {
            Session.Abandon();
            return RedirectToAction("Login", "Student");
        }

        public ActionResult SearchDorm(Student student)
        {
            return View(commonRepository.SearchDorm(student.DormNum));
        }
        [HttpPost]
        public ActionResult RoomService(string DormNum, string goods, string why)//寝室报修
        {
            int res = studentRepository.ReportForRepair(DormNum, goods, why, DateTime.Now);
            if (res == 1)
                TempData["success"] = "上报成功";
            else if (res == 2)
                TempData["error"] = "上报失败，数据库异常";
            //todo
            return View();//跳转你写
        }
        [HttpPost]
        public ActionResult RenInfo(string dormnum)//租赁查询，寝室号
        {
            IEnumerable<Lease> getleases = commonRepository.SearchLeases();
            IEnumerable<Lease> studentLeases = getleases.Where(e => e.DormNum == dormnum);
            return View(studentLeases);
        }
        [HttpPost]
        public ActionResult Utilities(string dormnum)//水电费，寝室号
        {
            DormDetail dormDetail = commonRepository.SearchDorm(dormnum);
            IEnumerable<WaterAndElectricity> waterAndElectricities = dormDetail.waterAndElectricity;
            return View(waterAndElectricities);
        }
        [HttpPost]
        public ActionResult SearchChangeDorm(string code)//换寝查询，学号
        {           
            return View(commonRepository.SearchChangeDorm(code));
        }
        /// <summary>
        /// 换寝时使用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SpareDorm()
        {
            return Json(commonRepository.SpareDorm());
        }
        /// <summary>
        /// 换寝申请
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangeDorm(Student student)
        {
            return View(new changeDormView
            {
                student = student,
                spareDorm = commonRepository.SpareDorm()
            }) ;
        }
        public ActionResult ChangeDorm(ChangeDormHelp model)
        {
            if (ModelState.IsValid)
            {
                int res = studentRepository.ChangeDorm(model.studentid, model.newDormNum, model.why);
                switch (res)
                {
                    case 1:
                        TempData["success"] = "提交成功";
                        break;
                    case 2:
                        TempData["error"] = "数据库异常，请重试";
                        break;
                    case 0:
                        TempData["error"] = "当前寝室床位不足，请重新选择";
                        break;
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        /// <summary>
        /// 维修上报
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public ActionResult ReportForRepair(Student student)
        {
            return View(new ReportForRepairHelp
            {
                DormNum = student.DormNum,
                good="",
                why=""
            });
        }
        public ActionResult ReportForRepair(ReportForRepairHelp model)
        {
            if (ModelState.IsValid)
            {
                int res = studentRepository.ReportForRepair(model.DormNum, model.good, model.why, DateTime.Now);
                switch (res)
                {
                    case 1:
                        TempData["success"] = "提交成功";
                        break;
                    case 2:
                        TempData["error"] = "数据库异常，请重试";
                        break;
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
  
}