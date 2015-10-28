﻿using LoveBank.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveBank.Core.Domain
{
    public class Machine : Entity
    {
        public string Title { get; set; }
        public DateTime AddTime { get; set; }

        public int AddUserId { get; set; }

        /// <summary>
        /// 添加机器的人所属社区
        /// </summary>
        public string AddUserDeptId { get; set; }

        /// <summary>
        /// 机器唯一编码，一体机通过该编码找到属于该机器的广告信息
        /// </summary>
        public string MachineCode { get; set; }
        /// <summary>
        /// 机器所属社区
        /// </summary>
        public string DeptId { get; set; }
      
        [ForeignKey("DeptId")]
        public Department Department { get; set; }
        /// <summary>
        ///数据状态
        /// </summary>
        public RowState State { get; set; }

        /// <summary>
        /// 备注，对机器的使用描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 机器名称
        /// </summary>
        public string Name { get; set; }
    }
}
