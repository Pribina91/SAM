using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace VisualAnalytics.Controllers.Helpers
{
	public static class JSONHelper
	{
		public static string ToJSON(this object obj)
		{
			JavaScriptSerializer serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 };
			return serializer.Serialize(obj);
		}

		public static string ToJSON(this object obj, int recursionDepth)
		{
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			serializer.RecursionLimit = recursionDepth;
			return serializer.Serialize(obj);
		}
	}
}