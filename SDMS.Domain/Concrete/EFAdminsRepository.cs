using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDMS.Domain.Abstract;
namespace SDMS.Domain.Concrete
{
    public class EFAdminsRepository:IAdminsRepository
    {
        private SDMSEntities db = new SDMSEntities();
        public IEnumerable<Admin> Admins
        {
            get
            {
                return db.Admin;
            }
        }
    }
}
