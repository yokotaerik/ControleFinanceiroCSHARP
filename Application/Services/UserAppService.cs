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

    public async Task<string> Login(LoginViewModel dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            var authClaims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        };


            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Y2FzZGZhc2RmYXNkZmFzZGZhc2RmYXNkZmFzZGZhczEyMzQ1Ng=="));

            var token = new JwtSecurityToken(
                issuer: "erik",
                audience: "yokota",
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
              
        }
        throw new SystemContextException("Email ou senha inválidos.");
    }
}
