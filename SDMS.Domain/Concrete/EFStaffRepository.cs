using SDMS.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDMS.Domain.Concrete
{
    public class EFStaffRepository : IStaffRepository
    {
        SDMSEntities db = new SDMSEntities();
        public int DealChangeDorm(int Id, bool flag)
        {
            using (var db = new SDMSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        ChangeDorm changeDorm = db.ChangeDorm.Find(Id);
                        if (changeDorm.StaffOpinion != null) return 0;//已被工作人员处理
                        changeDorm.StaffOpinion = flag;
                        return 1;//处理完成
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return 2;//数据库添加异常
                    }
                }
            }
        }
        //未完成
        public int DealRepair(int Id, bool flag)
        {
            using (var db = new SDMSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Repair repair = db.Repair.Find(Id);
                        return 1;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return 2;//数据库添加异常
                    }
                }
            }
        }
    }
}
