using System.Text.Json.Serialization;

namespace BrickCollection.Models
{

    public class BricksetImage
    {
        public string? ThumbnailURL { get; set; }
        public string? ImageURL { get; set; }
    }

    public class BricksetSet
    {
        public int SetID { get; set; }
        public string? Number { get; set; }
        public string? Name { get; set; }
        public int Year { get; set; }
        public string? Theme { get; set; }
        public string? Subtheme { get; set; }
        public int? Pieces { get; set; }
        public int? Minifigs { get; set; }
        public BricksetImage? Image { get; set; }
    }

    internal class GetSetsRawResponse
    {
        public string? status { get; set; }
        public int matches { get; set; }
        public List<BricksetSet> sets { get; set; }
        public string? message { get; set; }
    }
}