using ControleFinanceiro.Domain.Enums.Transactions;

namespace ControleFinanceiro.Domain.Models
{
    public class Transaction : BaseEntity
    {
        public string? Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public string? Category { get; set; }
        public TransactionType Type { get; set; }
        public decimal BalanceBefore { get; set; }

        public Transaction() { }
        public Transaction(decimal value, DateTime date, TransactionType type, Account account, string? description = "", string? category = "")
        {
            Value = value;
            Date = date;
            Type = type;
            BalanceBefore = account.Balance;
            AccountId = account.Id;
            Description = description;
            Category = category;
        }

        // Relacionamentos
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
