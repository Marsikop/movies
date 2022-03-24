using MongoDB.Bson;

namespace Movies.Models
{
    public class Movie
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LengthInMinutes { get; set; }
        public string TrailerUrl { get; set; }
    }
}
