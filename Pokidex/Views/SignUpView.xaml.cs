using Pokidex.ViewModels;

namespace Pokidex.Views;

public partial class SignUpView : ContentPage
{
	public SignUpView()
    {
        var vm = new SignUpVM();

        InitializeComponent();
        BindingContext = vm;
    }
}