using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Data.Repositories;

public class MonthBalanceRepository : Repository<MonthBalance>, IMonthBalanceRepository
{
    public MonthBalanceRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<MonthBalance?> GetByMonthAndYear(DateTime date, Guid accountId)
    {
        return await _dbSet
            .Where(x => x.Month == date.Month && x.Year == date.Year && x.AccountId == accountId)
            .FirstOrDefaultAsync();
    }

    public async Task<decimal?> GetBalanceByMonthAndYear(DateTime date, Guid accountId)
    {
        return await _dbSet
            .Where(x => x.Month == date.Month && x.Year == date.Year && x.AccountId == accountId)
            .Select(x => x.Balance)
            .FirstOrDefaultAsync();
    }
}
