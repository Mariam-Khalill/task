using Microsoft.AspNetCore.Mvc;
using Task1.Backend.Interfaces;

namespace Task1.Controllers
{
	public class HomeController : Controller
	{
        private readonly ICarRepository _carRepository;

        public HomeController(ICarRepository repository)
        {
            _carRepository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carRepository.GetAllCars();

            return View(cars);
        }
    }
}
