using Movies.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Repositories
{
    public interface IRepository<T>
    {
        // Create
        Task<ObjectId> Create(T obj);

        // Read
        Task<T> Get(ObjectId objectId);
        Task<IEnumerable<T>> Get();

        // Update
        Task<bool> Update(ObjectId objectId, T obj);

        // Delete
        Task<bool> Delete(ObjectId objectId);
    }
}
