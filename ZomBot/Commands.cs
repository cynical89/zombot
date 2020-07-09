using System;
using System.Collections.Generic;
using System.Text;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.Services;

namespace ZomBot
{
    class Commands
    {
        [Command("ping")]
        [Description("Testing ping command")]
        [Aliases("pong")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var emoji = DiscordEmoji.FromName(ctx.Client, ":ping_pong:");
            await ctx.RespondAsync($"{emoji} Pong! {ctx.Client.Ping}ms");
        }
        //[Command("starthunt")]
        //[Description("Testing ping command")]
        //public async Task StartHunt(CommandContext ctx)
        //{
        //    await ctx.TriggerTypingAsync();
        //    await ctx.RespondAsync($"Quack quack. Hunt is started!");
        //}
        //[Command("youtube")]
        //[Description("Fetches a youtube video")]
        //[Aliases("yt")]
        //public async Task RunYoutubeSearch(CommandContext ctx, params string[] searchTerms)
        //{
        //    var url = "https://www.youtube.com/watch?v=";

        //    await ctx.TriggerTypingAsync();
        //    var searchTerm = "";
        //    foreach (var term in searchTerms)
        //    {
        //        if (searchTerm.Length == 0)
        //            searchTerm = term;
        //        searchTerm += " " + term;
        //    }

        //    var youtubeService = new YouTubeService(new BaseClientService.Initializer()
        //    {
        //        ApiKey = Configuration.Config.YoutubeKey,
        //        ApplicationName = this.GetType().ToString()
        //    });

        //    var searchListRequest = youtubeService.Search.List("snippet");
        //    searchListRequest.Q = searchTerm;
        //    searchListRequest.MaxResults = 50;

        //    var searchListResponse = await searchListRequest.ExecuteAsync();

        //    List<string> videos = new List<string>();

        //    foreach (var searchResult in searchListResponse.Items)
        //    {
        //        switch (searchResult.Id.Kind)
        //        {
        //            case "youtube#video":
        //                videos.Add(String.Format("{0} - {1}{2})", searchResult.Snippet.Title, url, searchResult.Id.VideoId));
        //                break;
        //        }
        //    }

        //    await ctx.RespondAsync(videos[0]);
        //}
    }
}
