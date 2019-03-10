using AspectCore.DynamicProxy;
using AspectCore.Injector;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using PictureManager.Logs;
using PictureManager.Exceptions;

namespace PictureManager.Api.Exceptions
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class ExceptionManagementAttribute : AbstractInterceptorAttribute
    {
        [FromContainer]
#pragma warning disable RCS1170 // Use read-only auto-implemented property. - Public set needed for DI
        protected ILogger<ExceptionManagementAttribute> Logger { get; set; }
#pragma warning restore RCS1170 // Use read-only auto-implemented property.

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                await next(context).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                HandleException(e, context);
            }
        }

        private void HandleException(Exception e, AspectContext context)
        {
            if (e is AspectInvocationException aspectCoreException)
            {
                if (e.InnerException != null)
                {
                    HandleException(aspectCoreException.InnerException, context);
                }

                LogError("AspectCore exception", context, e.Message);
                throw e;
            }
            if (e is BaseCustomException customException)
            {
                LogError(customException.Description, context, customException.Message);
                throw e;
            }
            if (e is UnauthorizedAccessException)
            {
                LogError("Unauthorized access", context, e.Message);
                throw e;
            }
            if (e is InvalidOperationException)
            {
                LogError("Invalid operation", context, e.Message);
                throw e;
            }

            GenericException(e, context);
        }

        protected void LogError(string type, AspectContext context, string message)
        {
            string parametersInStringFormat = context.Parameters.Length > 0
                ? LogHelpers.ValueToLog(context.Parameters)
                : "No params";
            Logger.LogError($"{type} " +
                $"- {context.ImplementationMethod.ReflectedType.FullName}" +
                $".{context.ProxyMethod.Name} - {parametersInStringFormat}: {message}");
        }

        protected virtual void GenericException(Exception e, AspectContext context)
        {
            LogError("Exception", context, e.Message);
            throw new InternalErrorException(e.Message, e.InnerException);
        }
    }
}
