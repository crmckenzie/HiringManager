using System.Web.Mvc;
using NUnit.Framework;

namespace IntegrationTestHelpers
{
    public static class ActionResultExtensions
    {
        public static T GetViewModel<T>(this ActionResult result) where T : class
        {
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.InstanceOf<T>());
            var viewModel = viewResult.Model as T;
            return viewModel;
        }
    }
}