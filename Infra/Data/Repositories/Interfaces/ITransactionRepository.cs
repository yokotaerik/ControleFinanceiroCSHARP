using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Infra.Data.Repositories.Interfaces;

public interface ITransactionRepository : IRepository<Transaction>
{
    public Task<IEnumerable<Transaction>> GetTransactionsBetweenDates(Guid accountId, DateTime startDate, DateTime endDate);
    public Task<Transaction> GetLastTransactionByDate(DateTime dateTime, Guid accountId);
}
