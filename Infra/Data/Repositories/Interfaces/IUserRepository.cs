using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Infra.Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<User> GetByIdWithAccounts(Guid id);

}
