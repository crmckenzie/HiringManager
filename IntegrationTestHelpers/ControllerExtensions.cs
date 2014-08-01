using System.Web;
using System.Web.Mvc;
using Ninject;

namespace IntegrationTestHelpers
{
    public static class ControllerExtensions
    {
        public static T Fake<T>(this T controller, IKernel kernel) where T : Controller
        {
            var httpContextBase = kernel.Get<HttpContextBase>();
            return controller.Fake(httpContextBase);
        }

        public static T Fake<T>(this T controller, HttpContextBase fakeHttpContext = null) where T : Controller
        {
            var routeCollection = Fakes.GetRouteCollection();
            new AreaRegistrationContext("Admin", routeCollection);


            if (fakeHttpContext == null)
                fakeHttpContext = Fakes.FakeHttpContext();

            var routeData = Fakes.CreateRouteData<T>();
            controller.ControllerContext = new ControllerContext(fakeHttpContext, routeData, controller);
            controller.Url = Fakes.FakeUrlHelper(fakeHttpContext, routeData, routeCollection);
            return controller;
        }
    }
}