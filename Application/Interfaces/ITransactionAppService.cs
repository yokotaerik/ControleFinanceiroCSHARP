using ControleFinanceiro.Application.ViewModels.Transaction;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface ITransactionAppService
{
    Task<Transaction> Create(CreateTransactionViewModel dto);
    Task<IEnumerable<Transaction>> GetTransactionsBetweenDates(GetTransactionsBetweenDatesViewModel dto);
}
