using LoveBank.Core.Domain;
using LoveBank.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoveBank.Web.Admin.Models
{
    public class SeekHelperModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime AddTime { get; set; }
        public int AddUserId { get; set; }

        public decimal TotalMoney { get; set; }

        public decimal FinishMoney { get; set; }

        public string BankCard { get; set; }

        public Banks Bank { get; set; }

        public string DeptId { get; set; }
        public string DeptName { get; set; }

        public string Address { get; set; }

        public DateTime PublicTime { get; set; }

        public DateTime EndTime { get; set; }

        public RowState State { get; set; }

        public string Desc { get; set; }

        public string GuidSourceFileHeadImg { get; set; }

        public string HeaderImg { get; set; }

        public string LinkUrl { get; set; }

        public virtual IList<SourceFile> SourceFileList { get; set; }

        public  IList<SeekHelperRecorde> SeekHelperRecordeList { get; set; }
    }
}