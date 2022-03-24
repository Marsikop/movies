using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Movies.Models
{
    public class MovieScreening
    {
        public ObjectId Id { get; set; }
        public DateTime ScreeningTime { get; set; }
        public ObjectId MovieHallId { get; set; }
        public ObjectId MovieId { get; set; }
        public ICollection<Seat> Seats { get; set; }
    }
}
