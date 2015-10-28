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
using LoveBank.Core.SerializerHelp;
using System.Collections.Generic;

namespace LoveBank.Web.Admin.Controllers
{
    [SecurityModule(Name = "公会救助项目")]
    public class UnionHelpPojectController : BaseController
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        const int PageSize = 20;


        [SecurityNode(Name = "首页")]
        public ActionResult Index(int? page, int? pageSize)
        {
            var pageNumber = page ?? 1;
            var size = pageSize ?? PageSize;

            using (LoveBankDBContext db = new LoveBankDBContext())
            {

                var ad = db.T_UnionHelpPoject;

              
                var list = from a in ad
                           select a;

                //list = list.Where(x => x.State != RowState.删除);


                return View(list.OrderByDescending(x => x.ID).ToPagedList(pageNumber - 1, size));
            }
        }


        [SecurityNode(Name = "添加项目页面")]
        public ActionResult Add()
        {
            using (LoveBankDBContext db = new LoveBankDBContext())
            {
                var t_d = db.T_Department;
                var t_u = db.T_UnionHelpPoject;

                ////部门组织
                var list = t_d.Where(x => x.Id.Contains("851") && x.Level >= 3 && x.Level <= 4).ToList();
                ViewData["Department_List"] = HelpSerializer.JSONSerialize<List<Department>>(list);
                return View();
            }
        }

        [HttpPost]
        [SecurityNode(Name = "添加项目执行")]
        public ActionResult PostAdd(UnionHelpPoject parm)
        {
            #region 初始化参数
            //LoveBank_Ad model = new LoveBank_Ad();

            parm.AddTime = DateTime.Now;
            parm.AddUserId = AdminUser.ID;
            parm.State = RowState.有效;
            parm.GUID = Guid.NewGuid().ToString();
        
            #endregion

            using (LoveBankDBContext db = new LoveBankDBContext())
            {
                db.Add(parm);
                db.SaveChanges();
                return Success("添加成功");

            }


        }

        [SecurityNode(Name = "添加项目详细页面")]
        public ActionResult AddDetail(int unionHelpPojectID)
        {

            using (LoveBankDBContext db = new LoveBankDBContext())
            {
                var t_d = db.T_Department;
                var t_u = db.T_UnionHelpPoject;

                UnionHelpPoject model = t_u.Find(unionHelpPojectID);

                ////部门组织
                var list = t_d.Where(x => x.Id.Contains("851") && x.Level >= 3 && x.Level <= 4).ToList();
                ViewData["Department_List"] = HelpSerializer.JSONSerialize<List<Department>>(list);
                return PartialView(model);
            }
        }

        [HttpPost]
        [SecurityNode(Name = "添加项目详细执行")]
        public ActionResult PostAddDetail(UnionHelpPojectDetail parm)
        {
            #region 初始化参数
            //LoveBank_Ad model = new LoveBank_Ad();

            parm.AddTime = DateTime.Now;
            parm.AddUserId = AdminUser.ID;
            parm.State = RowState.有效;
            
            #endregion
            JsonMessage rJson = new JsonMessage();
            using (LoveBankDBContext db = new LoveBankDBContext())
            {
                db.Add(parm);
                db.SaveChanges();
                rJson.Status = true;
                rJson.Info = "新增成功";
                rJson.Data = parm;
                return Json(rJson);

            }


        }
     
        [SecurityNode(Name = "查看项目详细")]
        public ActionResult UnionHelpPojectDetailList(int? unionHelpPojectID, int? page, int pageSize=10)
        {
            var pageNumber = page ?? 1;
            var size = pageSize ;
            #region 初始化参数
            //LoveBank_Ad model = new LoveBank_Ad();


            #endregion
            JsonMessage rJson = new JsonMessage();
            using (LoveBankDBContext db = new LoveBankDBContext())
            {
                var t_uhpd = db.T_UnionHelpPojectDetail;

                var list = from u in t_uhpd
                           where u.UnionHelpPojectID == unionHelpPojectID
                           select new UnionHelpPojectDetailModel
                           {
                               Age = u.Age,
                               Desc = u.Desc,
                               IDCard = u.IDCard,
                               Money = u.Money,
                               Name = u.Name,
                               Phone = u.Phone,
                               Sex = u.Sex,
                               ID = u.ID,
                               AddTime = u.AddTime,
                               HelpTime=u.HelpTime
                           };

                return PartialView(list.OrderByDescending(x => x.ID).ToPagedList(pageNumber - 1, size));

            }
        }

        [SecurityNode(Name = "编辑页面")]
        public ActionResult Edit(int id)
        {
            using (LoveBankDBContext db = new LoveBankDBContext())
            {
                var t_a = db.T_LoveBank_Ad;
                var t_s = db.T_SourceFile;

                var model = (from a in t_a
                             where a.ID == id
                             select new LoveBank_AdModel
                             {
                                 AddTime = a.AddTime,
                                 Title = a.Title,
                                 ID = a.ID,
                                 DeptId = a.DeptId,
                                 Desc = a.Desc,
                                 State = a.State,
                                 SourceFileList = t_s.Where(x => x.Guid == a.Guid).ToList()
                             }).SingleOrDefault();

                return View(model);
            }

        }

        [HttpPost]
        [SecurityNode(Name = "编辑执行")]
        public ActionResult PostEdit(LoveBank_AdModel parm)
        {



            using (LoveBankDBContext db = new LoveBankDBContext())
            {

                var t_m = db.T_Machine;
                var t_a = db.T_LoveBank_Ad;
                var t_s = db.T_SourceFile;

                #region 初始化参数
                LoveBank_Ad model = t_a.Find(parm.ID);

                model.Title = parm.Title;
                model.Desc = parm.Desc;



                ///删除原来的,彻底以新增方式进行（修改通过删除在新增实现）
                var delSourceFile = from s in t_s where s.Guid == model.Guid select s;
                db.T_SourceFile.RemoveRange(delSourceFile);

                #endregion
                db.Update<LoveBank_Ad>(model);
                db.SaveChanges();


                db.T_SourceFile.AddRange(parm.SourceFileList);//重新绑定
                db.SaveChanges();

                return Success("添加成功");

            }

        }
    }
}