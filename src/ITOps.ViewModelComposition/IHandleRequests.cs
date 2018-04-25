namespace ITOps.ViewModelComposition
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public interface IHandleRequests : IInterceptRoutes
    {
        Task Handle(dynamic vm, RouteData routeData, HttpRequest request);
    }
}