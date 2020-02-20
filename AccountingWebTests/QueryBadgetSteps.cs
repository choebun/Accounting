using System;
using TechTalk.SpecFlow;

namespace AccountingWebTests
{
    [Binding]
    public class QueryBadgetSteps
    {
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
        
        [When(@"Query")]
        public void WhenQuery()
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
