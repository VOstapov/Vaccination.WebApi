using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vaccination.WebApi
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new
            {
                Message = context.Exception.Message,
                Code = StatusCodes.Status500InternalServerError
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = response.Code,
                DeclaredType = response.GetType()
            };
        }
    }
}
