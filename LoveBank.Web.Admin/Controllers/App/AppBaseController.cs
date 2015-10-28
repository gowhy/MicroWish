using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoveBank.Web.Admin.Controllers.App
{

    public class AppBaseController : Controller
    {
        // GET: AppBase
        public ActionResult Index()
        {
            return View();
        }
    }
}