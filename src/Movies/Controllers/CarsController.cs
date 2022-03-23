using Movies.Models;
using Movies.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly IRepository<Car> _carRepository;

        public CarsController(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            var id = await _carRepository.Create(car);

            return new JsonResult(id.ToString());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var car = await _carRepository.Get(ObjectId.Parse(id));

            return new JsonResult(car);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cars = await _carRepository.Get();

            return new JsonResult(cars);
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var cars = await _carRepository.GetByName(name);

            return new JsonResult(cars);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Car car)
        {
            var result = await _carRepository.Update(ObjectId.Parse(id), car);

            return new JsonResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _carRepository.Delete(ObjectId.Parse(id));

            return new JsonResult(result);
        }
    }
}
