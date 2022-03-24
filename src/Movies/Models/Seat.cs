using MongoDB.Bson;

namespace Movies.Models
{
    public enum SeatStatus{
        Free = 0,
        Occupied = 1,
        Locked = 2
    }

    public class Seat
    {
        public SeatStatus Status { get; set; }
    }
}
