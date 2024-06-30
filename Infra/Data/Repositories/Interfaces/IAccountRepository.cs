using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Infra.Data.Repositories.Interfaces;

public interface IAccountRepository : IRepository<Account>
{
    Task<IEnumerable<Account>> GetAccountsByUserId(Guid userId);
    Task<Account?> GetWith5Transactions(Guid accountId);
}
