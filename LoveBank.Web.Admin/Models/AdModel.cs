using LoveBank.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoveBank.Web.Admin.Models
{
    public class AppAdModel
    {
        public string LinkUrl { get; set; }
        public string Title { get; set; }
        public AdPostion Postion { get; set; }

        public dynamic  SourceFileList { get; set; }
    }
}