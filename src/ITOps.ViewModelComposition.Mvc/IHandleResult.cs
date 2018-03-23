using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace ITOps.ViewModelComposition.Mvc
{
    public interface IHandleResult : IInterceptRoutes
    {
        Task Handle(ResultExecutingContext context, dynamic viewModel, int httpStatusCode);
    }
}
