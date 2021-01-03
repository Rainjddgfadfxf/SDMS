using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDMS.WebUI.Models
{
    public class ReportForRepairHelp
    {
        public string DormNum { get; set; }
        [Required(ErrorMessage = "请填写维修物")]
        public string good { get; set; }
        public string why { get; set; }
    }
}