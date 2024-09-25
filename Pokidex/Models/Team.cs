using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokidex.Models
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Slot1 { get; set; }
        public int Slot2 { get; set; }
        public int Slot3 { get; set; }
        public int Slot4 { get; set; }
        public int Slot5 { get; set; }
        public int Slot6 { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public Team()
        {
        }

        public Team(string name, User user)
        {
            Name = name;
            Slot1 = 0;
            Slot2 = 0;
            Slot3 = 0;
            Slot4 = 0;
            Slot5 = 0;
            Slot6 = 0;
            UserId = user.Id;
        }

        public bool HasPokemon(int pokemonId)
        {
            return Slot1 == pokemonId ||
                   Slot2 == pokemonId ||
                   Slot3 == pokemonId ||
                   Slot4 == pokemonId ||
                   Slot5 == pokemonId ||
                   Slot6 == pokemonId;
        }

        public bool IsSpace()
        {
            return Slot1 == 0 ||
                   Slot2 == 0 || 
                   Slot3 == 0 || 
                   Slot4 == 0 || 
                   Slot5 == 0 || 
                   Slot6 == 0;
        }

        public void RemoveFromTeam(int pokemonId)
        {
            if (Slot1 == pokemonId)
            {
                Slot1 = 0;
                return;
            }

            if (Slot2 == pokemonId)
            {
                Slot2 = 0;
                return;
            }

            if (Slot3 == pokemonId)
            {
                Slot3 = 0;
                return;
            }

            if (Slot4 == pokemonId)
            {
                Slot4 = 0;
                return;
            }

            if (Slot5 == pokemonId)
            {
                Slot5 = 0;
                return;
            }

            if (Slot6 == pokemonId)
            {
                Slot6 = 0;
            }
        }

        public void PlaceInFirstAvailableSlot(int pokemonId)
        {
            if (!IsSpace()) return;

            if (Slot1 == 0)
            {
                Slot1 = pokemonId;
                return;
            }

            if (Slot2 == 0)
            {
                Slot2 = pokemonId;
                return;
            }

            if (Slot3 == 0)
            {
                Slot3 = pokemonId;
                return;
            }

            if (Slot4 == 0)
            {
                Slot4 = pokemonId;
                return;
            }

            if (Slot5 == 0)
            {
                Slot5 = pokemonId;
                return;
            }

            Slot6 = pokemonId;
        }
    }
}
