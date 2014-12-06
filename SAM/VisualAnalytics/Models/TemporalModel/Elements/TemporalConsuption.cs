using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualAnalytics.Models.TemporalModel.Elements
{
	public class TemporalConsuption : TemporalElement
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private SAMcontext db = new SAMcontext();

		public TemporalConsuption()
		{
			log.Debug("TemporalConsuption created");

		}
		protected override void agregate()
		{
			throw new NotImplementedException();
		}

		protected override void drillDown()
		{
			throw new NotImplementedException();
		}
	}
}