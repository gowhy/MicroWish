using System.Linq;
using System;
using System.Web.Mvc;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using LoveBank.Common;
using LoveBank.Core.Domain;
using LoveBank.Services.AdminModule;
using LoveBank.Common.Data;
using LoveBank.Web.Admin.Models;
using LoveBank.MVC.Security;
using LoveBank.Core.MSData;
using LoveBank.Core.Domain.Enums;
using System.IO;
using System.Web;
using LoveBank.Services;

namespace LoveBank.Web.Admin.Controllers.App
{
    public class AppCommController : Controller
    {
        const int PageSize = 10;
        /// <summary>
        ///  获取App的启动图
        /// </summary>
        /// <param name="postion">1:开机广告,2:滚动广告</param>
        /// <returns></returns>

        [OutputCache(Duration = 10)]
        public ActionResult StartAdList(int postion)
        {
            using (LoveBankDBContext db = new LoveBankDBContext())
            {

                var ad = db.T_LoveBank_Ad;
                var sf = db.T_SourceFile;

                var list = from a in ad
                           where a.State != RowState.删除 && ((int)a.Postion&postion)>0
                           select new AppAdModel
                           {
                               LinkUrl = a.LinkUrl,
                               Title = a.Title,
                               Postion = a.Postion,
                               SourceFileList = (from s in sf where s.Guid == a.Guid select new { ImgHttpUrl = s.Domain + s.Path }).ToList()
                           };

                return Json(list.ToList());
            }

        }

        public ActionResult AppUserReg(AppUser user, string code)
        {

            if (user == null || string.IsNullOrEmpty(code) || user.Type == null || string.IsNullOrEmpty(user.PassWord) && string.IsNullOrEmpty(user.Phone))
            {
                Json("注册账号、密码、验证码和用户类型是必填项");
            }
            user.PassWord = user.PassWord.Hash();//hash
            user.AddTime = DateTime.Now;
            user.LastLoginTime = DateTime.Now;

            JsonMessage returnJson = new JsonMessage();
            using (LoveBankDBContext db = new LoveBankDBContext())
            {
                try
                {
                    if (db.T_AppUser.Count(x => x.Phone == user.Phone) > 0)
                    {
                        returnJson.Info = "该手机号已经存在,不能注册";
                        returnJson.Status = false;
                       

                        return Json(returnJson);
                    }

                    DateTime codeOutTime = DateTime.Now.AddMinutes(-10);
                    int existCount = db.T_SMS.Count(x => x.Phone == user.Phone.Trim() && x.VCode == code.Trim() && x.Class == SmsClass.注册验证码 && x.AddTime > codeOutTime);
                    if (existCount == 0)
                    {
                        returnJson.Info = "注册验证码失效,请重新获取";
                        returnJson.Status = false;

                        return Json(returnJson);
                    }


                    db.Add(user);
                    db.SaveChanges();


                    returnJson.Status = true;
                    returnJson.Info = "注册成功";
                    returnJson.Data = user;
                    return Json(returnJson);
                }
                catch (Exception ex)
                {
                  
                    returnJson.Info = ex.Message;
                    returnJson.Status = false;

                    return Json(returnJson);
                }
            }
        }

        /// <summary>
        /// 注册验证码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult AppRegSMS(string phone)
        {
            JsonMessage ret = new JsonMessage();


            Random rad = new Random();//实例化随机数产生器rad；
            int value = rad.Next(1000, 10000);//用rad生成大于等于1000，小于等于9999的随机数；
            string code = value.ToString();

            //[微愿]你的验证码是xxxx
            string msg = "【微愿】你的注册码是" + code + "。";

            SMS entity = new SMS();
            entity.AddTime = DateTime.Now;
            entity.Msg = msg;
            entity.VCode = code.Trim();
            entity.Class = SmsClass.注册验证码;
            entity.Phone = phone;


            JsonMessage retMSM = SMSComm.Send(entity.Phone, msg);
            if (retMSM.Status == true)
            {
                using (LoveBankDBContext db = new LoveBankDBContext())
                {
                    db.Add<SMS>(entity);
                    db.SaveChanges();

                    retMSM.Info = "发送成功";

                }
            }
            else
            {
                retMSM.Info = "发送失败." + retMSM.Info;
            }
            return Json(retMSM);
        }

        /// <summary>
        /// App登录
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public ActionResult AppLogin(string phone,string passWord)
        {
            
            JsonMessage returnJson = new JsonMessage();
            using (LoveBankDBContext db = new LoveBankDBContext())
            {

                var t_a = db.T_AppUser;
                string passWordHash = passWord.Hash();
                AppUser appUser = db.T_AppUser.Where(x => x.Phone == phone && x.PassWord == passWordHash).FirstOrDefault();
                if (appUser != null)
                {
                    returnJson.Status = true;
                    appUser.PassWord = string.Empty;
                    returnJson.Data =
                        new
                        {
                            Ticket = AutheTicketManager.CreateAppLoginUserTicket(appUser.ID.ToString()),
                            User=appUser
                        };
                    returnJson.Info = "登录成功";
                    return Json(returnJson);
                }
              
            }
            returnJson.Status = false;
            returnJson.Info = "登录失败,系统异常";
            returnJson.Data = HttpContext.Error;
            return Json(returnJson);

        }
        /// <summary>
        /// 修改用户基本信息接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
       [OutputCache(Duration = 10)]
        public ActionResult App_UpdateUser(AppUser user)
        {

            JsonMessage returnJson = new JsonMessage();
            using (LoveBankDBContext db = new LoveBankDBContext())
            {

                var t_a = db.T_AppUser;

                AppUser entityAppUser = db.T_AppUser.Where(x=>x.ID==user.ID).FirstOrDefault();

                entityAppUser.Age = user.Age;
                entityAppUser.Name = user.Name;
                entityAppUser.NickName = user.NickName;
                entityAppUser.Sex = user.Sex;

                db.Update(entityAppUser);
                db.SaveChanges();
                returnJson.Status = true;
                returnJson.Info = "修改成功";
                returnJson.Data = HttpContext.Error;
                return Json(returnJson);
               
            }
            returnJson.Status = false;
            returnJson.Info = "登录失败,系统异常";
            returnJson.Data = HttpContext.Error;
            return Json(returnJson);

        }

        //保存头像的Url地址
        public ActionResult AppSaveImg(HttpPostedFileBase file)
        {
            JsonMessage returnJson = new JsonMessage();
            SourceFile res = UploadFileInstance.SaveFile(file, "MicroWihsAppUserHeader", -1);

            returnJson.Status = true;
            returnJson.Info = Path.Combine(res.Domain, res.Path);//直接返URL地址
            returnJson.Data = res;

            return Json(returnJson);
        }
       

        public ActionResult App_InfoManage(int? page, int? pageSize)
        {
            var pageNumber = page ?? 1;
            var size = pageSize ?? PageSize;
            JsonMessage returnJson = new JsonMessage();
            using (LoveBankDBContext db = new LoveBankDBContext())
            {

                var ad = db.T_InfoManage;

                var t_d = db.T_Department;

                var list = from a in ad
                           join d in t_d on a.DeptId equals d.Id
                           select new InfoManageModel
                           {

                               AddTime = a.AddTime,
                               Title = a.Title,
                               ID = a.ID,
                               DeptId = a.DeptId,
                               Desc = a.Desc,
                               State = a.State,
                               LinkUrl = a.LinkUrl,
                               Contact = a.Contact,
                               Phone = a.Phone,
                               Type = a.Type,
                               Department = d
                           };

                list = list.Where(x => x.State != RowState.删除);
                returnJson.Status = true;
                returnJson.Data = list.OrderBy(x => x.ID).ToPagedList(pageNumber - 1, size).ToList();

                return Json(returnJson);
            }
        }
        /// <summary>
        /// 今秋助学
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [OutputCache(Duration = 10)]
        public ActionResult App_SeekHelper(int? page, int? pageSize)
        {
            var pageNumber = page ?? 1;
            var size = pageSize ?? PageSize;

            JsonMessage returnJson = new JsonMessage();
            using (LoveBankDBContext db = new LoveBankDBContext())
            {

                var ad = db.T_SeekHelper;

                var t_s = db.T_SourceFile;
                var t_d = db.T_Department;

                var list = from a in ad
                           join s in t_s on a.GuidSourceFileHeadImg equals s.Guid
                           join d in t_d on a.DeptId equals d.Id
                           select new SeekHelperModel
                           {

                               AddTime = a.AddTime,
                               Address = a.Address,
                               BankCard = a.BankCard,
                               EndTime = a.EndTime,
                               Bank = a.Bank,
                               FinishMoney = a.FinishMoney,
                               TotalMoney = a.TotalMoney,
                               Name = a.Name,
                               PublicTime = a.PublicTime,
                               ID = a.ID,
                               DeptId = a.DeptId,
                               Desc = a.Desc,
                               State = a.State,
                               Phone = a.Phone,
                               GuidSourceFileHeadImg = a.GuidSourceFileHeadImg,
                               DeptName = d.Name,
                               HeaderImg = s.Domain + s.Path

                           };

                returnJson.Status = true;
                returnJson.Data = list.OrderBy(x => x.ID).ToPagedList(pageNumber - 1, size).ToList();

                return Json(returnJson);
            }
        }

        /// <summary>
        /// 医疗帮扶
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
       [OutputCache(Duration = 10)]
        public ActionResult App_UnionHelpPojectList(int? id,int? page, int? pageSize)
        {
            var pageNumber = page ?? 1;
            var size = pageSize ?? PageSize;
            JsonMessage returnJson = new JsonMessage();
            using (LoveBankDBContext db = new LoveBankDBContext())
            {

                var t_u = db.T_UnionHelpPoject;
                var t_ud = db.T_UnionHelpPojectDetail;
            

                var list = from u in t_u
                           select new UnionHelpPojectModel
                           {
                               Desc = u.Desc,
                               PojectAddUser = u.PojectAddUser,
                               PojectDate = u.PojectDate,
                               PojectTitle = u.PojectTitle,
                               PojectType = u.PojectType,
                               PojectUnit = u.PojectUnit,
                               ID = u.ID,
                               PojectPhone = u.PojectPhone,
                               UnionHelpPojectDetailList = t_ud.Where(x => x.UnionHelpPojectID == u.ID).ToList()
                           };

                        

              
                returnJson.Status = true;
                if (id.HasValue)
                {
                    list = list.Where(x => x.ID == id);
                }
                returnJson.Data = list.OrderBy(x => x.ID).ToPagedList(pageNumber - 1, size).ToList();
                
                return Json(returnJson);
            }
        }

        public ActionResult ApGetUserById(int userId)
        {

            JsonMessage returnJson = new JsonMessage();
            using (LoveBankDBContext db = new LoveBankDBContext())
            {

                var t_a = db.T_AppUser;

                AppUser entityAppUser = db.T_AppUser.Where(x => x.ID == userId).FirstOrDefault();

                entityAppUser.PassWord = string.Empty;
                returnJson.Status = true;
                returnJson.Info = "获取成功";
                returnJson.Data = entityAppUser;
                return Json(returnJson);

            }
            returnJson.Status = false;
            returnJson.Info = "登录失败,系统异常";
            returnJson.Data = HttpContext.Error;
            return Json(returnJson);

        }

       [OutputCache(Duration = 10)]
        public ActionResult App_GetLastApk()
        {
            var pageNumber = 1;
            var size = 1;
            using (LoveBankDBContext db = new LoveBankDBContext())
            {

                var adimg = db.T_AppVer;

                var list = from a in adimg select a;

                list = list.Where(x => x.State == 0);

                return Json(list.OrderByDescending(x => x.Id).ToPagedList(pageNumber - 1, size), JsonRequestBehavior.AllowGet);
            }
        }
    }
}