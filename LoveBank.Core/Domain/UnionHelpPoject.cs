using LoveBank.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveBank.Core.Domain
{
  public  class UnionHelpPoject
    {
        public int ID { get; set; }
        public DateTime AddTime { get; set; }
        public string GUID { get; set; }
        public int AddUserId { get; set; }

        public UnionHelpPojectType PojectType { get; set; }

        public string PojectTitle { get; set; }

        public string PojectUnit { get; set; }
        public DateTime PojectDate { get; set; }
        public string PojectAddUser { get; set; }
        public string PojectPhone { get; set; }
        public string Desc { get; set; }
        public RowState State { get; set; }

    }
}
