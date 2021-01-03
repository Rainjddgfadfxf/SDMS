using SDMS.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDMS.WebUI.Infrastructure.Binders
{
    public class AdminModelBinder : IModelBinder
    {
        private const string sessionKey = "Admin";
        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            Admin admin = null;
            {
                if (controllerContext.HttpContext.Session != null)
                {
                    admin = controllerContext.HttpContext.Session[sessionKey] as Admin;
                }
                if (admin == null)
                {
                    admin = new Admin();
                    if (controllerContext.HttpContext.Session != null)
                    {
                        controllerContext.HttpContext.Session[sessionKey] = admin;
                    }
                }
                return admin;
            }
        }
    }
}