namespace ITOps.ViewModelComposition
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public interface IInterceptRoutes
    {
        bool Matches(RouteData routeData, string httpVerb, HttpRequest request);
    }
}