using Pokidex.ViewModels;

namespace Pokidex.Views;

public partial class PokemonList : ContentPage
{
	public PokemonList(PokeListVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}