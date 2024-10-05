using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

        }
        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }

        // Get method to load the form
        public IActionResult AssignCars(int orderId=1)
        {
            var order = _context.Orders.Include(o => o.OrderCars).FirstOrDefault(o => o.OrderId == orderId);
            var cars = _context.Cars.ToList();

            var viewModel = new OrderCarViewModel
            {
                OrderId = order.OrderId,
                OrderName = order.OrderName,
                Cars = cars.Select(car => new CarViewModel
                {
                    CarId = car.CarId,
                    CarName = car.CarName,
                    IsSelected = order.OrderCars.Any(oc => oc.CarId == car.CarId),
                    DriverName = car.OrderCars?.FirstOrDefault(x=>x.CarId==car.CarId)?.DriverName  // This will be filled in the form
                }).ToList()
            };

            return View(viewModel);
        }

        // Post method to save the data
        [HttpPost]
        public IActionResult SaveOrderCars(OrderCarViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var carVm in model.Cars)
                {
                    if (carVm.IsSelected)
                    {
                        var orderCar = new OrderCar
                        {
                            OrderId = model.OrderId,
                            CarId = carVm.CarId,
                            DriverName = carVm.DriverName,
                            CreatedDate = DateTime.Now
                        };
                         var ExistingCar = _context.OrderCars.Any(x => x.OrderId == model.OrderId && x.CarId == carVm.CarId);
                        if(!ExistingCar)
                        _context.OrderCars.Add(orderCar);
                    }
                    else
                    {
                        var UnCheckedCar=_context.OrderCars.FirstOrDefault(x=>x.OrderId==model.OrderId && x.CarId==carVm.CarId);
                        if(UnCheckedCar is not null)
                        _context.OrderCars.Remove(UnCheckedCar);
                    }
                }
               
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //public IActionResult Index()
        //{
            
            
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
