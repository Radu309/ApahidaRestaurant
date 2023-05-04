using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apahida.Data;
using Apahida.Models;
using Apahida.Services;
using System.Dynamic;

namespace Apahida.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApahidaContext _context;
        FoodService foodService;
        OrderService orderService;

        public OrdersController(ApahidaContext context)
        {
            _context = context;
            foodService = new FoodService(_context);
            orderService = new OrderService(_context);
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
              return _context.Order != null ? 
                          View(await _context.Order.ToListAsync()) :
                          Problem("Entity set 'ApahidaContext.Order' is null.");
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            return _context.Food != null ?
                          View(await _context.Food.ToListAsync()) :
                          Problem("Entity set 'ApahidaContext.Food' is null.");
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<Food> listFood, List<int> foodQuantities)
        {
            if (ModelState.IsValid)
            {
                Order newOrder = new Order();
                List<Food> foods = foodService.getAllFoods();
                for(var i = 0; i < foodQuantities.Count; i++)
                {
                    newOrder.TotalPrice += (foods[i].Price * foodQuantities[i]);
                    foods[i].Stock = foods[i].Stock - foodQuantities[i];
                    _context.Update<Food>(foods[i]);
                    await _context.SaveChangesAsync();
                }
                _context.Add<Order>(newOrder);
                await _context.SaveChangesAsync();
                ///
                for (var i = 0; i < foodQuantities.Count; i++)
                {
                    while (foodQuantities[i] > 0)
                    {
                        OrderFood orderFood = new OrderFood(newOrder.Id, foods[i].Id);
                        _context.Add<OrderFood>(orderFood);
                        await _context.SaveChangesAsync();
                        foodQuantities[i]--;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            List<Food> listFoods = orderService.GetOrderFoodsById(order.Id);
            ViewBag.FoodList = listFoods;
            return View(order);

        }
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TotalPrice,Date,Hour,Status")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingOrder = await _context.Order.FindAsync(id);
                    existingOrder.Status = order.Status;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'ApahidaContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
