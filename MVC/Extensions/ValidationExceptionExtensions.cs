using Application.Common.Exceptions;
using System.Web.Mvc;

namespace MVC.Extensions
{
    public static class ValidationExceptionExtensions
    {
        public static void AddToModelState(this ValidationException exception, ModelStateDictionary modelState)
        {
            foreach (var error in exception.Errors)
            {
                modelState.AddModelError(error.Key, error.Value);
            }
        }
    }
}