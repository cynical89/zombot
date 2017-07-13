using System;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using ZomBot.Models;
using ZomBot.DAL;

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
            await Configuration.SetupConfig();

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
                await e.Channel.SendMessageAsync("Does this mean more bains to eat?");
            };

            discord.MessageCreated += async e =>
            {
                if (e.Message.Author.IsBot)
                {
                    if (e.Message.Content.ToLower() == "ping")
                        await e.Message.RespondAsync("pong");
                }
                var name = $"{e.Message.Author.Username}#{e.Message.Author.Discriminator}";
                if (LevelDAL.UserDoesExists(name, e.Guild.Name))
                {
                    var user = LevelDAL.GetUserLevel(name, e.Guild.Name);
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

                    LevelDAL.EditEntry(user);
                }
                else
                {
                    var user = new LevelModel();
                    user.User = name;
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