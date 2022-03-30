using Movies.Models;
using Movies.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieScreeningsController : MongoBasicController<MovieScreening>
    {
        private readonly MovieScreeningsRepository _repository;

        public MovieScreeningsController(IRepository<MovieScreening> repository) : base(repository)
        {
            _repository = (MovieScreeningsRepository)repository;
        }

        [HttpGet("Filter/{movie?}/{cinema?}")]
        public async Task<IActionResult> Filter(string movie = null, string cinema = null)
        {
            var result = await _repository.Filter(movieName: movie, cinemaName: cinema);

            return new JsonResult(result);
        }
    }
}
