using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisualAnalytics.Models.TemporalModel.Elements;

namespace VisualAnalytics.Models.TemporalModel
{
	public enum TemporalElementType {TempConsunsuption, TempConsuptionPlace,TempWeather,TempEvent }
	public class TemporalElementStore
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private SAMcontext db = new SAMcontext();

		public TemporalElement factoryMethod(TemporalElementType tp)
		{
			switch (tp)
			{
				case TemporalElementType.TempConsunsuption:
					return new TemporalConsuption();
				case TemporalElementType.TempConsuptionPlace:
					break;
				case TemporalElementType.TempWeather:
					break;
				case TemporalElementType.TempEvent:
					break;
				default:
					break;
					throw new ArgumentOutOfRangeException();
			}
			return null;
		}
	}
}