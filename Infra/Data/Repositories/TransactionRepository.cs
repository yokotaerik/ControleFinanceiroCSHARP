using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Data.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsBetweenDates(Guid accountId, DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(t => t.Account.Id == accountId && t.Date >= startDate && t.Date <= endDate)
            .ToListAsync();
    }

    public async Task<Transaction> GetLastTransactionByDate(DateTime dateTime, Guid accountId)
    {
#pragma warning disable CS8603 // Possível retorno de referência nula.
        return await _dbSet
            .AsNoTracking()
            .Where(t => t.Account.Id == accountId && t.Date <= dateTime)
            .OrderByDescending(t => t.Date)
            .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possível retorno de referência nula.
    }
}
