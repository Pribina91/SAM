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
		public void parseCSV(string filename, Boolean events)
		{
			using (TextFieldParser parser = new TextFieldParser(filename))
			{
				parser.CommentTokens = new string[] { "#" };
				parser.SetDelimiters(new string[] { "," });
				parser.HasFieldsEnclosedInQuotes = false;

				// Skip over header line.
				parser.ReadLine();
				int id = 0;

				while (!parser.EndOfData)
				{
					string[] fields = parser.ReadFields();

					int i = 0;
					
					Event newc = new Event
					{
						IDEvents = id++,
						weekday = fields[i++],
						IDDate = DateTime.Parse(fields[1]).Year * 10000 + DateTime.Parse(fields[1]).Month * 100 + DateTime.Parse(fields[1]).Day,
						eventDate = DateTime.Parse(fields[i++]),
						eventName = fields[i++],
						eventType = fields[i++],
					};


					log.Debug("New event:" + newc.eventDate.ToString() + "\t" + newc.eventName + "\t" + newc.eventType + "\t" );
					db.Events.Add(newc);

				}
				db.SaveChanges();
			}

		}
		public void parseCSVBelgium(string filename)
		{
			using (TextFieldParser parser = new TextFieldParser(filename))
			{
				parser.CommentTokens = new string[] { "#" };
				parser.SetDelimiters(new string[] { ";" });
				parser.HasFieldsEnclosedInQuotes = false;

				// Skip over header line.
				parser.ReadLine();
				parser.ReadLine();
				int id = 0;
				while (!parser.EndOfData)
				{
					string[] fields = parser.ReadFields();

					long dt = long.Parse(fields[2]) * 10000 + long.Parse(fields[1]) * 100 + long.Parse(fields[0]); 
					for (int k = 3; k < fields.Length-4; k++)
					{
						Consuption newc = new Consuption()
						{

							IDConsuptionPlace = 9999,
							DayOffset = -1,
							MeasurementSequence = k-3,
							Amount = float.Parse(fields[k]),
							IDDate = dt,
							MeasurementTime = -1,
							//IDConsuption = ++,
							source = 2

						};


						log.Debug("New Consuption:" + newc.IDConsuptionPlace.ToString() + "\t" + newc.Amount.ToString() + "\t" + newc.DayOffset.ToString() + "\t" + newc.MeasurementSequence.ToString());
						db.Consuptions.Add(newc);


					}
					db.SaveChanges();

				}
				db.SaveChanges();
			}

		}
	}
}