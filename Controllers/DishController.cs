using AutoMapper;
using dotnet_api_test.Models.Dtos;
using dotnet_api_test.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api_test.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly ILogger<DishController> _logger;
        private readonly IMapper _mapper;
        private readonly IDishRepository _dishRepository;

        public DishController(ILogger<DishController> logger, IMapper mapper, IDishRepository dishRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _dishRepository = dishRepository;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<DishesAndAveragePriceDto> GetDishesAndAverageDishPrice()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ReadDishDto> GetDishById(int id)
        {
            try
            {
                var dishModel = _dishRepository.GetDishById(id);
                if (dishModel == null)
                {
                    return NotFound($"Dish with Id = {id} doesn't exist");
                }

             return Ok(dishModel);   
            }
            catch (System.Exception)
            {
                
                throw new System.NotImplementedException();
            }
            
        }

        [HttpPost]
        [Route("")]
        public ActionResult<ReadDishDto> CreateDish([FromBody] CreateDishDto createDishDto)
        {
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<ReadDishDto> UpdateDishById(int id, UpdateDishDto updateDishDto)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteDishById(int id)
        {
            try
            {
                var dishModel = DeleteDishById(id);
                if (dishModel == null)
                {
                    return NotFound($"Dish with Id = {id} doesn't exist");
                }
                _dishRepository.SaveChanges();

                return Ok($"dish with id: {id} has been deleted");

            }
            catch (System.Exception)
            {
                
                throw new System.NotImplementedException();
            }

        }
    }
}