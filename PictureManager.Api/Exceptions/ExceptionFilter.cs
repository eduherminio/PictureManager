using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PictureManager.Exceptions;

namespace PictureManager.Api.Exceptions
{
    public class ExceptionFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute
    {
        protected virtual ExceptionToHttpCodeConverter Converter => new ExceptionToHttpCodeConverter();

        public override void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception.GetInnerAspectInvocationException();

            HttpExceptionResponseInfo info = Converter.GetMessageAndHttpCode(ex);

            context.HttpContext.Response.StatusCode = (int)info.Status;
            if (info.Message != null)
            {
                context.Result = new JsonResult(info.Message);
            }
        }
    }
}
