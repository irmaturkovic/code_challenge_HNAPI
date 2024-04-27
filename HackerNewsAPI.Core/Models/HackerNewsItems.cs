using System.Text.Json.Serialization;

namespace HackerNewsAPI.Core.Models
{
    public class HackerNewsItems
    {
        public string By { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public long Time { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Deleted { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<int> Kids { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? Parent { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Dead { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Title { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Score { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? Poll { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Url { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<int> Parts { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Text { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Descendants { get; set; }

    }

    public class Story : HackerNewsItems { }
    public class Comment : HackerNewsItems { }
    public class Ask : HackerNewsItems { }
    public class Poll : HackerNewsItems { }
    public class Job : HackerNewsItems { }
    public class PollOption : HackerNewsItems { }
}
