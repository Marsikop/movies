using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Movies.Models
{
    public class MovieHall
    {
        public int HallIndex { get; set; }
        public int SeatsCount { get; set; }
        // public ICollection<ObjectId> Screenings { get; set; }
        // public Dictionary<DateTime, ICollection<ObjectId>> Screenings { get; set; }   
    }
}
