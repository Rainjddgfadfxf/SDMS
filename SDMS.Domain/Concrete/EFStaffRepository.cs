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
                        if (changeDorm.AdminOpinion != null) return 0;//管理员已处理，无需处理
                        else
                        {
                            if (changeDorm.StaffOpinion != null) return 3;//已被工作人员处理
                            changeDorm.StaffOpinion = flag;
                            return 1;//处理完成
                        }

                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return 2;//数据库添加异常
                    }
                }
            }
        }


        public int DealRepair(int Id,decimal money, string ReasonsForUncompletion="无")
        {
            using (var db = new SDMSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Repair repair = db.Repair.Find(Id);
                        if (repair != null && repair.ResolutionDate == null)
                        {
                            repair.ResolutionDate = DateTime.Now;
                            repair.Money = money;
                            repair.ReasonsForUncompletion = ReasonsForUncompletion;
                            return 1;//维修完成
                        }
                        return 0;//该维修已处理
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
