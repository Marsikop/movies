﻿using Movies.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Repositories
{
    public class CinemaCityRepository : IRepository<CinemaCity>
    {
        private readonly IMongoCollection<CinemaCity> _collection;

        public CinemaCityRepository(IMongoClient client)
        {
            var database = client.GetDatabase("MyDb");
            var collection = database.GetCollection<CinemaCity>(nameof(CinemaCity));

            _collection = collection;
        }

        public async Task<ObjectId> Create(CinemaCity obj)
        {
            await _collection.InsertOneAsync(obj);

            return obj.Id;
        }

        public async Task<bool> Delete(ObjectId objectId)
        {
            var filter = Builders<CinemaCity>.Filter.Eq(c => c.Id, objectId);
            var result = await _collection.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }

        public Task<CinemaCity> Get(ObjectId objectId)
        {
            var filter = Builders<CinemaCity>.Filter.Eq(c => c.Id, objectId);
            var result = _collection.Find(filter).FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<CinemaCity>> Get()
        {
            var results = await _collection.Find(_ => true).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<CinemaCity>> GetByName(string name)
        {
            var filter = Builders<CinemaCity>.Filter.Eq(c => c.Name, name);
            var results = await _collection.Find(filter).ToListAsync();

            return results;
        }

        public async Task<bool> Update(ObjectId objectId, CinemaCity obj)
        {
            var filter = Builders<CinemaCity>.Filter.Eq(c => c.Id, objectId);
            var update = Builders<CinemaCity>.Update
                .Set(c => c.Name, obj.Name)
                .Set(c => c.Halls, obj.Halls);
            var result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
    }
}
