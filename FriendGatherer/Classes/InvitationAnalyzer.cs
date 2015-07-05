using System;
using System.Collections.Generic;
using FriendWrangler.Core.Enumerations;
using FriendWrangler.Core.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace FriendWrangler.Core.Classes
{
    public static class InvitationAnalyzer
    {


        public static MessageSentiment AnalyzeMessage(string message)
        {
            var client = new HttpClient { BaseAddress = new Uri("http://text-processing.com") };
            var content = new FormUrlEncodedContent(new[] 
                {
                    new KeyValuePair<string, string>("text", message)
                });
            var result = client.PostAsync("/api/sentiment/", content).Result;
            var resultContent = result.Content.ReadAsStringAsync().Result;
            var sentiment = JsonConvert.DeserializeObject<Sentiment>(resultContent);

            switch (sentiment.Label)
            {
                case "pos":
                    return MessageSentiment.Yes;
                case "neg":
                    return MessageSentiment.No;
                default:
                    return MessageSentiment.Unknown;
            } 
        }

        
                 
    }
}
