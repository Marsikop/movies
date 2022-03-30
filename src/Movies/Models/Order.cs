using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Movies.Models
{
	public class Order : MongoObject
	{
        public DateTime OrderTime { get; set; }
        public string MovieScreeningId { get; set; }
        public string CinemaId { get; set; }
        public int MovieHallIndex { get; set; }
        public string MovieId { get; set; }
        public ICollection<int> Seats { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public int TotalPrice => 50 * Seats.Count;

        public Order()
        {
            OrderTime = DateTime.Now;
        }
    }
}

