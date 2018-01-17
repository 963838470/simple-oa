using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace oa_server
{
    public class WebApiErrorHandleAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //Logger.Error(actionExecutedContext.Exception);
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK,
                new AjaxModel
                {
                    StatusCode = 500,
                    Message = actionExecutedContext.Exception.Message
                });
        }
    }

    internal class AjaxModel
    {
        public string Message { get; set; }
        public object StatusCode { get; set; }
    }
}