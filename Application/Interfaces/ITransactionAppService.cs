using ControleFinanceiro.Application.ViewModels.Transaction;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface ITransactionAppService
{
    Task Create(CreateTransactionViewModel viewModel);
    Task<IEnumerable<Transaction>> GetTransactionsBetweenDates(GetTransactionsBetweenDatesViewModel viewModel);
}
