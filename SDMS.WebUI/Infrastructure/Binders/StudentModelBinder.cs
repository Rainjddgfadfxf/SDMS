using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDMS.Domain.Concrete;
namespace SDMS.WebUI.Infrastructure.Binders
{
    public class StudentModelBinder : IModelBinder
    {
        private const string sessionKey = "Student";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Student student = null;
            if (controllerContext.HttpContext.Session != null)
            {
                student = controllerContext.HttpContext.Session[sessionKey] as Student;
            }
            if (student == null)
            {
                student = new Student();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = student;
                }
            }
            return student;
        }
    }
}