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
            Dictionary<string, decimal> AmountOfDay = new Dictionary<string, decimal>();
            Dictionary<string, decimal> AmountOfMon = new Dictionary<string, decimal>();

            using (var dbContext = new AccountingEntities())
            {

                var BundleList = dbContext.Budgets.ToList();
                foreach (Budget bufgetList in BundleList)
                {
                    DateTime firstDay = DateTime.ParseExact(bufgetList.YearMonth + "01", "yyyyMMdd", null);
                    var daysinMonth = DateTime.DaysInMonth(firstDay.Year, firstDay.Month);
                    var dayAmnout = bufgetList.Amount / daysinMonth;

                    AmountOfDay.Add(bufgetList.YearMonth, dayAmnout);
                    AmountOfMon.Add(bufgetList.YearMonth, bufgetList.Amount);
                }
            }

            //DateTime dt1 = DateTime.ParseExact(QueryFrom, "yyyyMMdd", null);
            //DateTime dt2 = DateTime.ParseExact(QueryTo, "yyyyMMdd", null);
            //var ts = (dt2 - dt1).Days+1;

            DateTime queryFromMonthDay = DateTime.ParseExact(YearMonth_Start, "yyyyMMdd", null);
            var queryFormdaysinMonth = DateTime.DaysInMonth(queryFromMonthDay.Year, queryFromMonthDay.Month);
            var monthFromDays = queryFromMonthDay.Day - 1;
            var startMonthAccount = monthFromDays * AmountOfDay[(queryFromMonthDay.Year+ queryFromMonthDay.Month).ToString()];

            DateTime queryToMonthDay = DateTime.ParseExact(YearMonth_End, "yyyyMMdd", null);
            var queryTodaysinMonth = DateTime.DaysInMonth(queryToMonthDay.Year, queryToMonthDay.Month);
            var monthToDays = queryTodaysinMonth - queryToMonthDay.Day;
            var EndMonthAccount = monthToDays * AmountOfDay[(queryToMonthDay.Year + queryToMonthDay.Month).ToString()];

            decimal total = 0;
            while (queryFromMonthDay.Month!= queryToMonthDay.Month)
            {
                total += AmountOfMon[(queryFromMonthDay.Year + queryFromMonthDay.Month).ToString()];
                queryFromMonthDay.AddMonths(1);
            }

            total = total - EndMonthAccount - startMonthAccount;

            ViewBag.Message = total;
            return View();
        }
    }
}