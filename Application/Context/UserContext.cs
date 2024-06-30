using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Util.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ControleFinanceiro.Application.Context;
    public interface IUserContext
    {
        Task<Guid> GetUserId();
    }

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

    public UserContext(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<Guid> GetUserId()
    {
        var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

        if (username == null) throw new SystemContextException("Usuário não encontrado");

        var user = await _userManager.FindByNameAsync(username);

        return user.Id;
    }
}
