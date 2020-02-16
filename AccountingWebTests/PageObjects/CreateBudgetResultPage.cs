using Atata;

namespace AccountingWebTests.PageObjects
{
    using _ = CreateBudgetResultPage;
    public class CreateBudgetResultPage : Page<_>
    {
        [FindById("status")]
        public Text<_> Status { get; set; }
    }
}