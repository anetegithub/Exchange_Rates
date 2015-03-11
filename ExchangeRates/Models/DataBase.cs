using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Logic
{
    public static class DataBase
    {
        /// <summary>
        /// Return decimal by date and key
        /// </summary>
        /// <param name="d"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static decimal GetRateByDateKey(DateTime d,Gold Key)
        {
            //open connection
            using (var db = new RatesContext())
            {
                //select values where date equeal our date

                //but we can't work with data
                var query = (from b in db.Rates
                             //where b.Date.Date.Equals(d.Date)
                             select b).ToList();

                //search values where key equal our key
                foreach(Rate r in query)
                {
                    //return our value
                    if (r.Key == Key && r.Date.Date.Equals(d.Date))
                        return r.Value;
                }

                //data is not availible yet
                return -1;
            }
        }

        public static Rate NewRate
        {
            set
            {
                //open connection
                using (var db = new RatesContext())
                {
                    //disable auto updates
                    db.Configuration.AutoDetectChangesEnabled = false;
                    //add new item
                    db.Rates.Add(value);
                    //apply changes
                    db.ChangeTracker.DetectChanges();
                    //save result
                    db.SaveChanges();
                }
            }
        }

        public class Rate
        {
            //autoincerement
            public Int32 Id { get; set; }            
            public DateTime Date { get; set; }
            //rub,eur, e.t.c.
            public Gold Key { get; set; }
            public Decimal Value { get; set; }
        }

        public class RatesContext : DbContext
        {
            //main table
            public DbSet<Rate> Rates { get; set; }
        }
    }
}