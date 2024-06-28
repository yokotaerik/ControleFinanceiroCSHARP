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
}
