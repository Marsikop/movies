using System.Collections.Generic;
using MongoDB.Bson;

namespace Movies.Models
{
    public class CinemaCity : MongoObject
    {
        public string Name { get; set; }
        public ICollection<MovieHall> Halls { get; set; }
    }
}
