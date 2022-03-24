using MongoDB.Bson;
using System.Collections.Generic;

namespace Movies.Models
{
    public class MovieHall
    {
        public int HallIndex { get; set; }
        public int SeatsCount { get; set; }
        // public ICollection<MovieScreening> Screenings { get; set; }
    }
}
