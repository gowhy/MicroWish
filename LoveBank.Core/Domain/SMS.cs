using LoveBank.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveBank.Core.Domain
{
    public class SMS
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Msg { get; set; }
        public System.DateTime AddTime { get; set; }
        public string VCode { get; set; }

        public SmsClass Class { get; set; }
    }
}
