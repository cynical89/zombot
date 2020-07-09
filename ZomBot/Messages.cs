using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomBot.DAL;
using ZomBot.Models;

namespace ZomBot
{
    public class Messages
    {
        public static async Task HandlePoints(MessageCreateEventArgs e, DiscordClient client, string msg, ulong userId)
        {
            long id = Convert.ToInt64(userId);
            var name = $"{e.Message.MentionedUsers[0].Username}#{e.Message.MentionedUsers[0].Discriminator}";

            long point = 0;
            var message = "";

            if (msg.Contains("++"))
            {
                var emoji = DiscordEmoji.FromName(client, ":clap:");
                message = $"Me share a point with you {emoji}";
                point = 1;
            }
            if (msg.Contains("--"))
            {
                var emoji = DiscordEmoji.FromName(client, ":anger:");
                message = $"{emoji} Me eat that point... nom.";
                point = -1;
            }

            if (PointDAL.UserDoesExists(id, e.Guild.Name)) {
                var user = PointDAL.GetUserLevel(id, e.Guild.Name);
                user.Points += point;

                // Lets save off the username again just to catch it when they change it
                user.User = name;
                PointDAL.EditEntry(user);

                message += $" You has {user.Points} points";
            }
            else
            {
                var user = new PointModel();
                user.User = name;
                user.User_Id = id;
                user.Guild = e.Guild.Name;
                user.Points = point;
                PointDAL.CreateEntry(user);

                message += $" You has {point} points";
            }

            await e.Message.RespondAsync(message);
        }
    }
}
