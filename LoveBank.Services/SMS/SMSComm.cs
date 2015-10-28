
using LoveBank.Common;
using LoveBank.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace LoveBank.Services
{
    public class SMSComm
    {

        public static JsonMessage Send(string phone, string sendMsg)
        {

            sendMsg = HttpUtility.UrlEncode(sendMsg, Encoding.GetEncoding("GBK"));
            string res = string.Empty;
            JsonMessage ret = new JsonMessage();
            try
            {
                string SMSUserName = System.Configuration.ConfigurationManager.AppSettings["SMSUserName"];
                string SMSPassWord = System.Configuration.ConfigurationManager.AppSettings["SMSPassWord"];
                string SMSServiceUrl = System.Configuration.ConfigurationManager.AppSettings["SMSServiceUrl"];

                HttpItem parm = new HttpItem();
                parm.ResultType = ResultType.String;
                parm.URL = string.Format("{0}?name={1}&password={2}&mobile={3}&message={4}",
                    SMSServiceUrl, SMSUserName, SMSPassWord, phone, sendMsg);

                HttpHelper httpHelper = new HttpHelper();
                HttpResult httpResult = httpHelper.GetHtml(parm);
                res = httpResult.Html;
                if (httpResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string[] str = res.Split(',');
                    if (str[0] == "succ")
                    {
                        ret.Info = "发送成功";
                        ret.Status = true;
                    }
                    else
                    {
                        ret.Info = "发送失败,返回内容：" + httpResult.StatusCode + "   " + res;
                        ret.Status = false;
                        ret.Data = res;
                    }
                }
                else
                {
                    ret.Info = "发送失败,返回内容：" + httpResult.StatusCode + "   " + res;
                    ret.Status = false;
                    ret.Data = res;
                }
            }
            catch (Exception ex)
            {
                ret.Info = ex.Message + ex.InnerException + ex.Source + ex.TargetSite + ex.StackTrace;
                ret.Data = res;
                ret.Status = false;
            }
            return ret;
        }

    }
}