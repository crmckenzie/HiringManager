using System.Web.Mvc;
using HiringManager.DomainServices;

namespace HiringManager.Web.Infrastructure
{
    public static class ValidatedResponseExtensions
    {
        public static void WriteValidationErrorsTo(this IValidatedResponse response, ModelStateDictionary modelState)
        {
            foreach (var validationResult in response.ValidationResults)
            {
                modelState.AddModelError(validationResult.PropertyName, validationResult.Message);
            }
        }
    }
}