using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.DashBoard.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard/DashBoard
        public ActionResult Index()
        {
            return View();
        }
    }
}