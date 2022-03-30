using Movies.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Repositories
{
    public class CinemaCityRepository : MongoRepositoryBase<CinemaCity>
    {
        public CinemaCityRepository(IMongoClient client) : base(client)
        {

        }

        protected override IMongoCollection<CinemaCity> GetMongoCollection(IMongoDatabase database)
        {
            return database.GetCollection<CinemaCity>(nameof(CinemaCity));
        }

        protected override UpdateDefinition<CinemaCity> CreateUpdateMapping(CinemaCity obj)
        {
            var update = Builders<CinemaCity>.Update
               .Set(c => c.Name, obj.Name)
               .Set(c => c.Halls, obj.Halls);

            return update;
        }

        public async Task<IEnumerable<CinemaCity>> GetByName(string name)
        {
            var filter = Builders<CinemaCity>.Filter.Eq(c => c.Name, name);
            var results = await Collection.Find(filter).ToListAsync();

            return results;
        }

        public async Task<bool> CreateHall(ObjectId cinemaId, MovieHall hall)
        {
            var filter = Builders<CinemaCity>.Filter.Eq(c => c.Id, cinemaId);
            var filterResult = await Collection.Find(filter).FirstOrDefaultAsync();
            var halls = ((CinemaCity)filterResult).Halls;

            if (halls == null)
            {
                halls = new List<MovieHall>();
            }

            halls.Add(hall);

            var update = Builders<CinemaCity>.Update
                .Set(c => c.Halls, halls);

            var result = await Collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
    }
}
