using ControleFinanceiro.Domain.Enums.Transactions;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Application.ViewModels.Transaction
{
    public class CreateTransactionViewModel : IValidatableObject
    {
        public string? Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public string? Category { get; set; }
        public TransactionType Type { get; set; }
        public Guid AccountId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Value <= 0)
            {
                yield return new ValidationResult("O campo valor deve ser maior que zero", new[] { nameof(Value) });
            }

            if (Value == default)
            {
                yield return new ValidationResult("O campo valor é obrigatório", new[] { nameof(Value) });
            }

            if (Date == default)
            {
                yield return new ValidationResult("O campo data é obrigatório", new[] { nameof(Date) });
            }

            if (Type == default)
            {
                yield return new ValidationResult("O tipo de transação é obrigatório", new[] { nameof(Type) });
            }

            if (AccountId == Guid.Empty)
            {
                yield return new ValidationResult("O campo de Id da conta é obrigatório", new[] { nameof(AccountId) });
            }
        }
    }
}
