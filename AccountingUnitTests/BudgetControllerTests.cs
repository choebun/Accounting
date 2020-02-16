using AccountingWeb.Controllers;
using AccountingWeb.Models.Services;
using NSubstitute;
using NUnit.Framework;

namespace AccountingUnitTests
{
    [TestFixture]
    public class BudgetControllerTests
    {
        [Test]
        public void create_a_budget()
        {
            var budgetService = Substitute.For<IBudgetService>();

            var budgetsController = new BudgetsController(budgetService);
            budgetsController.CreateBudget("202003", 31);

            budgetService.Received().Save("202003", 31); 
        }
    }
}