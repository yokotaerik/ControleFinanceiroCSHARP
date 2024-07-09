using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.ViewModels.Account;
using ControleFinanceiro.Util.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {

        private readonly IAccountAppService _accountAppService;
        private readonly IMapper _mapper;

        public AccountController(IAccountAppService accountAppService, IMapper mapper)
        {
            _accountAppService = accountAppService;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{accountId}")]
        [Authorize]
        public async Task<IActionResult> GetUniqueById(Guid accountId)
        {
            try
            {
                var account = await _accountAppService.GetWith5Transactions(accountId);
                return CustomResponse(_mapper.Map<AccountCompleteViewModel>(account));
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

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetAccountsByUserId()
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var accounts = await _accountAppService.GetAccountsByUserId();
                return CustomResponse(accounts);
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

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var account = await _accountAppService.Create(viewModel);
                return CustomResponse(account);
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
    }
}
