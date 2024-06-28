using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data.Repositories.Interfaces;
using ControleFinanceiro.Util.Exceptions;
using ControleFinanceiro.Application.ViewModels.Account;


namespace ControleFinanceiro.Application.Services;

public class AccountAppService : IAccountAppService
{

    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;

    public AccountAppService(IAccountRepository accountRepository, IUserRepository userRepository, ITransactionRepository transactionRepository)
    {
        _accountRepository=accountRepository;
        _userRepository=userRepository;
        _transactionRepository=transactionRepository;
    }

    public async Task Create(CreateAccountViewModel dto)
    {
        await ValidateName(dto);

        Account newAccount = new(dto.Name, dto.Description, dto.UserId);
        
        await _accountRepository.AddAsync(newAccount);

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
    private async Task ValidateName(CreateAccountViewModel dto)
    {
        User user = await _userRepository.GetByIdWithAccounts(dto.UserId);
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
