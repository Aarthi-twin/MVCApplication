using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Example()
        {
            return View();
        }

        public ActionResult Sample()
        {
            return View("About");
        }
        [Route("SearchByIdAndName/{id}/{Name}")]
        public ActionResult Edit(int id,string Name)
        {
            if(id>0)
            {
                return Content("The employee id is" +"\t" + id + "\t"+ "And employee name is" + Name);
            }
            else
            {
                return Content("Id cannot be negative");
            }
        }
    }
}