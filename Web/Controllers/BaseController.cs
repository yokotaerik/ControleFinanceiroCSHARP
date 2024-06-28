using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace ControleFinanceiro.Web.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly ICollection<string> _errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            return Ok(new
            {
                code = (int)HttpStatusCode.OK,
                success = true,
                model = result,
                errors = false
            });
        }


        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            //VIEW MODEL
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors) AddError(error.ErrorMessage);

            return BadRequest(
                new
                {
                    code = (int)HttpStatusCode.BadRequest,
                    success = false,
                    model = false,
                    errors = errors.ToArray()
                });
        }

        protected ActionResult CustomResponse(SystemException exception)
        {            
                return BadRequest(
                new
                {
                    code = (int)HttpStatusCode.BadRequest,
                    success = false,
                    model = false,
                    errors = exception.Message
                });
        }

        protected ActionResult CustomResponse(Exception exception)
        {
            return StatusCode(500,
            new
            {
                code = (int)HttpStatusCode.InternalServerError,
                success = false,
                model = false,
                errors = exception.Message
            });
        }

        protected void AddError(string error)
        {
            _errors.Add(error);
        }
    }
}
