﻿using LoveBank.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoveBank.Web.Admin.Models
{
    public class UnionHelpPojectDetailModel
    {
        public int ID { get; set; }
        public DateTime AddTime { get; set; }
        public int UnionHelpPojectID { get; set; }
        public int AddUserId { get; set; }

        public string Name { get; set; }
        public SexEnum Sex { get; set; }

        public int Age { get; set; }
        public string Phone { get; set; }
        public string IDCard { get; set; }
        public decimal Money { get; set; }
        public string Address { get; set; }
        public string Desc { get; set; }
        public RowState State { get; set; }
        public DateTime? HelpTime { get; set; }
    }
}