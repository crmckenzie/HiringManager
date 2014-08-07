using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NSubstitute;

namespace IntegrationTestHelpers
{
    public static class Fakes
    {
        public static UrlHelper FakeUrlHelper(HttpContextBase httpContext, RouteData routeData, RouteCollection routeCollection)
        {
            var requestContext = new RequestContext(httpContext, routeData);
            var urlHelper = new UrlHelper(requestContext, routeCollection);
            return urlHelper;
        }

        public static RouteCollection GetRouteCollection()
        {
            var routes = new RouteCollection();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                )
                ;

            //var adminAreaRegistration = new AdminAreaRegistration();
            //var areaRegistrationContext = new AreaRegistrationContext(adminAreaRegistration.AreaName, routes);
            //adminAreaRegistration.RegisterArea(areaRegistrationContext);

            return routes;
        }

        public static HttpContextBase FakeHttpContext()
        {
            var context = Substitute.For<HttpContextBase>();
            var request = FakeHttpRequest();
            var response = FakeHttpResponse();

            var session = Substitute.For<HttpSessionStateBase>();
            var server = Substitute.For<HttpServerUtilityBase>();



            context.Request.Returns(request);
            context.Response.Returns(response);
            context.Session.Returns(session);
            context.Server.Returns(server);

            return context;
        }

        private static HttpResponseBase FakeHttpResponse()
        {
            var response = Substitute.For<HttpResponseBase>();
            response.ApplyAppPathModifier(Arg.Any<string>()).Returns(ci => ci.Arg<string>());
            return response;
        }

        private static HttpRequestBase FakeHttpRequest()
        {
            var request = Substitute.For<HttpRequestBase>();
            request.AppRelativeCurrentExecutionFilePath.Returns("/");
            request.ApplicationPath.Returns("/");
            request.Url.Returns(new Uri("http://localhost", UriKind.Absolute));
            request.ServerVariables.Returns(new NameValueCollection());
            return request;
        }


        public static RouteData CreateRouteData<T>() where T : Controller
        {
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Home");
            routeData.Values.Add("action", "Index");
            return routeData;
        }
    }
}