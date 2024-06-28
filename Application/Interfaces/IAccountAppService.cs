using ControleFinanceiro.Application.ViewModels.Account;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface IAccountAppService
{
    Task Create(CreateAccountViewModel viewModel);
    Task<Account> Update(UpdateAccountViewModel viewModel);
    Task Delete(Guid id);
    Task<BalanceResultViewModel> GetBalanceByDate(DateTime dateTime, Guid accountId);
}
