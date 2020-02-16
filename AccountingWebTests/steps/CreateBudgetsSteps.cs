using System;
using AccountingWebTests.PageObjects;
using Atata;
using TechTalk.SpecFlow;

namespace AccountingWebTests.steps
{
    [Binding]
    public class CreateBudgetsSteps : Steps
    {
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

            Go.To<CreateBudgetPage>();
        }

        [AfterScenario]
        public static void TearDownScenario()
        {
            AtataContext.Current?.CleanUp();
        }

        [Given(@"budget for setting")]
        public void GivenBudgetForSetting(Table table)
        {
            ScenarioContext.Pending();
        }

        [When(@"I create")]
        public void WhenICreate()
        {
            ScenarioContext.Pending();
        }

        [Then(@"it should be created successfully")]
        public void ThenItShouldBeCreatedSuccessfully()
        {
            ScenarioContext.Pending();
        }

        [Then(@"there should be budgets existed")]
        public void ThenThereShouldBeBudgetsExisted(Table table)
        {
            ScenarioContext.Pending();
        }
    }
}