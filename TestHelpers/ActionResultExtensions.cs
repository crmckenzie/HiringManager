using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using System.Linq;
namespace TestHelpers
{
    public static class ActionResultExtensions
    {

        public static void AssertRedirect(this ActionResult actual, string action, string id)
        {
            Assert.That(actual, Is.InstanceOf<RedirectToRouteResult>());

            var actualRoute = actual as RedirectToRouteResult;
            Assert.That(actualRoute.RouteValues["Action"], Is.EqualTo(action));
            Assert.That(actualRoute.RouteValues["id"].ToString(), Is.EqualTo(id));
        }

        public static void AssertView(this ActionResult actual, string viewName)
        {
            Assert.That(actual, Is.InstanceOf<ViewResult>());
            var view = actual as ViewResult;
            Assert.That(view.ViewName, Is.EqualTo(viewName));
        }
    }

    public static class ModelStateExtensions
    {
        public static void AssertModelStateError(this ModelStateDictionary modelState, string propertyName, string message)
        {
            Assert.That(modelState.ContainsKey(propertyName));

            var errors = modelState[propertyName].Errors;
            Assert.That(errors.Any(row => row.ErrorMessage == message));
        }
    }
}