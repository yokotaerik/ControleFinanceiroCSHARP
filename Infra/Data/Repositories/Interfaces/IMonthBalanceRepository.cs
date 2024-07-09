using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Infra.Data.Repositories.Interfaces;

public interface IMonthBalanceRepository : IRepository<MonthBalance>
{
    Task<MonthBalance?> GetByMonthAndYear(DateTime date, Guid accountId);
    Task<decimal?> GetBalanceByMonthAndYear(DateTime date, Guid accountId);
}
