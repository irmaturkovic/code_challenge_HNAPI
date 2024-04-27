namespace HackerNewsAPI.Core.Models
{
    public class HackerNewsUsers
    {
        public string Id { get; set; }
        public long Created { get; set; }
        public int Karma { get; set; }
        public string About { get; set; }
        public List<int> Submitted { get; set; }
    }
}
