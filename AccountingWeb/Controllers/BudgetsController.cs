using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingWeb.Models.Entities;
using AccountingWeb.Models.Services;

namespace AccountingWeb.Controllers
{
    public class BudgetsController : Controller
    {
        private readonly IBudgetService _budgetService;

        public BudgetsController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBudget(string yearMonth, int amount)
        {
            var isUpdated = _budgetService.Save(yearMonth, amount);

            ViewBag.Status = isUpdated ? "budget updated succeeded!" : "budget created succeeded!";

            return View();
        }

        public ActionResult Query(string YearMonth_Start, string YearMonth_End)
        {
            Dictionary<DateTime, decimal> MyDic = new Dictionary<DateTime, decimal>();

            using (var dbContext = new AccountingEntities())
            {

                var BundleList = dbContext.Budgets.ToList();
                foreach (Budget bufgetList in BundleList)
                {
                    DateTime firstDay = DateTime.ParseExact(bufgetList.YearMonth + "01", "yyyyMMdd", null);
                    var daysinMonth = DateTime.DaysInMonth(firstDay.Year, firstDay.Month);
                    var dayAmnout = bufgetList.Amount / daysinMonth;

                    MyDic.Add(firstDay, dayAmnout);
                }
            }

            //DateTime dt1 = DateTime.ParseExact(QueryFrom, "yyyyMMdd", null);
            //DateTime dt2 = DateTime.ParseExact(QueryTo, "yyyyMMdd", null);
            //var ts = (dt2 - dt1).Days+1;

            DateTime queryFromMonthDay = DateTime.ParseExact(YearMonth_Start, "yyyyMMdd", null);
            var queryFormdaysinMonth = DateTime.DaysInMonth(queryFromMonthDay.Year, queryFromMonthDay.Month);
            var monthFromDays = queryFormdaysinMonth - queryFromMonthDay.Day + 1;
            var startMonthAccount = monthFromDays * MyDic[DateTime.ParseExact(YearMonth_Start, "yyyyMMdd", null)];

            DateTime queryToMonthDay = DateTime.ParseExact(YearMonth_End, "yyyyMMdd", null);
            var queryTodaysinMonth = DateTime.DaysInMonth(queryToMonthDay.Year, queryToMonthDay.Month);
            var monthToDays = queryTodaysinMonth - queryToMonthDay.Day + 1;
            var EndMonthAccount = monthToDays * MyDic[DateTime.ParseExact(YearMonth_End, "yyyyMMdd", null)];

            ViewBag.Message = YearMonth_Start;
            return View();
        }
    }
}