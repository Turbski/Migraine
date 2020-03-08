using Discord;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Migraine_v2.Classes
{
    public class Webhook
    {

        private readonly string _webhookUrl;

        public string Content { get; set; }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsTTS { get; set; }
        public List<Embed> Embeds { get; set; } = new List<Embed>();

        public Webhook(string webhookUrl)
        {
            _webhookUrl = webhookUrl;
        }

        public async Task<HttpResponseMessage> Send()
        {
            var content = new StringContent(JsonConvert.SerializeObject(this), Encoding.UTF8, "application/json");
            HttpClient _httpClient = new HttpClient();
            return await _httpClient.PostAsync(_webhookUrl, content);
        }
        public async Task<HttpResponseMessage> Send(string content, string username = null, string avatarUrl = null, bool isTTS = false, IEnumerable<Embed> embeds = null)
        {
            Content = content;
            Username = username;
            AvatarUrl = avatarUrl;
            IsTTS = isTTS;
            Embeds.Clear();
            if (embeds != null)
            {
                Embeds.AddRange(embeds);
            }

            return await Send();
        }
    }
}
