using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ITOps.ViewModelComposition.Gateway
{
    public class CompositionHandler
    {
        public static async Task<(dynamic ViewModel, int StatusCode)> HandleRequest(HttpContext context)
        {
            var pending = new List<Task>();
            var routeData = context.GetRouteData();
            var request = context.Request;
            var vm = new ExpandoObject();
            var interceptors = context.RequestServices.GetServices<IInterceptRoutes>();

            //matching interceptors could be cached by URL
            var matching = interceptors
                .Where(a => a.Matches(routeData, request.Method, request))
                .ToArray();

            foreach (var handler in matching.OfType<IHandleRequests>())
            {
                pending.Add
                (
                    handler.Handle(vm, routeData, request)
                );
            }

            if (pending.Count == 0)
            {
                return (null, StatusCodes.Status404NotFound);
            }
            else
            {
                await Task.WhenAll(pending);

                return (vm, StatusCodes.Status200OK);
            }
        }
    }
}
