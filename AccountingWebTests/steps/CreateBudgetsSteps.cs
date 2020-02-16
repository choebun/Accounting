using System;
using System.Linq;
using AccountingWebTests.DataModels;
using AccountingWebTests.PageObjects;
using Atata;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace AccountingWebTests.steps
{
    [Binding]
    public class CreateBudgetsSteps : Steps
    {
        private static CreateBudgetPage _createBudgetPage;
        private CreateBudgetResultPage _createBudgetResultPage;

        [BeforeTestRun]
        public static void SetUpTestRun()
        {
            AtataContext.GlobalConfiguration
                        .UseChrome()
                        //.WithArguments("start-maximized")
                        .WithLocalDriverPath()
                        .UseBaseUrl("http://localhost:50564/")
                        .UseCulture("en-us")
                        .UseAllNUnitFeatures();
        }

        [BeforeScenario]
        public static void SetUpScenario()
        {
            AtataContext.Configure().Build();

            using (var dbContext = new AccountingEntitiesForTest())
            {
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [Budgets] ");
            }

            _createBudgetPage = Go.To<CreateBudgetPage>();
        }

        [AfterScenario]
        public static void TearDownScenario()
        {
            AtataContext.Current?.CleanUp();
        }

        [Given(@"budget for setting")]
        public void GivenBudgetForSetting(Table table)
        {
            var budget = table.CreateInstance<Budget>();
            ScenarioContext.Set(budget);
        }

        [Given(@"there are budgets")]
        public void GivenThereAreBudgets(Table table)
        {
            using (var dbContext = new AccountingEntitiesForTest())
            {
                var budgets = table.CreateSet<Budget>();
                dbContext.Budgets.AddRange(budgets);
                dbContext.SaveChanges();
            } 
        }

        [Then(@"it should be updated succeeded")]
        public void ThenItShouldBeUpdatedSucceeded()
        {
            _createBudgetResultPage
                .Status.Should.Contain("succeeded")
                .Status.Should.Contain("updated");
        }

        [When(@"I create")]
        public void WhenICreate()
        {
            var budget = ScenarioContext.Get<Budget>();
            _createBudgetResultPage = _createBudgetPage.Create(budget);
        }

        [Then(@"it should be created succeeded")]
        public void ThenItShouldBeCreatedSuccessfully()
        {
            _createBudgetResultPage
                .Status.Should.Contain("succeeded")
                .Status.Should.Contain("created");
        }

        [Then(@"there should be budgets existed")]
        public void ThenThereShouldBeBudgetsExisted(Table table)
        {
            using (var dbContext = new AccountingEntitiesForTest())
            {
                table.CompareToInstance(dbContext.Budgets.First());
            }
        }
    }
}