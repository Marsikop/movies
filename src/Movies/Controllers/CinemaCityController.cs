using Movies.Models;
using Movies.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemasController : MongoBasicController<CinemaCity>
    {
        private readonly CinemaCityRepository _repository;

        public CinemasController(IRepository<CinemaCity> repository) : base(repository)
        {
            _repository = (CinemaCityRepository)repository;
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var cinemas = await _repository.GetByName(name);

            return new JsonResult(cinemas);
        }
    }
}
