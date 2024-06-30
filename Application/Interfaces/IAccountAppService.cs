using ControleFinanceiro.Application.ViewModels.Account;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface IAccountAppService
{
    Task<Account> Create(CreateAccountViewModel viewModel);
    Task<Account> Update(UpdateAccountViewModel viewModel);
    Task Delete(Guid id);
    Task<BalanceResultViewModel> GetBalanceByDate(DateTime dateTime, Guid accountId);
    Task<IEnumerable<Account>> GetAccountsByUserId();
    Task<Account> GetWith5Transactions(Guid id);
}
