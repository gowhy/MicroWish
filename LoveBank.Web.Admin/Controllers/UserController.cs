using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using LoveBank.Common;
using LoveBank.Core;
using LoveBank.Core.Members;
using LoveBank.Common.Data;
using LoveBank.Services.Members;
using LoveBank.Web.Admin.Models;
using LoveBank.MVC.Security;
using LoveBank.Core.Domain;
using LoveBank.Core.SerializerHelp;
using LoveBank.Core.MSData;

namespace LoveBank.Web.Admin.Controllers
{

    [SecurityModule(Name = "用户管理")]
    public class UserController : BaseController
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        const int pageSize = 20;


        [SecurityNode(Name = "首页")]
        public ActionResult Index(int? page)
        {

            var pageNumber = page ?? 1;

            var backAdminUser = DbProvider.D<User>().Include(x => x.Role)//
                .Include(x => x.Department)//
                .AsQueryable<User>().ToList();
            return View(backAdminUser.ToPagedList(pageNumber - 1, pageSize));

        }
        [SecurityNode(Name = "列表页")]
        public ActionResult List()
        {

            IList<User> backAdminUser = DbProvider.D<User>().ToList();
            return View(backAdminUser);

        }
        [SecurityNode(Name = "添加页")]
        public PartialViewResult Add()
        {


            IList<Role> role = DbProvider.D<Role>().ToList();

            ViewData["UserRole"] = role;


            using (LoveBankDBContext db = new LoveBankDBContext())
            {
                var t_d = db.T_Department;


                ////部门组织
                var list = t_d.Where(x => x.Level <= 6).ToList();
                ViewData["Department_List"] = HelpSerializer.JSONSerialize<List<Department>>(list);


                return PartialView();
            }

        }
        [SecurityNode(Name = "增加用户")]
        public ActionResult PostAdd(User user)
        {

            if (DbProvider.D<User>().Count(u => u.UserName.Trim() == user.UserName.Trim()) > 0)
            {

                return Error("用户已经存在");
            }
            user.Role = DbProvider.D<Role>().FirstOrDefault(r => r.ID == user.RoleId);
            DbProvider.Add(user);
            return Success("操作成功");

        }

        [SecurityNode(Name = "删除用户")]
        public ActionResult Delete(int id)
        {


            DbProvider.Delete<User>(x => x.ID == id);
            DbProvider.SaveChanges();
            return Success("操作成功");

        }

        [SecurityNode(Name = "编辑页面")]
        public PartialViewResult Edit(int id)
        {


            User user = DbProvider.GetByID<User>(id);

            List<Role> role = DbProvider.D<Role>().ToList();

            ViewData["UserRole"] = role;


            using (LoveBankDBContext db = new LoveBankDBContext())
            {
                var t_d = db.T_Department;


                ////部门组织
                var list = t_d.Where(x => x.Level <= 6).ToList();
                ViewData["Department_List"] = HelpSerializer.JSONSerialize<List<Department>>(list);
            }
            return PartialView(user);
        }



        [SecurityNode(Name = "编辑保存用户")]
        public ActionResult PostEdit(User model)
        {
            User user = DbProvider.GetByID<User>(model.ID);
            if (user == null) return Error("用户不存在");

            user.DeptId = model.DeptId;
            user.RealName = model.RealName;
            user.RoleId = model.RoleId;

            DbProvider.Update<User>(user);
            DbProvider.SaveChanges();
            return Success("操作成功");

        }
    }
}