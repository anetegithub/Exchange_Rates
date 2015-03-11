using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logic
{
    /// <summary>
    /// For work with data
    /// </summary>
    public class Data
    {
        /// <summary>
        /// return list of decimals by data range
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public List<Decimal> GetValueRange(DateTime Start, DateTime End, Gold Money)
        {
            //new list
            List<Decimal> Values = new List<decimal>();

            //array of dates betwen range
            var Dates = Enumerable.Range(0, 1 + End.Subtract(Start).Days)
          .Select(offset => Start.AddDays(offset))
          .ToArray();

            //get decimal for each date in range
            foreach (DateTime d in Dates)
            {
                //add to list
                Values.Add(GetValueByDate(d, Money));
            }

            //magic, right?)
            return Values;
        }

        /// <summary>
        /// return value by incoming date from db or parse into db and then return
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private decimal GetValueByDate(DateTime d, Gold Money)
        {
            var db=GetValueFromDB(d,Money);

            return db>0?db:GetValueFromWeb(d,Money);
        }

        /// <summary>
        /// retun value by date or -1
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private decimal GetValueFromDB(DateTime d, Gold Money)
        {
            return DataBase.GetRateByDateKey(d, Money);
        }

        /// <summary>
        /// parse openexchangerates.org to db and return value
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private decimal GetValueFromWeb(DateTime d, Gold Money)
        {
            return WebBase.GetRateByDateKey(d, Money);
        }
    }
}