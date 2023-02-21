﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using WebApplication2.Domain.Models;

namespace WebApplication2.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);


       

        }
        protected override void OnModelCreating(ModelBuilder modelBuiler)

        {

            base.OnModelCreating(modelBuiler);
            modelBuiler.Entity<Event>().HasOne(e => e.Adress).WithMany(a => a.Events).HasForeignKey("AdressId").OnDelete(DeleteBehavior.NoAction);
            modelBuiler.Entity<User>().HasOne(u => u.Country).WithMany(c => c.UsersInCountry).HasForeignKey("CountryId").OnDelete(DeleteBehavior.Restrict);
            modelBuiler.Entity<User>().HasOne(u => u.city).WithMany(c => c.UserInCity).HasForeignKey("CityId").OnDelete(DeleteBehavior.Restrict);




        }



        public DbSet<Event> Events { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Orginization> orginizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<EventUser> EventUsers { get; set; }
        public DbSet<UserOrg> UserOrgs { get; set; }



    }
}
