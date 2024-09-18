using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokidex.Models.Database
{
    public class PokidexDatabase : DbContext
    {
        public DbSet<Team> Teams {get; set; }
        public DbSet<User> Users {get; set; }

        public string DbPath { get; }

        public PokidexDatabase()
        {
            //set up the database name and location
            DbPath = Path.Combine(FileSystem.AppDataDirectory, "Pokidex.db");
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>();
            builder.Entity<Team>();

            //PopulateUsers(builder);
            //PopulateTeams(builder);
            EnsureUserPasswordSecurity(builder);
        }

        private void PopulateUsers(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
                new { Id = 1, FirstName = "Will", LastName= "Warren", email="wildebeeststudios@gmail.com", IsDeleted = false, CreatedDate = DateTime.UtcNow, Password = "Pa55w0rd!" }
            );
        }

        private void PopulateTeams(ModelBuilder builder)
        {
            builder.Entity<Team>().HasData(
                new { Id = 1, Name = "Will's Beasts", Slot1 = 0, Slot2 = 0, Slot3 = 0, Slot4 = 0, Slot5 = 0, Slot6 = 0, UserId = 1 }
            );
        }

        private void EnsureUserPasswordSecurity(ModelBuilder builder)
        {
            builder.Entity<User>().Property(x => x.Password).HasConversion(
                x => EncryptPassword(x),
                x => DecryptPassword(x));
        }

        private string EncryptPassword(string password)
        {
           return new string(password.Reverse().ToArray());
        }

        private string DecryptPassword(string password)
        {
            return new string(password.Reverse().ToArray());
        }

    }
}
