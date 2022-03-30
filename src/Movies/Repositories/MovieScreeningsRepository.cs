using Movies.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Movies.Repositories
{
    public class MovieScreeningsRepository : MongoRepositoryBase<MovieScreening>
    {
        private readonly CinemaCityRepository _cinemaCityRepository;
        private readonly MoviesRepository _moviesRepository;

        public MovieScreeningsRepository(
            IMongoClient client,
            IRepository<CinemaCity> cinemaCityRepository,
            IRepository<Movie> moviesRepository
            ) : base(client)
        {
            _cinemaCityRepository = (CinemaCityRepository)cinemaCityRepository;
            _moviesRepository = (MoviesRepository)moviesRepository;
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

        public async Task<IEnumerable<MovieScreening>> Filter(
            string movieName = null,
            string cinemaName = null)
        {
            FilterDefinition<MovieScreening> movieFilter = Builders<MovieScreening>.Filter.Empty;
            FilterDefinition<MovieScreening> cinemaFilter = Builders<MovieScreening>.Filter.Empty;

            if (movieName != null)
            {
                var movieId = (await _moviesRepository.GetByName(movieName)).First().Id.ToString();
                movieFilter = Builders<MovieScreening>.Filter.Eq(c => c.MovieId, movieId);
            }

            if (cinemaName != null)
            {
                var cinemaId = (await _cinemaCityRepository.GetByName(cinemaName)).First().Id.ToString();
                cinemaFilter = Builders<MovieScreening>.Filter.Eq(c => c.CinemaCityId, cinemaId);
            }

            var filter = Builders<MovieScreening>.Filter.And(movieFilter, cinemaFilter);
            var results = await Collection.Find(filter).ToListAsync();

            return results;
        }
    }
}
