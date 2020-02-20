using System;
using TechTalk.SpecFlow;

namespace AccountingWebTests.steps
{
    [Binding]
    public class BudgetQuerySteps
    {
        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"Amount in DB")]
        public void GivenAmountInDB(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"intput (.*)")]
        public void GivenIntput(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"Query")]
        public void WhenQuery()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"it should show result (.*)")]
        public void ThenItShouldShowResult(Decimal p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
