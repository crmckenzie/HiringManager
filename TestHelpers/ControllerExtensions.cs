using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Simple.Validation;

namespace TestHelpers
{
    public static class ControllerExtensions
    {
        public static void OutputModelState(this Controller controller)
        {
            foreach (var key in controller.ModelState.Keys)
            {
                var errors = controller.ModelState[key].Errors;
                foreach (var error in errors)
                {
                    Debug.WriteLine(error.ErrorMessage);
                }
            }

        }

        public static IEnumerable<ValidationResult> GetModelStateValidationResults(
            this Controller controller)
        {
            var modelState = controller.ModelState;

            var actualResults = modelState
                .Keys
                .SelectMany(key => modelState[key]
                    .Errors
                    .Select(actualError => new ValidationResult()
                    {
                        PropertyName = key,
                        Message = actualError.ErrorMessage
                    }))
                .ToList()
                ;

            return actualResults;

        }

        public static void AssertModelStateError(this Controller controller, string propertyName, string message)
        {
            controller.ModelState.AssertModelStateError(propertyName, message);
        }

    }
}
