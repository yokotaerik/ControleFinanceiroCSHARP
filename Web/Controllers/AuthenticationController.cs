using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.ViewModels.Identity;
using ControleFinanceiro.Util.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ApiController
    {
        private readonly IUserAppService _userAppService;
        private readonly IMapper _mapper;

        public AuthenticationController(IUserAppService userAppService, IMapper mapper)
        {
            _userAppService = userAppService;
            _mapper=mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var newUser = await _userAppService.Register(viewModel);
                return CustomResponse(newUser);
            }
            catch (SystemContextException ex)
            {
                return CustomResponse(ex);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);

            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var token = await _userAppService.Login(viewModel);
                return CustomResponse(token);

            }
            catch (SystemContextException ex)
            {
                return CustomResponse(ex);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string GetAuthenticated() => $"Autenticado - {User?.Identity?.Name} ";
    }
}
