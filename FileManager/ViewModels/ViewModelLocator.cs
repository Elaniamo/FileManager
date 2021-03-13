using Microsoft.Extensions.DependencyInjection;

namespace TreeSize.ViewModels
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.Host.Services.GetRequiredService<MainViewModel>();
    }
}
