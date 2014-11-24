using System;
using System.Collections.Generic;
using System.IO;
using VisualAnalytics.Models;
using Microsoft.VisualBasic.FileIO;

namespace VisualAnalytics.Controllers.Helpers
{
	public class InputParser
	{
		private SAMcontext db = new SAMcontext();
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public void parseCSV(string filename) 
		{
			using (TextFieldParser parser = new TextFieldParser(filename))
			{
				parser.CommentTokens = new string[] { "#" };
				parser.SetDelimiters(new string[] { "\t" });
				parser.HasFieldsEnclosedInQuotes = true;

				// Skip over header line.
				parser.ReadLine();
				int i = 0;
				while (!parser.EndOfData)
				{
					string[] fields = parser.ReadFields();



					Consuption newc = new Consuption()
					{

						IDConsuptionPlace = long.Parse(fields[0]),
						DayOffset = int.Parse(fields[1].Substring(0, fields[1].Length - 2)),
						MeasurementSequence = int.Parse(fields[1].Substring(fields[1].Length - 2)),
						Amount = float.Parse(fields[2]),
						IDDate = 20120101,
						MeasurementTime = 1,
						IDConsuption = ++i,

					};


					log.Debug("New Consuption:" + newc.IDConsuptionPlace.ToString() + "\t" + newc.Amount.ToString() + "\t" + newc.DayOffset.ToString() + "\t" + newc.MeasurementSequence.ToString());
					db.Consuptions.Add(newc);

				}
				db.SaveChanges();
			}


		}
	}
}