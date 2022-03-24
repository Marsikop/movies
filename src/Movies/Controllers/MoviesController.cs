using Movies.Models;
using Movies.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IRepository<Movie> _repository;

        public MoviesController(IRepository<Movie> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie obj)
        {
            var id = await _repository.Create(obj);

            return new JsonResult(id.ToString());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var movie = await _repository.Get(ObjectId.Parse(id));

            return new JsonResult(movie);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var movies = await _repository.Get();

            return new JsonResult(movies);
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var movies = await _repository.GetByName(name);

            return new JsonResult(movies);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Movie obj)
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
