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
            if(String.IsNullOrEmpty(Configuration.Config.ConnectionString))
            {
                Configuration.SetupConfig();
            }
            options.UseSqlServer(Configuration.Config.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public BotsDbContext() { }
    }
}
