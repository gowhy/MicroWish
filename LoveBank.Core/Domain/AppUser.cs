using LoveBank.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveBank.Core.Domain
{
    public class AppUser
    {
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Phone { get; set; }
        public DateTime AddTime { get; set; }

        public string Name { get; set; }

        public UserType Type { get; set; }

        public string PassWord { get; set; }

        public SexEnum Sex { get; set; }

        public int Age { get; set; }

        public string NickName { get; set; }

        public DateTime LastLoginTime { get; set; }
    }


}
