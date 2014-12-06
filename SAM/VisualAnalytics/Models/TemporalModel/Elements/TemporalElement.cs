using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualAnalytics.Models.TemporalModel.Elements
{
	public abstract class TemporalElement
	{
		protected float amount;
		protected abstract void agregate();
		protected abstract void drillDown();
	
	}
}