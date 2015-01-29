using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeteoInfo.Classes
{
    /// <summary>
    /// Statistics
    /// </summary>
    public static class Statistics
    {
        #region Methods
        /// <summary>
        /// Mean
        /// </summary>
        /// <param name="aDataList">data list</param>
        /// <returns>Mean</returns>
        public static double Mean(List<double> aDataList)
        {
            double aSum = 0.0;

            for (int i = 0; i < aDataList.Count; i++)
                aSum = aSum + aDataList[i];

            return aSum / aDataList.Count;
        }

        /// <summary>
        /// Maximum
        /// </summary>
        /// <param name="aDataList">data list</param>
        /// <returns>Maximum</returns>
        public static double Maximum(List<double> aDataList)
        {
            double aMax;

            aMax = aDataList[0];
            for (int i = 1; i < aDataList.Count; i++)
                aMax = Math.Max(aMax, aDataList[i]);

            return aMax;
        }

        /// <summary>
        /// Maximum
        /// </summary>
        /// <param name="aDataList">data list</param>
        /// <returns>Maximum</returns>
        public static int Maximum(List<int> aDataList)
        {
            int aMax;

            aMax = aDataList[0];
            for (int i = 1; i < aDataList.Count; i++)
                aMax = Math.Max(aMax, aDataList[i]);

            return aMax;
        }

        /// <summary>
        /// Minimum
        /// </summary>
        /// <param name="aDataList">data list</param>
        /// <returns>Minimum</returns>
        public static double Minimum(List<double> aDataList)
        {
            double aMin;

            aMin = aDataList[0];
            for (int i = 1; i < aDataList.Count; i++)
                aMin = Math.Min(aMin, aDataList[i]);

            return aMin;
        }

        /// <summary>
        /// Median
        /// </summary>
        /// <param name="aDataList">data list</param>
        /// <returns>Median</returns>
        public static double Median(List<double> aDataList)
        {
            aDataList.Sort();
            if (aDataList.Count % 2 == 0)
                return (aDataList[aDataList.Count / 2] + aDataList[aDataList.Count / 2 - 1]) / 2.0;
            else
                return aDataList[aDataList.Count / 2];

        }

        /// <summary>
        /// Quantile
        /// </summary>
        /// <param name="aDataList">data list</param>
        /// <param name="aNum">quantile index</param>
        /// <returns>quantile value</returns>
        public static double Quantile(List<double> aDataList, int aNum)
        {
            aDataList.Sort();
            double aData = 0;
            switch (aNum)
            {
                case 0:
                    aData = Minimum(aDataList);
                    break;
                case 1:
                    if ((aDataList.Count + 1) % 4 == 0)
                        aData = aDataList[(aDataList.Count + 1) / 4 - 1];
                    else
                        aData = aDataList[(aDataList.Count + 1) / 4 - 1] + 0.75 * (aDataList[(aDataList.Count + 1) / 4] -
                          aDataList[(aDataList.Count + 1) / 4 - 1]);
                    break;
                case 2:
                    aData = Median(aDataList);
                    break;
                case 3:
                    if ((aDataList.Count + 1) % 4 == 0)
                        aData = aDataList[(aDataList.Count + 1) * 3 / 4 - 1];
                    else
                        aData = aDataList[(aDataList.Count + 1) * 3 / 4 - 1] + 0.25 * (aDataList[(aDataList.Count + 1) * 3 / 4] -
                          aDataList[(aDataList.Count + 1) * 3 / 4 - 1]);
                    break;
                case 4:
                    aData = Maximum(aDataList);
                    break;
            }

            return aData;
        }

        /// <summary>
        /// StandartDeviation
        /// </summary>
        /// <param name="aDataList">data list</param>
        /// <returns>standard deviation</returns>
        public static double StandardDeviation(List<double> aDataList)
        {
            double theMean, theSqDev, theSumSqDev, theVariance, theStdDev, theValue;
            int i;

            theMean = Mean(aDataList);
            theSumSqDev = 0;
            for (i = 0; i < aDataList.Count; i++)
            {
                theValue = aDataList[i];
                theSqDev = (theValue - theMean) * (theValue - theMean);
                theSumSqDev = theSqDev + theSumSqDev;
            }

            if (aDataList.Count > 1)
            {
                theVariance = theSumSqDev / (aDataList.Count - 1);
                theStdDev = Math.Sqrt(theVariance);
            }
            else
            {
                theVariance = 0;
                theStdDev = 0;
            }

            return theStdDev;
        }
        #endregion
    }
}
