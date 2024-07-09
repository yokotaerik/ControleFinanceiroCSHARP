using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Util.Exceptions;
using ControleFinanceiro.Application.ViewModels.Account;
using ControleFinanceiro.Infra.Data.Repositories.Interfaces;
using ControleFinanceiro.Application.Context;
using System.Collections.Generic;
using AutoMapper;


namespace ControleFinanceiro.Application.Services;

public class AccountAppService : IAccountAppService
{

    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMonthBalanceRepository _monthBalanceRepository;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;

    public AccountAppService(
        IAccountRepository accountRepository, IUserRepository userRepository,
        ITransactionRepository transactionRepository, IUserContext userContext, IMapper mapper,
        IMonthBalanceRepository monthBalanceRepository)
    {
        _accountRepository = accountRepository;
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
        _userContext = userContext;
        _mapper=mapper;
        _monthBalanceRepository=monthBalanceRepository;
    }

    public async Task<AccountViewModel> Create(CreateAccountViewModel dto)
    {
        var userId = await _userContext.GetUserId();

        await ValidateName(dto, userId);

        Account newAccount = new(dto.Name, dto.Description, userId);
        
        await _accountRepository.AddAsync(newAccount);

        return _mapper.Map<AccountViewModel>(newAccount);
    }

    public async Task<IEnumerable<AccountListViewModel>> GetAccountsByUserId()
    {
        var userId = await _userContext.GetUserId();
        
        return  _mapper.Map<IEnumerable<AccountListViewModel>>(await _accountRepository.GetAccountsByUserId(userId));
    }

    public async Task<AccountCompleteViewModel> GetWith5Transactions(Guid id)
    {
        return _mapper.Map<AccountCompleteViewModel>(await _accountRepository.GetWith5Transactions(id) ?? throw new SystemContextException("Conta não encontrada"));
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<AccountViewModel> Update(UpdateAccountViewModel dto)
    {
        Account account = await _accountRepository.GetByIdAsync(dto.Id) ?? throw new SystemContextException("Conta não encontrada");

        await ValidateName(dto, account);

        await _accountRepository.UpdateAsync(account.Update(dto.Name, dto.Description));

        return _mapper.Map<AccountViewModel>(account);
    }

    public async Task<BalanceResultViewModel> GetBalanceByDate(GetBalanceByDateViewModel viewModel)
    {
        decimal balance = await _monthBalanceRepository.GetBalanceByMonthAndYear(viewModel.Date, viewModel.AccountId) ?? 0;

        return new BalanceResultViewModel(viewModel.AccountId, balance, viewModel.Date);
    }


    #region Validacoes 
    private async Task ValidateName(CreateAccountViewModel dto, Guid userId)
    {
        User user = await _userRepository.GetByIdWithAccounts(userId);
        bool nomeEmUso = user.Accounts.Where(a => a.Name == dto.Name).Any();
        if (nomeEmUso) throw new SystemContextException("Você possui uma conta com esse nome");
    }

    private async Task ValidateName(UpdateAccountViewModel dto, Account account)
    {
        User user = await _userRepository.GetByIdWithAccounts(account.UserId);
        bool nomeEmUso = user.Accounts.Where(a => a.Name == dto.Name).Any();
        if (nomeEmUso) throw new SystemContextException("Você possui uma conta com esse nome");

    }
    #endregion
}
