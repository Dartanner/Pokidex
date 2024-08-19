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
using Pokidex.Models;

namespace Pokidex.ViewModels
{
    public class PokeListVM : ObservableObject
    {
        public ObservableCollection<Pokemon> AllPokemon { get; set; } = [];

        public ObservableCollection<PokemonOverview> AllPokemonOverview { get; set; } = [];

        private PokeApiClient pokeClient;

        //public ICommand GetMorePokesCommand { private set; get; }

        int loadedPokeCount = 0;

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
            LoadSomeOverviews();
        }




        async void LoadSomeOverviews()
        {
            var pokes = await pokeClient.GetNamedResourcePageAsync<Pokemon>(151,0);
            for(var i = 0; i < pokes.Results.Count; i++)
            {
                var pokemon = pokes.Results[i];
                var overview = new PokemonOverview();
                overview.Name = pokemon.Name;
                overview.Id = i + 1;
                overview.Sprite = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/" + (i + 1) + ".png";
                AllPokemonOverview.Add(overview);
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
    }
}
