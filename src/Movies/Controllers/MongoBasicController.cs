using Movies.Models;
using Movies.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    public class MongoBasicController<T> : ControllerBase
    {
        private readonly IRepository<T> _repository;

        public MongoBasicController(IRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(T obj)
        {
            var id = await _repository.Create(obj);

            return new JsonResult(id.ToString());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _repository.Get(ObjectId.Parse(id));

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _repository.Get();

            return new JsonResult(results);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, T obj)
        {
            var result = await _repository.Update(ObjectId.Parse(id), obj);

            return new JsonResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _repository.Delete(ObjectId.Parse(id));

            return new JsonResult(result);
        }
    }
}
