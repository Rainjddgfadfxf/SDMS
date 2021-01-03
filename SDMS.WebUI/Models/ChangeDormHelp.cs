using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDMS.WebUI.Models
{
    public class ChangeDormHelp
    {
        public string studentid { get; set; }
        [Required(ErrorMessage = "请选择寝室号")]
        public string newDormNum { get; set; }
        public string why { get; set; }
    }
}