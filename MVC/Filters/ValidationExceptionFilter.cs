using Application.Common.Exceptions;
using MVC.Extensions;
using System.Web.Mvc;

namespace MVC.Filters
{
    public class ValidationExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        private object _argument;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _argument = context.ActionParameters["command"];  
            base.OnActionExecuting(context);
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is ValidationException ex)
            {
                string actionName = filterContext.RouteData.Values["action"].ToString();
                var viewData = filterContext.Controller.ViewData;

                ex.AddToModelState(viewData.ModelState);

                filterContext.Result = new ViewResult()
                {
                    ViewName = actionName,
                    ViewData = new ViewDataDictionary(viewData)
                    {
                        Model = _argument
                    }
                };
            }
            filterContext.ExceptionHandled = true;
        }
    }
}