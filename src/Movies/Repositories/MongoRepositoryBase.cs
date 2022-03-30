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

        protected abstract IMongoCollection<T> GetMongoCollection(IMongoDatabase database);
        protected abstract UpdateDefinition<T> CreateUpdateMapping(T obj);

        public MongoRepositoryBase(IMongoClient client)
        {
            var database = client.GetDatabase("MyDb");
            var collection = GetMongoCollection(database);

            Collection = collection;
        }

        public virtual async Task<ObjectId> Create(T obj)
        {
            await Collection.InsertOneAsync(obj);

            return obj.Id;
        }

        public virtual async Task<bool> Delete(ObjectId objectId)
        {
            var filter = Builders<T>.Filter.Eq(c => c.Id, objectId);
            var result = await Collection.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }

        public virtual Task<T> Get(ObjectId objectId)
        {
            var filter = Builders<T>.Filter.Eq(c => c.Id, objectId);
            var result = Collection.Find(filter).FirstOrDefaultAsync();

            return result;
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            var results = await Collection.Find(_ => true).ToListAsync();

            return results;
        }

        public virtual async Task<bool> Update(ObjectId objectId, T obj)
        {
            var filter = Builders<T>.Filter.Eq(c => c.Id, objectId);
            var update = CreateUpdateMapping(obj);
            var result = await Collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
    }
}

