﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveBank.Core.Domain
{
    public class Vol:Entity
    {
        /// <summary>
        /// auto_increment
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Number
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string DepId
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RealNameState
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string RealName
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string CardType
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string CardNum
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string UerName
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Honor
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string EMail
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string QQ
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string WeiXin
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int? GroupID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ThsScore
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Score
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string VID
        {
            set;
            get;
        }

        /// <summary>
        /// 社会组织编号
        /// </summary>
        public string SocialNO { get; set; }
        /// <summary>
        /// 审核通过是1,其他状态为审核未通过
        /// </summary>
        public int? State
        {
            set;
            get;
        }

        public string Msg { get; set; }

        public string PassWord { get; set; }

        /// <summary>
        /// 是否接受任务
        /// </summary>
        public int? Doing { get; set; }
        public byte[] VolHeadImg { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int LoginState { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }

        public string SerAreas { get; set; }
        public string Speciality { get; set; }
        /// <summary>
        /// 小区Id
        /// </summary>
        public string VillDeptId { get; set; }

        public int VolType { get; set; }
    }
}
