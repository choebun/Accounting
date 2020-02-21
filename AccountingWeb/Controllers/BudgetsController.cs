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

            DateTime queryFromMonthDay = DateTime.ParseExact(YearMonth_Start, "yyyyMMdd", null);
            var monthFromDays = queryFromMonthDay.Day - 1;
            var startMonthAccount = monthFromDays * AmountOfDay[(queryFromMonthDay.Year+ queryFromMonthDay.Month.ToString("00")).ToString()];

            DateTime queryToMonthDay = DateTime.ParseExact(YearMonth_End, "yyyyMMdd", null);
            var queryTodaysinMonth = DateTime.DaysInMonth(queryToMonthDay.Year, queryToMonthDay.Month);
            var monthToDays = queryTodaysinMonth - queryToMonthDay.Day;
            var EndMonthAccount = monthToDays * AmountOfDay[(queryToMonthDay.Year + queryToMonthDay.Month.ToString("00")).ToString()];

            decimal total = 0;
            while ((queryFromMonthDay.Month!= queryToMonthDay.Month) || (queryFromMonthDay.Year != queryToMonthDay.Year))
            {
                total += AmountOfMon[(queryFromMonthDay.Year + queryFromMonthDay.Month.ToString("00")).ToString()];
                queryFromMonthDay = queryFromMonthDay.AddMonths(1);
            }
            total += AmountOfMon[(queryFromMonthDay.Year + queryFromMonthDay.Month.ToString("00")).ToString()];

            total = total - EndMonthAccount - startMonthAccount;

            ViewBag.Message = total;
            return View();
        }
    }
}