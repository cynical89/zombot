using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZomBot.Models;

namespace ZomBot.DAL
{
    class BotsDbContext : DbContext
    {
        public BotsDbContext(DbContextOptions<BotsDbContext> options)
            : base(options)
        { }

        public DbSet<LevelModel> Levels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=bots;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public BotsDbContext() { }
    }
}
