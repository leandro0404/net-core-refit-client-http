using Newtonsoft.Json;

namespace App.Demo.Client.Entities
{
    public class Post
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
