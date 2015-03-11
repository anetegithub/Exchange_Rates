using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Logic;

namespace ExchangeRates.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Some()
        {
            DateTime Start = Convert.ToDateTime(Request["from"], System.Globalization.CultureInfo.InvariantCulture),
                End = Convert.ToDateTime(Request["to"], System.Globalization.CultureInfo.InvariantCulture);

            Gold Money = Gold.EUR;
                Enum.TryParse<Logic.Gold>(Request["Money"], out Money);

            var MyData = new Data();
            var Valuesq = MyData.GetValueRange(Start, End, Money);

            List<String> DateTimeList = new List<string>();
            var dtes= Enumerable.Range(0, 1 + End.Subtract(Start).Days)
                .Select(offset => Start.AddDays(offset))
                .ToArray();
            foreach(var d in dtes)
                DateTimeList.Add(d.ToString("MM.dd.yyyy"));

            ViewData["Dates"] = DateTimeList;
            ViewData["Values"] = Valuesq;
            ViewData["Money"] = Request["Money"];
            return View();
        }
    }
}