using Movies.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Repositories
{
    public class MovieScreeningsRepository : MongoRepositoryBase<MovieScreening>
    {
        public MovieScreeningsRepository(IMongoClient client) : base(client)
        {
            
        }

        protected override IMongoCollection<MovieScreening> GetMongoCollection(IMongoDatabase database)
        {
            return database.GetCollection<MovieScreening>(nameof(MovieScreening));
        }

        protected override UpdateDefinition<MovieScreening> CreateUpdateMapping(MovieScreening obj)
        {
            var update = Builders<MovieScreening>.Update
                .Set(c => c.ScreeningTime, obj.ScreeningTime)
                .Set(c => c.MovieHallId, obj.MovieHallId)
                .Set(c => c.MovieId, obj.MovieId)
                .Set(c => c.Seats, obj.Seats);

            return update;
        }
    }
}
