using Movies.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Repositories
{
    public class MoviesRepository : MongoRepositoryBase<Movie>
    {
        public MoviesRepository(IMongoClient client) : base(client)
        {
            
        }

        protected override IMongoCollection<Movie> GetMongoCollection(IMongoDatabase database)
        {
            return database.GetCollection<Movie>(nameof(Movie));
        }

        protected override UpdateDefinition<Movie> CreateUpdateMapping(Movie obj)
        {
            var update = Builders<Movie>.Update
                .Set(c => c.Name, obj.Name)
                .Set(c => c.Description, obj.Description)
                .Set(c => c.LengthInMinutes, obj.LengthInMinutes)
                .Set(c => c.TrailerUrl, obj.TrailerUrl);

            return update;
        }

        public async Task<IEnumerable<Movie>> GetByName(string name)
        {
            var filter = Builders<Movie>.Filter.Eq(c => c.Name, name);
            var results = await Collection.Find(filter).ToListAsync();

            return results;
        }
    }
}
