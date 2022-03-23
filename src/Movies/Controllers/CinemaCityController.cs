using Movies.Models;
using Movies.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemasController : ControllerBase
    {
        private readonly IRepository<CinemaCity> _repository;

        public CinemasController(IRepository<CinemaCity> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CinemaCity obj)
        {
            var id = await _repository.Create(obj);

            return new JsonResult(id.ToString());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var cinema = await _repository.Get(ObjectId.Parse(id));

            return new JsonResult(cinema);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cinemas = await _repository.Get();

            return new JsonResult(cinemas);
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var cinemas = await _repository.GetByName(name);

            return new JsonResult(cinemas);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, CinemaCity obj)
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
