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
     [SecurityModule(Name = "资讯管理")]
    public class InfoManageController : BaseController
    {
        // GET: InfoManage
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
                               Department=d
                           };

                //list = list.Where(x => x.State != RowState.删除);


                return View(list.OrderByDescending(x => x.ID).ToPagedList(pageNumber - 1, size));
            }
        }


        [SecurityNode(Name = "添加页面")]
        public ActionResult Add()
        {
            using (LoveBankDBContext db = new LoveBankDBContext())
            {
                var t_d = db.T_Department;

                ////部门组织
                var list = t_d.Where(x =>x.Id.Contains("851")&& x.Level>=3 &&x.Level <= 4).ToList();
                ViewData["Department_List"] = HelpSerializer.JSONSerialize<List<Department>>(list);
                return View();
            }
        }

        [HttpPost]
        [SecurityNode(Name = "添加执行")]
        public ActionResult PostAdd(InfoManageModel parm)
        {
            #region 初始化参数
            InfoManage model = new InfoManage();

            model.AddTime = DateTime.Now;
            model.AddUserId = AdminUser.ID;
            model.State = RowState.有效;
            model.Guid = Guid.NewGuid().ToString();
            model.LinkUrl = parm.LinkUrl;

            model.Title = parm.Title;
            model.Desc = parm.Desc;
            model.LinkUrl = parm.LinkUrl;
            model.Contact = parm.Contact;
            model.Phone = parm.Phone;
            model.DeptId = parm.DeptId;
            model.Type = parm.Type;

            foreach (var item in parm.SourceFileList)
            {
                item.Guid = model.Guid;
                item.AddTime = DateTime.Now;

            }
            #endregion

            using (LoveBankDBContext db = new LoveBankDBContext())
            {


                db.Add(model);
                db.SaveChanges();
                db.T_SourceFile.AddRange(parm.SourceFileList);
                db.SaveChanges();

                return Success("添加成功");

            }


        }

        [SecurityNode(Name = "编辑页面")]
        public ActionResult Edit(int id)
        {
            using (LoveBankDBContext db = new LoveBankDBContext())
            {
                var t_a = db.T_InfoManage;
                var t_s = db.T_SourceFile;

                var model = (from a in t_a
                             where a.ID == id
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
                                 SourceFileList = t_s.Where(x => x.Guid == a.Guid).ToList()
                             }).SingleOrDefault();

                return View(model);
            }

        }

        [HttpPost]
        [SecurityNode(Name = "编辑执行")]
        public ActionResult PostEdit(InfoManageModel parm)
        {



            using (LoveBankDBContext db = new LoveBankDBContext())
            {

                var t_m = db.T_Machine;
                var t_a = db.T_InfoManage;
                var t_s = db.T_SourceFile;

                #region 初始化参数
                InfoManage model = t_a.Find(parm.ID);

                model.Title = parm.Title;
                model.Desc = parm.Desc;

                model.DeptId = parm.DeptId;
                model.LinkUrl = parm.Desc;
                model.Phone = parm.Desc;
                model.Contact = parm.Desc;
                model.Type = parm.Type;
             



                ///删除原来的,彻底以新增方式进行（修改通过删除在新增实现）
                var delSourceFile = from s in t_s where s.Guid == model.Guid select s;
                db.T_SourceFile.RemoveRange(delSourceFile);

                #endregion
                db.Update<InfoManage>(model);
                db.SaveChanges();


                db.T_SourceFile.AddRange(parm.SourceFileList);//重新绑定
                db.SaveChanges();

                return Success("添加成功");

            }

        }

        [HttpPost]
        [SecurityNode(Name = "删除执行")]
        public ActionResult Delete(int id)
        {
            using (LoveBankDBContext db = new LoveBankDBContext())
            {

              
                var t_i = db.T_InfoManage;
                InfoManage iEntity = t_i.Find(id);
              
                iEntity.State = RowState.删除;

                db.Update(iEntity);
                db.SaveChanges();

                return Success("删除成功");
            }
          
        }

        public ActionResult UpLoadProcess(string id, string name, string type, string lastModifiedDate, int size, HttpPostedFileBase file)
        {

            if (file == null)
            {
                Error("请选择文件");
            }
            SourceFile res = UploadFileInstance.SaveFile(file, "InfoManageImg", AdminUser.ID);
            return Json(res);


        }
    }
}