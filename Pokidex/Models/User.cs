using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokidex.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted {get; set; }
        public int TeamModelId {get; set; }
        public Team? Team { get; set; }

        public User() { }

        public User(string first, string last, string email, string pass)
        {
            FirstName = first; 
            LastName = last;
            Email = email;
            Password = pass;
            CreatedDate = DateTime.UtcNow;
            IsDeleted = false;
        }
    }
}
