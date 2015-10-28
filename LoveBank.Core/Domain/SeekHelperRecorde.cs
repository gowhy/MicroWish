using LoveBank.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveBank.Core.Domain
{
   public class SeekHelperRecorde
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime AddTime { get; set; }

        public decimal Money { get; set; }
        public string BankCard { get; set; }
        public int PayType { get; set; }
        public Banks Bank { get; set; }
       /// <summary>
       /// 捐款人ID
       /// </summary>
        public int AppUserId { get; set; }

       /// <summary>
       /// 被捐助对象
       /// </summary>
        public int SeekHelperId { get; set; }
       
        public RowState State { get; set; }

        public string Desc { get; set; }
        public UserType AppUserType { get; set; }
    }
}
