using System;
using System.Collections.Generic;
using System.Text;
using ZomBot.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ZomBot.DAL
{
    class PointDAL
    {
        public static List<PointModel> GetAllUserLevels()
        {
            using(var ctx = new BotsDbContext())
            {
                return ctx.Points.ToList();
            }
        }

        public static PointModel GetUserLevel(long id, string guild)
        {
            using (var ctx = new BotsDbContext())
            {
                return ctx.Points.Single(x => x.User_Id == id && x.Guild == guild);
            }
        }

        public static void CreateEntry(PointModel model)
        {
            using (var ctx = new BotsDbContext())
            {
                ctx.Points.Add(model);
                ctx.SaveChanges();
            }
        }

        public static void DeleteEntry(long id, string guild)
        {
            var toDelete = GetUserLevel(id, guild);
            using (var ctx = new BotsDbContext())
            {
                ctx.Entry(toDelete).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        public static void EditEntry(PointModel model)
        {
            using (var ctx = new BotsDbContext())
            {
                ctx.Entry(model).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public static bool UserDoesExists(long id, string guild)
        {
            using(var ctx = new BotsDbContext())
            {
                return ctx.Points.Any(x => x.User_Id == id && x.Guild == guild);
            }
        }
    }
}
