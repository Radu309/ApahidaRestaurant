using Apahida.Data;
using Apahida.Models;

namespace Apahida.Services
{
    public class OrderService
    {
        private readonly ApahidaContext _context;

        public OrderService(ApahidaContext context)
        {
            _context = context;
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = _context.Order.Where(order => order.TotalPrice >= 0).ToList();
            return orders;
        }

        public List<Food> GetOrderFoodsById(int id)
        {
            List<OrderFood> list = _context.OrderFood.Where(orderFood => orderFood.OrderId == id).ToList();
            List<int> foodIdList = new List<int>();
            List<Food> foodList = new List<Food>();
            List<Food> finalList = new List<Food>();
            foreach(var ord in list)
            {
                foodIdList.Add(ord.FoodId);
            }
            foodList = _context.Food.ToList();
            foreach(var food in foodList)
            {
                int count = foodIdList.Count(id => id == food.Id);
                while (count > 0)
                {
                    finalList.Add(food);
                    count--;
                }
            }
            return finalList;
        }
        public List<Order> getOrdersBetween2Dates(DateTime startDate, DateTime endDate){
            List<Order> orders = _context.Order.Where(order => order.TotalPrice >= 0).ToList();
            List<Order> resultedOrders = orders.Where(o => o.Date >= startDate && o.Date <= endDate).ToList();
            return resultedOrders;
        }
    }
}
