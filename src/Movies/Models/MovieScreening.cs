using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Movies.Models
{
    public class MovieScreening : MongoObject
    {
        public DateTime ScreeningTime { get; set; }
        public string CinemaCityId { get; set; }
        public int MovieHallIndex { get; set; }   
        public string MovieId { get; set; }
        public ICollection<Seat> Seats { get; set; }
    }
}
