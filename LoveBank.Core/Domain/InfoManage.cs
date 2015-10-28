using LoveBank.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveBank.Core.Domain
{
   public class InfoManage
    {

        public int ID { get; set; }
        public string Guid { get; set; }

        public string Title { get; set; }
        public DateTime AddTime { get; set; }

        public int AddUserId { get; set; }
        public InfoManageType Type { get; set; }
    
        /// <summary>
        /// 添加人所属社区
        /// </summary>
        public string DeptId { get; set; }
        public string Desc { get; set; }

        public string LinkUrl { get; set; }

        public string Contact { get; set; }
        public string Phone { get; set; }
        /// <summary>
        ///数据状态
        /// </summary>
        public RowState State { get; set; }
         
    }
}
