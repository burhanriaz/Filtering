using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Filtering.Custom_Filter
{
    public class ResponseAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            filterContext.HttpContext.Response.Headers.Add("ControlerName", controllerName);

            var actionName = filterContext.RouteData.Values["action"].ToString();
            filterContext.HttpContext.Response.Headers.Add("ActionName", actionName);

            var scheme = filterContext.HttpContext.Request.Scheme;
            filterContext.HttpContext.Response.Headers.Add("schemeName", scheme);

            //var port = filterContext.HttpContext.Connection.LocalPort.ToString();
            var port = filterContext.HttpContext.Request.Host.Port.ToString();

            filterContext.HttpContext.Response.Headers.Add("portName", port);

            var host = filterContext.HttpContext.Request.Host.ToString();
            filterContext.HttpContext.Response.Headers.Add("HostName", host);


            string DateTime = System.DateTime.Now.ToString();
            filterContext.HttpContext.Response.Headers.Add("DateTime", DateTime);

            var watch = new Stopwatch();

            //filterContext.HttpContext.Response.OnStarting.Subscribe(watch.Elapsed);
            //{ watch.Stop();
            //    filterContext.HttpContext.Response.Headers.Add("ExcuationTime",
            //                    new[] { (watch.ElapsedMilliseconds).ToString() });
            //}
            

                base.OnActionExecuted(filterContext);
        }

        public class ProcessingTimeMiddleware
        {
            private readonly RequestDelegate _next;

            public ProcessingTimeMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task Invoke(HttpContext filterContext)
            {
                var watch = new Stopwatch();

                filterContext.Response.OnStarting(() =>
                {
                    
                    watch.Stop();

                    filterContext.Response.Headers.Add("ExcuationTime",
                                    new[] { (watch.ElapsedMilliseconds).ToString() });

                    return Task.CompletedTask;
                });

                watch.Start();

                await _next(filterContext);
            }

        }



    }
      


    }

