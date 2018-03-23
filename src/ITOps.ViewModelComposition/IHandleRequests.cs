﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace ITOps.ViewModelComposition
{
    public interface IHandleRequests : IInterceptRoutes
    {
        Task Handle(dynamic vm, RouteData routeData, HttpRequest request);
    }
}
