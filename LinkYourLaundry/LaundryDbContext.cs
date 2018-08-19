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
        public DbSet<Group> Groups { get; protected set; }
        public DbSet<LaundryTemplate> LaundryTemplates { get; protected set; }
        public DbSet<ActiveLaundry> ActiveLaundries { get; protected set; }

        public LaundryDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
