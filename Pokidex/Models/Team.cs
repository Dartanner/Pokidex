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
    }
}
