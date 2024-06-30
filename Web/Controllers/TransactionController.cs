using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Application.ViewModels.Account;
using ControleFinanceiro.Application.ViewModels.Transaction;
using ControleFinanceiro.Util.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ApiController
{

    private readonly ITransactionAppService _transactionAppService;
    private readonly IMapper _mapper;

    public TransactionController(ITransactionAppService transactionAppService, IMapper mapper)
    {
        _transactionAppService = transactionAppService;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateTransactionViewModel viewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        try
        {
            var transaction = await _transactionAppService.Create(viewModel);
            return CustomResponse(_mapper.Map<TransactionViewModel>(transaction));
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
