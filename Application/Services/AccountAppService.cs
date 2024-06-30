using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Util.Exceptions;
using ControleFinanceiro.Application.ViewModels.Account;
using ControleFinanceiro.Infra.Data.Repositories.Interfaces;
using ControleFinanceiro.Application.Context;


namespace ControleFinanceiro.Application.Services;

public class AccountAppService : IAccountAppService
{

    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;

    public AccountAppService(IAccountRepository accountRepository, IUserRepository userRepository, ITransactionRepository transactionRepository, IUserContext userContext)
    {
        _accountRepository = accountRepository;
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
        _userContext = userContext;
    }

    public async Task<Account> Create(CreateAccountViewModel dto)
    {
        var userId = await _userContext.GetUserId();

        await ValidateName(dto, userId);

        Account newAccount = new(dto.Name, dto.Description, userId);
        
        await _accountRepository.AddAsync(newAccount);

        return newAccount;
    }

    public async Task<IEnumerable<Account>> GetAccountsByUserId()
    {
        var userId = await _userContext.GetUserId();
        
        return await _accountRepository.GetAccountsByUserId(userId);
    }

    public async Task<Account> GetWith5Transactions(Guid id)
    {
        return await _accountRepository.GetWith5Transactions(id) ?? throw new SystemContextException("Conta não encontrada");
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Account> Update(UpdateAccountViewModel dto)
    {
        Account account = await _accountRepository.GetByIdAsync(dto.Id) ?? throw new SystemContextException("Conta não encontrada");
        await ValidateName(dto, account);

        await _accountRepository.UpdateAsync(account.Update(dto.Name, dto.Description));

        return account;
    }

    public async Task<BalanceResultViewModel> GetBalanceByDate(DateTime dateTime, Guid accountId)
    {
        Account account = await _accountRepository.GetByIdAsync(accountId) ?? throw new SystemContextException("Conta não encontrada");

        decimal balance = 0;

        var transaction = await _transactionRepository.GetLastTransactionByDate(dateTime, accountId);

        if (transaction == null) return new BalanceResultViewModel(accountId, balance, dateTime);

        if (transaction!.Type == Domain.Enums.Transactions.TransactionType.Expense)
            balance = transaction.BalanceBefore - transaction.Value;
        else
            balance = transaction.BalanceBefore + transaction.Value;

        return new BalanceResultViewModel(accountId, balance, dateTime);
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
