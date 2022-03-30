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
    }
}
