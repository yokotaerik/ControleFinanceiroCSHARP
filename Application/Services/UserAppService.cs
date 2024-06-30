using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.ViewModels.Identity;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data.Repositories.Interfaces;
using ControleFinanceiro.Util.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControleFinanceiro.Application.Services;

public class UserAppService : IUserAppService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public UserAppService(IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _userManager=userManager;
        _signInManager=signInManager;
        _configuration=configuration;
    }

    public async Task<User> Register(RegisterUserViewModel dto)
    {
        var newUser = new User(dto.Username, dto.Email);
        var result = await _userManager.CreateAsync(newUser, dto.Password);
        if (result.Succeeded)
        {
            // Usuário criado com sucesso
            return newUser;
        }
        else
        {
            throw new SystemContextException(string.Join("\n", result.Errors.Select(e => e.Description)));
        }
    }

    public async Task<TokenViewModel> Login(LoginViewModel viewModel)
    {
        var user = await _userManager.FindByEmailAsync(viewModel.Email);
        if (user is not null && await _userManager.CheckPasswordAsync(user, viewModel.Password))
        {

            var authClaims = new List<Claim>
            {
                new (ClaimTypes.Name, user.UserName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
                authClaims.Add(new(ClaimTypes.Role, userRole));

            return GetToken(authClaims);
        }
        throw new SystemContextException("Email ou senha inválidos.");
    }

    private TokenViewModel GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ValidTo = token.ValidTo
        };

    }
}
