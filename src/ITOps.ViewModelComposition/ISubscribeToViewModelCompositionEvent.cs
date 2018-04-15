namespace ITOps.ViewModelComposition
{
    public interface ISubscribeToViewModelCompositionEvent : IInterceptRoutes
    {
        void RegisterCallback(DynamicViewModel viewModel);
    }
}
