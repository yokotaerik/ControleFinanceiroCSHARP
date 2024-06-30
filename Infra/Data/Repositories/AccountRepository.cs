using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Data.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Account>> GetAccountsByUserId(Guid userId)
    {
       return await _dbSet
            .AsNoTracking()
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<Account?> GetWith5Transactions(Guid accountId)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(a => a.Goal)
            .Include(a => a.Transactions)
                .Take(5)
            .FirstOrDefaultAsync(a => a.Id == accountId);
    }
}
