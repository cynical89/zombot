using System;
using System.Collections.Generic;
using System.Text;
using ZomBot.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ZomBot.DAL
{
    class LevelDAL
    {
        public static List<LevelModel> GetAllUserLevels()
        {
            using(var ctx = new BotsDbContext())
            {
                return ctx.Levels.ToList();
            }
        }

        public static LevelModel GetUserLevel(string name, string guild)
        {
            using (var ctx = new BotsDbContext())
            {
                return ctx.Levels.Single(x => x.User == name && x.Guild == guild);
            }
        }

        public static void CreateEntry(LevelModel model)
        {
            using (var ctx = new BotsDbContext())
            {
                ctx.Levels.Add(model);
                ctx.SaveChanges();
            }
        }

        public static void DeleteEntry(string name, string guild)
        {
            var toDelete = GetUserLevel(name, guild);
            using (var ctx = new BotsDbContext())
            {
                ctx.Entry(toDelete).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        public static void EditEntry(LevelModel model)
        {
            using (var ctx = new BotsDbContext())
            {
                ctx.Entry(model).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public static bool UserDoesExists(string name, string guild)
        {
            using(var ctx = new BotsDbContext())
            {
                return ctx.Levels.Any(x => x.User == name && x.Guild == guild);
            }
        }
    }
}
