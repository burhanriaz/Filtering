using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;

namespace Filtering.Custom_Filter
{
   
    public class ResponseAttribute : ActionFilterAttribute,IActionFilter
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            filterContext.HttpContext.Response.Headers.Add("ControlerName", controllerName);

            var actionName = filterContext.RouteData.Values["action"].ToString();
            filterContext.HttpContext.Response.Headers.Add("ActionName", actionName);


            var MethodName = filterContext.HttpContext.Request.Method;
            filterContext.HttpContext.Response.Headers.Add("MethodName", MethodName);

            var scheme = filterContext.HttpContext.Request.Scheme;
            filterContext.HttpContext.Response.Headers.Add("schemeName", scheme);

            //var port = filterContext.HttpContext.Connection.LocalPort.ToString();
            var port = filterContext.HttpContext.Request.Host.Port.ToString();

            filterContext.HttpContext.Response.Headers.Add("portName", port);

            var host = filterContext.HttpContext.Request.Host.ToString();
            filterContext.HttpContext.Response.Headers.Add("HostName", host);


            string DateTime = System.DateTime.Now.ToString();
            filterContext.HttpContext.Response.Headers.Add("DateTime", DateTime);


            base.OnActionExecuted(filterContext);
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
     
        Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //Task.Delay(2000).Wait();
            await next();

            stopwatch.Stop();

            var time = stopwatch.ElapsedMilliseconds;
            context.HttpContext.Response.Headers.Add("ExcuationTime", time.ToString());
            //context.HttpContext.Response.Headers.Add("ExcuationTime", stopwatch.ElapsedMilliseconds.ToString());

        }
       

        //public class ProcessingTimeMiddleware
        //{
        //    private readonly RequestDelegate _next;

        //    public ProcessingTimeMiddleware(RequestDelegate next)
        //    {
        //        _next = next;
        //    }

        //    public async Task Invoke(HttpContext filterContext)
        //    {
        //        var watch = new Stopwatch();

        //        filterContext.Response.OnStarting(() =>
        //        {

        //            watch.Stop();

        //            filterContext.Response.Headers.Add("ExcuationTime",
        //                            new[] { (watch.ElapsedMilliseconds).ToString() });

        //            return Task.CompletedTask;
        //        });

        //        watch.Start();

        //        await _next(filterContext);
        //    }
    }
}

