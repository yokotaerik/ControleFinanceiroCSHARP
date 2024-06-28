using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data.Repositories.Interfaces;

namespace ControleFinanceiro.Infra.Data.Repositories;

public class GoalRepository : Repository<User>, IGoalRepository
{
    public GoalRepository(ApplicationContext context) : base(context)
    {
    }
}
