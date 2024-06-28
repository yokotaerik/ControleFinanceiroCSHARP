using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Infra.Data.Repositories.Interfaces;

public interface IAccountRepository : IRepository<Account>
{
    public Task<IEnumerable<Account>> GetAccountsByUserId(Guid userId);
}
