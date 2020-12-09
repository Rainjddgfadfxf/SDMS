using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDMS.Domain.Abstract;
namespace SDMS.Domain.Concrete
{
    public class EFStudentRepository : IStudentRepository
    {
        public int ChangeDorm(string StudentId, string newDormNum, string why)
        {
            using (var db = new SDMSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Student student = db.Student.Find(StudentId);
                        ChangeDorm dorm = new ChangeDorm();
                        if (db.Dorm.Find(newDormNum).UsedNum != 0)
                        {
                            dorm.OldDormNum = student.DormNum;
                            dorm.StudentId = StudentId;
                            dorm.Begindate = DateTime.Now;
                            dorm.Why = why;
                            dorm.NewDormNum = newDormNum;
                            db.SaveChanges();
                            dbContextTransaction.Commit();
                            return 1;//申请成功
                        }
                        return 0;//当前寝室床位不足，请更换寝室
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return 2;//数据库添加异常
                    }
                }
            }
        }

        public int ReportForRepair(Repair repair)
        {
            using (var db = new SDMSEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        
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
