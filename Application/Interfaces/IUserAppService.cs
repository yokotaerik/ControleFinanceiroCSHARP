using ControleFinanceiro.Application.ViewModels.Identity;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface IUserAppService 
{
    Task<User> Register(RegisterUserViewModel viewModel);
    Task<string> Login(LoginViewModel viewModel);
}
