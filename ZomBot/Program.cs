using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.Threading.Tasks;
using ZomBot.DAL;
using ZomBot.Models;

namespace ZomBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var prog = new Program();
            Run().GetAwaiter().GetResult();
        }

        public static async Task Run()
        {
            Configuration.SetupConfig();

            var discord = new DiscordClient(new DiscordConfig
            {
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                Token = Configuration.Config.Token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true
            });

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = Configuration.Config.CommandPrefix,
                EnableDms = true,
                EnableMentionPrefix = true
            });

            commands.RegisterCommands<Commands>();

            discord.GuildMemberAdd += async e =>
            {
                if (!e.Member.IsBot)
                {
                    await e.Guild.DefaultChannel.SendMessageAsync($"Welcome {e.Member.Mention}. Nice to ~~eat You!~~ I mean... greet you!");
                }
            };

            discord.GuildMemberRemove += async e =>
            {
                if (!e.Member.IsBot)
                {
                    await e.Guild.DefaultChannel.SendMessageAsync($"Come back {e.Member.Mention}! I just wanted to eat your brains..");
                }
            };

            discord.ChannelCreated += async e =>
            {
                await e.Channel.SendMessageAsync("Does this mean more brains to eat?");
            };

            discord.MessageCreated += async e =>
            {
                if (e.Message.Author.IsBot)
                {
                    if (e.Message.Content.ToLower() == "ping")
                        await e.Message.RespondAsync("pong");
                    return;
                }

                // Handle point ++ or point -- messages.
                if (e.Message.MentionedUsers.Count == 1)
                {
                    // Abstract this shit out of this method

                    var user = e.Message.MentionedUsers[0].Id;

                    // We don't want people to give points to themselves
                    if (user == e.Message.Author.Id)
                    {
                        var emoji = DiscordEmoji.FromName(discord, ":no_entry_sign:");
                        await e.Message.RespondAsync($"{emoji} Nice try.");
                    }
                    else
                    {
                        var content = e.Message.Content.Replace($"<@{user}>", "").Trim();
                        var substr = content.Substring(0, 2);

                        if (substr.Contains("++") || substr.Contains("--"))
                        {
                            await Messages.HandlePoints(e, discord, substr, user);
                        }
                    }
                }

                var name = $"{e.Message.Author.Username}#{e.Message.Author.Discriminator}";
                long id = Convert.ToInt64(e.Message.Author.Id);
                if (LevelDAL.UserDoesExists(id, e.Guild.Name))
                {
                    var user = LevelDAL.GetUserLevel(id, e.Guild.Name);
                    var nextLevel = user.Level + 1;

                    user.Messages += 1;
                    if (Levels.LevelScale.TryGetValue(nextLevel, out var nextLevelValue))
                    {

                        if (user.Messages >= nextLevelValue)
                        {
                            user.Level += 1;
                            await e.Message.RespondAsync($"I sense a level {user.Level} brain in {e.Message.Author.Mention}.. yum");
                        }
                    }

                    // Lets save off the username again just to catch it when they change it
                    user.User = name;
                    LevelDAL.EditEntry(user);
                }
                else
                {
                    var user = new LevelModel();
                    user.User = name;
                    user.User_Id = id;
                    user.Guild = e.Guild.Name;
                    user.Level = 1;
                    user.Messages = 1;
                    LevelDAL.CreateEntry(user);
                }
            };

            Console.WriteLine(" .: Rising from the dead.. :. ");

            discord.Ready += async (e) =>
            {
                await Task.Yield();
                Console.WriteLine(" .: Bot is ready to snack.. :. ");
                await discord.UpdateStatusAsync(new Game("Cold Comfort"));
            };

            Console.WriteLine(" .: Finding brains.. :. ");

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Discord_GuildMemberAdd(GuildMemberAddEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}