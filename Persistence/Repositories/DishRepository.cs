using dotnet_api_test.Persistence.Repositories.Interfaces;

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
            throw new System.NotImplementedException();
        }

        public IEnumerable<Dish> GetAllDishes()
        {
            try
            {
                 var dishModel = _context.Dishes.ToList();
                 var dishes = dishModel.Select(d =>
                 new Dish
                 {
                     Id = d.Id,
                     MadeBy = d.MadeBy,
                     Name = d.Name
                 });
                 return (dishes);
            }
            catch (System.Exception)
            {
                
                throw new System.NotImplementedException();
            }
            
        }

        public dynamic? GetAverageDishPrice()
        {
            throw new System.NotImplementedException();
        }

        public Dish GetDishById(int Id)
        {

            try
            {
                 var dishModel = _context.Dishes.Find(Id);
                 var dish = new Dish
                 {
                     Id = dishModel.Id,
                     MadeBy = dishModel.MadeBy,
                     Name = dishModel.Name

                 };
                 return (dish);
            }
            catch (System.Exception)
            {
                
                throw new System.NotImplementedException();
            }
            
        }

        public void DeleteDishById(int Id)
        {
            try
            {
                var dishModel = _context.Dishes.Find(Id);
            }
            catch (System.Exception)
            {
                
                throw new System.NotImplementedException();
            }
            
        }

        public Dish CreateDish(Dish dish)
        {
            try
            {
                var dishModel = new Dish
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    MadeBy = dish.MadeBy,
                    Cost = dish.Cost
                };

                _context.Dishes.Add(dishModel);
                _context.SaveChanges();

                return (dishModel);
                 
            }
            catch (System.Exception)
            {
                
                throw new System.NotImplementedException();;
            }
            
        }

        public Dish UpdateDish(Dish dish)
        {
            throw new System.NotImplementedException();
        }
    }
}