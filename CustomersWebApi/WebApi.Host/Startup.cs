using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using WebApi.Domain;

namespace WebApi.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<CustomersDbContext>().InstancePerRequest();

            var container = builder.Build();

            var webApiConfiguration = ConfigureWebApi();
            webApiConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(webApiConfiguration);
            app.UseWebApi(webApiConfiguration);
        }

        private HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            var jsonFormatter = new JsonMediaTypeFormatter();
            //optional: set serializer settings here
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
            return config;
        }


    }
    public class JsonContentNegotiator : IContentNegotiator
    {
        private readonly JsonMediaTypeFormatter _jsonFormatter;

        public JsonContentNegotiator(JsonMediaTypeFormatter formatter)
        {
            _jsonFormatter = formatter;
        }

        public ContentNegotiationResult Negotiate(
                Type type,
                HttpRequestMessage request,
                IEnumerable<MediaTypeFormatter> formatters)
        {
            return new ContentNegotiationResult(
                _jsonFormatter,
                new MediaTypeHeaderValue("application/json"));
        }
    }
}
