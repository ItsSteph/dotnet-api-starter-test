using dotnet_api_test.Persistence.Repositories.Interfaces;
using dotnet_api_test.Exceptions.ExceptionResponses;
namespace dotnet_api_test.Persistence.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly AppDbContext _context;

        public DishRepository(AppDbContext context)
        {
            _context = context;
        }

        void IDishRepository.SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Dish> GetAllDishes()
        {

            IEnumerable<Dish> dishFromDB = _context.Dishes.ToList();

            if(dishFromDB == null)
            {
                throw new NotFoundRequestExceptionResponse(msg: "No dishes were found");
            }
             return (dishFromDB);
        }

        public dynamic? GetAverageDishPrice()
        {
            
            var dishFromDB = GetAllDishes();
            return dishFromDB.Select(x => x.Cost).Average();
        }

        public Dish GetDishById(int Id)
        {
            var dishFromDB = _context.Dishes.Find(Id);
            if(dishFromDB == null)
            {
                throw new NotFoundRequestExceptionResponse($"No dish with the id: {Id} was found");
            }
            return (dishFromDB);
        }

        public void DeleteDishById(int Id)
        {
            var dishFromDB = _context.Dishes.Find(Id);
            if(dishFromDB == null)
            {
                throw new NotFoundRequestExceptionResponse($"No dish with the id: {Id} was found");
            }
            _context.Remove(dishFromDB);
            _context.SaveChanges();
        }

        public Dish CreateDish(Dish dish)
        {
            _context.Add(dish);
            _context.SaveChanges();

            return (dish);           
        }

        public Dish UpdateDish(Dish dish)
        {
            _context.Update(dish);
            _context.SaveChanges();

            return (dish);
        }
    }
}