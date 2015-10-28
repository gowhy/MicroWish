using LoveBank.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveBank.Core.Domain.Enums
{
    /// 消息类型
    /// </summary>
    public enum SmsClass
    {

        [EnumItemDescription("注册验证码")]
        注册验证码 = 0,

        [EnumItemDescription("找回验证码")]
        找回验证码 = 1
    }

}
