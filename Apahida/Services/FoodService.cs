using Apahida.Data;
using Apahida.Models;

namespace Apahida.Services
{
    public class FoodService
    {
        private readonly ApahidaContext _context;

        public FoodService(ApahidaContext context)
        {
            _context = context;
        }
        public List<Food> getAllFoods()
        {
            List<Food> foods = _context.Food.Where(food => food.Stock >= 1).ToList();
            return foods;
        }
        public List<Food> GetTopFoods()
        {
            List<Food> foods = _context.Food.Where(food => food.Stock >= 1).ToList();
            //To do 
            List<int> numberElements = new List<int>();
            for (int i = 1; i <= foods.Count; i++)
            {
                List<OrderFood> list = _context.OrderFood.Where(orderFood => orderFood.FoodId == i).ToList();
                numberElements.Add(list.Count);
            }
            for(int i = 0; i < numberElements.Count-1; i++)
                for(int j = i+1; j < numberElements.Count; j++)
                {
                    if (numberElements[i] < numberElements[j])
                    {
                        var aux = numberElements[i];
                        numberElements[i] = numberElements[j];
                        numberElements[j] = aux;
                        var auxList = foods[i];
                        foods[i] = foods[j];
                        foods[j] = auxList;
                    }
                }
            return foods;
        }
    }
}
