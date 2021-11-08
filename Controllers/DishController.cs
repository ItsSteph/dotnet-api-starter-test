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
        [Route("GetDishesAndAverageDishPrice")]
        public ActionResult<DishesAndAveragePriceDto> GetDishesAndAverageDishPrice()
        {
            try
            {
                var dishModel = _dishRepository.GetAllDishes();
                _logger.LogInformation("test");
                var dishModelDto = _mapper.Map<IEnumerable<ReadDishDto>>(dishModel);
                var averagePrice = _dishRepository.GetAverageDishPrice();  

                var dishesAndAveragePriceDto = _mapper.Map<DishesAndAveragePriceDto>(
                    new DishesAndAveragePriceDto()
                    {
                        Dishes = dishModelDto,
                        AveragePrice = averagePrice
                    }
                );
                 return Ok(dishesAndAveragePriceDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                throw;
                
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ReadDishDto> GetDishById(int id)
        {
            try
            {
                var dishModel = _dishRepository.GetDishById(id);
                var dish = _mapper.Map<ReadDishDto>(dishModel);

                if (dish == null)
                {
                    return NotFound($"Dish with Id = {id} doesn't exist");
                }
                return Ok(dish);   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                throw;
            } 
        }

        [HttpPost]
        [Route("CreateDish")]
        public ActionResult<ReadDishDto> CreateDish([FromBody] CreateDishDto createDishDto)
        {
            try
            {
                var dishModel = _mapper.Map<Dish>(createDishDto);
                dishModel = _dishRepository.CreateDish(dishModel);
                
                var dish = _mapper.Map<ReadDishDto>(dishModel);
                return Ok(dish);
 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                throw;
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<ReadDishDto> UpdateDishById(int id, UpdateDishDto updateDishDto)
        {
            try
            {
                 var dishModel = _dishRepository.GetDishById(id);
                 dishModel.Name = updateDishDto.Name;
                 dishModel.MadeBy = updateDishDto.MadeBy;
                 dishModel.Cost = (double)updateDishDto.Cost;

                 dishModel = _dishRepository.UpdateDish(dishModel);
                 _logger.LogInformation("dish is now updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                throw;
            }
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteDishById(int id)
        {
            try
            {
                 _dishRepository.GetDishById(id);            

                return Ok($"dish with id: {id} has been deleted");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                throw;
            }

        }
    }
}