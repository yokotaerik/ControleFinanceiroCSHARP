using ControleFinanceiro.Application.ViewModels.Account;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface IAccountAppService
{
    Task<AccountViewModel> Create(CreateAccountViewModel viewModel);
    Task<AccountViewModel> Update(UpdateAccountViewModel viewModel);
    Task Delete(Guid id);
    Task<BalanceResultViewModel> GetBalanceByDate(GetBalanceByDateViewModel viewModel);
    Task<IEnumerable<AccountListViewModel>> GetAccountsByUserId();
    Task<AccountCompleteViewModel> GetWith5Transactions(Guid id);
}
