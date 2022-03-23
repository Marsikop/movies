using Movies.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        private readonly IMongoCollection<Car> _cars;

        public CarRepository(IMongoClient client)
        {
            var database = client.GetDatabase("MyDb");
            var collection = database.GetCollection<Car>(nameof(Car));

            _cars = collection;
        }

        public async Task<ObjectId> Create(Car obj)
        {
            await _cars.InsertOneAsync(obj);

            return obj.Id;
        }

        public async Task<bool> Delete(ObjectId objectId)
        {
            var filter = Builders<Car>.Filter.Eq(c => c.Id, objectId);
            var result = await _cars.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }

        public Task<Car> Get(ObjectId objectId)
        {
            var filter = Builders<Car>.Filter.Eq(c => c.Id, objectId);
            var car = _cars.Find(filter).FirstOrDefaultAsync();

            return car;
        }

        public async Task<IEnumerable<Car>> Get()
        {
            var cars = await _cars.Find(_ => true).ToListAsync();

            return cars;
        }

        public async Task<IEnumerable<Car>> GetByName(string name)
        {
            var filter = Builders<Car>.Filter.Eq(c => c.Make, name);
            var cars = await _cars.Find(filter).ToListAsync();

            return cars;
        }

        public async Task<bool> Update(ObjectId objectId, Car obj)
        {
            var filter = Builders<Car>.Filter.Eq(c => c.Id, objectId);
            var update = Builders<Car>.Update
                .Set(c => c.Make, obj.Make)
                .Set(c => c.Model, obj.Model)
                .Set(c => c.TopSpeed, obj.TopSpeed);
            var result = await _cars.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
    }
}
