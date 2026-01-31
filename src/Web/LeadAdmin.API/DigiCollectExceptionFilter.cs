using Microsoft.AspNetCore.Mvc.Filters;

namespace LeadAdmin.API
{
    public class DigiCollectExceptionFilter : ExceptionFilterAttribute
    {
        //private ILogger<DigiCollectExceptionFilterAttribute> log;

        //public DigiCollectExceptionFilterAttribute(ILogger<DigiCollectExceptionFilterAttribute> log)
        //{
        //    this.log = log;
        //}

        public override void OnException(ExceptionContext context)
        {
            //log = Digiclove.ERP.DIContainer.ServiceLocator.Instance.Get<ILogger<DigiCollectExceptionFilterAttribute>>();

            //var errorMessage = context.Exception.GetBaseException().Message;
            //log.LogError(context.Exception, errorMessage);

            //var apiError = new ApplicationException(errorMessage, context.Exception);
            //context.HttpContext.Response.StatusCode = 500;

            //context.Result = new JsonResult(apiError);

            //var telemetry = new TelemetryClient();
            //telemetry.TrackException(context.Exception);

            base.OnException(context);
        }
    }
}
