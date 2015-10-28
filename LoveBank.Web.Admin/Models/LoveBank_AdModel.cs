using LoveBank.Core.Domain;
using LoveBank.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoveBank.Web.Admin.Models
{
    public class LoveBank_AdModel
    {
        public int ID { get; set; }
        public string Guid { get; set; }

        public string Title { get; set; }
        public DateTime AddTime { get; set; }

        public int AddUserId { get; set; }
  


        /// <summary>
        /// 添加人所属社区
        /// </summary>
        public string DeptId { get; set; }

        public string Desc { get; set; }
        public string LinkUrl { get; set; }
        public AdPostion Postion { get; set; }

        /// <summary>
        ///数据状态
        /// </summary>
        public RowState State { get; set; }

        /// <summary>
        /// 资源文件
        /// </summary>
        public virtual IList<SourceFile> SourceFileList { get; set; }
    }


}