using ControleFinanceiro.Domain.Enums.Transactions;

namespace ControleFinanceiro.Domain.Models;

public class MonthBalance
{
    public Guid Id { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal Balance { get; set; }
    public ICollection<Transaction> Transactions { get; set; }

    // Relacionamentos
    public Guid AccountId { get; set; }
    public Account Account { get; set; }

    public MonthBalance(int month, int year, decimal balance, Guid accountId)
    {
        Id = Guid.NewGuid();
        Month = month;
        Year = year;
        Balance = balance;
        AccountId = accountId;
    }

    public void UpdateBalance(decimal value, TransactionType type)
    {
        if(type == TransactionType.Expense)
            Balance -= value;
        else
            Balance += value;
    }
}
