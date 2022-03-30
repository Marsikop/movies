using Movies.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Movies.Repositories
{
    public class OrdersRepository : MongoRepositoryBase<Order>
    {
        private readonly MovieScreeningsRepository _movieScreeningsRepository;

        public OrdersRepository(
            IMongoClient client,
            IRepository<MovieScreening> movieScreeningsRepository
            ) : base(client)
        {
            _movieScreeningsRepository = (MovieScreeningsRepository)movieScreeningsRepository;
        }

        protected override IMongoCollection<Order> GetMongoCollection(IMongoDatabase database)
        {
            return database.GetCollection<Order>(nameof(Order));
        }

        protected override UpdateDefinition<Order> CreateUpdateMapping(Order obj)
        {
            var update = Builders<Order>.Update
                .Set(c => c.OrderTime, obj.OrderTime)
                .Set(c => c.CinemaId, obj.CinemaId)
                .Set(c => c.MovieHallIndex, obj.MovieHallIndex)
                .Set(c => c.MovieId, obj.MovieId)
                .Set(c => c.Seats, obj.Seats);

            return update;
        }

        public override async Task<ObjectId> Create(Order obj)
        {
            var movieScreening = await _movieScreeningsRepository.Get(ObjectId.Parse(obj.MovieScreeningId));
            var seats = movieScreening.Seats.Where(s => obj.Seats.Contains(s.SeatIndex));

            if (seats.Any(s => s.Status != SeatStatus.Free))
            {
                throw new ArgumentException("One or more of the slected seats is not free");
            }

            foreach (var seat in seats)
            {
                seat.Status = SeatStatus.Locked;
            }

            await _movieScreeningsRepository.Update(movieScreening.Id, movieScreening);

            var result = await base.Create(obj);

            foreach (var seat in seats)
            {
                seat.Status = SeatStatus.Occupied;
            }

            await _movieScreeningsRepository.Update(movieScreening.Id, movieScreening);

            return result;
        }
    }
}
