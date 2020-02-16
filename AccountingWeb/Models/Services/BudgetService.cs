using System;
using AccountingWeb.Models.Entities;

namespace AccountingWeb.Models.Services
{
    public class BudgetService : IBudgetService
    {
        public void Save(string yearMonth, int amount)
        {
            using (var dbContext = new AccountingEntities())
            {
                dbContext.Budgets.Add(new Budget() { Amount = amount, YearMonth = yearMonth });
                dbContext.SaveChanges();
            }
        }
    }
}