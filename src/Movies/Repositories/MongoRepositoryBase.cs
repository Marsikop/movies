using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Movies.Models;

namespace Movies.Repositories
{
	public abstract class MongoRepositoryBase<T> : IRepository<T> where T : MongoObject
	{
        protected readonly IMongoCollection<T> Collection;

        protected abstract UpdateDefinition<T> CreateUpdateMapping(T obj);

        public MongoRepositoryBase(IMongoClient client)
        {
            var database = client.GetDatabase("MyDb");
            var collection = database.GetCollection<T>(nameof(T));

            Collection = collection;
        }

        public async Task<ObjectId> Create(T obj)
        {
            await Collection.InsertOneAsync(obj);

            return obj.Id;
        }

        public async Task<bool> Delete(ObjectId objectId)
        {
            var filter = Builders<T>.Filter.Eq(c => c.Id, objectId);
            var result = await Collection.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }

        public Task<T> Get(ObjectId objectId)
        {
            var filter = Builders<T>.Filter.Eq(c => c.Id, objectId);
            var result = Collection.Find(filter).FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<T>> Get()
        {
            var results = await Collection.Find(_ => true).ToListAsync();

            return results;
        }

        public async Task<bool> Update(ObjectId objectId, T obj)
        {
            var filter = Builders<T>.Filter.Eq(c => c.Id, objectId);
            var update = CreateUpdateMapping(obj);
            var result = await Collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
    }
}

