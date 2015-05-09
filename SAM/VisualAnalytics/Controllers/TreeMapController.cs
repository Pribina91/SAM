using System;
using System.Linq;
using System.Web.Mvc;
using VisualAnalytics.Controllers.Helpers;

namespace VisualAnalytics.Views
{
    public class TreeMapController : Controller
    {
        // GET: TreeMap
        public ActionResult Index()
        {
            return View();
        }

        public ContentResult getTreemapData(string model = "SUM", string weatherDependency = "000000", int IDConsuptionPlace = 1622, int level = 5, int startIDDate = 20141201)
        {
            object ret = 0;

            return new ContentResult { Content = ret.ToJSON(), ContentType = "application/json" };
        }
    }
}