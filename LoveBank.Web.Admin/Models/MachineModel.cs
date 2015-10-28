using LoveBank.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoveBank.Web.Admin.Models
{
    public class MachineModel
    {
        public Machine Machine { get; set; }
        public Department Department { get; set; }

    }
}