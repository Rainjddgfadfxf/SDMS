﻿using SDMS.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDMS.Domain.Abstract
{
    public interface IAdminsRepository
    {
        IEnumerable<Admin> Admins { get; }
    }
}
