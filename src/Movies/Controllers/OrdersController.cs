using Movies.Models;
using Movies.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : MongoBasicController<Order>
    {
        private readonly OrdersRepository _repository;

        public OrdersController(IRepository<Order> repository) : base(repository)
        {
            _repository = (OrdersRepository)repository;
        }
    }
}
