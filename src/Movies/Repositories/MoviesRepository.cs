using Movies.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Repositories
{
    public class MoviesRepository : IRepository<Movie>
    {
        private readonly IMongoCollection<Movie> _collection;

        public MoviesRepository(IMongoClient client)
        {
            var database = client.GetDatabase("MyDb");
            var collection = database.GetCollection<Movie>(nameof(Movie));

            _collection = collection;
        }

        public async Task<ObjectId> Create(Movie obj)
        {
            await _collection.InsertOneAsync(obj);

            return obj.Id;
        }

        public async Task<bool> Delete(ObjectId objectId)
        {
            var filter = Builders<Movie>.Filter.Eq(c => c.Id, objectId);
            var result = await _collection.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }

        public Task<Movie> Get(ObjectId objectId)
        {
            var filter = Builders<Movie>.Filter.Eq(c => c.Id, objectId);
            var result = _collection.Find(filter).FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<Movie>> Get()
        {
            var results = await _collection.Find(_ => true).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<Movie>> GetByName(string name)
        {
            var filter = Builders<Movie>.Filter.Eq(c => c.Name, name);
            var results = await _collection.Find(filter).ToListAsync();

            return results;
        }

        public async Task<bool> Update(ObjectId objectId, Movie obj)
        {
            var filter = Builders<Movie>.Filter.Eq(c => c.Id, objectId);
            var update = Builders<Movie>.Update
                .Set(c => c.Name, obj.Name)
                .Set(c => c.Description, obj.Description)
                .Set(c => c.LengthInMinutes, obj.LengthInMinutes)
                .Set(c => c.TrailerUrl, obj.TrailerUrl);
            var result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
    }
}
