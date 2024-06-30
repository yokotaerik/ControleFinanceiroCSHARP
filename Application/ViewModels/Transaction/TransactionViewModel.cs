using ControleFinanceiro.Domain.Enums.Transactions;

namespace ControleFinanceiro.Application.ViewModels.Transaction;

public class TransactionViewModel
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
    public string? Category { get; set; }
    public TransactionType Type { get; set; }
    public decimal BalanceBefore { get; set; }
}
