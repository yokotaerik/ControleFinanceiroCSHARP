using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.ViewModels.Transaction;
using ControleFinanceiro.Domain.Enums.Transactions;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data.Repositories.Interfaces;
using ControleFinanceiro.Util.Exceptions;

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

    public async Task<CreateTransactionViewModel> Create(CreateTransactionViewModel dto)
    {
        var account = await _accountRepository.GetByIdAsync(dto.AccountId) 
                                ?? throw new Exception("Conta não encontrada");

        if(dto.IsRepeat) throw new SystemContextException("Faltando campo de quantidade de vezes");
        
        if(dto.RepeatTimes > 1) {
            if(dto.RepeatTimes > 120) throw new SystemContextException("O número máximo de repetições é 120");
            if(dto.RepeatTimes < 1) throw new SystemContextException("O número mínimo de repetições é 1");
        }

        dto.RepeatTimes = 1;

        for(int i = 0; i < dto.RepeatTimes; i++) {
            Transaction newTransaction = new Transaction(
                                            dto.Value,
                                            dto.Date.AddMonths(i),
                                            dto.Type, 
                                            account,
                                            dto.Description,
                                            dto.Category
                                            );

            if(newTransaction.Type == TransactionType.Expense)
                account.Balance -= newTransaction.Value;
            else
                account.Balance += newTransaction.Value;

            await _transactionRepository.AddAsync(newTransaction);
        }

        await _accountRepository.UpdateAsync(account);

        return CreateTransactionViewModel;
        }
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsBetweenDates(GetTransactionsBetweenDatesViewModel viewModel)
    {
        var transactions = await _transactionRepository.GetTransactionsBetweenDates(viewModel.AccountId, viewModel.StartDate, viewModel.EndDate);
        return transactions;
    }
}
