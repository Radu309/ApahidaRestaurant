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

namespace Apahida.Controllers
{
    public class LogInController : Controller
    {
        private readonly ApahidaContext _context;
        public LogInController(ApahidaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        // POST: /Login/Authenticate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Authenticate(Users user)
        {
            UsersService usersService = new UsersService(_context);
            string logIn = usersService.LogIn(user);
            if(logIn == "Admin")
                return RedirectToAction("Index", "Foods");
            else if (logIn == "Employee")
                return RedirectToAction("Index", "Orders");
            else
                return RedirectToAction("Index", "LogIn");
        }
    }
}
