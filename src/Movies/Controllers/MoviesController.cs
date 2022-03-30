using Movies.Models;
using Movies.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : MongoBasicController<Movie>
    {
        private readonly MoviesRepository _repository;

        public MoviesController(IRepository<Movie> repository) : base(repository)
        {
            _repository = (MoviesRepository)repository;
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var movies = await _repository.GetByName(name);

            return new JsonResult(movies);
        }
    }
}
