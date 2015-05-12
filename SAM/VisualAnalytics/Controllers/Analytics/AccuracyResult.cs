using System;
using System.Linq;

namespace VisualAnalytics.Controllers.Analytics
{
    public class AccuracyResult
    {
        public long IDDistrict;
        public long idConsumtionPlace;
        public long startDate;
        public long endDate;

        //ME     RMSE      MAE       MPE     MAPE     MASE       ACF1

        public double ME;
        public double RMSE;
        public double MAE;
        public double MPE;
        public double MAPE;
        public double MASE;
        public double ACF1;

        public double testME;
        public double testRMSE;
        public double testMAE;
        public double testMPE;
        public double testMAPE;
        public double testMASE;

        public override string ToString()
        {
            string ret = string.Empty;
            ret += Math.Round(ME).ToString() + ";";
            ret += Math.Round(RMSE).ToString() + ";";
            ret += Math.Round(MAE).ToString() + ";";
            ret += Math.Round(MPE).ToString() + ";";
            ret += Math.Round(MAPE).ToString() + ";";
            ret += Math.Round(MASE).ToString() + ";";
            ret += Math.Round(ACF1).ToString() + ";";

            ret += Math.Round(testME).ToString() + ";";
            ret += Math.Round(testRMSE).ToString() + ";";
            ret += Math.Round(testMAE).ToString() + ";";
            ret += Math.Round(testMPE).ToString() + ";";
            ret += Math.Round(testMAPE).ToString() + ";";
            ret += Math.Round(testMASE).ToString() + ";";
            // ret += Math.Round(testACF1).ToString();

            return ret;
        }
    }
}