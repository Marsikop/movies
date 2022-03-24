using Movies.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Repositories
{
    public class CinemaCityRepository : IRepository<CinemaCity>
    {
        private readonly IMongoCollection<CinemaCity> _cinema;

        public CinemaCityRepository(IMongoClient client)
        {
            var database = client.GetDatabase("MyDb");
            var collection = database.GetCollection<CinemaCity>(nameof(CinemaCity));

            _cinema = collection;
        }

        public async Task<ObjectId> Create(CinemaCity obj)
        {
            await _cinema.InsertOneAsync(obj);

            return obj.Id;
        }

        public async Task<bool> Delete(ObjectId objectId)
        {
            var filter = Builders<CinemaCity>.Filter.Eq(c => c.Id, objectId);
            var result = await _cinema.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }

        public Task<CinemaCity> Get(ObjectId objectId)
        {
            var filter = Builders<CinemaCity>.Filter.Eq(c => c.Id, objectId);
            var CinemaCity = _cinema.Find(filter).FirstOrDefaultAsync();

            return CinemaCity;
        }

        public async Task<IEnumerable<CinemaCity>> Get()
        {
            var CinemaCitys = await _cinema.Find(_ => true).ToListAsync();

            return CinemaCitys;
        }

        public async Task<IEnumerable<CinemaCity>> GetByName(string name)
        {
            var filter = Builders<CinemaCity>.Filter.Eq(c => c.Name, name);
            var CinemaCitys = await _cinema.Find(filter).ToListAsync();

            return CinemaCitys;
        }

        public async Task<bool> Update(ObjectId objectId, CinemaCity obj)
        {
            var filter = Builders<CinemaCity>.Filter.Eq(c => c.Id, objectId);
            var update = Builders<CinemaCity>.Update
                .Set(c => c.Name, obj.Name)
                .Set(c => c.Halls, obj.Halls);
            var result = await _cinema.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
    }
}
