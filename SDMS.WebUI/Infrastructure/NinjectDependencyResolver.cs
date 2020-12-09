using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using SDMS.Domain.Abstract;
using SDMS.Domain.Concrete;
namespace SDMS.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            kernel.Bind<IAdminsRepository>().To<EFAdminsRepository>();
            kernel.Bind<IStudentRepository>().To<EFStudentRepository>();
            kernel.Bind<ICommonRepository>().To<EFCommonRepository>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}