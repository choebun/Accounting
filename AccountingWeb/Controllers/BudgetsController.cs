using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            ViewBag.Message = YearMonth_Start;
            return View();
        }
    }
}