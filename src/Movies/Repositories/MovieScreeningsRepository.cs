using Movies.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Movies.Repositories
{
    public class MovieScreeningsRepository : MongoRepositoryBase<MovieScreening>
    {
        private readonly IRepository<CinemaCity> _cinemaCityRepository;

        public MovieScreeningsRepository(
            IMongoClient client,
            IRepository<CinemaCity> cinemaCityRepository
            ) : base(client)
        {
            _cinemaCityRepository = cinemaCityRepository;
        }

        protected override IMongoCollection<MovieScreening> GetMongoCollection(IMongoDatabase database)
        {
            return database.GetCollection<MovieScreening>(nameof(MovieScreening));
        }

        protected override UpdateDefinition<MovieScreening> CreateUpdateMapping(MovieScreening obj)
        {
            var update = Builders<MovieScreening>.Update
                .Set(c => c.ScreeningTime, obj.ScreeningTime)
                .Set(c => c.CinemaCityId, obj.CinemaCityId)
                .Set(c => c.MovieHallIndex, obj.MovieHallIndex)
                .Set(c => c.MovieId, obj.MovieId)
                .Set(c => c.Seats, obj.Seats);

            return update;
        }

        public override async Task<ObjectId> Create(MovieScreening obj)
        {
            var result = await base.Create(obj);

            var cinema = await _cinemaCityRepository.Get(ObjectId.Parse(obj.CinemaCityId));
            var seatsCount = cinema.Halls.Where(h => h.HallIndex == obj.MovieHallIndex).First().SeatsCount;

            obj.Seats = new List<Seat>(seatsCount);
            for (int i = 0; i < seatsCount; i++)
            {
                obj.Seats.Add(new Seat { SeatIndex = i + 1, Status = SeatStatus.Free });
            }

            await Update(result, obj);

            return result;
        }
    }
}
