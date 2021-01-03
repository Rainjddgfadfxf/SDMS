using SDMS.Domain.Abstract;
using SDMS.Domain.Concrete;
using SSLS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDMS.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IAdminsRepository adminsRepository;
        private IStudentRepository studentRepository;
        private ICommonRepository commonRepository;
        public AdminController(IAdminsRepository repository, IStudentRepository studentRepository, ICommonRepository commonRepository)
        {
            this.adminsRepository = repository;
            this.studentRepository = studentRepository;
            this.commonRepository = commonRepository;
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
                    Admin adminEntry = adminsRepository.Admins.FirstOrDefault(c => c.Name == model.Id
                        && c.PassWord == model.Password);
                    if (adminEntry != null)
                    {
                        HttpContext.Session["Student"] = adminEntry;
                        TempData["success"] = "欢迎你," + adminEntry.Name;
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
        //退出
        public RedirectToRouteResult LoginOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }

        /// <summary>
        /// 新增修改学生，界面跳转你改 
        /// </summary>
        /// <param name="code">学号</param>
        /// <returns></returns>
        public ActionResult EditStudent(string code)
        {
            if (adminsRepository.Students.Where(e => e.Code == code) == null)
            {
                TempData["error"] = "学生不存在，请重试";
                return RedirectToAction("IndexStudent");
            }
            return View(adminsRepository.Students.FirstOrDefault(e=>e.Code==code));
        }
        [HttpPost]
        public ActionResult EditStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                int res=adminsRepository.EditStudent(student);
                switch (res)
                {
                    case 1:
                        TempData["success"] = "新增学生成功";
                        break;
                    case 2:
                        TempData["error"] = "数据库异常";
                        break;
                    case 0:
                        TempData["success"] = "修改学生成功";
                        break;
                }
                return RedirectToAction("IndexStudent");
            }
            return View();
        }
        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="code">学号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteStudent(string code)
        {
            if (adminsRepository.Students.Where(e => e.Code == code) == null)
            {
                TempData["error"] = "学生不存在，请重试";
                return RedirectToAction("IndexStudent");
            }
            int res = adminsRepository.DeleteStudent(code);
            switch (res)
            {
                case 1:
                    TempData["success"] = "删除学生成功";
                    break;
                case 2:
                    TempData["error"] = "数据库异常";
                    break;
                case 0:
                    TempData["error"] = "学生不存在";
                    break;
            }
            return RedirectToAction("IndexStudent");
        }

        /// <summary>
        /// 新增修改寝室，界面跳转你改
        /// </summary>
        /// <param name="dormNum">寝室号</param>
        /// <returns></returns>
        public ActionResult EditDorm(string dormNum)
        {
            if (adminsRepository.Dorms.Where(e => e.DormNum == dormNum) == null)
            {
                TempData["error"] = "寝室不存在，请重试";
                return RedirectToAction("IndexDorm");
            }
            return View(adminsRepository.Dorms.FirstOrDefault(e => e.DormNum == dormNum));
        }
        [HttpPost]
        public ActionResult EditDorm(Dorm dorm)
        {
            if (ModelState.IsValid)
            {
                int res = adminsRepository.EditDorm(dorm);
                switch (res)
                {
                    case 1:
                        TempData["success"] = "新增寝室成功";
                        break;
                    case 2:
                        TempData["error"] = "数据库异常";
                        break;
                    case 0:
                        TempData["success"] = "修改寝室成功";
                        break;
                }
                return RedirectToAction("IndexDorm");
            }
            return View();
        }
        /// <summary>
        /// 删除寝室
        /// </summary>
        /// <param name="code">寝室号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteDorm(string dormnum)
        {
            if (adminsRepository.Dorms.Where(e => e.DormNum == dormnum) == null)
            {
                TempData["error"] = "寝室不存在，请重试";
                return RedirectToAction("IndexStudent");
            }
            int res = adminsRepository.DeleteDorm(dormnum);
            switch (res)
            {
                case 1:
                    TempData["success"] = "删除寝室成功";
                    break;
                case 2:
                    TempData["error"] = "数据库异常";
                    break;
                case 3:
                    TempData["error"] = "寝室有人在使用，清空";
                    break;
                case 0:
                    TempData["error"] = "寝室不存在";
                    break;
            }
            return RedirectToAction("Indexdorm");
        }

        /// <summary>
        /// 换寝处理
        /// </summary>
        /// <param name="Id">换寝表id</param>
        /// <param name="flag">意见，是否同意</param>
        /// <returns></returns>
        public ActionResult DealChangeDorm(int Id)
        {
            return View(commonRepository.changeDorms.FirstOrDefault(e=>e.Id==Id));
        }
        [HttpPost]
        public ActionResult DealChangeDorm(int Id,bool flag, string why = "无")
        {
            int res = adminsRepository.DealChangeDorm(Id, flag, why);
            switch (res)
            {
                case 1:
                    TempData["success"] = "处理完成";
                    break;
                case 2:
                    TempData["error"] = "数据库异常，请重试";
                    break;
            }
            return RedirectToAction("IndexDealChangDorm");
        }

        /// <summary>
        /// 维修处理
        /// </summary>
        /// <param name="Id">换寝表id</param>
        /// <param name="flag">意见，是否同意</param>
        /// <returns></returns>
        public ActionResult DealRepair(int Id)
        {
            return View(commonRepository.Repairs.FirstOrDefault(e => e.Id == Id));
        }
        [HttpPost]
        public ActionResult DealRepair(int Id, decimal money=0, string why = "无")
        {
            int res = adminsRepository.DealRepair(Id, money, why);
            switch (res)
            {
                case 1:
                    TempData["success"] = "处理完成";
                    break;
                case 2:
                    TempData["error"] = "数据库异常，请重试";
                    break;
                case 0:
                    TempData["error"] = "该申请已处理";
                    break;
            }
            return RedirectToAction("IndexDealRepair");
        }
        public ActionResult DormList()
        {
            return View(adminsRepository.Dorms);
        }
        /// <summary>
        /// 寝室查询
        /// </summary>
        /// <param name="dormnum">寝室号</param>
        /// <returns></returns>
        public ActionResult DormDetail(string dormnum)
        {
            return View(commonRepository.SearchDorm(dormnum));
        }
        /// <summary>
        /// 租赁查询
        /// </summary>
        /// <returns></returns>
        public ActionResult LeaseList(string dormnum=null)
        {
            if (dormnum == null)
            {
                return View(commonRepository.SearchLeases());
            }
            IEnumerable<Lease> leases = commonRepository.SearchLeases();
            return View(leases.Where(e => e.DormNum == dormnum));
        }
        /// <summary>
        /// 空余寝室查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SpareDorm()
        {
            return View(commonRepository.SpareDorm());
        }
        /// <summary>
        /// 换寝查询
        /// </summary>
        /// <param name="dormnum"></param>
        /// <returns></returns>
        public ActionResult SearchChangeDorm(string dormnum = null)
        {
            if (dormnum == null)
            {
                return View(commonRepository.changeDorms);
            }
            return View(commonRepository.SearchChangeDorm(dormnum));
        }
        /// <summary>
        /// 维修查询
        /// </summary>
        /// <param name="dormnum"></param>
        /// <returns></returns>
        public ActionResult SearchRepair(string dormnum = null)
        {
            if (dormnum == null)
            {
                return View(commonRepository.Repairs);
            }
            return View(commonRepository.SearchRepairs(dormnum));
        }
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