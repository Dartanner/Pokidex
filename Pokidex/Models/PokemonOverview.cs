using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Pokidex.Models
{
    public partial class PokemonOverview : ObservableObject
    {
        [ObservableProperty] 
        private string name;

        [ObservableProperty] 
        private int id;

        [ObservableProperty] 
        private string sprite;
    }
}
