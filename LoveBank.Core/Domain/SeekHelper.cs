using LoveBank.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveBank.Core.Domain
{
    public class SeekHelper
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public string Address { get; set; }

        public DateTime PublicTime { get; set; }

        public DateTime EndTime { get; set; }

        public RowState State { get; set; }

        public string Desc { get; set; }

        public string LinkUrl { get; set; }

        public string GuidSourceFileHeadImg { get; set; }

    }
}
