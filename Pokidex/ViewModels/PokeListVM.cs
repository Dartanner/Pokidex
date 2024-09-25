using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using PokeApiNet;
using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.Input;
using Pokidex.Models;

namespace Pokidex.ViewModels
{
    public partial class PokeListVM : ObservableObject, IQueryAttributable
    {
        public ObservableCollection<Pokemon> AllPokemon { get; set; } = [];

        public ObservableCollection<PokemonOverview> AllPokemonOverview { get; set; } = [];

        private PokeApiClient pokeClient;

        //public ICommand GetMorePokesCommand { private set; get; }

        [ObservableProperty] 
        private bool _isSpaceInTeam;

        int loadedPokeCount = 0;

        private User currentUser;

        public bool isLoading { get; set; }

        public PokeListVM()
        {
            pokeClient = new PokeApiClient();
            isLoading = true;

            /*GetMorePokesCommand = new Command(
                execute: ()=>
                {
                    LoadPokes().SafeFireAndForget();
                });
            */



            //LoadPokes();
        }




        async void LoadSomeOverviews()
        {
            var pokes = await pokeClient.GetNamedResourcePageAsync<Pokemon>(151,0);
            for(var i = 0; i < pokes.Results.Count; i++)
            {
                var id = i + 1;
                var pokemon = pokes.Results[i];
                var overview = new PokemonOverview();
                overview.Name = pokemon.Name;
                overview.Id = id;
                overview.Sprite = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/" + (i + 1) + ".png";
                overview.AddedToTeam = currentUser.Team.HasPokemon(id);
                AllPokemonOverview.Add(overview);
            }
        }

        [RelayCommand]
        private void Toggle(PokemonOverview selectedPokemon)
        {
            var id = selectedPokemon.Id;
            if (currentUser.Team.HasPokemon(id))
            {
                currentUser.Team.RemoveFromTeam(id);
                selectedPokemon.AddedToTeam = false;
                IsSpaceInTeam = currentUser.Team.IsSpace();
            }
            else
            {
                currentUser.Team.PlaceInFirstAvailableSlot(id);
                selectedPokemon.AddedToTeam = true;
                IsSpaceInTeam = currentUser.Team.IsSpace();
            }
        }

        private async Task GetPokemonAtIndex(int index)
        {
            var poke = await pokeClient.GetResourceAsync<Pokemon>(index);

            AllPokemon.Add(poke);
            loadedPokeCount++;

            if (AllPokemon.Count == 10)
            {
                var sorted = AllPokemon.OrderBy(x => x.Id).ToList();
                for (var i = 0; i < sorted.Count; i++)
                {
                    AllPokemon.Move(AllPokemon.IndexOf(sorted[i]), i);
                }

                isLoading = false;
            }
        }

        /*
        [RelayCommand]
        public void Toggle(Pokemon poke)
        {
            var index = poke.Id;

           // if(user)
        }
        */

        public void LoadPokes()
        {
            //var index = loadedPokeCount;

            for (var i = 1; i <= 10; i++)
            {
                //if(loadedPokeCount + index < 102)
                GetPokemonAtIndex(i).SafeFireAndForget();
            }
            //loadedPokeCount += 10;
        }

        

        public void ApplyQueryAttributes(IDictionary<string, object> dictionary)
        {
            User user = dictionary["user"] as User;
            currentUser = user;
            IsSpaceInTeam = currentUser.Team.IsSpace();
            LoadSomeOverviews();
        }
    }
}
