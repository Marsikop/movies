using Movies.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Repositories
{
    public class CinemaCityRepository : MongoRepositoryBase<CinemaCity>
    {
        public CinemaCityRepository(MongoClient client) : base(client)
        {

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
    }
}
