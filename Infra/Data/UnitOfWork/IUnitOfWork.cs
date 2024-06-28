using ControleFinanceiro.Infra.Data.Repositories.Interfaces;

namespace ControleFinanceiro.Infra.Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
}
