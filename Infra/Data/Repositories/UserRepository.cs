using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<User> GetByIdWithAccounts(Guid id)
    {
        return await _dbSet
            .Include(u => u.Accounts)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}
