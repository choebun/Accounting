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
        private BudgetsController _budgetsController;
        private IBudgetService _budgetService;

        [SetUp]
        public void SetUp()
        {
            _budgetService = Substitute.For<IBudgetService>();
            _budgetsController = new BudgetsController(_budgetService);
        }

        [Test]
        public void create_a_budget_should_invoke_budgetService_save()
        {
            WhenCreateBudget("202003", 31);
            BudgetServiceShouldSave("202003", 31);
        }

        [Test]
        public void create_a_budget_and_viewResult_should_contain_succeed_status()
        {
            var viewResult = WhenCreateBudget("202003", 31) as ViewResult;

            (viewResult.ViewBag.Status as string).Should().ContainAll("created", "succeeded");
        }

        [Test]
        public void update_a_budget_when_budget_existed()
        {
            _budgetService.Save(Arg.Any<string>(), Arg.Any<int>()).ReturnsForAnyArgs(true);

            var viewResult = WhenCreateBudget("202003", 31) as ViewResult;

            (viewResult.ViewBag.Status as string).Should().ContainAll("updated", "succeeded");
        }


        private void BudgetServiceShouldSave(string yearMonth, int amount)
        {
            _budgetService.Received().Save(yearMonth, amount);
        }

        private ActionResult WhenCreateBudget(string yearMonth, int amount)
        {
            return _budgetsController.CreateBudget(yearMonth, amount);
        }
    }
}