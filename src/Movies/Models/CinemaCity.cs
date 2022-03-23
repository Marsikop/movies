using System.Collections.Generic;
using MongoDB.Bson;

namespace Movies.Models
{
    public class CinemaCity
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieHall> Halls { get; set; }
    }
}
