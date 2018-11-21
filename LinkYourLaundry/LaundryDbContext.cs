using LinkYourLaundry.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkYourLaundry
{
    public class LaundryDbContext : DbContext
    {
        public DbSet<User> Users { get; protected set; }
        public DbSet<Role> Roles { get; protected set; }
        public DbSet<LaundryTemplate> LaundryTemplates { get; protected set; }
        public DbSet<ActiveLaundry> ActiveLaundries { get; protected set; }
        public DbSet<Invitation> Invitations { get; protected set; }

        public LaundryDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(true);

            modelBuilder.Entity<User>()
                .HasOne(u => u.GroupOwner)
                .WithMany(g => g.GroupMembers)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<LaundryTemplate>()
                .HasIndex(l => new { l.Name, l.UserId })
                .IsUnique(true);

            modelBuilder.Entity<LaundryTemplate>()
                .HasMany(l => l.ActiveLaundries)
                .WithOne(a => a.LaundryTemplate)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.GroupOwner)
                .WithMany(g => g.PendingActiveInvitations)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.InvitedUser)
                .WithMany(g => g.PendingPassiveInvitations)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invitation>()
                .HasIndex(i => new { i.GroupOwnerId, i.InvitedUserId })
                .IsUnique(true);
        }
    }
}
