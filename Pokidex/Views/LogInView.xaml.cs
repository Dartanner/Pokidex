using Pokidex.ViewModels;

namespace Pokidex.Views;

public partial class LogInView : ContentPage
{
    public LogInView()
    {
        var vm = new LogInVM();
        InitializeComponent();
        BindingContext = vm;
    }
}