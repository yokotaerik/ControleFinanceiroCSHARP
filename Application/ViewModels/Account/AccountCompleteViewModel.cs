
using ControleFinanceiro.Application.ViewModels.Transaction;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.ViewModels.Account

{
    public class AccountCompleteViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }

        // Entidades relacionadas
        public Goal? Goal { get; set; }
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
    }
}
