namespace AccountingWeb.Models.Services
{
    public interface IBudgetService
    {
        void Save(string yearMonth, int amount);
    }
}