namespace ControleFinanceiro.Application.ViewModels.Account;

public class BalanceResultViewModel
{
    public BalanceResultViewModel(Guid accountId, decimal balance, DateTime date)
    {
        AccountId = accountId;
        Balance = balance;
        Date = date;
    }

    public Guid AccountId { get; set; }
    public decimal Balance { get; set; }
    public DateTime Date { get; set; }
}
