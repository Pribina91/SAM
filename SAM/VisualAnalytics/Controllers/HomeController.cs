using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisualAnalytics.Controllers.Helpers;
using log4net.Config;
using log4net;
using VisualAnalytics.Models;

namespace VisualAnalytics.Controllers
{
	public class HomeController : Controller
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public ActionResult Index()
		{
			InputParser ip = new InputParser();
			ip.parseCSV(@"C:\Workspace\SAM\SAM\VisualAnalytics\DataBackUp\ConsuptionCSV.txt");
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
	}
}