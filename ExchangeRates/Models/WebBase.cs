using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net;
using System.IO;
using System.Web.Helpers;

namespace Logic
{
    public static class WebBase
    {
        public static readonly string app_id = "b5fe6697bc01467893149ccc4fc9c103";

        public static decimal GetRateByDateKey(DateTime d, Gold Money)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://openexchangerates.org/api/historical/" + d.Date.ToString("yyyy-MM-dd") + ".json?app_id=" + app_id);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var Rates = Json.Decode(responseString);
            switch (Money)
            {
                case Gold.RUB: return AddToDb(Money, Rates.rates.RUB, d);
                case Gold.EUR: return AddToDb(Money, Rates.rates.EUR, d);
                case Gold.USD: return AddToDb(Money, Rates.rates.USD, d);
                case Gold.GBP: return AddToDb(Money, Rates.rates.GBP, d);
                case Gold.JPY: return AddToDb(Money, Rates.rates.JPY, d);
            }
            return -1;
        }

        public static decimal AddToDb(Gold Key, Decimal Value, DateTime When)
        {
            DataBase.NewRate = new DataBase.Rate() { Date = When, Key = Key, Value = Value };
            return Value;
        }
    }
}