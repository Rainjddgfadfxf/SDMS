using SDMS.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDMS.WebUI.Models
{
    public class changeDormView
    {
        public Student student { get; set; }
        public IEnumerable<Dorm> spareDorm { get; set; }
    }
}