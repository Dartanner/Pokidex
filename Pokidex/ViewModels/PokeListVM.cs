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
                var pokemon = pokes.Results[i];
                var overview = new PokemonOverview();
                overview.Name = pokemon.Name;
                overview.Id = i + 1;
                overview.Sprite = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/" + (i + 1) + ".png";
                overview.AddedToTeam = IsPokemonInTeam(i + 1);
                AllPokemonOverview.Add(overview);
            }
        }

        private bool IsPokemonInTeam(int index)
        {
            if (currentUser == null || currentUser.Team == null)
                return false;

            /*
            bool isSlot1 = currentUser.Team.Slot1 == index;
            bool isSlot2 = currentUser.Team.Slot2 == index;
            bool isSlot3 = currentUser.Team.Slot3 == index;
            bool isSlot4 = currentUser.Team.Slot4 == index;
            bool isSlot5 = currentUser.Team.Slot5 == index;
            bool isSlot6 = currentUser.Team.Slot6 == index;

            return isSlot1 || isSlot2 || isSlot3 || isSlot4 || isSlot5 || isSlot6;
            */

            return currentUser.Team.Slot1 == index ||
                   currentUser.Team.Slot2 == index ||
                   currentUser.Team.Slot3 == index ||
                   currentUser.Team.Slot4 == index ||
                   currentUser.Team.Slot5 == index ||
                   currentUser.Team.Slot6 == index;
        }

        [RelayCommand]
        private void Toggle()
        {
            TogglePokemonInTeam(1);
        }

        private void TogglePokemonInTeam(int index)
        {
            var isInTeam = IsPokemonInTeam(index);
            if (isInTeam)
                RemovePokemonFromTeam(index);
            else
                AddPokemonToTeam(index);
        }

        private void RemovePokemonFromTeam(int index)
        {
            /*
            if (currentUser.Team.Slot1 == index)
            {
                currentUser.Team.Slot1 = 0;
                return;
            }

            if (currentUser.Team.Slot2 == index)
            {
                currentUser.Team.Slot2 = 0;
                return;
            }

            if (currentUser.Team.Slot3 == index)
            {
                currentUser.Team.Slot3 = 0;
                return;
            }

            ...

            */
            
            WasInSlot(currentUser.Team.Slot1, index);
            WasInSlot(currentUser.Team.Slot2, index);
            WasInSlot(currentUser.Team.Slot3, index);
            WasInSlot(currentUser.Team.Slot4, index);
            WasInSlot(currentUser.Team.Slot5, index);
            WasInSlot(currentUser.Team.Slot6, index);
        }

        private void WasInSlot(int slot, int index)
        {
            if (slot == index)
               slot = 0;
        }


        private void AddPokemonToTeam(int index)
        {
            //TODO: Add to team :)
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
            LoadSomeOverviews();
        }
    }
}
