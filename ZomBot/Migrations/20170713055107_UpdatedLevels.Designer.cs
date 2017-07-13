using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ZomBot.DAL;

namespace ZomBot.Migrations
{
    [DbContext(typeof(BotsDbContext))]
    [Migration("20170713055107_UpdatedLevels")]
    partial class UpdatedLevels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ZomBot.Models.LevelModel", b =>
                {
                    b.Property<int>("Level_Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Guild");

                    b.Property<int>("Level");

                    b.Property<long>("Messages");

                    b.Property<string>("User");

                    b.HasKey("Level_Id");

                    b.ToTable("Levels");
                });
        }
    }
}
