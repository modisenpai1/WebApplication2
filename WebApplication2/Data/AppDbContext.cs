using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using WebApplication2.Domain.Models;

namespace WebApplication2.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt){}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfigrations());

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Adress)
                .WithMany(a => a.Events)
                .HasForeignKey("AdressId")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Country)
                .WithMany(c => c.UsersInCountry)
                .HasForeignKey("CountryId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.city)
                .WithMany(c => c.UserInCity)
                .HasForeignKey("CityId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserOrg>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserOrgs)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Country>()
                .HasMany(u => u.Adresses)
                .WithOne(c => c.Country)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Adress>()
                .HasOne(u => u.Country)
                .WithMany(c => c.Adresses)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasOne(u => u.Country)
                .WithMany(c => c.Events)
                .OnDelete(DeleteBehavior.Restrict);

        }


       
        public DbSet<Event> Events { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Orginization> orginizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<EventUser> EventUsers { get; set; }
        public DbSet<UserOrg> UserOrgs { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
    }
}
