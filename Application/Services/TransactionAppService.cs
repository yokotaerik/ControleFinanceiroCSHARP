using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.ViewModels.Transaction;
using ControleFinanceiro.Domain.Enums.Transactions;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data.Repositories.Interfaces;

namespace ControleFinanceiro.Application.Services;

public class TransactionAppService : ITransactionAppService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;

    public TransactionAppService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
    {
        _transactionRepository=transactionRepository;
        _accountRepository=accountRepository;
    }

    public async Task<Transaction> Create(CreateTransactionViewModel dto)
    {
        var account = await _accountRepository.GetByIdAsync(dto.AccountId) 
                                ?? throw new Exception("Conta não encontrada");

        Transaction newTransaction = new Transaction(
                                            dto.Value,
                                            dto.Date,
                                            dto.Type, 
                                            account,
                                            dto.Description,
                                            dto.Category
                                            );

        if(newTransaction.Type == TransactionType.Expense) account.Balance -= newTransaction.Value;
        else account.Balance += newTransaction.Value;

        await _transactionRepository.AddAsync(newTransaction);
        await _accountRepository.UpdateAsync(account);
        return newTransaction;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsBetweenDates(GetTransactionsBetweenDatesViewModel viewModel)
    {
        var transactions = await _transactionRepository.GetTransactionsBetweenDates(viewModel.AccountId, viewModel.StartDate, viewModel.EndDate);
        return transactions;
    }
}
