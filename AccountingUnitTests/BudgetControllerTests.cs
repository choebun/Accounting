using System.Web.Mvc;
using AccountingWeb.Controllers;
using AccountingWeb.Models.Services;
using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

namespace AccountingUnitTests
{
    [TestFixture]
    public class BudgetControllerTests
    {
        [Test]
        public void create_a_budget_should_invoke_budgetService_save()
        {
            var budgetService = Substitute.For<IBudgetService>();

            var budgetsController = new BudgetsController(budgetService);
            budgetsController.CreateBudget("202003", 31);

            budgetService.Received().Save("202003", 31);
        }

        [Test]
        public void create_a_budget_and_viewResult_should_contain_succeed_status()
        {
            var budgetService = Substitute.For<IBudgetService>();

            var budgetsController = new BudgetsController(budgetService);
            var viewResult = budgetsController.CreateBudget("202003", 31) as ViewResult;

            (viewResult.ViewBag.Status as string).Should().Contain("succeeded");
        }
    }
}