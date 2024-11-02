using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FRCSPreparationPortal.Common;

namespace FRCSPreparationPortal.API
{
    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                HandleError(ex);

                //context.Response.Redirect($"http://{context.Request.Host}/error");
                throw ex;
            }
        }

        private void HandleError(Exception ex)
        {
            string msg = GetErrorMessage(ex.GetBaseException());

            _logger.LogError(ex, msg);

            //SessionContext.Error = msg;

            // Send Exception Email
            if (AppSettings.BuildMode == BuildMode.Production && !string.IsNullOrEmpty(AppSettings.ExceptionEmail))
            {
                //Helper.SendEmail(AppSettings.ExceptionEmail, AppSettings.InstanceName, msg);
            }
        }

        public string GetErrorMessage(Exception exception)
        {
            if (exception == null)
            {
                return "no message";
            }

            String pageUrl = "";//HttpContext.Current.Request.Url.AbsoluteUri;

            DateTime currentDateTime = Convert.ToDateTime(DateTime.UtcNow.ToLongDateString() + " " + DateTime.UtcNow.ToLongTimeString());

            String fileName = String.Empty;
            String methodName = String.Empty;
            String lineNumber = String.Empty;
            String columnNumber = String.Empty;

            // Check if stack trace have frames of information to get information
            // related to file causing exception
            StackTrace trace = new StackTrace(exception, true);
            if (trace.FrameCount > 0)
            {
                StackFrame frame = trace.GetFrame(0);
                fileName = frame.GetFileName();
                methodName = frame.GetMethod().Name;
                lineNumber = Convert.ToString(frame.GetFileLineNumber());
                columnNumber = Convert.ToString(frame.GetFileColumnNumber());
            }

            string msg = $"{currentDateTime}=>FileName:{fileName},MethodName:{methodName},Line:{lineNumber},Column:{columnNumber}<br/>"
                    + pageUrl + "<br/>" + exception.Message + " <br/><br/>" + exception.StackTrace.Replace("at ", "<br/> at ");

            return msg;
        }
    }
}
