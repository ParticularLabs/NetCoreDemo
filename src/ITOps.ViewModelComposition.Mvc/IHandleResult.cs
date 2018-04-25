namespace ITOps.ViewModelComposition.Mvc
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Filters;

    public interface IHandleResult : IInterceptRoutes
    {
        Task Handle(ResultExecutingContext context, dynamic viewModel, int httpStatusCode);
    }
}