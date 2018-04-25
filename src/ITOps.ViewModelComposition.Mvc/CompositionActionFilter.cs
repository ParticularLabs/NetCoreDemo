namespace ITOps.ViewModelComposition.Mvc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ITOps.ViewModelComposition;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    internal class CompositionActionFilter : IAsyncResultFilter
    {
        private IEnumerable<IHandleResult> resultHandlers;

        public CompositionActionFilter(IEnumerable<IHandleResult> resultHandlers)
        {
            this.resultHandlers = resultHandlers;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            (var viewModel, var statusCode) = await CompositionHandler.HandleRequest(context.HttpContext);

            DefaultHandler();

            await next();

            void DefaultHandler()
            {
                if (context.Result is ViewResult viewResult && viewResult.ViewData.Model == null)
                {
                    //MVC
                    if (statusCode == StatusCodes.Status200OK) viewResult.ViewData.Model = viewModel;
                }
                else if (context.Result is ObjectResult objectResult && objectResult.Value == null)
                {
                    //WebAPI
                    if (statusCode == StatusCodes.Status200OK) objectResult.Value = viewModel;
                }
            }
        }
    }
}