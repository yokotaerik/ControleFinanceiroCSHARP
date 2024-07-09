using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.ViewModels.Transaction;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data.Repositories.Interfaces;
using ControleFinanceiro.Util.Exceptions;

namespace ControleFinanceiro.Application.Services;

public class TransactionAppService : ITransactionAppService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMonthBalanceRepository _monthBalanceRepository;

    public TransactionAppService(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IMonthBalanceRepository monthBalanceRepository)
    {
        _transactionRepository=transactionRepository;
        _accountRepository=accountRepository;
        _monthBalanceRepository=monthBalanceRepository;
    }

    public async Task Create(CreateTransactionViewModel viewModel)
    {
        var account = await _accountRepository.GetByIdAsync(viewModel.AccountId) 
                                ?? throw new Exception("Conta não encontrada");

        if(viewModel.IsRepeat && !viewModel.RepeatTimes.HasValue) throw new SystemContextException("Faltando campo de quantidade de vezes");
        
        if(viewModel.RepeatTimes > 1) {
            if(viewModel.RepeatTimes > 120) throw new SystemContextException("O número máximo de repetições é 120");
            if(viewModel.RepeatTimes < 1) throw new SystemContextException("O número mínimo de repetições é 1");
        }

        viewModel.RepeatTimes = 1;

        for(int i = 0; i < viewModel.RepeatTimes; i++) {
            Transaction newTransaction = new(
                                            viewModel.Value,
                                            viewModel.Date.AddMonths(i),
                                            viewModel.Type, 
                                            account,
                                            viewModel.Description,
                                            viewModel.Category
                                            );

            await CreateOrUpdateMonthBalance(newTransaction);

            await _transactionRepository.AddAsync(newTransaction);
        }

        await _accountRepository.UpdateAsync(account);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsBetweenDates(GetTransactionsBetweenDatesViewModel viewModel)
    {
        var transactions = await _transactionRepository.GetTransactionsBetweenDates(viewModel.AccountId, viewModel.StartDate, viewModel.EndDate);
        return transactions;
    }

    //#region Métodos privados
    private async Task CreateOrUpdateMonthBalance(Transaction transaction)
    {
        var monthBalance = await _monthBalanceRepository.GetByMonthAndYear(transaction.Date, transaction.AccountId);
        if (monthBalance == null)
        {
            var newMonthBalance = new MonthBalance(transaction.Date.Month, transaction.Date.Year, transaction.Value, transaction.AccountId);
            await _monthBalanceRepository.AddAsync(newMonthBalance);
        }
        else
        {
            monthBalance.UpdateBalance(transaction.Value, transaction.Type);
            await _monthBalanceRepository.UpdateAsync(monthBalance);
        }
    }

}

