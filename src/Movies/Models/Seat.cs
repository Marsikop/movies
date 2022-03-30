using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Movies.Models
{
    public enum SeatStatus{
        Free = 0,
        Occupied = 1,
        Locked = 2
    }

    public class Seat
    {
        public int SeatIndex { get; set; }
        public SeatStatus Status { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string StatusStr => Status.ToString();
    }
}
